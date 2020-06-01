#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSkillsMask : MonoBehaviour
{
    [SerializeField]
    private GameObject Knight, ShadowPriest;

    [SerializeField]
    private GameObject[] KnightSkills, ShadowPriestSkills;

    public void ToggleMask()
    {
        if(Knight.activeInHierarchy)
        {
            TurnOnKnightSkills();
        }
        else if(ShadowPriest.activeInHierarchy)
        {
            TurnOnShadowPriestSkills();
        }
    }

    public void NToggleMask()
    {
        if (Knight.activeInHierarchy)
        {
            TurnOffKnightSkills();
        }
        else if (ShadowPriest.activeInHierarchy)
        {
            TurnOffShadowPriestSkills();
        }
    }

    private void TurnOnKnightSkills()
    {
        for (int i = 0; i < KnightSkills.Length; i++)
        {
            foreach (Mask m in KnightSkills[i].GetComponentsInChildren<Mask>())
            {
                m.showMaskGraphic = true;
            }
            foreach (Image img in KnightSkills[i].GetComponentsInChildren<Image>())
            {
                img.raycastTarget = true;
            }
        }
    }

    private void TurnOnShadowPriestSkills()
    {
        for (int i = 0; i < ShadowPriestSkills.Length; i++)
        {
            foreach (Mask m in ShadowPriestSkills[i].GetComponentsInChildren<Mask>())
            {
                m.showMaskGraphic = true;
            }
            foreach (Image img in ShadowPriestSkills[i].GetComponentsInChildren<Image>())
            {
                img.raycastTarget = true;
            }
        }
    }

    private void TurnOffKnightSkills()
    {
        for (int i = 0; i < KnightSkills.Length; i++)
        {
            foreach (Mask m in KnightSkills[i].GetComponentsInChildren<Mask>())
            {
                m.showMaskGraphic = false;
            }
            foreach (Image img in KnightSkills[i].GetComponentsInChildren<Image>())
            {
                img.raycastTarget = false;
            }
        }
    }

    private void TurnOffShadowPriestSkills()
    {
        for (int i = 0; i < ShadowPriestSkills.Length; i++)
        {
            foreach (Mask m in ShadowPriestSkills[i].GetComponentsInChildren<Mask>())
            {
                m.showMaskGraphic = true;
            }
            foreach (Image img in ShadowPriestSkills[i].GetComponentsInChildren<Image>())
            {
                img.raycastTarget = true;
            }
        }
    }
}
