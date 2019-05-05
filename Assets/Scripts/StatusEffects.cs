using UnityEngine;
using UnityEngine.UI;

public enum statuseffects { NONE, StrengthUP, DefenseUP, IntelliegenceUP, SpeedUP,
                            StrengthDOWN, DefenseDOWN, IntelligenceDOWN, SpeedDOWN }

public class StatusEffects : MonoBehaviour
{
    [SerializeField]
    private statuseffects effects;

    [SerializeField]
    private Character character;

    [SerializeField]
    private Image StatusIcon;

    [SerializeField]
    private Transform StatusBuffIconTrans, StatusDebuffIconTrans;

    [SerializeField]
    private string StatusEffectName, StatusDescription;

    [SerializeField]
    private float StatusDuration;

    public Character GetCharacter
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
        }
    }

    public Image GetStatusIcon
    {
        get
        {
            return StatusIcon;
        }
        set
        {
            StatusIcon = value;
        }
    }

    public Transform GetBuffIconTrans
    {
        get
        {
            return StatusBuffIconTrans;
        }
        set
        {
            StatusBuffIconTrans = value;
        }
    }

    public Transform GetDeBuffIconTrans
    {
        get
        {
            return StatusDebuffIconTrans;
        }
        set
        {
            StatusDebuffIconTrans = value;
        }
    }

    public string GetStatusEffectName
    {
        get
        {
            return StatusEffectName;
        }
        set
        {
            StatusEffectName = value;
        }
    }

    public string GetStatusDescription
    {
        get
        {
            return StatusDescription;
        }
        set
        {
            StatusDescription = value;
        }
    }

    public float GetStatusDuration
    {
        get
        {
            return StatusDuration;
        }
        set
        {
            StatusDuration = value;
        }
    }

    public void StrengthUP(Character chara, int value, float duration)
    {
        duration = StatusDuration;

        float Percentage = (float)value / 100;

        float TempStrength = (float)chara.CharacterStrength;

        Mathf.FloorToInt(TempStrength);

        TempStrength += (float)chara.CharacterStrength * Percentage;

        chara.CharacterStrength = (int)TempStrength;

        StatusIcon.sprite = SkillsManager.Instance.GetSkills[SkillsManager.Instance.GetKeyInput].GetComponent<Button>().GetComponent<Image>().sprite;
    }

    public void DefenseUP(Character chara, int value, float duration)
    {
        duration = StatusDuration;

        float Percentage = (float)value / 100;

        float TempDefense = (float)chara.CharacterDefense;

        Mathf.FloorToInt(TempDefense);

        TempDefense += (float)chara.CharacterDefense * Percentage;

        chara.CharacterDefense = (int)TempDefense;

        StatusIcon.sprite = SkillsManager.Instance.GetSkills[SkillsManager.Instance.GetKeyInput].GetComponent<Button>().GetComponent<Image>().sprite;
    }

    public void RemoveStatusAffix(Character chara)
    {
        switch(effects)
        {
            case (statuseffects.StrengthUP):
                chara.CharacterStrength = chara.GetCharacterData.Strength;
                break;
            case (statuseffects.StrengthDOWN):
                chara.CharacterStrength = chara.GetCharacterData.Strength;
                break;
            case (statuseffects.DefenseUP):
                chara.CharacterDefense = chara.GetCharacterData.Defense;
                break;
            case (statuseffects.DefenseDOWN):
                chara.CharacterDefense = chara.GetCharacterData.Defense;
                break;
            case (statuseffects.IntelliegenceUP):
                chara.CharacterIntelligence = chara.GetCharacterData.Intelligence;
                break;
            case (statuseffects.IntelligenceDOWN):
                chara.CharacterIntelligence = chara.GetCharacterData.Intelligence;
                break;
        }
    }
}
