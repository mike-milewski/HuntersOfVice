#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentMenu : MonoBehaviour
{
    [SerializeField]
    private Character character, Knight, ShadowPriest, Toadstool;

    [SerializeField]
    private Equipment[] KnightEquipment;

    [SerializeField]
    private Equipment[] ShadowPriestEquipment;

    [SerializeField]
    private Equipment[] ToadstoolEquipment;

    [SerializeField]
    private GameObject WeaponPanel, ArmorPanel, WeaponSlot, ArmorSlot, EquipmentDescriptionPanel;

    [SerializeField]
    private Transform SkillPointParent;

    private bool IsOpened;

    [SerializeField]
    private int MaxWeapons, MaxArmor;

    private int AdditionalSkillPoints;

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

    public int GetAdditionalSkillPoints
    {
        get
        {
            return AdditionalSkillPoints;
        }
        set
        {
            AdditionalSkillPoints = value;
        }
    }

    public int GetMaxWeapons
    {
        get
        {
            return MaxWeapons;
        }
        set
        {
            MaxWeapons = value;
        }
    }

    public int GetMaxArmor
    {
        get
        {
            return MaxArmor;
        }
        set
        {
            MaxArmor = value;
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
        if(GameManager.Instance.GetKnight.activeInHierarchy)
        {
            character = Knight;
        }
        if(GameManager.Instance.GetShadowPriest.activeInHierarchy)
        {
            character = ShadowPriest;
        }
        if (GameManager.Instance.GetToadstool.activeInHierarchy)
        {
            character = Toadstool;
        }

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
        if(character.GetCharacterData.name == "ShadowPriest")
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
        if (character.GetCharacterData.name == "Toadstool")
        {
            if (ToadstoolEquipment.Length > 0)
            {
                if (ToadstoolEquipment[0].GetEquipmentType == EquipmentType.Weapon)
                {
                    ToadstoolEquipment[0].transform.SetParent(WeaponSlot.transform, true);
                    ToadstoolEquipment[0].Equip();

                    ToadstoolEquipment[0].transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    ToadstoolEquipment[0].transform.SetParent(ArmorSlot.transform, true);
                    ToadstoolEquipment[0].Equip();

                    ToadstoolEquipment[0].transform.localScale = new Vector3(1, 1, 1);
                }

                if (ToadstoolEquipment[1].GetEquipmentType == EquipmentType.Armor)
                {
                    ToadstoolEquipment[1].transform.SetParent(ArmorSlot.transform, true);
                    ToadstoolEquipment[1].Equip();

                    ToadstoolEquipment[1].transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    ToadstoolEquipment[1].transform.SetParent(WeaponSlot.transform, true);
                    ToadstoolEquipment[1].Equip();

                    ToadstoolEquipment[1].transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    public void GainSkillPoints(int value)
    {
        if (WeaponSlot.transform.childCount > 0)
        {
            if (WeaponSlot.transform.GetComponentInChildren<Equipment>(true).GetRequiredSkillPoints > 0)
            {
                WeaponSlot.transform.GetComponentInChildren<Equipment>(true).GetAcquiredSkillPoints += value + AdditionalSkillPoints;
                WeaponSlot.transform.GetComponentInChildren<Equipment>(true).UpdateSkillPoinText();
                WeaponSlot.transform.GetComponentInChildren<Equipment>(true).SkillPointTextInMenu();
            }
        }
        if (ArmorSlot.transform.childCount > 0)
        {
            if (ArmorSlot.transform.GetComponentInChildren<Equipment>(true).GetRequiredSkillPoints > 0)
            {
                ArmorSlot.transform.GetComponentInChildren<Equipment>(true).GetAcquiredSkillPoints += value + AdditionalSkillPoints;
                ArmorSlot.transform.GetComponentInChildren<Equipment>(true).UpdateSkillPoinText();
                ArmorSlot.transform.GetComponentInChildren<Equipment>(true).SkillPointTextInMenu();
            }
        }
    }

    public TextMeshProUGUI ReturnSkillPointText()
    {
        var SkillPointText = ObjectPooler.Instance.GetSkillPointText();

        SkillPointText.SetActive(true);

        SkillPointText.transform.SetParent(SkillPointParent.transform, false);

        return SkillPointText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ToggleWeaponsInPanel()
    {
        foreach (Equipment Equip in WeaponPanel.GetComponentsInChildren<Equipment>(true))
        {
            Equip.gameObject.SetActive(true);
        }
    }

    public void DisableWeaponsInPanel()
    {
        foreach (Equipment Equip in WeaponPanel.GetComponentsInChildren<Equipment>(true))
        {
            Equip.gameObject.SetActive(false);
        }
    }

    public void ToggleArmorInPanel()
    {
        foreach (Equipment Equip in ArmorPanel.GetComponentsInChildren<Equipment>(true))
        {
            Equip.gameObject.SetActive(true);
        }
    }

    public void DisableArmorInPanel()
    {
        foreach (Equipment Equip in ArmorPanel.GetComponentsInChildren<Equipment>(true))
        {
            Equip.gameObject.SetActive(false);
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