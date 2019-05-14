using UnityEngine;
using UnityEngine.UI;

public class Skills : StatusEffects
{
    [SerializeField]
    private SkillBar skillbar;

    [SerializeField]
    private Button button;

    [SerializeField] [Tooltip("The transform that holds the damage/heal/status effect text values. Keep this empty for damage type skills!")]
    private Transform TextHolder = null;

    [SerializeField]
    private Text SkillTextObject, SkillPanelText;

    [SerializeField]
    private ParticleSystem SkillParticle;

    [SerializeField]
    private float CoolDown, AttackRange, ApplySkill;

    [SerializeField]
    private int ManaCost, Potency;
    
    [SerializeField] [Tooltip("Skills that have a cast time greater than 0 will activate the skill casting bar.")]
    private int CastTime;

    [SerializeField]
    private string SkillName;

    [SerializeField] [TextArea]
    private string SkillDescription;

    public int GetCastTime
    {
        get
        {
            return CastTime;
        }
        set
        {
            CastTime = value;
        }
    }

    public int GetManaCost
    {
        get
        {
            return ManaCost;
        }
        set
        {
            ManaCost = value;
        }
    }

    public string GetSkillName
    {
        get
        {
            return SkillName;
        }
        set
        {
            SkillName = value;
        }
    }

    public Button GetButton
    {
        get
        {
            return button;
        }
        set
        {
            button = value;
        }
    }

    public Text GetSkillTextObject
    {
        get
        {
            return SkillTextObject;
        }
        set
        {
            SkillTextObject = value;
        }
    }

    public Transform GetTextHolder
    {
        get
        {
            return TextHolder;
        }
        set
        {
            TextHolder = value;
        }
    }

    private void Update()
    {
        if(GetCharacter != null)
        CheckCoolDownStatus();
    }

    private void CheckCoolDownStatus()
    {
        if (this.button.GetComponent<Image>().fillAmount >= 1 && GetCharacter.CurrentHealth > 0 && GetCharacter.CurrentMana >= ManaCost && !SkillsManager.Instance.GetActivatedSkill)
        {
            button.interactable = true;
            return;
        }
        else
        {
            this.button.GetComponent<Image>().fillAmount += Time.deltaTime / CoolDown;
            this.button.interactable = false;
        }
    }

    public void TestHealSkill()
    {
        if (skillbar.GetSkillBar.fillAmount < 1)
        {
            SkillsManager.Instance.GetActivatedSkill = true;

            skillbar.gameObject.SetActive(true);

            skillbar.GetSkill = this.button.GetComponent<Skills>();
        }
        if (skillbar.GetSkillBar.fillAmount >= 1)
        {
            this.button.GetComponent<Image>().fillAmount = 0;

            SkillsManager.Instance.GetActivatedSkill = false;

            var HealParticle = Instantiate(SkillParticle, new Vector3(GetCharacter.transform.position.x, GetCharacter.transform.position.y + 1.0f, GetCharacter.transform.position.z),
                                           Quaternion.identity);

            HealParticle.transform.SetParent(GetCharacter.transform, true);

            GetCharacter.GetComponent<Mana>().ModifyMana(-ManaCost);

            Invoke("InvokeHealthRestore", ApplySkill);
        }
    }

    private void InvokeHealthRestore()
    {
        GetCharacter.GetComponent<Health>().IncreaseHealth(Potency + GetCharacter.CharacterIntelligence);
        GetCharacter.GetComponent<Health>().GetTakingDamage = false;

        HealSkillText();
    }

    public void TestDamageSkill()
    {
        if (GetCharacter.GetComponent<BasicAttack>().GetTarget != null)
        {
            TextHolder = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetComponentInChildren<NoRotationHealthBar>().transform;
            if(Vector3.Distance(GetCharacter.transform.position, GetCharacter.GetComponent<BasicAttack>().GetTarget.transform.position) <= AttackRange)
            {
                SkillsManager.Instance.GetActivatedSkill = true;

                GetCharacter.GetComponent<PlayerAnimations>().PlaySkillAnimation();

                this.button.GetComponent<Image>().fillAmount = 0;

                GetCharacter.GetComponent<Mana>().ModifyMana(-ManaCost);

                var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

                var DamageParticle = Instantiate(SkillParticle, new Vector3(Target.transform.position.x, Target.transform.position.y + 1.0f, Target.transform.position.z),
                                                 Quaternion.identity);

                DamageParticle.transform.SetParent(Target.transform, true);
            }
            else
            {
                GameManager.Instance.ShowTargetOutOfRangeText();
            }
        }
        else
        {
            GameManager.Instance.InvalidTargetText();
            TextHolder = null;
        }
    }

    public void ForHonor()
    {
        this.button.GetComponent<Image>().fillAmount = 0;

        StrengthUP(GetCharacter, 10, GetStatusDuration);

        BuffStatusEffectSkillText();
    }

    public void Shield()
    {
        this.button.GetComponent<Image>().fillAmount = 0;

        DefenseUP(GetCharacter, 10, GetStatusDuration);

        BuffStatusEffectSkillText();
    }

    //Place this on an animation as an animation event.
    //This way damage will be dealt during a specific portion of the attack animation.
    //If used for spell damage, invoke this function.
    public void SkillDamage()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        Target.GetComponent<Health>().ModifyHealth(-Potency - -Target.GetComponent<Character>().CharacterDefense);
        Target.GetComponent<Health>().GetTakingDamage = true;

        Target.GetComponent<EnemyAI>().GetStates = States.Damaged;

        SkillsManager.Instance.GetActivatedSkill = false;

        DamageSkillText();
    }

    private Text HealSkillText()
    {
        var SkillObj = Instantiate(SkillTextObject);

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = SkillName + " " + (Potency + GetCharacter.CharacterIntelligence).ToString();

        return SkillObj;
    }

    public Text BuffStatusEffectSkillText()
    {
        var SkillObj = Instantiate(SkillTextObject);

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = "+" + GetStatusEffectName;

        var StatusIcon = Instantiate(GetStatusIcon);

        StatusIcon.transform.SetParent(GetBuffIconTrans.transform, false);

        SkillObj.GetComponentInChildren<Image>().sprite = StatusIcon.sprite;

        return SkillObj;
    }

    public Text DebuffStatusEffectSkillText()
    {
        var SkillObj = Instantiate(SkillTextObject);

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = "+" + GetStatusEffectName;

        var StatusIcon = Instantiate(GetStatusIcon);

        StatusIcon.transform.SetParent(GetDeBuffIconTrans.transform, false);

        SkillObj.GetComponentInChildren<Image>().sprite = StatusIcon.sprite;

        return SkillObj;
    }

    private Text DamageSkillText()
    {
        var SkillObj = Instantiate(SkillTextObject);

        var Target = GetCharacter.GetComponent<BasicAttack>();

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = SkillName + " " + (Potency - Target.GetTarget.GetComponent<Character>().CharacterDefense).ToString();

        return SkillObj;
    }

    public void ShowSkillPanel(GameObject Panel)
    {
        Panel.gameObject.SetActive(true);

        if(CastTime <= 0 || ManaCost <= 0 || Potency <= 0)
        {
            SkillPanelText.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Cooldown: " + CoolDown + " Seconds"
                                    + "\n" + "Cast Time: Instant";
        }
        else
        {
            SkillPanelText.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Mana: " + ManaCost + "\n" + "Potency: " + Potency + "\n" + "Cooldown: " + CoolDown + " Seconds"
                                    + "\n" + "Cast Time: " + CastTime + " Seconds";
        }
    }
}