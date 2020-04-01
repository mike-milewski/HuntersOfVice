using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField]
    private AudioClip[] audioclips;

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

    public void ItemBottle()
    {
        audiosource[1].PlayOneShot(audioclips[3]);
    }

    public void ItemHeal()
    {
        audiosource[1].PlayOneShot(audioclips[4]);
    }

    public void SwordHit()
    {
        audiosource[1].PlayOneShot(audioclips[5]);
    }

    public void EnemyHit()
    {
        audiosource[2].PlayOneShot(audioclips[6]);
    }

    public void ButtonClick()
    {
        audiosource[0].PlayOneShot(audioclips[7]);
    }

    public void Heal()
    {
        audiosource[0].PlayOneShot(audioclips[10]);
    }

    public void SkillLearned()
    {
        audiosource[0].PlayOneShot(audioclips[11]);
    }

    public void PuckRightFootStep()
    {
        audiosource[3].PlayOneShot(audioclips[1]);
    }

    public void PuckLeftFootStep()
    {
        audiosource[3].PlayOneShot(audioclips[2]);
    }

    public void PuckHit()
    {
        audiosource[3].PlayOneShot(audioclips[12]);
    }

    public void PuckFall()
    {
        audiosource[3].PlayOneShot(audioclips[0]);
    }

    public void RecievedItem()
    {
        audiosource[0].PlayOneShot(audioclips[13]);
    }
}
