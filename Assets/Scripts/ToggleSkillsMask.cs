using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSkillsMask : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obj;

    public void ToggleMask()
    {
        for(int i = 0; i < obj.Length; i++)
        {
            foreach (Mask m in obj[i].GetComponentsInChildren<Mask>())
            {
                m.showMaskGraphic = true;
            }
            foreach (Image img in obj[i].GetComponentsInChildren<Image>())
            {
                img.raycastTarget = true;
            }
        }
    }

    public void NToggleMask()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            foreach (Mask m in obj[i].GetComponentsInChildren<Mask>())
            {
                m.showMaskGraphic = false;
            }
            foreach (Image img in obj[i].GetComponentsInChildren<Image>())
            {
                img.raycastTarget = false;
            }
        }
    }
}
