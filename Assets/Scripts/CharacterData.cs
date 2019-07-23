using UnityEngine;

public enum ElementalWeaknesses { NONE, Physical, Fire, Water, Wind, Earth, Light, Dark }

public enum ElementalResistances { NONE, Physical, Fire, Water, Wind, Earth, Light, Dark }

public enum ElementalImmunities { NONE, Physical, Fire, Water, Wind, Earth, Light, Dark }

public enum ElementalAbsorbtion { NONE, Physical, Fire, Water, Wind, Earth, Light, Dark }

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Character Class")]
public class CharacterData : ScriptableObject
{
    public ElementalWeaknesses[] Weaknesses;

    public ElementalResistances[] Resistances;

    public ElementalImmunities[] Immunities;

    public ElementalAbsorbtion[] Absorbtions;

    public string CharacterName;

    public int CharacterLevel, Health, Mana, Strength, Defense, Intelligence, CriticalHitChance;

    public float MoveSpeed;
}
