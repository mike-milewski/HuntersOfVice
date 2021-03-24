#pragma warning disable 0649
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Settings settings;

    private void OnEnable()
    {
        if(!settings.MuteAudio)
        {
            audioSource.volume = 1;
        }
        else
        {
            audioSource.volume = 0;
        }
    }
}
