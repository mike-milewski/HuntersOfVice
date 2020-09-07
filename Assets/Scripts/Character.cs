﻿#pragma warning disable 0414
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField]
    private CharacterData CharData;

    [SerializeField]
    private Rigidbody _rigidBody;
    
    [SerializeField]
    private int CharacterLevel, Health, Mana, Strength, Defense, Intelligence, CriticalHitChance;

    private int DefaultHealth, DefaultMana, DefaultStrength, DefaultDefense, DefaultIntelligence;

    [SerializeField]
    private float MoveSpeed;

    [SerializeField]
    private string CharacterName;

    private ElementalWeaknesses[] weaknesses;

    private ElementalResistances[] resistances;

    private ElementalImmunities[] immunities;

    private ElementalAbsorbtion[] absorbtions;

    //Region that gets and returns all of the character's stat values.
    #region StatProperties
    public string characterName
    {
        get
        {
            return CharacterName;
        }
        set
        {
            CharacterName = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            return Health;
        }
        set
        {
            Health = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return CharData.Health;
        }
        set
        {
            CharData.Health = value;
        }
    }

    public int CurrentMana
    {
        get
        {
            return Mana;
        }
        set
        {
            Mana = value;
        }
    }

    public int MaxMana
    {
        get
        {
            return CharData.Mana;
        }
        set
        {
            CharData.Mana = value;
        }
    }

    public int Level
    {
        get
        {
            return CharacterLevel;
        }
        set
        {
            CharacterLevel = value;
        }
    }

    public int CharacterStrength
    {
        get
        {
            return Strength;
        }
        set
        {
            Strength = value;
        }
    }

    public int CharacterDefense
    {
        get
        {
            return Defense;
        }
        set
        {
            Defense = value;
        }
    }

    public int CharacterIntelligence
    {
        get
        {
            return Intelligence;
        }
        set
        {
            Intelligence = value;
        }
    }

    public int GetCriticalChance
    {
        get
        {
            return CriticalHitChance;
        }
        set
        {
            CriticalHitChance = value;
        }
    }

    public float GetMoveSpeed
    {
        get
        {
            return MoveSpeed;
        }
        set
        {
            MoveSpeed = value;
        }
    }

    public Rigidbody GetRigidbody
    {
        get
        {
            return _rigidBody;
        }
        set
        {
            GetRigidbody = value;
        }
    }

    public CharacterData GetCharacterData
    {
        get
        {
            return CharData;
        }
        set
        {
            CharData = value;
        }
    }
    #endregion

    private void Reset()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        var cData = Instantiate(CharData);

        CharacterName = cData.CharacterName;
        CharacterLevel = cData.CharacterLevel;
        Health = cData.Health;
        Mana = cData.Mana;
        Strength = cData.Strength;
        Defense = cData.Defense;
        Intelligence = cData.Intelligence;
        CriticalHitChance = cData.CriticalHitChance;
        MoveSpeed = cData.MoveSpeed;

        weaknesses = cData.Weaknesses;
        resistances = cData.Resistances;
        immunities = cData.Immunities;
        absorbtions = cData.Absorbtions;

        DefaultHealth = Health;
        DefaultMana = Mana;
        DefaultStrength = Strength;
        DefaultDefense = Defense;
        DefaultIntelligence = Intelligence;
    }

    public void DefaultStats()
    {
        CharData.Health = DefaultHealth;
        CharData.Mana = DefaultMana;
        CharData.Strength = DefaultStrength;
        CharData.Defense = DefaultDefense;
        CharData.Intelligence = DefaultIntelligence;
    }
}
