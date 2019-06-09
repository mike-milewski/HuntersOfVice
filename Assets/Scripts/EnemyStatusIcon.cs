using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum StatusEffect { NONE, DamageOverTime, HealthRegen, Stun, Sleep };

public class EnemyStatusIcon : MonoBehaviour
{
    private PlayerController player = null;

    [SerializeField]
    private Character character = null;

    [SerializeField]
    private StatusEffect effect;

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
        EnemyInput();
    }

    private void OnDisable()
    {
        if (player == null)
        {
            RemoveStatusEffectText();
        }
        else
        {
            RemoveEnemyStatusEffectText();
        }
    }

    public void EnemyInput()
    {
        character = GetComponentInParent<Character>();

        effect = (StatusEffect)character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatus;

        KeyInput = character.GetComponent<EnemySkills>().GetRandomValue;

        Duration = character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName + "\n" +
                                     character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDescription;

        RegenHealTick = 3f;
        DamageTick = 3f;
    }

    public void PlayerInput()
    {
        character = GetComponentInParent<Character>();

        character.GetComponentInChildren<Health>().GetSleepHit = false;

        KeyInput = SkillsManager.Instance.GetKeyInput;

        Duration = SkillsManager.Instance.GetSkills[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "\n" +
                                     SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;

        DamageTick = 3f;
    }

    public TextMeshProUGUI RemoveStatusEffectText()
    {
        var SkillObj = Instantiate(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectHolder);

        SkillObj.transform.SetParent(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

        SkillObj.GetComponentInChildren<TextMeshProUGUI>().text = "-" + character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        return SkillObj.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveEnemyStatusEffectText()
    {
        var SkillObj = Instantiate(SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectText);

        SkillObj.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        SkillObj.GetComponentInChildren<TextMeshProUGUI>().text = "-" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        switch (effect)
        {
            case (StatusEffect.Stun):
                CheckEnemyStates();
                break;
            case (StatusEffect.Sleep):
                CheckEnemyStates();
                break;
        }
        return SkillObj.GetComponentInChildren<TextMeshProUGUI>();
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

    private void ToggleStatusIcon()
    {
        if (GameManager.Instance.GetLastObject == character.GetComponent<Enemy>().gameObject)
        {
            this.GetComponentInChildren<Image>().enabled = true;
            DurationText.enabled = true;
        }
        else if (GameManager.Instance.GetLastObject != character.GetComponent<Enemy>().gameObject)
        {
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

            var Healtxt = Instantiate(character.GetComponent<Enemy>().GetHealth.GetHealText);

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
            character.GetComponent<Enemy>().GetHealth.GetTakingDamage = true;
            character.GetComponent<Enemy>().GetHealth.ModifyHealth(-value);
            character.GetComponent<Enemy>().GetLocalHealthInfo();

            var EnemyDamagetxt = Instantiate(character.GetComponent<Enemy>().GetHealth.GetDamageText);

            EnemyDamagetxt.transform.SetParent(character.GetComponentInChildren<Health>().GetDamageTextParent.transform, false);

            EnemyDamagetxt.GetComponentInChildren<Text>().text = value.ToString();

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
        }
    }

    private int RegenAndDOTCalculation()
    {
        float percent = 0.1f * (float)character.MaxHealth;

        int GetHealth = (int)percent;

        return GetHealth;
    }

    private void LateUpdate()
    {
        ToggleStatusIcon();

        CheckStatusEffect();

        if(Duration > -1)
        {
            DurationText.text = Duration.ToString("F0");
            Duration -= Time.deltaTime;
            if (Duration <= 0 || character.CurrentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}