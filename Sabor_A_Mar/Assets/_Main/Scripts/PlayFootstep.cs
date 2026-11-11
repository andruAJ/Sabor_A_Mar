using UnityEngine;

public class PlayFootstep : MonoBehaviour
{

    public void PlaySound(SoundType tipoPisada)
    {
        SoundManager.PlaySound(tipoPisada);
    }
}
