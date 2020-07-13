#pragma warning disable 0649
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

    public void LevelUp()
    {
        audiosource[0].PlayOneShot(audioclips[14]);
    }

    public void Error()
    {
        audiosource[0].PlayOneShot(audioclips[15]);
    }

    public void Menu()
    {
        audiosource[0].PlayOneShot(audioclips[16]);
    }

    public void ReverseMenu()
    {
        audiosource[0].PlayOneShot(audioclips[17]);
    }

    public void ShopMaterials()
    {
        audiosource[0].PlayOneShot(audioclips[18]);
    }

    public void EquipItem()
    {
        audiosource[0].PlayOneShot(audioclips[19]);
    }

    public void UnEquipItem()
    {
        audiosource[0].PlayOneShot(audioclips[20]);
    }

    public void BuyItem()
    {
        audiosource[0].PlayOneShot(audioclips[21]);
    }

    public void TreasureChest()
    {
        audiosource[0].PlayOneShot(audioclips[22]);
    }

    public void ReverseMouseClick()
    {
        audiosource[0].PlayOneShot(audioclips[23]);
    }

    public void ContractCast()
    {
        audiosource[1].PlayOneShot(audioclips[24]);
    }

    public void StaffHit()
    {
        audiosource[1].PlayOneShot(audioclips[25]);
    }

    public void SwordSwing()
    {
        audiosource[1].PlayOneShot(audioclips[26]);
    }
}
