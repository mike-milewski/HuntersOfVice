#pragma warning disable 0649
#pragma warning disable 0414
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EffectStatus { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste, Doom, StrengthUP, DefenseUP, IntelligenceUP, StrengthDOWN, DefenseDOWN,
                           IntelligenceDOWN, ContractWithEvil, ContractWithTheVile, ContractWithNefariousness, MaliciousPossession, ConsecratedDefense, Aegis }

public class StatusIcon : MonoBehaviour
{
    private Enemy enemyTarget = null;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private GameObject StatusPanel;

    private GameObject PoisonParticle = null, StunParticle = null;

    [SerializeField]
    private TextMeshProUGUI DurationText, StatusDescriptionText;

    [SerializeField]
    private ParticleSystem StatusRemovalParticle, DoomParticle;

    [SerializeField]
    private EffectStatus status;

    private Skills skill;

    private int TempSkillIndex;

    private int index, PlayerMaxHealth;

    [SerializeField]
    private float Duration;

    private float DamageOrHealTick, ContractHpTick, ContractMpTick, ContractValue; //The amount of seconds that passes before taking damage from poison effects or healing from regen effects.

    private float TempTick, ContractHpTempTick, ContractMpTempTick;

    private float RegenTick;

    private bool ObstacleEffect;

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

    public bool GetObstacleEffect
    {
        get
        {
            return ObstacleEffect;
        }
        set
        {
            ObstacleEffect = value;
        }
    }

    public float GetDuration
    {
        get
        {
            return Duration;
        }
        set
        {
            Duration = value;
        }
    }

    public float GetDamageOrHealTick
    {
        get
        {
            return DamageOrHealTick;
        }
        set
        {
            DamageOrHealTick = value;
        }
    }

    public float GetTempTick
    {
        get
        {
            return TempTick;
        }
        set
        {
            TempTick = value;
        }
    }

    public TextMeshProUGUI GetStatusDescription
    {
        get
        {
            return StatusDescriptionText;
        }
        set
        {
            StatusDescriptionText = value;
        }
    }

