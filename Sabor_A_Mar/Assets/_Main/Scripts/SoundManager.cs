using System;
using UnityEngine;

public enum SoundType 
{
	MAR,
	ACEITE,
	PISADAS,
	SELVA
}
[RequiredComponent(typeof(AudioSource)), ExecuteInEditMode]

public class SoundManager: MonoBheaviour
{
	[SerializeField] private SoundList[] soundList;
	private static SoundManager instance;
	private AudioSource audioSource;

	private void Awake() 
	{
		instance = this;
	}
	private void Start() 
	{
		audioSource = GetComponent<AudioSource>();
	}
	public void PlaySound(SoundType sound, float volume = 1) 
	{
		AudioClip[] clips = instance.soundList[(int)sound].Sounds;
		AudioClip randomClip = clips[UnityEngine.Random.Range(0,clips.Length)];
		instance.audioSource.PlayOneShot(randomClip, volume);
	}

#if UNITY_EDITOR
	private void OnEnable() 
	{
		string[] names = Enum.GetNames(typeof(SoundType));
		Array.Resize(ref SoundList, names.Length);
		for(int i = 0; i < soundList.Length; i++)
		{
			soundList[i].name = names[i];
		}
	}
}

[Serializable]
public struct SoundList 
{
	public AudioClip[] Sounds {get => sounds;}
	[HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}
