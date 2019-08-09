﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EffectStatus { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste, Doom, StrengthUP, DefenseUP, BloodAndSinew, DefenseDOWN }

public class StatusIcon : MonoBehaviour
{
    private Enemy enemyTarget = null;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private GameObject StatusPanel;

    [SerializeField]
    private TextMeshProUGUI DurationText, StatusDescriptionText;

    [SerializeField]
    private ParticleSystem StatusRemovalParticle, DoomParticle;

    [SerializeField]
    private EffectStatus status;

    private Skills skill;

    private int TempSkillIndex;

    private int index;

    [SerializeField]
    private float Duration;

    private float DamageOrHealTick; //The amount of seconds that passes before taking damage from poison effects or healing from regen effects.

    private float TempTick;

    [SerializeField]
    private int KeyInput;

    public Enemy GetEnemyTarget
    {
        get
        {
            return enemyTarget;
        }
        set
        {
            enemyTarget = value;
        }
    }

    public EffectStatus GetEffectStatus
    {
        get
        {
            return status;
        }
        set
        {
            status = value;
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

    private void Start()
    {
        switch(status)
        {
            case (EffectStatus.StrengthUP):
                StrengthUP((int)SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency);
                SkillsManager.Instance.GetCharacterMenu.GetStatColor = "<#EFDFB8>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.Haste):
                Haste((int)SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency);
                break;
        }
    }

    private void OnEnable()
    {
        StatusPanel.SetActive(false);
    }

    public void RemoveEffect()
    {
        if (enemyTarget != null)
        {
            RemoveEnemyStatusEffectText();
            ObjectPooler.Instance.ReturnPlayerStatusIconToPool(this.gameObject);
        }
        else
        {
            RemoveStatusEffectText();
            ObjectPooler.Instance.ReturnPlayerStatusIconToPool(this.gameObject);
        }
    }

    public void PlayerInput()
    {
        KeyInput = SkillsManager.Instance.GetKeyInput;

        skill = SkillsManager.Instance.GetSkills[KeyInput];

        TempSkillIndex = skill.GetIndex;

        Duration = SkillsManager.Instance.GetSkills[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=16>" + "<u>" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "</u>" + "</color>" + "</size>" +
                                     "\n" + "<size=15>" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;

        DamageOrHealTick = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency;

        TempTick = DamageOrHealTick;
    }

    public void EnemyInput()
    {
        SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetSleepHit = false;

        KeyInput = enemyTarget.GetAI.GetAiStates[enemyTarget.GetAI.GetStateArrayIndex].GetSkillIndex;

        Duration = enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=16>" + "<u>" + enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName + "</u>" + "</color>" +
                                     "\n" + "</size>" + "<size=15>" + enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDescription;

        DamageOrHealTick = enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectPotency;

        TempTick = DamageOrHealTick;
    }

    //Called when a status effect cast by the player onto themselves gets removed.
    public TextMeshProUGUI RemoveStatusEffectText()
    {
        var StatusEffectTxt = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectTxt.SetActive(true);

        StatusEffectTxt.transform.SetParent(skill.GetTextHolder.transform, false);

        StatusEffectTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- " + skill.GetStatusEffectName;

        StatusEffectTxt.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        CreateParticleOnRemovePlayer();

        switch (status)
        {
            case (EffectStatus.StrengthUP):
                SetStrengthToDefault();
                SkillsManager.Instance.GetCharacterMenu.GetStatColor = "<#FFFFFF>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.Haste):
                ResetSpeedAndCoolDowns();
                break;
        }   

        return StatusEffectTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    //Called when a status effect cast by an enemy onto the player gets removed.
    public TextMeshProUGUI RemoveEnemyStatusEffectText()
    {
        var StatusEffectTxt = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectTxt.SetActive(true);

        StatusEffectTxt.transform.SetParent(enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectHolder.transform, false);

        StatusEffectTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- " + enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;

        StatusEffectTxt.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        CreateParticleOnRemovePlayer();

        switch (status)
        {
            case (EffectStatus.Stun):
                SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = true;
                SkillsManager.Instance.GetDisruptedSkill = false;
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().enabled = true;
                break;
            case (EffectStatus.Sleep):
                SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = true;
                SkillsManager.Instance.GetDisruptedSkill = false;
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().enabled = true;
                break;
            case (EffectStatus.Doom):
                SkillsManager.Instance.GetCharacter.GetComponent<Health>().ModifyHealth(-SkillsManager.Instance.GetCharacter.CurrentHealth);
                break;
        }
        return StatusEffectTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void DamageOverTime(int value)
    {
        TempTick -= Time.deltaTime;
        if (TempTick <= 0)
        {
            SkillsManager.Instance.GetCharacter.GetComponent<Health>().ModifyHealth(-value);

            var Damagetxt = ObjectPooler.Instance.GetPlayerDamageText();

            Damagetxt.SetActive(true);

            Damagetxt.transform.SetParent(SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetDamageTextParent.transform, false);

            Damagetxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + value.ToString();

            TempTick = DamageOrHealTick;
        }
    }

    private void HealOverTime(int value)
    {
        TempTick -= Time.deltaTime;
        if (TempTick <= 0)
        {
            SkillsManager.Instance.GetCharacter.GetComponent<Health>().ModifyHealth(value);

            var HealTxt = ObjectPooler.Instance.GetPlayerHealText();

            HealTxt.SetActive(true);

            HealTxt.transform.SetParent(SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetDamageTextParent.transform, false);

            HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + value.ToString();

            TempTick = DamageOrHealTick;
        }
    }

    private void Stun()
    {
        SkillsManager.Instance.GetDisruptedSkill = true;
        SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = false;

        SkillsManager.Instance.GetCharacter.GetComponent<PlayerAnimations>().EndAttackAnimation();
        SkillsManager.Instance.GetCharacter.GetComponent<PlayerAnimations>().GetAnimator.SetFloat("Speed", 0.0f);


        SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().enabled = false;
    }

    private void Sleep()
    {
        SkillsManager.Instance.GetDisruptedSkill = true;
        SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = false;
        SkillsManager.Instance.GetCharacter.GetComponent<PlayerAnimations>().EndAttackAnimation();
        SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().enabled = false;
        if (SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetSleepHit)
        {
            Duration = 0;
        }
    }

    private void Haste(int value)
    {
        float Percentage = (float)value / 100;

        float TempSpeed = (float)SkillsManager.Instance.GetCharacter.GetMoveSpeed;

        TempSpeed += (float)SkillsManager.Instance.GetCharacter.GetMoveSpeed * Percentage;

        Mathf.Round(TempSpeed);

        SkillsManager.Instance.GetCharacter.GetMoveSpeed = (int)TempSpeed;

        foreach (Skills s in SkillsManager.Instance.GetSkills)
        {
            float TempCoolDown = s.GetCoolDown;
            TempCoolDown -= s.GetCoolDown * Percentage;
            Mathf.Round(TempCoolDown);
            s.GetCoolDown = (int)TempCoolDown;
        }
    }

    private void StrengthUP(int value)
    {
        float Percentage = (float)value / 100;

        float TempStrength = (float)SkillsManager.Instance.GetCharacter.CharacterStrength;

        TempStrength += (float)SkillsManager.Instance.GetCharacter.CharacterStrength * Percentage;

        Mathf.Round(TempStrength);

        SkillsManager.Instance.GetCharacter.CharacterStrength = (int)TempStrength;
    }

    private void DefenseUP(int value)
    {
        float Percentage = (float)value / 100;

        float TempDefense = (float)SkillsManager.Instance.GetCharacter.CharacterDefense;

        TempDefense += (float)SkillsManager.Instance.GetCharacter.CharacterDefense * Percentage;

        Mathf.Round(TempDefense);

        SkillsManager.Instance.GetCharacter.CharacterDefense = (int)TempDefense;
    }

    private void IntelligenceUP(int value)
    {
        float Percentage = (float)value / 100;

        float TempIntelligence = (float)SkillsManager.Instance.GetCharacter.CharacterIntelligence;

        TempIntelligence += (float)SkillsManager.Instance.GetCharacter.CharacterIntelligence * Percentage;

        Mathf.Round(TempIntelligence);

        SkillsManager.Instance.GetCharacter.CharacterIntelligence = (int)TempIntelligence;
    }

    private void StrengthDOWN(int value)
    {
        float Percentage = (float)value / 100;

        float TempStrength = (float)SkillsManager.Instance.GetCharacter.CharacterStrength;

        TempStrength -= (float)SkillsManager.Instance.GetCharacter.CharacterStrength * Percentage;

        Mathf.Round(TempStrength);

        SkillsManager.Instance.GetCharacter.CharacterStrength = (int)TempStrength;
    }

    private void DefenseDOWN(int value)
    {
        float Percentage = (float)value / 100;

        float TempDefense = (float)SkillsManager.Instance.GetCharacter.CharacterDefense;

        TempDefense -= (float)SkillsManager.Instance.GetCharacter.CharacterDefense * Percentage;

        Mathf.Round(TempDefense);

        SkillsManager.Instance.GetCharacter.CharacterDefense = (int)TempDefense;
    }

    private void IntelligenceDOWN(int value)
    {
        float Percentage = (float)value / 100;

        float TempIntelligence = (float)SkillsManager.Instance.GetCharacter.CharacterIntelligence;

        TempIntelligence -= (float)SkillsManager.Instance.GetCharacter.CharacterIntelligence * Percentage;

        Mathf.Round(TempIntelligence);

        SkillsManager.Instance.GetCharacter.CharacterIntelligence = (int)TempIntelligence;
    }

    private void ResetSpeedAndCoolDowns()
    {
        float DefaultSpeed = SkillsManager.Instance.GetCharacter.GetCharacterData.MoveSpeed;

        SkillsManager.Instance.GetCharacter.GetMoveSpeed = DefaultSpeed;
    }

    private void SetStrengthToDefault()
    {
        int DefaultStrength = SkillsManager.Instance.GetCharacter.GetCharacterData.Strength;

        SkillsManager.Instance.GetCharacter.CharacterStrength = DefaultStrength;
    }

    private void SetDefenseToDefault()
    {
        int DefaultDefense = SkillsManager.Instance.GetCharacter.GetCharacterData.Defense;

        SkillsManager.Instance.GetCharacter.CharacterDefense = DefaultDefense;
    }

    private void SetIntelligenceToDefault()
    {
        int DefaultIntelligence = SkillsManager.Instance.GetCharacter.GetCharacterData.Intelligence;

        SkillsManager.Instance.GetCharacter.CharacterIntelligence = DefaultIntelligence;
    }

    private void BloodAndSinew()
    {

    }

    private void CheckStatusEffectIcon()
    {
        switch (status)
        {
            case (EffectStatus.DamageOverTime):
                DamageOverTime(RegenAndDOTCalculation());
                break;
            case (EffectStatus.HealthRegen):
                HealOverTime(RegenAndDOTCalculation());
                break;
            case (EffectStatus.Stun):
                Stun();
                break;
            case (EffectStatus.Sleep):
                Sleep();
                break;
        }
    }

    private int RegenAndDOTCalculation()
    {
        float percent = Mathf.Round(0.1f * (float)SkillsManager.Instance.GetCharacter.MaxHealth);

        int GetHealth = (int)percent;

        Mathf.Round(GetHealth);

        return GetHealth;
    }

    private void CreateParticleOnRemovePlayer()
    {
        if(status == EffectStatus.Doom)
        {
            var chara = SkillsManager.Instance.GetCharacter;

            var Statusparticle = Instantiate(DoomParticle, new Vector3(chara.transform.position.x,
                                                                       chara.transform.position.y + 1f,
                                                                       chara.transform.position.z), transform.rotation);

            Statusparticle.transform.SetParent(chara.transform, true);
        }
        else
        {
            if(settings.UseParticleEffects)
            {
                var character = SkillsManager.Instance.GetCharacter;

                var StatusParticle = ObjectPooler.Instance.GetRemoveStatusParticle();

                StatusParticle.SetActive(true);

                StatusParticle.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 1.0f, character.transform.position.z);

                StatusParticle.transform.SetParent(character.transform);
            }
        }
    }

    private void CreateParticleOnRemoveEnemy()
    {
        if(settings.UseParticleEffects)
        {
            var StatusParticle = ObjectPooler.Instance.GetRemoveStatusParticle();

            StatusParticle.SetActive(true);

            StatusParticle.transform.position = new Vector3(enemyTarget.transform.position.x, enemyTarget.transform.position.y + 1.0f, enemyTarget.transform.position.z);

            StatusParticle.transform.SetParent(enemyTarget.transform, true);
        }
    }

    private void Update()
    {
        CheckStatusEffectIcon();

        if(Duration <= -1)
        {
            if(SkillsManager.Instance.GetCharacter.CurrentHealth <= 0)
            {
                RemoveEffect();
            }
        }
        else if(Duration > -1)
        {
            DurationText.text = Duration.ToString("F0");
            Duration -= Time.deltaTime;
            if (Duration <= 0 || SkillsManager.Instance.GetCharacter.CurrentHealth <= 0)
            {
                RemoveEffect();
            }
        }
    }
}