    private void Start()
    {
        switch(status)
        {
            case (EffectStatus.StrengthUP):
                StrengthUP((int)SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency);
                SkillsManager.Instance.GetCharacterMenu.GetStrengthStatColor = "<#EFDFB8>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.DefenseUP):
                DefenseUP((int)SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency);
                SkillsManager.Instance.GetCharacterMenu.GetDefenseStatColor = "<#EFDFB8>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.IntelligenceUP):
                IntelligenceUP((int)SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency);
                SkillsManager.Instance.GetCharacterMenu.GetIntelligenceStatColor = "<#EFDFB8>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.StrengthDOWN):
                StrengthDOWN(15);
                SkillsManager.Instance.GetCharacterMenu.GetStrengthStatColor = "<#FA2900>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.DefenseDOWN):
                DefenseDOWN(15);
                SkillsManager.Instance.GetCharacterMenu.GetDefenseStatColor = "<#FA2900>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.IntelligenceDOWN):
                IntelligenceDOWN(15);
                SkillsManager.Instance.GetCharacterMenu.GetIntelligenceStatColor = "<#FA2900>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.ContractWithEvil):
                IntelligenceUP((int)SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency);
                DefenseDOWN(15);
                SkillsManager.Instance.GetCharacterMenu.GetIntelligenceStatColor = "<#EFDFB8>";
                SkillsManager.Instance.GetCharacterMenu.GetDefenseStatColor = "<#FA2900>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.MaliciousPossession):
                MaliciousPossessionBuff();
                break;
            case (EffectStatus.ConsecratedDefense):
                ConsecratedDefenses();
                break;
            case (EffectStatus.Aegis):
                Aegis();
                break;
            case (EffectStatus.Haste):
                Haste((int)SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency);
                break;
            case (EffectStatus.DamageOverTime):
                CreatePoisonEffectParticle();
                break;
            case (EffectStatus.Stun):
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
        if (enemyTarget != null)
        {
            RemoveEnemyStatusEffectText();
            CheckStatus();
            ObjectPooler.Instance.ReturnPlayerStatusIconToPool(this.gameObject);
        }
        else
        {
            if(!ObstacleEffect)
            {
                RemoveStatusEffectText();
            }
            else
            {
                RemoveStatusObstacleEffectText();
            }
            CheckStatus();
            ObjectPooler.Instance.ReturnPlayerStatusIconToPool(this.gameObject);
        }
    }

    private void CheckStatus()
    {
        switch (status)
        {
            case (EffectStatus.DamageOverTime):
                CheckPoisonParticleActive();
                break;
            case (EffectStatus.Stun):
                CheckStunParticleActive();
                break;
        }
    }

    private void CheckPoisonParticleActive()
    {
        if(PoisonParticle.activeInHierarchy)
        {
            ObjectPooler.Instance.ReturnPoisonEffectParticleToPool(PoisonParticle);
        }
    }

    private void CheckStunParticleActive()
    {
        if(StunParticle.activeInHierarchy)
        {
            ObjectPooler.Instance.ReturnStunEffectParticleToPool(StunParticle);
        }
    }

    public void PlayerInput()
    {
        ObstacleEffect = false;

        KeyInput = SkillsManager.Instance.GetKeyInput;

        skill = SkillsManager.Instance.GetSkills[KeyInput];

        TempSkillIndex = skill.GetIndex;

        Duration = SkillsManager.Instance.GetSkills[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=16>" + "<u>" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "</u>" + "</color>" + "</size>" +
                                     "\n" + "<size=15>" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;

        DamageOrHealTick = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency;

        ContractHpTick = SkillsManager.Instance.GetSkills[KeyInput].GetContractHp;
        ContractMpTick = SkillsManager.Instance.GetSkills[KeyInput].GetContractMp;

        TempTick = DamageOrHealTick;
        ContractHpTempTick = ContractHpTick;
        ContractMpTempTick = ContractMpTick;

        ContractValue = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectPotency;

        PlayerMaxHealth = SkillsManager.Instance.GetCharacter.GetCharacterData.Health;

        RegenTick = SkillsManager.Instance.GetSkills[KeyInput].GetHpAndDamageOverTimeTick / 100f;
    }

    public void EnemyInput()
    {
        ObstacleEffect = false;

        SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetSleepHit = false;

        KeyInput = enemyTarget.GetAI.GetAiStates[enemyTarget.GetAI.GetStateArrayIndex].GetSkillIndex;

        Duration = enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = "<#EFDFB8>" + "<size=16>" + "<u>" + enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName + "</u>" + 
                                     "</color>" + "\n" + "</size>" + "<size=15>" + enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDescription;

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
                SkillsManager.Instance.GetCharacterMenu.GetStrengthStatColor = "<#FFFFFF>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.DefenseUP):
                SetDefenseToDefault();
                SkillsManager.Instance.GetCharacterMenu.GetDefenseStatColor = "<#FFFFFF>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.IntelligenceUP):
                SetIntelligenceToDefault();
                SkillsManager.Instance.GetCharacterMenu.GetIntelligenceStatColor = "<#FFFFFF>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.ContractWithEvil):
                SetIntelligenceToDefault();
                SetDefenseToDefault();
                SkillsManager.Instance.GetCharacterMenu.GetIntelligenceStatColor = "<#FFFFFF>";
                SkillsManager.Instance.GetCharacterMenu.GetDefenseStatColor = "<#FFFFFF>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.ConsecratedDefense):
                SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetIsImmune = false;
                break;
            case (EffectStatus.Aegis):
                SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetReflectingDamage = false;
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
            case (EffectStatus.StrengthDOWN):
                SetStrengthToDefault();
                SkillsManager.Instance.GetCharacterMenu.GetStrengthStatColor = "<#FFFFFF>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.DefenseDOWN):
                SetDefenseToDefault();
                SkillsManager.Instance.GetCharacterMenu.GetDefenseStatColor = "<#FFFFFF>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.IntelligenceDOWN):
                SetIntelligenceToDefault();
                SkillsManager.Instance.GetCharacterMenu.GetIntelligenceStatColor = "<#FFFFFF>";
                SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
                break;
            case (EffectStatus.Doom):
                SkillsManager.Instance.GetCharacter.GetComponent<Health>().ModifyHealth(-SkillsManager.Instance.GetCharacter.CurrentHealth);
                break;
        }
        return StatusEffectTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveStatusObstacleEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(GameManager.Instance.GetStatusEffectTransform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#969696>- Poison";

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        CreateParticleOnRemovePlayer();

        ObstacleEffect = false;

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
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

    private void ContractDamageOverTime(int value)
    {
        ContractHpTempTick -= Time.deltaTime;
        if (ContractHpTempTick <= 0)
        {
            SkillsManager.Instance.GetCharacter.GetComponent<Health>().ModifyHealth(-value);

            var Damagetxt = ObjectPooler.Instance.GetPlayerDamageText();

            Damagetxt.SetActive(true);

            Damagetxt.transform.SetParent(SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetDamageTextParent.transform, false);

            Damagetxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + value.ToString();

            ContractHpTempTick = ContractHpTick;
        }
    }

    private void HealMpOverTime(int value)
    {
        ContractMpTempTick -= Time.deltaTime;
        if (ContractMpTempTick <= 0)
        {
            SkillsManager.Instance.GetCharacter.GetComponent<Mana>().IncreaseMana(value);

            var HealTxt = ObjectPooler.Instance.GetPlayerHealText();

            HealTxt.SetActive(true);

            HealTxt.transform.SetParent(SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetDamageTextParent.transform, false);

            HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + value.ToString() + "</size>" + "<size=20>" + " MP";

            ContractMpTempTick = ContractMpTick;
        }
    }

    private void HealOverTime(int value)
    {
        TempTick -= Time.deltaTime;
        if (TempTick <= 0)
        {
            SkillsManager.Instance.GetCharacter.GetComponent<Health>().IncreaseHealth(value);

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

    private void HalfHealth()
    {
        int MaxHealth = PlayerMaxHealth / 2;

        Mathf.Round(MaxHealth);

        SkillsManager.Instance.GetCharacter.GetCharacterData.Health = MaxHealth;

        SkillsManager.Instance.GetCharacter.MaxHealth = SkillsManager.Instance.GetCharacter.GetCharacterData.Health;

        if(SkillsManager.Instance.GetCharacter.CurrentHealth > SkillsManager.Instance.GetCharacter.MaxHealth)
        {
            SkillsManager.Instance.GetCharacter.CurrentHealth = SkillsManager.Instance.GetCharacter.MaxHealth;
        }
    }

    private void RestoreMaxHealth()
    {
        SkillsManager.Instance.GetCharacter.GetCharacterData.Health = PlayerMaxHealth;
    }

    private void ConsecratedDefenses()
    {
        SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetIsImmune = true;
    }

    private void Aegis()
    {
        SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetReflectingDamage = true;
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

        float TempStrength = SkillsManager.Instance.GetCharacter.CharacterStrength;

        TempStrength += SkillsManager.Instance.GetCharacter.CharacterStrength * Percentage;

        Mathf.Round(TempStrength);

        SkillsManager.Instance.GetCharacter.CharacterStrength = (int)TempStrength;
    }

    private void DefenseUP(int value)
    {
        float Percentage = (float)value / 100;

        float TempDefense = SkillsManager.Instance.GetCharacter.CharacterDefense;

        TempDefense += SkillsManager.Instance.GetCharacter.CharacterDefense * Percentage;

        Mathf.Round(TempDefense);

        SkillsManager.Instance.GetCharacter.CharacterDefense = (int)TempDefense;
    }

    private void IntelligenceUP(int value)
    {
        float Percentage = (float)value / 100;

        float TempIntelligence = SkillsManager.Instance.GetCharacter.CharacterIntelligence;

        TempIntelligence += SkillsManager.Instance.GetCharacter.CharacterIntelligence * Percentage;

        Mathf.Round(TempIntelligence);

        SkillsManager.Instance.GetCharacter.CharacterIntelligence = (int)TempIntelligence;
    }

    private void StrengthDOWN(int value)
    {
        float Percentage = (float)value / 100;

        float TempStrength = SkillsManager.Instance.GetCharacter.CharacterStrength;

        TempStrength -= SkillsManager.Instance.GetCharacter.CharacterStrength * Percentage;

        Mathf.Round(TempStrength);

        SkillsManager.Instance.GetCharacter.CharacterStrength = (int)TempStrength;
    }

    private void DefenseDOWN(int value)
    {
        float Percentage = (float)value / 100;

        float TempDefense = SkillsManager.Instance.GetCharacter.CharacterDefense;

        TempDefense -= SkillsManager.Instance.GetCharacter.CharacterDefense * Percentage;

        Mathf.Round(TempDefense);

        SkillsManager.Instance.GetCharacter.CharacterDefense = (int)TempDefense;
    }

    private void IntelligenceDOWN(int value)
    {
        float Percentage = (float)value / 100;

        float TempIntelligence = SkillsManager.Instance.GetCharacter.CharacterIntelligence;

        TempIntelligence -= SkillsManager.Instance.GetCharacter.CharacterIntelligence * Percentage;

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
        int DefaultStrength = 0;
        int TempStrength = 0;

        if(SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0] != null &&
           SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1] != null)
        {
            if(SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length >= 1 &&
               SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length >= 1)
            {
                for(int i = 0; i < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length; i++)
                {
                    switch(SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatusTypes)
                    { 
                        case (StatIncreaseType.Strength):
                            DefaultStrength += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatIncrease;
                            break;
                    }
                }
                for (int j = 0; j < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length; j++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[j].GetStatusTypes)
                    {
                        case (StatIncreaseType.Strength):
                            DefaultStrength += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[j].GetStatIncrease;
                            break;
                    }
                }
                TempStrength = SkillsManager.Instance.GetCharacter.GetCharacterData.Strength + DefaultStrength;
            }
            else
            {
                TempStrength = SkillsManager.Instance.GetCharacter.GetCharacterData.Strength +
                              SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetEquipmentData.StatIncrease +
                              SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetEquipmentData.StatIncrease;
            }
        }
        else if(SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0] != null &&
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1] == null)
        {
            if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length >= 1)
            {
                for (int i = 0; i < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length; i++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatusTypes)
                    {
                        case (StatIncreaseType.Strength):
                            DefaultStrength += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatIncrease;
                            break;
                    }
                }
                TempStrength = SkillsManager.Instance.GetCharacter.GetCharacterData.Strength + DefaultStrength;
            }
            else
            {
                TempStrength = SkillsManager.Instance.GetCharacter.GetCharacterData.Strength +
                              SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetEquipmentData.StatIncrease;
            }
        }
        else if(SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0] == null &&
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1] != null)
        {
            if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length >= 1)
            {
                for (int i = 0; i < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length; i++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[i].GetStatusTypes)
                    {
                        case (StatIncreaseType.Strength):
                            DefaultStrength += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[i].GetStatIncrease;
                            break;
                    }
                }
                TempStrength = SkillsManager.Instance.GetCharacter.GetCharacterData.Strength + DefaultStrength;
            }
            else
            {
                DefaultStrength += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetEquipmentData.StatIncrease;
            }
        }
        else
        {
            TempStrength = SkillsManager.Instance.GetCharacter.GetCharacterData.Strength;
        }

        SkillsManager.Instance.GetCharacter.CharacterStrength = TempStrength;
    }

    private void SetDefenseToDefault()
    {
        int DefaultDefense = 0;
        int TempDefense = 0;

        if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0] != null &&
           SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1] != null)
        {
            if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length >= 1 &&
               SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length >= 1)
            {
                for (int i = 0; i < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length; i++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatusTypes)
                    {
                        case (StatIncreaseType.Defense):
                            DefaultDefense += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatIncrease;
                            break;
                    }
                }
                for (int j = 0; j < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length; j++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[j].GetStatusTypes)
                    {
                        case (StatIncreaseType.Defense):
                            DefaultDefense += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[j].GetStatIncrease;
                            break;
                    }
                }
                TempDefense = SkillsManager.Instance.GetCharacter.GetCharacterData.Defense + DefaultDefense;
            }
            else
            {
                TempDefense = SkillsManager.Instance.GetCharacter.GetCharacterData.Defense +
                              SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetEquipmentData.StatIncrease +
                              SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetEquipmentData.StatIncrease;
            }
        }
        else if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0] != null &&
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1] == null)
        {
            if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length >= 1)
            {
                for (int i = 0; i < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length; i++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatusTypes)
                    {
                        case (StatIncreaseType.Defense):
                            DefaultDefense += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatIncrease;
                            break;
                    }
                }
                TempDefense = SkillsManager.Instance.GetCharacter.GetCharacterData.Defense + DefaultDefense;
            }
            else
            {
                TempDefense = SkillsManager.Instance.GetCharacter.GetCharacterData.Defense +
                              SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetEquipmentData.StatIncrease;
            }
        }
        else if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0] == null &&
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1] != null)
        {
            if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length >= 1)
            {
                for (int i = 0; i < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length; i++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[i].GetStatusTypes)
                    {
                        case (StatIncreaseType.Defense):
                            DefaultDefense += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[i].GetStatIncrease;
                            break;
                    }
                }
                TempDefense = SkillsManager.Instance.GetCharacter.GetCharacterData.Defense + DefaultDefense;
            }
            else
            {
                DefaultDefense += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetEquipmentData.StatIncrease;
            }
        }
        else
        {
            TempDefense = SkillsManager.Instance.GetCharacter.GetCharacterData.Defense;
        }

        SkillsManager.Instance.GetCharacter.CharacterDefense = TempDefense;
    }

    private void SetIntelligenceToDefault()
    {
        int DefaultIntelligence = 0;
        int TempIntelligence = 0;

        if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0] != null &&
           SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1] != null)
        {
            if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length >= 1 &&
               SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length >= 1)
            {
                for (int i = 0; i < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length; i++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatusTypes)
                    {
                        case (StatIncreaseType.Intelligence):
                            DefaultIntelligence += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatIncrease;
                            break;
                    }
                }
                for (int j = 0; j < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length; j++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[j].GetStatusTypes)
                    {
                        case (StatIncreaseType.Intelligence):
                            DefaultIntelligence += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[j].GetStatIncrease;
                            break;
                    }
                }
                TempIntelligence = SkillsManager.Instance.GetCharacter.GetCharacterData.Intelligence + DefaultIntelligence;
            }
            else
            {
                TempIntelligence = SkillsManager.Instance.GetCharacter.GetCharacterData.Intelligence +
                              SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetEquipmentData.StatIncrease +
                              SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetEquipmentData.StatIncrease;
            }
        }
        else if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0] != null &&
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1] == null)
        {
            if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length >= 1)
            {
                for (int i = 0; i < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType.Length; i++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatusTypes)
                    {
                        case (StatIncreaseType.Intelligence):
                            DefaultIntelligence += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetStatusType[i].GetStatIncrease;
                            break;
                    }
                }
                TempIntelligence = SkillsManager.Instance.GetCharacter.GetCharacterData.Intelligence + DefaultIntelligence;
            }
            else
            {
                TempIntelligence = SkillsManager.Instance.GetCharacter.GetCharacterData.Intelligence +
                              SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0].GetEquipmentData.StatIncrease;
            }
        }
        else if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[0] == null &&
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1] != null)
        {
            if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length >= 1)
            {
                for (int i = 0; i < SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType.Length; i++)
                {
                    switch (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[i].GetStatusTypes)
                    {
                        case (StatIncreaseType.Intelligence):
                            DefaultIntelligence += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetStatusType[i].GetStatIncrease;
                            break;
                    }
                }
                TempIntelligence = SkillsManager.Instance.GetCharacter.GetCharacterData.Intelligence + DefaultIntelligence;
            }
            else
            {
                DefaultIntelligence += SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetEquipment[1].GetEquipmentData.StatIncrease;
            }
        }
        else
        {
            TempIntelligence = SkillsManager.Instance.GetCharacter.GetCharacterData.Intelligence;
        }

        SkillsManager.Instance.GetCharacter.CharacterIntelligence = TempIntelligence;
    }

    private void CheckStatusEffectIcon()
    {
        switch (status)
        {
            case (EffectStatus.DamageOverTime):
                DamageOverTime(RegenAndDOTCalculation());
                break;
            case (EffectStatus.HealthRegen):
                HealOverTime(RegenCalculation());
                break;
            case (EffectStatus.ContractWithTheVile):
                ContractDamageOverTime(ContractWithTheVileDamageOverTime());
                HealMpOverTime(ContractWithTheVileMpRestore());
                break;
            case (EffectStatus.Stun):
                Stun();
                break;
            case (EffectStatus.Sleep):
                Sleep();
                break;
        }
    }

    private void MaliciousPossessionBuff()
    {
        foreach (Skills s in SkillsManager.Instance.GetSkills)
        {
            s.GetCoolDown = 0;
            s.GetManaCost = 0;
        }
    }

    private int ContractWithTheVileDamageOverTime()
    {
        float percent = Mathf.Round(0.01f * SkillsManager.Instance.GetCharacter.MaxHealth);

        int GetHealth = (int)percent;

        Mathf.Round(GetHealth);

        return GetHealth;
    }

    private int ContractWithTheVileMpRestore()
    {
        float percent = Mathf.Round(ContractValue * SkillsManager.Instance.GetCharacter.MaxMana);

        int GetMana = (int)percent;

        Mathf.Round(GetMana);

        return GetMana;
    }

    private int RegenAndDOTCalculation()
    {
        float percent = Mathf.Round(0.05f * SkillsManager.Instance.GetCharacter.MaxHealth);

        int GetHealth = (int)percent;

        Mathf.Round(GetHealth);

        return GetHealth;
    }

    private int RegenCalculation()
    {
        float percent = Mathf.Round(RegenTick * SkillsManager.Instance.GetCharacter.MaxHealth);

        int GetHealth = (int)percent;

        Mathf.Round(GetHealth);

        return GetHealth;
    }

    private void CreateParticleOnRemovePlayer()
    {
        if(status == EffectStatus.Doom)
        {
            if(settings.UseParticleEffects)
            {
                var chara = SkillsManager.Instance.GetCharacter;

                var Statusparticle = Instantiate(DoomParticle, new Vector3(chara.transform.position.x,
                                                                           chara.transform.position.y + 1f,
                                                                           chara.transform.position.z), transform.rotation);

                Statusparticle.transform.SetParent(chara.transform, true);
            }
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

    private void CreatePoisonEffectParticle()
    {
        if(settings.UseParticleEffects)
        {
            var character = SkillsManager.Instance.GetCharacter;

            var PP = ObjectPooler.Instance.GetPoisonEffectParticle();

            PoisonParticle = PP;

            if(PP.activeInHierarchy)
            {
                
            }
            else
            {
                PP.SetActive(true);

                PP.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 1.9f, character.transform.position.z);

                PP.transform.SetParent(character.transform, true);
            }
        }
    }

    private void CreateStunEffectParticle()
    {
        if (settings.UseParticleEffects)
        {
            var character = SkillsManager.Instance.GetCharacter;

            var SP = ObjectPooler.Instance.GetStunEffectParticle();

            StunParticle = SP;

            SP.SetActive(true);

            SP.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 0.8f, character.transform.position.z);

            SP.transform.SetParent(character.transform, true);
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

        if(enemyTarget != null)
        {
            if(enemyTarget.GetComponent<Puck>())
            {
                if(enemyTarget.GetComponent<Puck>().GetIsReseted)
                {
                    RemoveEffect();
                    enemyTarget.GetComponent<Puck>().GetIsReseted = false;
                }
            }
        }
    }
}