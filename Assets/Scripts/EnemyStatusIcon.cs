#pragma warning disable 0649
#pragma warning disable 0414
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public enum StatusEffect { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste, Doom, StrengthUP, DefenseUP, IntelligenceUP, StrengthDOWN, DefenseDOWN,
                           IntelligenceDOWN, StrengthAndCriticalUP, DefenseAndIntelligenceUP, Slow, Burning, Poison };

public class EnemyStatusIcon : MonoBehaviour
{
    [SerializeField]
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

    private bool HasBurnStatus, HasSlowStatus, HasPoisonStatus, HasStunStatus, HasDoomedStatus, HasDefenseDownStatus, HasIntelligenceDownStatus, HasStrengthDownStatus;

    [SerializeField]
    private TextMeshProUGUI DurationText, StatusDescriptionText;

    [SerializeField]
    private float Duration;

    private float DamageOrHealTick, TempTick;

    private int KeyInput;

    [SerializeField]
    private GameObject StatusPanel;

    private GameObject StunParticle = null, BurningParticle = null, PoisonParticle = null;

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

    public bool GetHasDefenseDownStatus
    {
        get
        {
            return HasDefenseDownStatus;
        }
        set
        {
            HasDefenseDownStatus = value;
        }
    }

    public bool GetHasIntelligenceDownStatus
    {
        get
        {
            return HasIntelligenceDownStatus;
        }
        set
        {
            HasIntelligenceDownStatus = value;
        }
    }

    public bool GetHasStrengthDownStatus
    {
        get
        {
            return HasStrengthDownStatus;
        }
        set
        {
            HasStrengthDownStatus = value;
        }
    }

    public bool GetHasStunStatus
    {
        get
        {
            return HasStunStatus;
        }
        set
        {
            HasStunStatus = value;
        }
    }

    public bool GetHasPoisonStatus
    {
        get
        {
            return HasPoisonStatus;
        }
        set
        {
            HasPoisonStatus = value;
        }
    }

    public bool GetHasSlowStatus
    {
        get
        {
            return HasSlowStatus;
        }
        set
        {
            HasSlowStatus = value;
        }
    }

    public bool GetHasDoomedStatus
    {
        get
        {
            return HasDoomedStatus;
        }
        set
        {
            HasDoomedStatus = value;
        }
    }

