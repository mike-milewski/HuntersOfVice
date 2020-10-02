﻿#pragma warning disable 0649
#pragma warning disable 0414
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public enum StatusEffect { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste, Doom, StrengthUP, DefenseUP, IntelligenceUP, StrengthDOWN, DefenseDOWN,
                           IntelligenceDOWN, StrengthAndCriticalUP, DefenseAndIntelligenceUP };

public class EnemyStatusIcon : MonoBehaviour
{
    private PlayerController player = null;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Character character = null;

    [SerializeField]
    private ParticleSystem StatusRemovalParticle;

    [SerializeField]
    private StatusEffect effect;

    private Skills skill;

    private int TempSkillIndex;

    private bool HasBurnStatus;

    [SerializeField]
    private TextMeshProUGUI DurationText, StatusDescriptionText;

    [SerializeField]
    private float Duration;

    private float DamageOrHealTick, TempTick;

    private int KeyInput;

    [SerializeField]
    private GameObject StatusPanel;

    private GameObject StunParticle = null;

    public PlayerController GetPlayer
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }

    public StatusEffect GetStatusEffect
    {
        get
        {
            return effect;
        }
        set
        {
            effect = value;
        }
    }

    public int GetKeyInput
    {
        get
        {
            return KeyInput;
        }
        set
        {
            KeyInput = value;
        }
    }

    public bool GetHasBurnStatus
    {
        get
        {
            return HasBurnStatus;
        }
        set
        {
            HasBurnStatus = value;
        }
    }

    private void Start()
    {
        if(player == null)
        {
            EnemyInput();
        }

        switch (effect)
        {
            case (StatusEffect.DefenseDOWN):
                DefenseDOWN(50);
                break;
            case (StatusEffect.DefenseUP):
                DefenseUP(50);
                break;
            case (StatusEffect.DefenseAndIntelligenceUP):
                IntelligenceUP(50);
                DefenseUP(50);
                break;
            case (StatusEffect.StrengthAndCriticalUP):
                StrengthUP(50);
                CriticalUP(5);
                break;
            case (StatusEffect.Stun):
                CreateStunEffectParticle();
                break;
        }
    }

    private void OnEnable()
    {
        StatusPanel.SetActive(false);
    }

    public void RemoveEffect()
    {
        if (character.GetComponent<PlayerController>())
        {
            RemoveStatusEffectText();
            ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
        }
        else
        {
            if(HasBurnStatus)
            {
                RemoveBurnStatusEffectText();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            else
            {
                RemoveEnemyStatusEffectText();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
        }
    }

    public void EnemyInput()
    {
        character = GetComponentInParent<Character>();

        effect = (StatusEffect)character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatus;

        if (character.GetComponent<Puck>())
        {
            KeyInput = character.GetComponent<Puck>().GetPhases[character.GetComponent<Puck>().GetPhaseIndex].GetBossAiStates[character.GetComponent<Puck>().GetStateArrayIndex].GetSkillIndex;
        }
        if(character.GetComponent<EnemyAI>())
        {
            KeyInput = character.GetComponent<EnemyAI>().GetAiStates[character.GetComponent<EnemyAI>().GetStateArrayIndex].GetSkillIndex;
        }

        Duration = character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u>" + character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName + "</u>" + "</color>" + 
                                     "</size>" + "\n" + "<size=10>" + character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDescription;

        DamageOrHealTick = character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectPotency;

        TempTick = DamageOrHealTick;
    }

    public void PlayerInput()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        KeyInput = SkillsManager.Instance.GetKeyInput;

        skill = SkillsManager.Instance.GetSkills[KeyInput];

        TempSkillIndex = skill.GetIndex;

        Duration = skill.GetStatusDuration;

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u>" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "</u>" + "</color>" +
                                     "</size>" + "\n" + "<size=10>" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;

        DamageOrHealTick = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency;

        TempTick = DamageOrHealTick;
    }

    public void BurnStatus()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        Duration = 10.0f;

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u> Burning </u>" + "</color>" +
                                     "</size>" + "\n" + "<size=10> Taking damage over time.";

        DamageOrHealTick = 3.0f;

        TempTick = DamageOrHealTick;
    }

    public TextMeshProUGUI RemoveStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- " + character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        CreateParticleOnRemovePlayer();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveEnemyStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        if(player == null)
        {
            if(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetIsBuff)
            {
                StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- " + 
                                                                                  character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;
            }
        }
        else
        {
            StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- " + skill.GetStatusEffectName;
        }

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        CreateParticleOnRemoveEnemy();

        switch (effect)
        {
            case (StatusEffect.Stun):
                CheckEnemyStates();
                CheckStunParticleActive();
                break;
            case (StatusEffect.Sleep):
                CheckEnemyStates();
                break;
            case (StatusEffect.DefenseDOWN):
                SetDefenseToDefault();
                break;
            case (StatusEffect.DefenseUP):
                SetDefenseToDefault();
                break;
            case (StatusEffect.StrengthAndCriticalUP):
                SetStrengthToDefault();
                SetCriticalToDefault();
                break;
            case (StatusEffect.DefenseAndIntelligenceUP):
                SetDefenseToDefault();
                SetIntelligenceToDefault();
                break;
            case (StatusEffect.Doom):
                character.GetComponentInChildren<Health>().ModifyHealth(-character.CurrentHealth);
                break;
        }
        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveBurnStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Burning";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        CreateParticleOnRemoveEnemy();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void CheckEnemyStates()
    {
        if (character.GetComponent<EnemyAI>().GetPlayerTarget == null)
        {
            character.GetComponent<EnemySkills>().GetDisruptedSkill = false;
            character.GetComponent<EnemyAI>().GetStates = States.Patrol;
        }
        else
        {
            character.GetComponent<EnemySkills>().GetDisruptedSkill = false;
            character.GetComponent<EnemyAI>().GetStates = States.Chase;
        }
    }

    private void ResetStatusEffect()
    {
        Duration = SkillsManager.Instance.GetSkills[KeyInput].GetStatusDuration;
    }

    private void ToggleStatusIcon()
    {
        if (GameManager.Instance.GetEnemyObject == character.GetComponent<Enemy>().gameObject)
        {
            foreach(Image i in this.GetComponentsInChildren<Image>())
            {
                i.enabled = true;
            }
            DurationText.enabled = true;
        }
        else if (GameManager.Instance.GetEnemyObject != character.GetComponent<Enemy>().gameObject)
        {
            foreach (Image i in this.GetComponentsInChildren<Image>())
            {
                i.enabled = false;
            }
            DurationText.enabled = false;
        }
    }

    private void HealthRegen(int value, float healTick)
    {
        DamageOrHealTick -= Time.deltaTime;
        if (DamageOrHealTick <= 0)
        {
            character.GetComponent<Enemy>().GetHealth.IncreaseHealth(value);
            character.GetComponent<Enemy>().GetLocalHealthInfo();

            var Healtxt = ObjectPooler.Instance.GetEnemyHealText();

            Healtxt.SetActive(true);

            Healtxt.transform.SetParent(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

            Healtxt.GetComponentInChildren<Text>().text = value.ToString();

            DamageOrHealTick = healTick;
        }
    }

    private void DamageOverTime(int value)
    {
        TempTick -= Time.deltaTime;
        if (TempTick <= 0)
        {
            var EnemyDamagetxt = ObjectPooler.Instance.GetEnemyDamageText();

            EnemyDamagetxt.transform.SetParent(character.GetComponentInChildren<Health>().GetDamageTextParent.transform, false);

            EnemyDamagetxt.SetActive(true);

            EnemyDamagetxt.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();

            character.GetComponent<Enemy>().GetHealth.ModifyHealth(-value);
            character.GetComponent<Enemy>().GetLocalHealthInfo();

            TempTick = DamageOrHealTick;
        }
    }

    private void Stun()
    {
        character.GetComponent<EnemySkills>().GetDisruptedSkill = true;
        character.GetComponent<EnemyAI>().GetStates = States.Immobile;
    }

    private void Sleep()
    {
        character.GetComponent<EnemySkills>().GetDisruptedSkill = true;
        character.GetComponent<EnemyAI>().GetStates = States.Immobile;
        if (character.GetComponentInChildren<Health>().GetSleepHit)
        {
            Duration = 0;
        }
    }

    private void DefenseDOWN(float value)
    {
        float Percentage = (float)value / 100;

        float TempDefense = (float)character.CharacterDefense;

        TempDefense -= (float)character.CharacterDefense * Percentage;

        Mathf.Round(TempDefense);

        character.CharacterDefense = (int)TempDefense;
    }

    private void StrengthUP(float value)
    {
        float Percentage = (float)value / 100;

        float TempStrength = (float)character.CharacterStrength;

        TempStrength += (float)character.CharacterStrength * Percentage;

        Mathf.Round(TempStrength);

        character.CharacterStrength = (int)TempStrength;
    }

    private void DefenseUP(float value)
    {
        float Percentage = (float)value / 100;

        float TempDefense = (float)character.CharacterDefense;

        TempDefense += (float)character.CharacterDefense * Percentage;

        Mathf.Round(TempDefense);

        character.CharacterDefense = (int)TempDefense;
    }

    private void IntelligenceUP(float value)
    {
        float Percentage = (float)value / 100;

        float TempIntelligence = (float)character.CharacterIntelligence;

        TempIntelligence += (float)character.CharacterIntelligence * Percentage;

        Mathf.Round(TempIntelligence);

        character.CharacterIntelligence = (int)TempIntelligence;
    }

    private void CriticalUP(int value)
    {
        character.GetCriticalChance += value;
    }

    private void SetCriticalToDefault()
    {
        int DefaultCritical = character.GetCharacterData.CriticalHitChance;

        character.GetCriticalChance = DefaultCritical;
    }

    private void SetStrengthToDefault()
    {
        int DefaultStrength = character.GetCharacterData.Strength;

        character.CharacterStrength = DefaultStrength;
    }

    private void SetDefenseToDefault()
    {
        int DefaultDefense = character.GetCharacterData.Defense;

        character.CharacterDefense = DefaultDefense;
    }

    private void SetIntelligenceToDefault()
    {
        int DefaultIntelligence = character.GetCharacterData.Intelligence;

        character.CharacterIntelligence = DefaultIntelligence;
    }

    private void CheckStatusEffect()
    {
        switch (effect)
        {
            case (StatusEffect.HealthRegen):
                HealthRegen(RegenAndDOTCalculation(), 3f);
                break;
            case (StatusEffect.DamageOverTime):
                DamageOverTime(RegenAndDOTCalculation());
                break;
            case (StatusEffect.Stun):
                Stun();
                break;
            case (StatusEffect.Sleep):
                Sleep();
                break;
        }
    }

    private int RegenAndDOTCalculation()
    {
        float percent = Mathf.Round(0.1f * (float)character.MaxHealth);

        int GetHealth = (int)percent;

        return GetHealth;
    }

    private void CreateParticleOnRemovePlayer()
    {
        if(settings.UseParticleEffects)
        {
            var StatusParticle = ObjectPooler.Instance.GetRemoveStatusParticle();

            StatusParticle.SetActive(true);

            StatusParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z);

            StatusParticle.transform.SetParent(player.transform, true);
        }
    }

    private void CreateParticleOnRemoveEnemy()
    {
        if(settings.UseParticleEffects)
        {
            var StatusParticle = ObjectPooler.Instance.GetRemoveStatusParticle();

            StatusParticle.SetActive(true);

            StatusParticle.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 1.0f, character.transform.position.z);

            StatusParticle.transform.SetParent(character.transform, true);
        }
    }

    private void CheckStunParticleActive()
    {
        if (StunParticle.activeInHierarchy)
        {
            ObjectPooler.Instance.ReturnStunEffectParticleToPool(StunParticle);
        }
    }

    private void CreateStunEffectParticle()
    {
        if (settings.UseParticleEffects)
        {
            var cHARACTER = character;

            var SP = ObjectPooler.Instance.GetStunEffectParticle();

            StunParticle = SP;

            SP.SetActive(true);

            SP.transform.position = new Vector3(cHARACTER.transform.position.x, cHARACTER.transform.position.y + 0.8f, cHARACTER.transform.position.z);

            SP.transform.SetParent(cHARACTER.transform, true);
        }
    }

    private void LateUpdate()
    {
        ToggleStatusIcon();

        CheckStatusEffect();

        if(Duration <= -1)
        {
            if(character.CurrentHealth <= 0)
            {
                RemoveEffect();
            }
        }
        if(Duration > -1)
        {
            DurationText.text = Duration.ToString("F0");
            Duration -= Time.deltaTime;
            if (Duration <= 0 || character.CurrentHealth <= 0)
            {
                RemoveEffect();
            }
        }

        if (character.GetComponent<Puck>())
        {
            if (character.GetComponent<Puck>().GetIsReseted)
            {
                RemoveEffect();
                character.GetComponent<Puck>().GetIsReseted = false;
            }
        }
    }
}