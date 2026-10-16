// SimpleWaterURP.shader
// Simple, lightweight stylized water shader for URP. Built for a free release.
//
// FEATURES: animated waves, world-position-based shallow/deep color + transparency,
// panning normal-map ripples, fresnel reflection, shoreline foam.
// Supports VR stereo rendering (Single Pass Instanced / Multiview).
//
// HOW DEPTH WORKS (world-position based, not a raw depth subtraction):
// The camera depth texture tells us how far the camera is from whatever is behind
// the water (the lake bed, a submerged rock, etc). We use that to reconstruct the
// actual WORLD POSITION of that point, then compare its height (Y) to the water
// surface's own height. The vertical gap between them is the "depth" of the water
// at that pixel. This stays accurate at any camera angle, unlike simply subtracting
// screen-space depth values, which stretches near the shore when viewed at a
// grazing angle.
//
// SETUP:
// 1. In your URP Asset, enable "Depth Texture" (Rendering section). That's the
//    only setting needed — no Opaque Texture required, and the water mesh can be
//    a plain flat plane (no special shaping needed).
// 2. Assign a tileable normal map + a soft noise texture for foam.
// 3. Set _WaterDepth to roughly how many world units deep your water body is
//    before it should read as "fully deep" colored.
//
// PERFORMANCE NOTES:
// - Normal/foam UV panning is computed per-vertex (exact for flat planes, since
//   UVs interpolate linearly) instead of per-pixel.
// - Colors and 0-1 values use half precision; positions and depth stay float.
// - exp() replaced by exp2() with a constant (identical result, cheaper).

