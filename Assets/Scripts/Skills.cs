using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skills : StatusEffects
{
    [SerializeField]
    private SkillBar skillbar;

    [SerializeField]
    private Button button;

    [SerializeField][Tooltip("The transform that holds the damage/heal/status effect text values. Keep this empty for damage type skills!")]
    private Transform TextHolder = null;

    [SerializeField]
    private GameObject DamageORHealText;

    [SerializeField]
    private TextMeshProUGUI SkillPanelText;

    [SerializeField]
    private GameObject StatusEffectText;

    [SerializeField]
    private ParticleSystem SkillParticle;

    [SerializeField]
    private float CoolDown, AttackRange, ApplySkill;

    private bool StatusIconCreated;

    private bool IsBeingDragged;

    [SerializeField]
    private int ManaCost, Potency;
    
    [SerializeField] [Tooltip("Skills that have a cast time greater than 0 will activate the skill casting bar.")]
    private int CastTime;

    [SerializeField]
    private int index;

    [SerializeField]
    private string SkillName;

    [SerializeField][TextArea]
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

    public int GetIndex
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
        }
    }

    public bool GetIsBeingDragged
    {
        get
        {
            return IsBeingDragged;
        }
        set
        {
            IsBeingDragged = value;
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

    public GameObject GetDamageOrHealText
    {
        get
        {
            return DamageORHealText;
        }
        set
        {
            DamageORHealText = value;
        }
    }

    public GameObject GetStatusEffectText
    {
        get
        {
            return StatusEffectText;
        }
        set
        {
            StatusEffectText = value;
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
        if (this.button.GetComponent<Image>().fillAmount >= 1 && GetCharacter.CurrentMana >= ManaCost && !GameManager.Instance.GetIsDead && 
            !SkillsManager.Instance.GetActivatedSkill && !SkillsManager.Instance.GetDisruptedSkill && !IsBeingDragged)
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

            Invoke("HealSkillText", ApplySkill);
        }
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

        SkillsManager.Instance.GetStatusIcon.PlayerInput();

        PlayerStatus();
    }

    public void Shield()
    {
        this.button.GetComponent<Image>().fillAmount = 0;

        SkillsManager.Instance.GetStatusIcon.PlayerInput();

        PlayerStatus();
    }

    public void BloodAndSinew()
    {
        this.button.GetComponent<Image>().fillAmount = 0;

        SkillsManager.Instance.GetStatusIcon.PlayerInput();

        PlayerStatus();
    }

    public void Poison()
    {
        TextHolder = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetUI;

        GetStatusEffectIconTrans = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetDebuffTransform;

        this.button.GetComponent<Image>().fillAmount = 0;

        EnemyStatus();
    }

    public void SwiftStrike()
    {
        if(GetCharacter.GetComponent<BasicAttack>().GetTarget != null)
        {
            if(GetCharacter.GetComponent<BasicAttack>().DistanceToTarget() <= AttackRange)
            {
                TextHolder = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetUI;

                SkillsManager.Instance.GetActivatedSkill = true;

                this.button.GetComponent<Image>().fillAmount = 0;

                GetCharacter.GetComponent<Mana>().ModifyMana(-ManaCost);

                GetCharacter.GetComponent<PlayerAnimations>().PlaySkillAnimation();
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

    public void StormThrust()
    {
        TextHolder = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetUI;

        SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = false;

        SkillsManager.Instance.GetActivatedSkill = true;

        GetStatusEffectIconTrans = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetDebuffTransform;

        this.button.GetComponent<Image>().fillAmount = 0;

        StatusEffectSkillText();
    }

    private TextMeshProUGUI HealSkillText()
    {
        var HealTxt = ObjectPooler.Instance.GetPlayerHealText();

        var Critical = GetCharacter.GetCriticalChance;

        HealTxt.SetActive(true);

        HealTxt.transform.SetParent(TextHolder.transform, false);

        GetCharacter.GetComponent<Health>().GetTakingDamage = false;

        #region CriticalHealChance
        if (Random.value * 100 <= Critical)
        {
            GetCharacter.GetComponent<Health>().IncreaseHealth((Potency + 10) + GetCharacter.CharacterIntelligence);

            HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = SkillName + " " + "<size=35>" + ((Potency + 10) + GetCharacter.CharacterIntelligence).ToString() + "!";
        }
        else
        {
            GetCharacter.GetComponent<Health>().IncreaseHealth(Potency + GetCharacter.CharacterIntelligence);

            HealTxt.GetComponentInChildren<TextMeshProUGUI>().fontSize = 25;

            HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = SkillName + " " + (Potency + GetCharacter.CharacterIntelligence).ToString();
        }
        #endregion

        return HealTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI PlayerStatus()
    {
        var StatusTxt = ObjectPooler.Instance.GetPlayerStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "+" + GetStatusEffectName;

        StatusTxt.GetComponentInChildren<Image>().sprite = button.GetComponent<Image>().sprite;

        StatusEffectSkillText();

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI EnemyStatus()
    {
        var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "+" + GetStatusEffectName;

        StatusTxt.GetComponentInChildren<Image>().sprite = button.GetComponent<Image>().sprite;

        StatusEffectSkillText();

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StatusEffectSkillText()
    {
        if(GetStatusIcon.GetComponent<StatusIcon>())
        {
            GetStatusIcon = ObjectPooler.Instance.GetPlayerStatusIcon();

            GetStatusIcon.SetActive(true);

            GetStatusIcon.transform.SetParent(GetStatusEffectIconTrans, false);

            GetStatusIcon.GetComponent<StatusIcon>().GetEffectStatus = GetPlayerStatusEffect;

            GetStatusIcon.GetComponent<StatusIcon>().PlayerInput();

            GetStatusIcon.GetComponentInChildren<Image>().sprite = button.GetComponent<Image>().sprite;
        }
        else
        {
            GetStatusIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

            GetStatusIcon.SetActive(true);

            GetStatusIcon.transform.SetParent(GetStatusEffectIconTrans, false);

            GetStatusIcon.GetComponentInChildren<Image>().sprite = button.GetComponent<Image>().sprite;

            GetStatusIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = GetEnemyStatusEffect;
            GetStatusIcon.GetComponent<EnemyStatusIcon>().GetPlayer = SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>();
            GetStatusIcon.GetComponentInChildren<Image>().sprite = button.GetComponent<Image>().sprite;
            GetStatusIcon.GetComponent<EnemyStatusIcon>().PlayerInput();
        }
    }

    public TextMeshProUGUI DamageSkillText()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        var DamageTxt = ObjectPooler.Instance.GetEnemyDamageText();

        if(Target != null)
        {
            DamageTxt.SetActive(true);

            DamageTxt.transform.SetParent(TextHolder.transform, false);

            var Critical = GetCharacter.GetCriticalChance;

            #region CriticalHitChance
            if (Random.value * 100 <= Critical)
            {
                Target.GetComponentInChildren<Health>().ModifyHealth(-Mathf.Abs((-Potency - 5) - -Target.GetComponent<Character>().CharacterDefense));

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = SkillName + " " + "<size=25>" + Mathf.Abs((-Potency - 5) -
                                                                         -Target.GetComponent<Character>().CharacterDefense).ToString() + "!";
            }
            else
            {
                Target.GetComponentInChildren<Health>().ModifyHealth(-Mathf.Abs(-Potency - -Target.GetComponent<Character>().CharacterDefense));

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().fontSize = 15;

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = SkillName + " " + Mathf.Abs((-Potency - -Target.GetComponent<Character>().CharacterDefense)).ToString();
            }
            #endregion

            if (Target.GetAI.GetStates != States.Skill)
                Target.GetAI.GetStates = States.Damaged;
        }
        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowSkillPanel(GameObject Panel)
    {
        Panel.gameObject.SetActive(true);

        if(CastTime <= 0 || ManaCost <= 0 || Potency <= 0)
        {
            if(GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillPanelText.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Cooldown: " + CoolDown + " Seconds"
                                    + "\n" + "Cast Time: Instant";
            }
            else
            {
                SkillPanelText.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Added effect: " + GetStatusEffectName + "\n" + "Status Duration: " + GetStatusDuration + 
                                                "\n\n" + "Cooldown: " + CoolDown + " Seconds" + "\n" + "Cast Time: Instant";
            }
        }
        else
        {
            if(GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillPanelText.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Mana: " + ManaCost + "\n" + "Potency: " + Potency + "\n" + "Cooldown: " + CoolDown + 
                                                " Seconds" + "\n" + "Cast Time: " + CastTime + " Seconds";
            }
            else
            {
                SkillPanelText.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Added effect: " + GetStatusEffectName + "\n" + "Status Duration: " + GetStatusDuration + 
                                                "\n\n" + "Mana: " + ManaCost + "\n" + "Potency: " + Potency + "\n" + "Cooldown: " + CoolDown + " Seconds" + "\n" + "Cast Time: " 
                                                + CastTime + " Seconds";
            }
        }
    }

    public void SetSkillIndex()
    {
        SkillsManager.Instance.GetKeyInput = index;
    }
}