using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public enum StatusEffect { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste, Doom, StrengthUP, DefenseUP, BloodAndSinew, DefenseDOWN };

public class EnemyStatusIcon : MonoBehaviour
{
    private PlayerController player = null;

    [SerializeField]
    private Character character = null;

    [SerializeField]
    private ParticleSystem StatusRemovalParticle;

    [SerializeField]
    private StatusEffect effect;

    private Skills skill;

    private int TempSkillIndex;

    [SerializeField]
    private TextMeshProUGUI DurationText, StatusDescriptionText;

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

    [SerializeField]
    private float Duration;

    private float RegenHealTick; //Value used for status effects with health regeneration.
    private float DamageTick; //Value used for status effects that damage over time;

    private int KeyInput;

    [SerializeField]
    private GameObject StatusPanel;

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
        if(player == null)
        {
            EnemyInput();
        }

        switch (effect)
        {
            case (StatusEffect.DefenseDOWN):
                DefenseDOWN(50);
                break;
        }
    }

    private void OnEnable()
    {
        StatusPanel.SetActive(false);
    }

    public void RemoveEffect()
    {
        if (player == null)
        {
            RemoveStatusEffectText();
            ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
        }
        else
        {
            RemoveEnemyStatusEffectText();
            ObjectPooler.Instance.ReturnEnemyStatusIconToPool(this.gameObject);
        }
    }

    public void EnemyInput()
    {
        character = GetComponentInParent<Character>();

        effect = (StatusEffect)character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatus;

        KeyInput = character.GetComponent<EnemySkills>().GetRandomValue;

        Duration = character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName + "\n" + "<size=12>" +
                                     character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDescription;

        RegenHealTick = 3f;
        DamageTick = 3f;
    }

    public void PlayerInput()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        KeyInput = SkillsManager.Instance.GetKeyInput;

        skill = SkillsManager.Instance.GetSkills[KeyInput];

        TempSkillIndex = skill.GetIndex;

        Duration = skill.GetStatusDuration;

        StatusDescriptionText.text = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "\n" + "<size=12>" +
                                     SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;

        DamageTick = 3f;
    }

    public TextMeshProUGUI RemoveStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "-" + character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        CreateParticleOnRemovePlayer();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveEnemyStatusEffectText()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "-" + skill.GetStatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        CreateParticleOnRemoveEnemy();

        switch (effect)
        {
            case (StatusEffect.Stun):
                CheckEnemyStates();
                break;
            case (StatusEffect.Sleep):
                CheckEnemyStates();
                break;
            case (StatusEffect.DefenseDOWN):
                SetDefenseToDefault();
                break;
            case (StatusEffect.Doom):
                character.GetComponentInChildren<Health>().ModifyHealth(-character.CurrentHealth);
                break;
        }
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
            this.GetComponentInChildren<Image>().enabled = true;
            DurationText.enabled = true;
        }
        else if (GameManager.Instance.GetEnemyObject != character.GetComponent<Enemy>().gameObject)
        {
            GameManager.Instance.GetLastEnemyObject = character.GetComponent<Enemy>().gameObject;
            this.GetComponentInChildren<Image>().enabled = false;
            DurationText.enabled = false;
        }
    }

    private void HealthRegen(int value, float healTick)
    {
        RegenHealTick -= Time.deltaTime;
        if (RegenHealTick <= 0)
        {
            character.GetComponent<Enemy>().GetHealth.GetTakingDamage = false;
            character.GetComponent<Enemy>().GetHealth.IncreaseHealth(value);
            character.GetComponent<Enemy>().GetLocalHealthInfo();

            var Healtxt = ObjectPooler.Instance.GetEnemyHealText();

            Healtxt.SetActive(true);

            Healtxt.transform.SetParent(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

            Healtxt.GetComponentInChildren<Text>().text = value.ToString();

            RegenHealTick = healTick;
        }
    }

    private void DamageOverTime(int value, float damageTick)
    {
        DamageTick -= Time.deltaTime;
        if (DamageTick <= 0)
        {
            var EnemyDamagetxt = ObjectPooler.Instance.GetEnemyDamageText();

            EnemyDamagetxt.transform.SetParent(character.GetComponentInChildren<Health>().GetDamageTextParent.transform, false);

            EnemyDamagetxt.SetActive(true);

            EnemyDamagetxt.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();

            character.GetComponent<Enemy>().GetHealth.GetTakingDamage = true;
            character.GetComponent<Enemy>().GetHealth.ModifyHealth(-value);
            character.GetComponent<Enemy>().GetLocalHealthInfo();

            DamageTick = damageTick;
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

    private void Haste()
    {

    }

    private void DefenseDOWN(float value)
    {
        float Percentage = (float)value / 100;

        float TempDefense = (float)character.CharacterDefense;

        Mathf.FloorToInt(TempDefense);

        TempDefense -= (float)character.CharacterDefense * Percentage;

        character.CharacterDefense = (int)TempDefense;
    }

    private void SetDefenseToDefault()
    {
        int DefaultDefense = character.GetCharacterData.Defense;

        character.CharacterDefense = DefaultDefense;

        Debug.Log(character.GetCharacterData.Defense);
    }

    private void CheckStatusEffect()
    {
        switch (effect)
        {
            case (StatusEffect.HealthRegen):
                HealthRegen(RegenAndDOTCalculation(), 3f);
                break;
            case (StatusEffect.DamageOverTime):
                DamageOverTime(RegenAndDOTCalculation(), 3f);
                break;
            case (StatusEffect.Stun):
                Stun();
                break;
            case (StatusEffect.Sleep):
                Sleep();
                break;
            case (StatusEffect.Haste):
                Haste();
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
        var StatusParticle = Instantiate(StatusRemovalParticle, new Vector3(player.transform.position.x,
                                                                            player.transform.position.y + 1f,
                                                                            player.transform.position.z), transform.rotation);

        StatusParticle.transform.SetParent(player.transform, true);
    }

    private void CreateParticleOnRemoveEnemy()
    {
        var StatusParticle = Instantiate(StatusRemovalParticle, new Vector3(character.transform.position.x,
                                                                            character.transform.position.y + 0.65f,
                                                                            character.transform.position.z), transform.rotation);

        StatusParticle.transform.SetParent(character.transform, true);
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
        else if(Duration > -1)
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