    public void CheckStatusEffects()
    {
        if(player == null)
        {
            EnemyInput();
        }

        switch (effect)
        {
            case (StatusEffect.DefenseDOWN):
                HasDefenseDownStatus = true;
                DefenseDOWN(50);
                break;
            case (StatusEffect.DefenseUP):
                DefenseUP(50);
                break;
            case (StatusEffect.IntelligenceDOWN):
                HasIntelligenceDownStatus = true;
                IntelligenceDOWN(50);
                break;
            case (StatusEffect.StrengthDOWN):
                HasStrengthDownStatus = true;
                StrengthDOWN(50);
                break;
            case (StatusEffect.DefenseAndIntelligenceUP):
                IntelligenceUP(50);
                DefenseUP(50);
                break;
            case (StatusEffect.StrengthAndCriticalUP):
                StrengthUP(15);
                CriticalUP(5);
                break;
            case (StatusEffect.Stun):
                HasStunStatus = true;
                CreateStunEffectParticle();
                break;
            case (StatusEffect.Poison):
                HasPoisonStatus = true;
                CreatePoisonEffectParticle();
                break;
            case (StatusEffect.Slow):
                HasSlowStatus = true;
                Slow();
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
            effect = StatusEffect.NONE;
            player = null;
            ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
        }
        else
        {
            if (HasBurnStatus)
            {
                RemoveBurnStatusEffectText();
                CheckBurningParticleActive();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            else if(HasSlowStatus)
            {
                RemoveSlowedStatusEffectText();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            else if(HasPoisonStatus)
            {
                RemovePoisonStatusEffectText();
                CheckPoisonParticleActive();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            else if(HasStunStatus)
            {
                RemoveStunStatusEffectText();
                CheckStunParticleActive();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            else if(HasDoomedStatus)
            {
                RemoveDoomedStatusEffectText();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            else if(HasDefenseDownStatus)
            {
                RemoveDefenseDownStatusEffectText();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            else if (HasIntelligenceDownStatus)
            {
                RemoveIntelligenceDownStatusEffectText();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            else if (HasStrengthDownStatus)
            {
                RemoveStrengthDownStatusEffectText();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            else
            {
                RemoveEnemyStatusEffectText();
                ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
            }
            effect = StatusEffect.NONE;

            player = null;
        }
        if(player != null)
        {
            effect = StatusEffect.NONE;
            player = null;
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

        if (Duration < 0)
        {
            DurationText.text = "";
        }
    }

    public void PlayerInput()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        KeyInput = SkillsManager.Instance.GetKeyInput;

        skill = SkillsManager.Instance.GetSkills[KeyInput];

        TempSkillIndex = skill.GetIndex;

        if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetDoublesStatusDuration)
        {
            Duration = skill.GetStatusDuration * 2;
        }
        else
        {
            Duration = skill.GetStatusDuration;
        }

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

    public void DoomedStatus()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        Duration = 5.0f;

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u> Doomed </u>" + "</color>" +
                                     "</size>" + "\n" + "<size=10> Sudden death approaches.";

        DamageOrHealTick = 3.0f;

        TempTick = DamageOrHealTick;
    }

    public void PoisonStatus()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        if(SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetDoublesStatusDuration)
        {
            Duration = 30.0f;
        }
        else
        {
            Duration = 15.0f;
        }

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u> Poison </u>" + "</color>" +
                                     "</size>" + "\n" + "<size=10> Taking damage";

        DamageOrHealTick = 3.0f;

        TempTick = DamageOrHealTick;
    }

    public void SlowStatus()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetDoublesStatusDuration)
        {
            Duration = 20.0f;
        }
        else
        {
            Duration = 10.0f;
        }

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u> Slowed </u>" + "</color>" +
                                     "</size>" + "\n" + "<size=10> Decreased movement & Increased Auto-attack time";

        Slow();
    }

    public void DefenseDownStatus()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetDoublesStatusDuration)
        {
            Duration = 30.0f;
        }
        else
        {
            Duration = 15.0f;
        }

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u> Defense Down </u>" + "</color>" +
                                     "</size>" + "\n" + "<size=10> Lowered Defense";

        DefenseDOWN(50);
    }

    public void IntelligenceDownStatus()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetDoublesStatusDuration)
        {
            Duration = 30.0f;
        }
        else
        {
            Duration = 15.0f;
        }

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u> Intelligence Down </u>" + "</color>" +
                                     "</size>" + "\n" + "<size=10> Lowered Intelligence";

        IntelligenceDOWN(50);
    }

