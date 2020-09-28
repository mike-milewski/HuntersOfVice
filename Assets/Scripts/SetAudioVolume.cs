#pragma warning disable 0649
using UnityEngine;

public class SetAudioVolume : MonoBehaviour
{
    [SerializeField]
    private Settings settings;

    public void OnEnable()
    {
        if(this.GetComponent<AudioSource>() != null && MenuButtons.Instance != null)
        {
            if(!settings.MuteAudio)
            {
                this.GetComponent<AudioSource>().volume = 1;
            }
            else
            {
                this.GetComponent<AudioSource>().volume = 0;
            }
        }
    }
}
