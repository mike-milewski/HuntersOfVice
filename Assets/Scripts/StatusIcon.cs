using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EffectStatus { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste }

public class StatusIcon : MonoBehaviour
{
    private Enemy enemyTarget = null;

    [SerializeField]
    private Status status;

    [SerializeField]
    private TextMeshProUGUI DurationText, StatusDescriptionText;

    [SerializeField]
    private float Duration;

    private float PoisonDamageTick;

    [SerializeField]
    private int KeyInput;

    [SerializeField]
    private GameObject StatusPanel;

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

    public Status GetEffectStatus
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

    private void OnDisable()
    {
        if (enemyTarget != null)
        {
            RemoveEnemyStatusEffectText();
        }
        else
        {
            RemoveStatusEffectText();
        }
    }

    public void PlayerInput()
    {
        KeyInput = SkillsManager.Instance.GetKeyInput;

        Duration = SkillsManager.Instance.GetSkills[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "\n" +
                                     SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;

        PoisonDamageTick = 3f;
    }

    public void EnemyInput()
    {
        SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetSleepHit = false;

        KeyInput = enemyTarget.GetComponent<EnemySkills>().GetRandomValue;

        Duration = enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName + "\n" +
                                     enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDescription;

        PoisonDamageTick = 3f;
    }

    public TextMeshProUGUI RemoveStatusEffectText()
    {
        var SkillObj = Instantiate(SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectText);

        SkillObj.transform.SetParent(SkillsManager.Instance.GetSkills[KeyInput].GetTextHolder.transform, false);

        SkillObj.GetComponentInChildren<TextMeshProUGUI>().text = "-" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        return SkillObj.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RemoveEnemyStatusEffectText()
    {
        KeyInput = enemyTarget.GetComponent<EnemySkills>().GetRandomValue;

        var SkillObj = Instantiate(enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectHolder);

        SkillObj.transform.SetParent(enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

        SkillObj.GetComponentInChildren<TextMeshProUGUI>().text = "-" + enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        switch (status)
        {
            case (Status.Stun):
                SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = true;
                SkillsManager.Instance.GetDisruptedSkill = false;
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().enabled = true;
                break;
            case (Status.Sleep):
                SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = true;
                SkillsManager.Instance.GetDisruptedSkill = false;
                SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().enabled = true;
                break;
        }
        return SkillObj.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void DamageOverTime(int value, float damageTick)
    {
        PoisonDamageTick -= Time.deltaTime;
        if (PoisonDamageTick <= 0)
        {
            SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetTakingDamage = true;
            SkillsManager.Instance.GetCharacter.GetComponent<Health>().ModifyHealth(-value);

            var Damagetxt = Instantiate(SkillsManager.Instance.GetCharacter.GetComponent<Health>().GetDamageText);

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

    private void CheckStatusEffectIcon()
    {
        switch (status)
        {
            case (Status.DamageOverTime):
                DamageOverTime(RegenAndDOTCalculation(), 3f);
                break;
            case (Status.Stun):
                Stun();
                break;
            case (Status.Sleep):
                Sleep();
                break;
            case (Status.Haste):
                Haste();
                break;
        }
    }

    private int RegenAndDOTCalculation()
    {
        float percent = 0.1f * (float)SkillsManager.Instance.GetCharacter.MaxHealth;

        int GetHealth = (int)percent;

        return GetHealth;
    }

    private void Update()
    {
        CheckStatusEffectIcon();

        if(Duration > -1)
        {
            DurationText.text = Duration.ToString("F0");
            Duration -= Time.deltaTime;
            if (Duration <= 0 || SkillsManager.Instance.GetCharacter.CurrentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}