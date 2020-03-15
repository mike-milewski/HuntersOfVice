using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentMenu : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Equipment[] KnightEquipment;

    [SerializeField]
    private Equipment[] ShadowPriestEquipment;

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
        if (KnightEquipment.Length > 0)
        {
            if (KnightEquipment[0].GetEquipmentType == EquipmentType.Weapon)
            {
                if(character.GetCharacterData.name == "Knight")
                {
                    KnightEquipment[0].transform.SetParent(WeaponSlot.transform, true);
                    KnightEquipment[0].Equip();
                }
                else
                {
                    ShadowPriestEquipment[0].transform.SetParent(WeaponSlot.transform, true);
                    ShadowPriestEquipment[0].Equip();
                }
            }
            else
            {
                if (character.GetCharacterData.name == "Knight")
                {
                    KnightEquipment[0].transform.SetParent(ArmorSlot.transform, true);
                    KnightEquipment[0].Equip();
                }
                else
                {
                    ShadowPriestEquipment[0].transform.SetParent(ArmorSlot.transform, true);
                    ShadowPriestEquipment[0].Equip();
                }
                
            }
            if (KnightEquipment[1].GetEquipmentType == EquipmentType.Armor)
            {
                if (character.GetCharacterData.name == "Knight")
                {
                    KnightEquipment[1].transform.SetParent(ArmorSlot.transform, true);
                    KnightEquipment[1].Equip();
                }
                else
                {
                    ShadowPriestEquipment[1].transform.SetParent(ArmorSlot.transform, true);
                    ShadowPriestEquipment[1].Equip();
                }
            }
            else
            {
                if (character.GetCharacterData.name == "Knight")
                {
                    KnightEquipment[1].transform.SetParent(WeaponSlot.transform, true);
                    KnightEquipment[1].Equip();
                }
                else
                {
                    ShadowPriestEquipment[1].transform.SetParent(WeaponSlot.transform, true);
                    ShadowPriestEquipment[1].Equip();
                }
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