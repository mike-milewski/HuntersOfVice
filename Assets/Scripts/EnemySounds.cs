#pragma warning disable 0649
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField]
    private Settings settings;

    [SerializeField]
    private AudioSource audiosource;

    [SerializeField]
    private AudioClip audioclip;

    public void PlayMushroomManWalkSE()
    {
        if (!settings.MuteAudio)
        {
            this.GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            this.GetComponent<AudioSource>().volume = 0;
        }
        audiosource.PlayOneShot(audioclip);
    }

    public void PlayBeeStingerSE()
    {
        if (!settings.MuteAudio)
        {
            this.GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            this.GetComponent<AudioSource>().volume = 0;
        }
        audiosource.PlayOneShot(audioclip);
    }

    public void GolemFragmentWalk()
    {
        if (!settings.MuteAudio)
        {
            this.GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            this.GetComponent<AudioSource>().volume = 0;
        }
        audiosource.PlayOneShot(audioclip);
    }
}