    public void StrengthDownStatus()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetDoublesStatusDuration)
        {
            Duration = 30.0f;
        }
        else
        {
            Duration = 15.0f;
        }

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u> Strength Down </u>" + "</color>" +
                                     "</size>" + "\n" + "<size=10> Lowered Strength";

        StrengthDOWN(50);
    }

    public void StunStatus()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetDoublesStatusDuration)
        {
            Duration = 10.0f;
        }
        else
        {
            Duration = 5.0f;
        }

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=12>" + "<u> Stun </u>" + "</color>" +
                                     "</size>" + "\n" + "<size=10> Unable to act";
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
                character.GetComponentInChildren<Health>().ModifyHealth(-character.MaxHealth);
                break;
        }

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemovePoisonStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Poison";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        HasPoisonStatus = false;

        CreateParticleOnRemoveEnemy();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveBurnStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Burning";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        HasBurnStatus = false;

        CreateParticleOnRemoveEnemy();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveDoomedStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Doomed";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        HasDoomedStatus = false;

        CreateParticleOnRemoveEnemy();

        character.GetComponentInChildren<Health>().ModifyHealth(-character.CurrentHealth);

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveSlowedStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Slowed";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        HasSlowStatus = false;

        CreateParticleOnRemoveEnemy();

        if(character.GetComponent<EnemyAI>())
        {
            character.GetComponent<EnemyAI>().GetMoveSpeed = character.GetComponent<EnemyAI>().GetDefaultMoveSpeed;
            character.GetComponent<EnemyAI>().GetAttackDelay = character.GetComponent<EnemyAI>().GetDefaultAttackDelay;
        }
        if(character.GetComponent<Puck>())
        {
            character.GetComponent<Puck>().GetMoveSpeed = character.GetComponent<Puck>().GetDefaultMoveSpeed;
            character.GetComponent<Puck>().GetAttackDelay = character.GetComponent<Puck>().GetDefaultAttackDelay;
        }
        if (character.GetComponent<RuneGolem>())
        {
            character.GetComponent<RuneGolem>().GetMoveSpeed = character.GetComponent<RuneGolem>().GetDefaultMoveSpeed;
            character.GetComponent<RuneGolem>().GetAttackDelay = character.GetComponent<RuneGolem>().GetDefaultAttackDelay;
        }
        if (character.GetComponent<SylvanDiety>())
        {
            character.GetComponent<SylvanDiety>().GetMoveSpeed = character.GetComponent<SylvanDiety>().GetDefaultMoveSpeed;
            character.GetComponent<SylvanDiety>().GetAttackDelay = character.GetComponent<SylvanDiety>().GetDefaultAttackDelay;
        }

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveDefenseDownStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Defense Down";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        HasDefenseDownStatus = false;

        CreateParticleOnRemoveEnemy();

        SetDefenseToDefault();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveIntelligenceDownStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Intelligence Down";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        HasIntelligenceDownStatus = false;

        CreateParticleOnRemoveEnemy();

        SetIntelligenceToDefault();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveStrengthDownStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Strength Down";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        HasStrengthDownStatus = false;

        CreateParticleOnRemoveEnemy();

        SetStrengthToDefault();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveStunStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Stun";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        HasStunStatus = false;

        CreateParticleOnRemoveEnemy();

        CheckEnemyStates();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void CheckEnemyStates()
    {
        if(character.GetComponent<EnemyAI>())
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
        if (character.GetComponent<Puck>())
        {
            if (character.GetComponent<Puck>().GetPlayerTarget == null)
            {
                character.GetComponent<EnemySkills>().GetDisruptedSkill = false;
            }
            else
            {
                character.GetComponent<EnemySkills>().GetDisruptedSkill = false;
                character.GetComponent<Puck>().GetStates = BossStates.Chase;
            }
        }
        if (character.GetComponent<RuneGolem>())
        {
            if (character.GetComponent<RuneGolem>().GetPlayerTarget == null)
            {
                character.GetComponent<EnemySkills>().GetDisruptedSkill = false;
            }
            else
            {
                character.GetComponent<EnemySkills>().GetDisruptedSkill = false;
                character.GetComponent<RuneGolem>().GetStates = RuneGolemStates.Chase;
            }
        }
        if (character.GetComponent<SylvanDiety>())
        {
            if (character.GetComponent<SylvanDiety>().GetPlayerTarget == null)
            {
                character.GetComponent<EnemySkills>().GetDisruptedSkill = false;
            }
            else
            {
                character.GetComponent<EnemySkills>().GetDisruptedSkill = false;
                character.GetComponent<SylvanDiety>().GetSylvanDietyStates = SylvanDietyBossStates.Chase;
            }
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

            if(character.GetComponent<Puck>())
            {
                character.GetComponent<Puck>().CheckHP();
            }
        }
    }

    private void Stun()
    {
        if (character.GetComponent<EnemyAI>())
        {
            character.GetComponent<EnemySkills>().GetDisruptedSkill = true;
            character.GetComponent<EnemyAI>().GetStates = States.Immobile;
        }
        if (character.GetComponent<Puck>() || character.GetComponent<RuneGolem>() || character.GetComponent<SylvanDiety>())
        {
            return;
        }
    }

    private void Sleep()
    {
        if (character.GetComponent<EnemyAI>())
        {
            character.GetComponent<EnemySkills>().GetDisruptedSkill = true;
            character.GetComponent<EnemyAI>().GetStates = States.Immobile;
            if (character.GetComponentInChildren<Health>().GetSleepHit)
            {
                Duration = 0;
            }
        }
        if (character.GetComponent<Puck>() || character.GetComponent<RuneGolem>() || character.GetComponent<SylvanDiety>())
        {
            return;
        }
    }

    private void Slow()
    {
        if(character.GetComponent<EnemyAI>())
        {
            character.GetComponent<EnemyAI>().GetMoveSpeed = character.GetComponent<EnemyAI>().GetMoveSpeed / 2;
            character.GetComponent<EnemyAI>().GetAttackDelay += 1;
        }
        if(character.GetComponent<Puck>())
        {
            character.GetComponent<Puck>().GetMoveSpeed = character.GetComponent<Puck>().GetMoveSpeed / 2;
            character.GetComponent<Puck>().GetAttackDelay += 1;
        }
        if (character.GetComponent<RuneGolem>())
        {
            character.GetComponent<RuneGolem>().GetMoveSpeed = character.GetComponent<RuneGolem>().GetMoveSpeed / 2;
            character.GetComponent<RuneGolem>().GetAttackDelay += 1;
        }
        if (character.GetComponent<SylvanDiety>())
        {
            character.GetComponent<SylvanDiety>().GetMoveSpeed = character.GetComponent<SylvanDiety>().GetMoveSpeed / 2;
            character.GetComponent<SylvanDiety>().GetAttackDelay += 1;
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

    private void StrengthDOWN(float value)
    {
        float Percentage = (float)value / 100;

        float TempStrength = (float)character.CharacterStrength;

        TempStrength -= (float)character.CharacterStrength * Percentage;

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

    private void IntelligenceDOWN(float value)
    {
        float Percentage = (float)value / 100;

        float TempIntelligence = (float)character.CharacterIntelligence;

        TempIntelligence -= (float)character.CharacterIntelligence * Percentage;

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

    private void CheckStatus()
    {
        switch (effect)
        {
            case (StatusEffect.HealthRegen):
                HealthRegen(RegenAndDOTCalculation(), 3f);
                break;
            case (StatusEffect.DamageOverTime):
                DamageOverTime(RegenAndDOTCalculation());
                break;
            case (StatusEffect.Burning):
                DamageOverTime(RegenAndDOTCalculation());
                break;
            case (StatusEffect.Poison):
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
        bool IsABoss = character.GetComponent<EnemyAI>() ? false : true;

        float percent = 0;
        
        if(!IsABoss)
        {
            percent = Mathf.Round(0.1f * (float)character.MaxHealth);
        }
        else
        {
            percent = Mathf.Round(0.01f * (float)character.MaxHealth);
        }

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

    private void CheckBurningParticleActive()
    {
        if (BurningParticle.activeInHierarchy)
        {
            ObjectPooler.Instance.ReturnBurningEffectParticleToPool(BurningParticle);
        }
    }

    private void CheckPoisonParticleActive()
    {
        if (PoisonParticle.activeInHierarchy)
        {
            ObjectPooler.Instance.ReturnPoisonEffectParticleToPool(PoisonParticle);
        }
    }

    public void CreatePoisonEffectParticle()
    {
        if (settings.UseParticleEffects)
        {
            var cHARACTER = character;

            var PP = ObjectPooler.Instance.GetPoisonEffectParticle();

            PoisonParticle = PP;

            PP.SetActive(true);

            PP.transform.position = new Vector3(cHARACTER.transform.position.x, cHARACTER.transform.position.y + 0.8f, cHARACTER.transform.position.z);

            PP.transform.SetParent(cHARACTER.transform, true);

            PP.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void CreateStunEffectParticle()
    {
        if(character.GetComponent<EnemyAI>())
        {
            if (settings.UseParticleEffects)
            {
                var cHARACTER = character;

                var SP = ObjectPooler.Instance.GetStunEffectParticle();

                StunParticle = SP;

                SP.SetActive(true);

                SP.transform.position = new Vector3(cHARACTER.transform.position.x, cHARACTER.transform.position.y + 0.8f, cHARACTER.transform.position.z);

                SP.transform.SetParent(cHARACTER.transform, true);

                SP.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void CreateBurningParticle()
    {
        var cHARACTER = character;

        var BP = ObjectPooler.Instance.GetBurningEffectParticle();

        BurningParticle = BP;

        BP.SetActive(true);

        BP.transform.position = new Vector3(cHARACTER.transform.position.x, cHARACTER.transform.position.y + 1f, cHARACTER.transform.position.z);

        BP.transform.SetParent(cHARACTER.transform, true);

        BP.transform.localScale = new Vector3(1, 1, 1);
    }

    private void LateUpdate()
    {
        ToggleStatusIcon();

        CheckStatus();

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
    }
}