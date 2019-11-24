using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentMenu : MonoBehaviour
{
    [SerializeField]
    private Equipment[] equipment;

    [SerializeField]
    private GameObject WeaponPanel, ArmorPanel, WeaponSlot, ArmorSlot, EquipmentDescriptionPanel;

    private void Start()
    {
        SetStartingEquipment();
    }

    private void SetStartingEquipment()
    {
        if (equipment.Length > 0)
        {
            if (equipment[0].GetEquipmentType == EquipmentType.Weapon)
            {
                equipment[0].transform.SetParent(WeaponSlot.transform, true);
                equipment[0].Equip();
            }
            else
            {
                equipment[0].transform.SetParent(ArmorSlot.transform, true);
                equipment[0].Equip();
            }
            if (equipment[1].GetEquipmentType == EquipmentType.Armor)
            {
                equipment[1].transform.SetParent(ArmorSlot.transform, true);
                equipment[1].Equip();
            }
            else
            {
                equipment[1].transform.SetParent(WeaponSlot.transform, true);
                equipment[1].Equip();
            }
        }
    }

    public void ToggleWeaponsInPanel()
    {
        foreach (Equipment Equip in WeaponPanel.GetComponentsInChildren<Equipment>(true))
        {
            if(!Equip.gameObject.activeInHierarchy)
            {
                Equip.gameObject.SetActive(true);
            }
            else
            {
                Equip.gameObject.SetActive(false);
            }
        }
    }

    public void ToggleArmorInPanel()
    {
        foreach (Equipment Equip in ArmorPanel.GetComponentsInChildren<Equipment>(true))
        {
            if (!Equip.gameObject.activeInHierarchy)
            {
                Equip.gameObject.SetActive(true);
            }
            else
            {
                Equip.gameObject.SetActive(false);
            }
        }
    }

    public void ToggleWeaponInSlot()
    {
        if(WeaponSlot.transform.childCount > 0)
        {
            if(WeaponSlot.GetComponentInChildren<Equipment>(true))
            {
                if(!WeaponSlot.GetComponentInChildren<Equipment>(true).gameObject.activeInHierarchy)
                {
                    WeaponSlot.GetComponentInChildren<Equipment>(true).gameObject.SetActive(true);
                }
                else
                {
                    WeaponSlot.GetComponentInChildren<Equipment>(true).gameObject.SetActive(false);
                }
            }
        }
    }

    public void ToggleArmorInSlot()
    {
        if(ArmorSlot.transform.childCount > 0)
        {
            if(ArmorSlot.GetComponentInChildren<Equipment>(true))
            {
                if(!ArmorSlot.GetComponentInChildren<Equipment>(true).gameObject.activeInHierarchy)
                {
                    ArmorSlot.GetComponentInChildren<Equipment>(true).gameObject.SetActive(true);
                }
                else
                {
                    ArmorSlot.GetComponentInChildren<Equipment>(true).gameObject.SetActive(false);
                }
            }
        }
    }

    public void DisableEquipmentPanel()
    {
        EquipmentDescriptionPanel.SetActive(false);
    }
}