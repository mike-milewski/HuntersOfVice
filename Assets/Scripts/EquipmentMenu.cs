#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentMenu : MonoBehaviour
{
    [SerializeField]
    private Character character, Knight, ShadowPriest;

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

    private void OnEnable()
    {
        if(Knight.gameObject.activeInHierarchy)
        {
            character = Knight;
        }
        else if(ShadowPriest.gameObject.activeInHierarchy)
        {
            character = ShadowPriest;
        }       
    }

    private void Start()
    {
        SetStartingEquipment();
    }

    private void SetStartingEquipment()
    {
        if(character.GetCharacterData.name == "Knight")
        {
            if (KnightEquipment.Length > 0)
            {
                if (KnightEquipment[0].GetEquipmentType == EquipmentType.Weapon)
                {
                    KnightEquipment[0].transform.SetParent(WeaponSlot.transform, true);
                    KnightEquipment[0].Equip();

                    KnightEquipment[0].transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    KnightEquipment[0].transform.SetParent(ArmorSlot.transform, true);
                    KnightEquipment[0].Equip();

                    KnightEquipment[0].transform.localScale = new Vector3(1, 1, 1);
                }

                if (KnightEquipment[1].GetEquipmentType == EquipmentType.Armor)
                {
                    KnightEquipment[1].transform.SetParent(ArmorSlot.transform, true);
                    KnightEquipment[1].Equip();

                    KnightEquipment[1].transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    KnightEquipment[1].transform.SetParent(WeaponSlot.transform, true);
                    KnightEquipment[1].Equip();

                    KnightEquipment[1].transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
        else if(character.GetCharacterData.name == "ShadowPriest")
        {
            if (ShadowPriestEquipment.Length > 0)
            {
                if (ShadowPriestEquipment[0].GetEquipmentType == EquipmentType.Weapon)
                {
                    ShadowPriestEquipment[0].transform.SetParent(WeaponSlot.transform, true);
                    ShadowPriestEquipment[0].Equip();

                    ShadowPriestEquipment[0].transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    ShadowPriestEquipment[0].transform.SetParent(ArmorSlot.transform, true);
                    ShadowPriestEquipment[0].Equip();

                    ShadowPriestEquipment[0].transform.localScale = new Vector3(1, 1, 1);
                }

                if (ShadowPriestEquipment[1].GetEquipmentType == EquipmentType.Armor)
                {
                    ShadowPriestEquipment[1].transform.SetParent(ArmorSlot.transform, true);
                    ShadowPriestEquipment[1].Equip();

                    ShadowPriestEquipment[1].transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    ShadowPriestEquipment[1].transform.SetParent(WeaponSlot.transform, true);
                    ShadowPriestEquipment[1].Equip();

                    ShadowPriestEquipment[1].transform.localScale = new Vector3(1, 1, 1);
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