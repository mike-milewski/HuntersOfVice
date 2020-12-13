#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField]
    private Settings settings;

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
        if(!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[0]);
    }

    public void RightFootStep()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[1]);
    }

    public void LeftFootStep()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[2]);
    }

    public void ItemBottle()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[3]);
    }

    public void ItemHeal()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[4]);
    }

    public void SwordHit()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[5]);
    }

    public void EnemyHit()
    {
        if (!settings.MuteAudio)
            audiosource[2].PlayOneShot(audioclips[6]);
    }

    public void ButtonClick()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[7]);
    }

    public void Heal()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[10]);
    }

    public void SkillLearned()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[11]);
    }

    public void PuckRightFootStep()
    {
        if (!settings.MuteAudio)
            audiosource[3].PlayOneShot(audioclips[1]);
    }

    public void PuckLeftFootStep()
    {
        if (!settings.MuteAudio)
            audiosource[3].PlayOneShot(audioclips[2]);
    }

    public void PuckHit()
    {
        if (!settings.MuteAudio)
            audiosource[3].PlayOneShot(audioclips[12]);
    }

    public void PuckFall()
    {
        if (!settings.MuteAudio)
            audiosource[3].PlayOneShot(audioclips[0]);
    }

    public void RecievedItem()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[13]);
    }

    public void LevelUp()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[14]);
    }

    public void Error()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[15]);
    }

    public void Menu()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[16]);
    }

    public void ReverseMenu()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[17]);
    }

    public void ShopMaterials()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[18]);
    }

    public void EquipItem()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[19]);
    }

    public void UnEquipItem()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[20]);
    }

    public void BuyItem()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[21]);
    }

    public void TreasureChest()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[22]);
    }

    public void ReverseMouseClick()
    {
        if (!settings.MuteAudio)
            audiosource[0].PlayOneShot(audioclips[23]);
    }

    public void ContractCast()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[24]);
    }

    public void StaffHit()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[25]);
    }

    public void SwordSwing()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[26]);
    }

    public void WoodStep()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[27]);
    }

    public void StormThrustSE()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[28]);
    }

    public void RuneGolemWalk()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[29]);
    }

    public void StoneStep()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[30]);
    }

    public void ToadstoolJump()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[31]);
    }

    public void ToadstoolWalk()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[32]);
    }

    public void ToadstoolHit()
    {
        if (!settings.MuteAudio)
            audiosource[1].PlayOneShot(audioclips[33]);
    }
}
