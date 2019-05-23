using UnityEngine;
using UnityEngine.UI;

public enum EffectStatus { NONE, DamageOverTime, HealthRegen }

public class StatusIcon : MonoBehaviour
{
    private Enemy enemyTarget = null;

    [SerializeField]
    private Status status;

    [SerializeField]
    private Text DurationText, StatusDescriptionText;

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
        KeyInput = enemyTarget.GetComponent<EnemySkills>().GetRandomValue;

        Duration = enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName + "\n" +
                                     enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDescription;

        PoisonDamageTick = 3f;
    }

    public Text RemoveStatusEffectText()
    {
        var SkillObj = Instantiate(SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectText);

        SkillObj.transform.SetParent(SkillsManager.Instance.GetSkills[KeyInput].GetTextHolder.transform, false);

        SkillObj.text = "-" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        return SkillObj;
    }

    public Text RemoveEnemyStatusEffectText()
    {
        KeyInput = enemyTarget.GetComponent<EnemySkills>().GetRandomValue;

        var SkillObj = Instantiate(enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectText);

        SkillObj.transform.SetParent(enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

        SkillObj.text = "-" + enemyTarget.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        return SkillObj;
    }

    private void OnDisable()
    {
        if(enemyTarget != null)
        {
            RemoveEnemyStatusEffectText();
        }
        else
        {
            RemoveStatusEffectText();
        }
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

            Damagetxt.text = value.ToString();

            PoisonDamageTick = damageTick;
        }
    }

    private void CheckStatusEffectIcon()
    {
        switch (status)
        {
            case (Status.DamageOverTime):
                DamageOverTime(10, 3f);
                break;
        }
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