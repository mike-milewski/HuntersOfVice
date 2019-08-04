using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField]
    private AudioClip[] audioclips;

    private Scene scene;

    [SerializeField]
    private AudioSource[] audiosource;

    public AudioSource[] GetAudioSource
    {
        get
        {
            return audiosource;
        }
        set
        {
            audiosource = value;
        }
    }

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        audiosource[1].volume = 0.3f;
    }

    public void FallSE()
    {
        audiosource[1].PlayOneShot(audioclips[0]);
    }

    public void RightFootStep()
    {
        audiosource[1].PlayOneShot(audioclips[1]);
    }

    public void LeftFootStep()
    {
        audiosource[1].PlayOneShot(audioclips[2]);
    }
}
