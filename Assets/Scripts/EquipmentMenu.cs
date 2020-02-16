using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentMenu : MonoBehaviour
{
    [SerializeField]
    private Equipment[] equipment;

    [SerializeField]
    private GameObject WeaponPanel, ArmorPanel, WeaponSlot, ArmorSlot, EquipmentDescriptionPanel;

    private bool IsOpened;

    public GameObject GetWeaponPanel
    {
        get
        {
            return WeaponPanel;
        }
        set
        {
            WeaponPanel = value;
        }
    }

    public GameObject GetArmorPanel
    {
        get
        {
            return ArmorPanel;
        }
        set
        {
            ArmorPanel = value;
        }
    }

    public bool GetIsOpened
    {
        get
        {
            return IsOpened;
        }
        set
        {
            IsOpened = value;
        }
    }

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

    public void SetEquipmentTypeTextW(TextMeshProUGUI WeaponsText)
    {
        WeaponsText.text = "Weapons";
    }

    public void SetEquipmentTypeTextA(TextMeshProUGUI ArmorText)
    {
        ArmorText.text = "Armor";
    }

    public void DisableEquipmentPanel()
    {
        EquipmentDescriptionPanel.SetActive(false);
    }

    public void SetIsOpenedToTrue()
    {
        IsOpened = true;
    }

    public void SetIsOpenedToFalse()
    {
        IsOpened = false;
    }
}