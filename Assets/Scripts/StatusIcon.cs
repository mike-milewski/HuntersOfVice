using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EffectStatus { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste, Doom, StrengthUP, DefenseUP, BloodAndSinew, DefenseDOWN }

public class StatusIcon : MonoBehaviour
{
    private Enemy enemyTarget = null;

    [SerializeField]
    private GameObject StatusPanel;

    [SerializeField]
    private TextMeshProUGUI DurationText, StatusDescriptionText;

    [SerializeField]
    private ParticleSystem StatusRemovalParticle;

    [SerializeField]
    private EffectStatus status;

    private Skills skill;

    private int TempSkillIndex;

    private int index;

    [SerializeField]
    private float Duration;

    private float PoisonDamageTick;

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
                StrengthUP(10);
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

        StatusDescriptionText.text = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "\n" + "<size=14>" +
                                     SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;

        PoisonDamageTick = 3f;
    }

    public void EnemyInput()
    {
        SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetSleepHit = false;

        KeyInput = enemyTarget.GetComponent<EnemySkills>().GetRandomValue;

        Duration = enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName + "\n" + "<size=14>" +
                                     enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDescription;

        PoisonDamageTick = 3f;
    }

    //Called when a status effect cast by the player onto themselves gets removed.
    public TextMeshProUGUI RemoveStatusEffectText()
    {
        var StatusEffectTxt = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectTxt.SetActive(true);

        StatusEffectTxt.transform.SetParent(skill.GetTextHolder.transform, false);

        StatusEffectTxt.GetComponentInChildren<TextMeshProUGUI>().text = "-" + skill.GetStatusEffectName;

        StatusEffectTxt.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        CreateParticleOnRemovePlayer();

        switch (status)
        {
            case (EffectStatus.StrengthUP):
                SetStrengthToDefault();
                break;
        }   

        return StatusEffectTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    //Called when a status effect cast by an enemy onto the player gets removed.
    public TextMeshProUGUI RemoveEnemyStatusEffectText()
    {
        KeyInput = enemyTarget.GetComponent<EnemySkills>().GetRandomValue;

        var StatusEffectTxt = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectTxt.SetActive(true);

        StatusEffectTxt.transform.SetParent(enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

        StatusEffectTxt.GetComponentInChildren<TextMeshProUGUI>().text = "-" + enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;

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

    private void DamageOverTime(int value, float damageTick)
    {
        PoisonDamageTick -= Time.deltaTime;
        if (PoisonDamageTick <= 0)
        {
            SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetTakingDamage = true;
            SkillsManager.Instance.GetCharacter.GetComponent<Health>().ModifyHealth(-value);

            var Damagetxt = ObjectPooler.Instance.GetPlayerDamageText();

            Damagetxt.SetActive(true);

            Damagetxt.transform.SetParent(SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetDamageTextParent.transform, false);

            Damagetxt.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();

            PoisonDamageTick = damageTick;
        }
    }

    private void Stun()
    {
        SkillsManager.Instance.GetDisruptedSkill = true;
        SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = false;
        SkillsManager.Instance.GetCharacter.GetComponent<PlayerAnimations>().EndAttackAnimation();
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

    private void Haste()
    {

    }

    private void StrengthUP(int value)
    {
        float Percentage = (float)value / 100;

        float TempStrength = (float)SkillsManager.Instance.GetCharacter.CharacterStrength;

        Mathf.FloorToInt(TempStrength);

        TempStrength += (float)SkillsManager.Instance.GetCharacter.CharacterStrength * Percentage;

        SkillsManager.Instance.GetCharacter.CharacterStrength = (int)TempStrength;
    }

    private void DefenseUP(int value)
    {

    }

    private void DefenseDOWN(int value)
    {

    }

    private void BloodAndSinew()
    {

    }

    private void SetStrengthToDefault()
    {
        int DefaultStrength = SkillsManager.Instance.GetCharacter.GetCharacterData.Strength;

        SkillsManager.Instance.GetCharacter.CharacterStrength = DefaultStrength;
    }

    private void CheckStatusEffectIcon()
    {
        switch (status)
        {
            case (EffectStatus.DamageOverTime):
                DamageOverTime(RegenAndDOTCalculation(), 3f);
                break;
            case (EffectStatus.Stun):
                Stun();
                break;
            case (EffectStatus.Sleep):
                Sleep();
                break;
            case (EffectStatus.Haste):
                Haste();
                break;
        }
    }

    private int RegenAndDOTCalculation()
    {
        float percent = Mathf.Round(0.1f * (float)SkillsManager.Instance.GetCharacter.MaxHealth);

        int GetHealth = (int)percent;

        return GetHealth;
    }

    private void CreateParticleOnRemovePlayer()
    {
        var character = SkillsManager.Instance.GetCharacter;

        var StatusParticle = Instantiate(StatusRemovalParticle, new Vector3(character.transform.position.x, 
                                                                            character.transform.position.y + 1f, 
                                                                            character.transform.position.z), transform.rotation);

        StatusParticle.transform.SetParent(character.transform, true);
    }

    private void CreateParticleOnRemoveEnemy()
    {
        var StatusParticle = Instantiate(StatusRemovalParticle, new Vector3(enemyTarget.transform.position.x,
                                                                            enemyTarget.transform.position.y + 0.65f,
                                                                            enemyTarget.transform.position.z), transform.rotation);

        StatusParticle.transform.SetParent(enemyTarget.transform, true);
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