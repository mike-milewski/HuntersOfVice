using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EquipmentType { Weapon, Armor }

public class Equipment : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private EquipmentData equipmentData;

    [SerializeField]
    private EquippedCheck equippedCheck;

    [SerializeField]
    private Sprite EquipmentSprite;

    [SerializeField]
    private TextMeshProUGUI WeaponText, ArmorText, EquipmentPanelText;

    [SerializeField]
    private EquipmentType equipmentType;

    public EquipmentData GetEquipmentData
    {
        get
        {
            return equipmentData;
        }
        set
        {
            equipmentData = value;
        }
    }

    public EquipmentType GetEquipmentType
    {
        get
        {
            return equipmentType;
        }
        set
        {
            equipmentType = value;
        }
    }

    public Sprite GetEquipmentSprite
    {
        get
        {
            return EquipmentSprite;
        }
        set
        {
            EquipmentSprite = value;
        }
    }

    private void Awake()
    {
        gameObject.GetComponent<Image>().sprite = EquipmentSprite;
    }

    public void Equip()
    {
        equippedCheck.GetIsEquipped = true;
        switch (equipmentType)
        {
            case (EquipmentType.Weapon):
                WeaponEquip();
                break;
            case (EquipmentType.Armor):
                ArmorEquip();
                break;
        }
    }

    public void UnEquip()
    {
        equippedCheck.GetIsEquipped = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        switch (equipmentType)
        {
            case (EquipmentType.Weapon):
                character.CharacterStrength = character.GetCharacterData.Strength;
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                WeaponText.text = "";
                break;
            case (EquipmentType.Armor):
                character.CharacterDefense = character.GetCharacterData.Defense;
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                ArmorText.text = "";
                break;
        }
    }

    private void WeaponEquip()
    {
        character.CharacterStrength += equipmentData.StatIncrease;

        SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();

        CurrentWeaponEquippedText();
    }

    private void ArmorEquip()
    {
        character.CharacterDefense += equipmentData.StatIncrease;

        SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();

        CurrentArmorEquippedText();
    }

    private void CurrentWeaponEquippedText()
    {
        WeaponText.text = "<u>" + equipmentData.EquipmentName + "</u>" + "\n\n" + "<size=9>" +  "Strength: " + "+" + equipmentData.StatIncrease;
    }

    private void CurrentArmorEquippedText()
    {
        ArmorText.text = "<u>" + equipmentData.EquipmentName + "</u>" + "\n\n" + "<size=9>" + "Defense: " +  "+" + equipmentData.StatIncrease;
    }

    public void PanelText(GameObject panel)
    {
        panel.SetActive(true);

        switch(equipmentType)
        {
            case (EquipmentType.Weapon):
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" + "Strength: " + "+" + 
                                          equipmentData.StatIncrease;
                break;
            case (EquipmentType.Armor):
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" + "Defense: " + "+" + 
                                          equipmentData.StatIncrease;
                break;
        }
    }
}