Shader "Custom/SimpleWaterURP_VR_Version"
{
    Properties
    {
        [Header(Waves)]
        _WaveSpeed ("Wave Speed", Range(0, 5)) = 1.0
        _WaveStrength ("Wave Strength", Range(0, 1)) = 0.15
        _WaveScale ("Wave Scale", Range(0.1, 10)) = 1.0

        [Header(Water Color)]
        _ShallowColor ("Shallow Color", Color) = (0.42, 0.75, 0.75, 0.55)
        _DeepColor ("Deep Color", Color) = (0.02, 0.18, 0.32, 0.95)
        _WaterDepth ("Water Depth (max, world units)", Range(0.1, 20)) = 3.0

        [Header(Ripples)]
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalTiling ("Normal Tiling", Float) = 1.0
        _NormalStrength ("Normal Strength", Range(0, 2)) = 0.5
        _NormalSpeed ("Normal Speed", Range(0, 2)) = 0.1

        [Header(Reflection)]
        _FresnelPower ("Fresnel Power", Range(0.1, 8)) = 3.0
        _ReflectionStrength ("Reflection Strength", Range(0, 1)) = 0.6

        [Header(Foam)]
        _FoamColor ("Foam Color", Color) = (1, 1, 1, 1)
        _FoamDistance ("Foam Distance (world units)", Range(0.01, 3)) = 0.4
        _FoamNoiseTex ("Foam Noise Texture", 2D) = "white" {}
        _FoamTiling ("Foam Tiling", Float) = 1.0
        _FoamSpeed ("Foam Speed", Range(0, 2)) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" "IgnoreProjector"="True" }

        Pass
        {
            Name "ForwardWater"
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float4 normalUV : TEXCOORD1;   // xy = layer A, zw = layer B
                float2 foamUV : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                half fogFactor : TEXCOORD4;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            CBUFFER_START(UnityPerMaterial)
                float _WaveSpeed;
                float _WaveStrength;
                float _WaveScale;

                half4 _ShallowColor;
                half4 _DeepColor;
                float _WaterDepth;

                float _NormalTiling;
                half _NormalStrength;
                float _NormalSpeed;

                half _FresnelPower;
                half _ReflectionStrength;

                half4 _FoamColor;
                float _FoamDistance;
                float _FoamTiling;
                float _FoamSpeed;
            CBUFFER_END

            TEXTURE2D(_NormalMap); SAMPLER(sampler_NormalMap);
            TEXTURE2D(_FoamNoiseTex); SAMPLER(sampler_FoamNoiseTex);

            // Local version of Unity's classic ComputeScreenPos, kept in-shader
            // so it doesn't depend on which URP version's helper macros exist.
            float4 ComputeScreenPosition(float4 positionCS)
            {
                float4 o = positionCS * 0.5;
                o.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w;
                o.zw = positionCS.zw;
                return o;
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                float3 positionWS = TransformObjectToWorld(IN.positionOS.xyz);

                // Two simple sine waves summed = cheap, natural-looking motion
                float t = _Time.y * _WaveSpeed;
                float wave1 = sin(positionWS.x * _WaveScale + t);
                float wave2 = sin((positionWS.z + positionWS.x * 0.5) * _WaveScale * 0.8 - t * 1.3);
                positionWS.y += (wave1 + wave2) * 0.5 * _WaveStrength;

                OUT.positionWS = positionWS;
                OUT.positionHCS = TransformWorldToHClip(positionWS);

                // Ripple + foam UV panning done per-vertex (linear in UV, so exact on a plane)
                float time = _Time.y;
                OUT.normalUV.xy = IN.uv * _NormalTiling + float2(1.0, 0.5) * (_NormalSpeed * time);
                OUT.normalUV.zw = IN.uv * (_NormalTiling * 1.5) - float2(0.5, 1.0) * (_NormalSpeed * time);
                OUT.foamUV = IN.uv * _FoamTiling + float2(1.0, 1.0) * (_FoamSpeed * time);

                OUT.screenPos = ComputeScreenPosition(OUT.positionHCS);
                OUT.fogFactor = ComputeFogFactor(OUT.positionHCS.z);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
                float surfaceEyeDepth = IN.screenPos.w;

                // ---- Reconstruct the world position behind the water from the depth ----
                // ---- texture, then compare its height to the water surface's height. ----
                float rawSceneDepth = SampleSceneDepth(screenUV);
                float sceneEyeDepth = LinearEyeDepth(rawSceneDepth, _ZBufferParams);

                float3 camPos = GetCameraPositionWS();
                float3 viewVector = camPos - IN.positionWS;
                half3 viewDir = (half3)normalize(viewVector);
                float3 scenePositionWS = camPos - viewVector * (sceneEyeDepth / surfaceEyeDepth);
                float depthY = max(IN.positionWS.y - scenePositionWS.y, 0.0);

                // ---- Ripple normal: one texture, panned in two directions ----
                half3 rippleA = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, IN.normalUV.xy), 1.0h);
                half3 rippleB = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, IN.normalUV.zw), 1.0h);
                half2 rippleXY = (rippleA.xy + rippleB.xy) * _NormalStrength;
                half3 normalWS = normalize(half3(rippleXY.x, 1.0h, rippleXY.y));

                half fresnel = pow(1.0h - saturate(dot(normalWS, viewDir)), _FresnelPower);

                // ---- Depth-based color and transparency ----
                // Exponential falloff (like light absorption through water) instead of a
                // linear ramp. exp2 with a constant is equivalent to exp() but cheaper.
                half depthFactor = 1.0h - saturate((half)exp2(-depthY * (1.4426950 / max(_WaterDepth, 0.0001))));
                half3 waterColor = lerp(_ShallowColor.rgb, _DeepColor.rgb, depthFactor);
                half alpha = lerp(_ShallowColor.a, _DeepColor.a, depthFactor);

                // ---- Reflection (nearest reflection probe / skybox) ----
                half3 reflectDir = reflect(-viewDir, normalWS);
                half4 reflectionRaw = SAMPLE_TEXTURECUBE(unity_SpecCube0, samplerunity_SpecCube0, reflectDir);
                half3 reflectionColor = DecodeHDREnvironment(reflectionRaw, unity_SpecCube0_HDR);
                half3 colorWithReflection = lerp(waterColor, reflectionColor, fresnel * _ReflectionStrength);

                // ---- Foam near shorelines / underwater objects ----
                half foamNoise = SAMPLE_TEXTURE2D(_FoamNoiseTex, sampler_FoamNoiseTex, IN.foamUV).r;
                half foamLine = saturate((half)smoothstep(_FoamDistance, 0.0, depthY) * foamNoise);
                half3 finalColor = lerp(colorWithReflection, _FoamColor.rgb, foamLine);
                alpha = lerp(alpha, 1.0h, foamLine);

                finalColor = MixFog(finalColor, IN.fogFactor);
                return half4(finalColor, alpha);
            }
            ENDHLSL
        }
    }
}