using UnityEngine;
using UnityEngine.UI;

public enum statuseffects { NONE, StrengthUP, DefenseUP, IntelliegenceUP, SpeedUP,
                            StrengthDOWN, DefenseDOWN, IntelligenceDOWN, SpeedDOWN }

public class StatusEffects : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Image StatusIcon = null;

    [SerializeField]
    private statuseffects effects;

    [SerializeField]
    private Transform StatusBuffIconTrans, StatusDebuffIconTrans;

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

    private void Awake()
    {
        effects = statuseffects.NONE;
    }

    public void StrengthUP(Character chara, int value, float duration)
    {
        float Percentage = (float)value / 100;

        float TempStrength = (float)chara.CharacterStrength;

        Mathf.FloorToInt(TempStrength);

        TempStrength += (float)chara.CharacterStrength * Percentage;

        chara.CharacterStrength = (int)TempStrength;
    }

    public void RemoveCharacterStatusAffix(Character chara)
    {
        chara.CharacterStrength = chara.GetCharacterData.Strength;
        chara.CharacterDefense = chara.GetCharacterData.Defense;
        chara.CharacterIntelligence = chara.GetCharacterData.Intelligence;
    }
}
