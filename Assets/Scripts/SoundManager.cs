using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField]
    private AudioClip[] audioclips;

    [SerializeField]
    private AudioSource audiosource;

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

        audiosource = GetComponent<AudioSource>();
    }

    public void FallSE()
    {
        audiosource.PlayOneShot(audioclips[0]);
    }
}
