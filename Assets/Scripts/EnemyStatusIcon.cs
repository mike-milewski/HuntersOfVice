using UnityEngine;
using UnityEngine.UI;

public enum StatusEffect { NONE, DamageOverTime, HealthRegen };

public class EnemyStatusIcon : MonoBehaviour
{
    private PlayerController player = null;

    [SerializeField]
    private Character character = null;

    [SerializeField]
    private StatusEffect effect;

    [SerializeField]
    private Text DurationText, StatusDescriptionText;

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

        KeyInput = SkillsManager.Instance.GetKeyInput;

        Duration = SkillsManager.Instance.GetSkills[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "\n" +
                                     SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;

        DamageTick = 3f;
    }

    public Text RemoveStatusEffectText()
    {
        var SkillObj = Instantiate(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectText);

        SkillObj.transform.SetParent(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

        SkillObj.text = "-" + character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        return SkillObj;
    }

    public Text RemoveEnemyStatusEffectText()
    {
        var SkillObj = Instantiate(SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectText);

        SkillObj.transform.SetParent(character.GetComponent<Enemy>().GetUI, false);

        SkillObj.text = "-" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        return SkillObj;
    }

    private void OnDisable()
    {
        if(player == null)
        {
            RemoveStatusEffectText();
        }
        else
        {
            RemoveEnemyStatusEffectText();
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

            Healtxt.text = value.ToString();

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

            EnemyDamagetxt.text = value.ToString();

            DamageTick = damageTick;
        }
    }

    private void CheckStatusEffect()
    {
        switch (effect)
        {
            case (StatusEffect.HealthRegen):
                HealthRegen(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetPotency, 3f);
                break;
            case (StatusEffect.DamageOverTime):
                DamageOverTime(10, 3f);
                break;
        }
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