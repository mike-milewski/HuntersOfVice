using UnityEngine;

public enum ElementalWeaknesses { NONE, Physical, Magic, Fire, Water, Wind, Earth, Light, Dark }

public enum ElementalResistances { NONE, Physical, Magic, Fire, Water, Wind, Earth, Light, Dark }

public enum ElementalImmunities { NONE, Physical, Magic, Fire, Water, Wind, Earth, Light, Dark }

public enum ElementalAbsorbtion { NONE, Physical, Magic, Fire, Water, Wind, Earth, Light, Dark }

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Character Class")]
public class CharacterData : ScriptableObject
{
    public ElementalWeaknesses[] Weaknesses;

    public ElementalResistances[] Resistances;

    public ElementalImmunities[] Immunities;

    public ElementalAbsorbtion[] Absorbtions;

    public string CharacterName;

    public int CharacterLevel, Health, Mana, Strength, Defense, Intelligence, HpIncrease, MpIncrease, CriticalHitChance, Coins, EXP, MaterialDataDropChance;

    public float MoveSpeed;

    public bool CheckedData, UsesStrength, UsesIntelligence;

    public MaterialData materialdata;
}
