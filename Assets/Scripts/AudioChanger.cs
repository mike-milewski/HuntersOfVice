#pragma warning disable 0649
using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip LevelTheme, MiniBossTheme, MainBossTheme;

    [SerializeField]
    private bool ChangeToLevelTheme, ChangeToMiniBossTheme, ChangeToMainBossTheme;

    private bool VolumeIncreasing, VolumeDecreasing;

    public AudioClip GetLevelTheme
    {
        get
        {
            return LevelTheme;
        }
        set
        {
            LevelTheme = value;
        }
    }

    public AudioClip GetMiniBossTheme
    {
        get
        {
            return MiniBossTheme;
        }
        set
        {
            MiniBossTheme = value;
        }
    }

    public AudioClip GetMainBossTheme
    {
        get
        {
            return MainBossTheme;
        }
        set
        {
            MainBossTheme = value;
        }
    }

    public bool GetChangeToLevelTheme
    {
        get
        {
            return ChangeToLevelTheme;
        }
        set
        {
            ChangeToLevelTheme = value;
        }
    }

    public bool GetChangeToMiniBossTheme
    {
        get
        {
            return ChangeToMiniBossTheme;
        }
        set
        {
            ChangeToMiniBossTheme = value;
        }
    }

    public bool GetChangeToMainBossTheme
    {
        get
        {
            return ChangeToMainBossTheme;
        }
        set
        {
            ChangeToMainBossTheme = value;
        }
    }

    private void OnEnable()
    {
        VolumeIncreasing = false;
        VolumeDecreasing = true;
    }

    private void Update()
    {
        ChangeAudio();
    }

    private void ChangeAudio()
    {
        if(ChangeToLevelTheme)
        {
            if(VolumeDecreasing)
            {
                source.volume -= Time.deltaTime;
            }
            
            if(source.volume <= 0)
            {
                VolumeDecreasing = false;
                VolumeIncreasing = true;

                source.clip = LevelTheme;
            }

            if(VolumeIncreasing)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                }

                source.volume += Time.deltaTime;
                if (source.volume >= 1)
                {
                    ChangeToLevelTheme = false;
                    gameObject.SetActive(false);
                }
            }
        }
        if(ChangeToMiniBossTheme)
        {
            if (VolumeDecreasing)
            {
                source.volume -= Time.deltaTime;
            }

            if (source.volume <= 0)
            {
                VolumeDecreasing = false;
                VolumeIncreasing = true;

                source.clip = MiniBossTheme;
            }

            if (VolumeIncreasing)
            {
                if(!source.isPlaying)
                {
                    source.Play();
                }
                
                source.volume += Time.deltaTime;
                if (source.volume >= 1)
                {
                    ChangeToMiniBossTheme = false;
                    gameObject.SetActive(false);
                }
            }
        }
        if(ChangeToMainBossTheme)
        {
            if (VolumeDecreasing)
            {
                source.volume -= Time.deltaTime;
            }

            if (source.volume <= 0)
            {
                VolumeDecreasing = false;
                VolumeIncreasing = true;

                source.clip = MainBossTheme;
            }

            if (VolumeIncreasing)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                }

                source.volume += Time.deltaTime;
                if (source.volume >= 1)
                {
                    ChangeToMainBossTheme = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
