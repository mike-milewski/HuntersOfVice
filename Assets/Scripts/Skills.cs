using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public enum PlayerElement { NONE, Fire, Water, Wind, Earth, Light, Dark };

public class Skills : StatusEffects
{
    [SerializeField]
    private PlayerElement playerElement;

    [SerializeField]
    private SkillBar skillbar;

    [SerializeField]
    private Image CoolDownImage;

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
    private float CoolDown, AttackRange, ApplySkill, StatusEffectPotency;

    private float AttackDistance;

    private bool StatusIconCreated;

    private bool IsBeingDragged;

    private bool StormThrustActivated;

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

    public PlayerElement GetPlayerElement
    {
        get
        {
            return playerElement;
        }
        set
        {
            playerElement = value;
        }
    }

    public Image GetCoolDownImage
    {
        get
        {
            return CoolDownImage;
        }
        set
        {
            CoolDownImage = value;
        }
    }

    public string GetSkillDescription
    {
        get
        {
            return SkillDescription;
        }
        set
        {
            SkillDescription = value;
        }
    }

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

    public int GetPotency
    {
        get
        {
            return Potency;
        }
        set
        {
            Potency = value;
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

    public float GetStatusEffectPotency
    {
        get
        {
            return StatusEffectPotency;
        }
        set
        {
            StatusEffectPotency = value;
        }
    }

    public float GetCoolDown
    {
        get
        {
            return CoolDown;
        }
        set
        {
            CoolDown = value;
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
        if (SkillsManager.Instance.GetWhirlwind)
        {
            WhirlwindSlashHit();
        }

        if (GetCharacter != null)
        CheckCoolDownStatus();
    }

    private void CheckCoolDownStatus()
    {
        if (CoolDownImage.fillAmount <= 0 && GetCharacter.CurrentMana >= ManaCost && !GameManager.Instance.GetIsDead && 
            !SkillsManager.Instance.GetActivatedSkill && !SkillsManager.Instance.GetDisruptedSkill && !IsBeingDragged)
        {
            button.interactable = true;
            return;
        }
        else
        {
            this.CoolDownImage.fillAmount -= Time.deltaTime / CoolDown;
            this.button.interactable = false;
        }

        if(StormThrustActivated)
        {
            StormThrustHit();
        }
    }

    public void Heal()
    {
        if (skillbar.GetSkillBar.fillAmount < 1)
        {
            SkillsManager.Instance.GetActivatedSkill = true;

            skillbar.gameObject.SetActive(true);

            skillbar.GetSkill = this.button.GetComponent<Skills>();

            GetCharacter.GetComponent<PlayerAnimations>().PlaySpellCastAnimation();
        }
        if (skillbar.GetSkillBar.fillAmount >= 1)
        {
            this.button.GetComponent<Image>().fillAmount = 1;

            SkillsManager.Instance.GetActivatedSkill = false;

            SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

            var HealParticle = Instantiate(SkillParticle, new Vector3(GetCharacter.transform.position.x, GetCharacter.transform.position.y + 1.0f, GetCharacter.transform.position.z),
                                           Quaternion.identity);

            HealParticle.transform.SetParent(GetCharacter.transform, true);

            GetCharacter.GetComponent<Mana>().ModifyMana(-ManaCost);

            GetCharacter.GetComponent<PlayerAnimations>().EndSpellCastingAnimation();

            Invoke("HealSkillText", ApplySkill);
        }
    }

    public void PlayerStatusEffectSkill()
    {
        this.button.GetComponent<Image>().fillAmount = 1;

        SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

        SkillsManager.Instance.GetStatusIcon.PlayerInput();

        PlayerStatus();  
    }

    public void Poison()
    {
        TextHolder = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetUI;

        GetStatusEffectIconTrans = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetDebuffTransform;

        this.button.GetComponent<Image>().fillAmount = 1;

        SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

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

                this.CoolDownImage.fillAmount = 1;

                SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

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

    private float DistanceToAttack()
    {
        if(GetCharacter.GetComponent<BasicAttack>().GetTarget != null)
        AttackDistance = Vector3.Distance(GetCharacter.transform.position, GetCharacter.GetComponent<BasicAttack>().GetTarget.transform.position);

        return AttackDistance;
    }

    public void StormThrust()
    {
        if(GetCharacter.GetComponent<BasicAttack>().GetTarget == null)
        {
            GameManager.Instance.InvalidTargetText();
        }
        else if(DistanceToAttack() <= 7)
        {
            GameManager.Instance.ShowTargetOutOfRangeText();
        }
        else if (DistanceToAttack() <= AttackRange)
        {
            StormThrustActivated = true;

            TextHolder = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetUI;

            SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = false;

            this.button.GetComponent<Image>().fillAmount = 1;

            SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

            GetCharacter.GetComponent<Mana>().ModifyMana(-ManaCost);

            SkillsManager.Instance.GetActivatedSkill = true;
        }
    }

    private void StormThrustHit()
    {
        GetCharacter.GetComponent<PlayerAnimations>().StormThrustAnimation();

        Vector3 Distance = new Vector3(GetCharacter.GetComponent<BasicAttack>().GetTarget.transform.position.x - GetCharacter.transform.position.x, 0,
                                       GetCharacter.GetComponent<BasicAttack>().GetTarget.transform.position.z - GetCharacter.transform.position.z).normalized;

        Quaternion Look = Quaternion.LookRotation(Distance);

        GetCharacter.transform.rotation = Quaternion.Slerp(GetCharacter.transform.rotation, Look, 10 * Time.deltaTime);
        GetCharacter.GetRigidbody.transform.position += Distance * 30 * Time.deltaTime;

        if(DistanceToAttack() <= 2)
        {
            GetCharacter.GetComponent<BasicAttack>().HitParticleEffect();

            DamageSkillText();

            GetStatusEffectIconTrans = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetDebuffTransform;

            EnemyStatus();

            StormThrustActivated = false;

            SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = true;

            SkillsManager.Instance.GetActivatedSkill = false;

            GetCharacter.GetComponent<PlayerAnimations>().EndStormThrustAnimation();
        }
    }

    public void WhirlwindSlash()
    {
        GetCharacter.GetComponent<PlayerAnimations>().WhirlwindSlashAnimation();

        SkillsManager.Instance.GetActivatedSkill = true;

        SkillsManager.Instance.GetWhirlwind = true;
    }

    private void WhirlwindSlashHit()
    {
        GetCharacter.transform.Rotate(0, 460 * Time.deltaTime, 0);

        SetUpDamagePerimiter(GetCharacter.transform.position, 2);
    }

    public void SetUpDamagePerimiter(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(GetCharacter.transform.position, radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Enemy>())
            {
                hitColliders[i].GetComponentInChildren<Health>().ModifyHealth(-Potency);

                hitColliders[i].GetComponent<EnemyAnimations>().DamageAni();
            }
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(GetCharacter.transform.position.x, GetCharacter.transform.position.y + 1, GetCharacter.transform.position.z), 2f);
    }
    */
    private TextMeshProUGUI HealSkillText()
    {
        var HealTxt = ObjectPooler.Instance.GetPlayerHealText();

        var Critical = GetCharacter.GetCriticalChance;

        HealTxt.SetActive(true);

        HealTxt.transform.SetParent(TextHolder.transform, false);

        #region CriticalHealChance
        if (GetCharacter.CurrentHealth > 0)
        {
            if (Random.value * 100 <= Critical)
            {
                float CriticalValue = Potency * 1.5f;
                Mathf.Round(CriticalValue);

                GetCharacter.GetComponent<Health>().IncreaseHealth((int)CriticalValue + GetCharacter.CharacterIntelligence);

                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = SkillName + " " + "<size=20>" + Mathf.Round(CriticalValue + GetCharacter.CharacterIntelligence) + "!";
            }
            else
            {
                GetCharacter.GetComponent<Health>().IncreaseHealth(Potency + GetCharacter.CharacterIntelligence);

                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + (Potency + GetCharacter.CharacterIntelligence);
            }
        }
        #endregion

        return HealTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI EnemyStatus()
    {
        var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4> +" + GetStatusEffectName;

        StatusTxt.GetComponentInChildren<Image>().sprite = button.GetComponent<Image>().sprite;

        StatusEffectSkillText();

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI PlayerStatus()
    {
        var StatusTxt = ObjectPooler.Instance.GetPlayerStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4> +" + GetStatusEffectName;

        StatusTxt.GetComponentInChildren<Image>().sprite = button.GetComponent<Image>().sprite;

        StatusEffectSkillText();

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StatusEffectSkillText()
    {
        if(!GetStatusIcon.activeInHierarchy)
        {
            if (GetStatusIcon.GetComponent<StatusIcon>())
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
        else
        {
            if (GetStatusIcon.GetComponent<StatusIcon>())
            {
                GetStatusIcon.GetComponent<StatusIcon>().PlayerInput();
            }
            else
            {
                GetStatusIcon.GetComponent<EnemyStatusIcon>().PlayerInput();
            }
        }
    }

    public TextMeshProUGUI DamageSkillText()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        var DamageTxt = ObjectPooler.Instance.GetEnemyDamageText();

        if(Target != null)
        {
            DamageTxt.SetActive(true);

            TextHolder = Target.GetUI;

            DamageTxt.transform.SetParent(TextHolder.transform, false);

            var Critical = GetCharacter.GetCriticalChance;

            #region CriticalHitChance
            if (Random.value * 100 <= Critical)
            {
                float CritValue = Potency * 1.5f;

                Mathf.Round(CritValue);

                Target.GetHealth.ModifyHealth(-(((int)CritValue + GetCharacter.CharacterStrength) - Target.GetCharacter.CharacterDefense));

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = SkillName + " " + "<size=20>" + Mathf.Round((CritValue + GetCharacter.CharacterStrength) - 
                                                                           Target.GetCharacter.CharacterDefense) + "!";
            }
            else
            {
                Target.GetHealth.ModifyHealth(-((Potency + GetCharacter.CharacterStrength) - Target.GetCharacter.CharacterDefense));

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + ((Potency + GetCharacter.CharacterStrength) - 
                                                                           Target.GetCharacter.CharacterDefense);
            }
            #endregion

            if (Target.GetAI.GetStates != States.Skill)
                Target.GetAI.GetStates = States.Damaged;
        }
        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowSkillPanel(GameObject Panel)
    {
        if (gameObject.transform.parent == gameObject.GetComponent<DragUiObject>().GetDropZone.transform)
        {
            Panel.gameObject.SetActive(true);
        }

        if(CastTime <= 0 && Potency <= 0 && ManaCost <= 0)
        {
            if(GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "Cooldown: " + CoolDown + "s" + "\n" + 
                                      "Cast Time: Instant";
            }
            else
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + 
                                      GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "Cooldown: " + CoolDown + "s" + 
                                      "\n" + "Cast Time: Instant";
            }
        }
        if(CastTime > 0 && Potency <= 0 && ManaCost <= 0)
        {
            if(GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "Cooldown: " + CoolDown + " Seconds" + "\n" + 
                                      "Cast Time: " + CastTime + "s";
            }
            else
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + 
                                      GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "Cooldown: " + CoolDown + "s" + 
                                      "\n" + "Cast Time: " + CastTime + "s";
            }
        }
        if(CastTime > 0 && Potency > 0 && ManaCost <= 0)
        {
            if (GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "Power: " + Potency + "\n" + "Cooldown: " + 
                                      CoolDown + "s" + "\n" + "Cast Time: " + CastTime + "s";
            }
            else
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + 
                                      GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "Power: " + Potency + "\n" + 
                                      "Cooldown: " + CoolDown + "s" + "\n" + "Cast Time: " + CastTime + "s";
            }
        }
        if(CastTime > 0 && Potency > 0 && ManaCost > 0)
        {
            if (GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "Mana: " + ManaCost + "\n" + "Power: " + Potency + 
                                      "\n" + "Cooldown: " + CoolDown + "s" + "\n" + "Cast Time: " + CastTime + "s";
            }
            else
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n" + "Mana: " + ManaCost + "\n\n" + "<#EFDFB8>" + 
                                      "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" 
                                      + "Power: " + Potency + "\n" + "Cooldown: " + CoolDown + "s" + "\n" + "Cast Time: " + CastTime + "s";
            }
        }
        if(CastTime <= 0 && Potency > 0 && ManaCost <= 0)
        {
            if (GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "Power: " + Potency + "\n" + "Cooldown: " + 
                                      CoolDown + "s" + "\n" + "Cast Time: Instant";
            }
            else
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + 
                                      GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "Power: " + Potency + "\n" + 
                                      "Cooldown: " + CoolDown + "s" + "\n" + "Cast Time: Instant";
            }
        }
        if(CastTime <= 0 && Potency > 0 && ManaCost > 0)
        {
            if (GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillPanelText.text = "<size=12>" + "<u>" + SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "Mana: " + ManaCost + "\n" + "Power: " + Potency + 
                                      "\n" + "Cooldown: " + CoolDown + "s" + "\n" + "Cast Time: Instant";
            }
            else
            {
                SkillPanelText.text = "<size=12>" + "<u>" +SkillName + "</u>" + "</size>" + "\n\n" + SkillDescription + "\n\n" + "Mana: " + ManaCost + "\n\n" + "<#EFDFB8>" + 
                                      "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>"+ GetStatusDuration + "s" + "\n\n" + 
                                      "Power: " + Potency + "\n" + "Cooldown: " + CoolDown + "s" + "\n" + "Cast Time: Instant";
            }
        }
    }

    public void SetSkillIndex()
    {
        SkillsManager.Instance.GetKeyInput = index;
    }
}