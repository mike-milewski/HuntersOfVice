using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Character Class")]
public class CharacterData : ScriptableObject
{
    public string CharacterName;

    public int CharacterLevel, Health, Mana, Strength, Defense, Intelligence, CriticalHitChance;
}
