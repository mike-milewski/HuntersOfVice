#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public enum PlayerElement { NONE, Physical, Magic, Fire, Water, Wind, Earth, Light, Dark };

public class Skills : StatusEffects
{
    [SerializeField]
    private PlayerElement playerElement;

    [SerializeField]
    private Settings settings;

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
    private TextMeshProUGUI SkillPanelText, SkillNameText, CoolDownText;

    [SerializeField]
    private GameObject StatusEffectText;

    private GameObject SkillParticle;

    [SerializeField]
    private Transform SkillParticleParent;

    [SerializeField]
    private float CoolDown, AttackRange, ApplySkill, StatusEffectPotency, HpAndDamageOverTimeTick, AreaOfEffectRange, InstantKnockOutValue, ContractHp, ContractMp;

    private float AttackDistance;

    private bool StatusIconCreated;

    private bool IsBeingDragged;

    private bool StormThrustActivated, FacingEnemy;

    [SerializeField]
    private bool GainedPassive, OffensiveSpell, UnlockedBonus;

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

    private float CD;

    private Quaternion rot;

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

    public float GetInstantKnockOutValue
    {
        get
        {
            return InstantKnockOutValue;
        }
        set
        {
            InstantKnockOutValue = value;
        }
    }

    public float GetHpAndDamageOverTimeTick
    {
        get
        {
            return HpAndDamageOverTimeTick;
        }
        set
        {
            HpAndDamageOverTimeTick = value;
        }
    }

    public float GetContractHp
    {
        get
        {
            return ContractHp;
        }
        set
        {
            ContractHp = value;
        }
    }

    public float GetContractMp
    {
        get
        {
            return ContractMp;
        }
        set
        {
            ContractMp = value;
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

    public float GetAreaOfEffectRange
    {
        get
        {
            return AreaOfEffectRange;
        }
        set
        {
            AreaOfEffectRange = value;
        }
    }

    public bool GetOffensiveSpell
    {
        get
        {
            return OffensiveSpell;
        }
        set
        {
            OffensiveSpell = value;
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

    public bool GetGainedPassive
    {
        get
        {
            return GainedPassive;
        }
        set
        {
            GainedPassive = value;
        }
    }

    public bool GetFacingEnemy
    {
        get
        {
            return FacingEnemy;
        }
        set
        {
            FacingEnemy = value;
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

    private void Start()
    {
        CD = CoolDown;
    }

    private void Update()
    {
        if (GetCharacter != null)
        CheckCoolDownStatus();
    }

    private void CheckCoolDownStatus()
    {
        if (CoolDownImage.fillAmount <= 0 && GetCharacter.CurrentMana >= ManaCost && !GameManager.Instance.GetIsDead && 
            !SkillsManager.Instance.GetActivatedSkill && !SkillsManager.Instance.GetDisruptedSkill && !IsBeingDragged)
        {
            button.interactable = true;

            CD = CoolDown;

            CoolDownText.enabled = false;

            return;
        }
        else
        {
            this.CoolDownImage.fillAmount -= Time.deltaTime / CoolDown;
            this.button.interactable = false;
        }
        if(CoolDownImage.fillAmount > 0)
        {
            CD -= Time.deltaTime;

            CoolDownText.text = Mathf.Clamp(CD, 0, CoolDown).ToString("F1");
        }

        if (StormThrustActivated)
        {
            StormThrustHit();
        }

        if (SkillsManager.Instance.GetWhirlwind)
        {
            WhirlwindSlashHit();
        }

        if(FacingEnemy)
        {
            FaceEnemy();
        }
    }

    public void Shatter()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        if (Target != null)
        {
            if (DistanceToAttack() <= AttackRange)
            {
                FacingEnemy = true;

                TextHolder = Target.GetUI;

                SkillsManager.Instance.GetActivatedSkill = true;

                if (skillbar.GetSkillBar.fillAmount < 1)
                {
                    skillbar.gameObject.SetActive(true);

                    skillbar.GetSkill = this.button.GetComponent<Skills>();

                    GetCharacter.GetComponent<PlayerAnimations>().PlaySpellCastAnimation();
                }
                if (skillbar.GetSkillBar.fillAmount >= 1)
                {
                    this.CoolDownImage.fillAmount = 1;

                    FacingEnemy = false;

                    SkillsManager.Instance.GetActivatedSkill = false;

                    SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

                    GetCharacter.GetComponent<PlayerAnimations>().SpellCast();
                    GetCharacter.GetComponent<PlayerAnimations>().EndSpellCastingAnimation();

                    Invoke("InvokeShatter", ApplySkill);
                }
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

    private void InvokeShatter()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        if(Target != null)
        {
            GetCharacter.GetComponent<Mana>().ModifyMana(-ManaCost);

            if (settings.UseParticleEffects)
            {
                SkillParticle = ObjectPooler.Instance.GetShatterParticle();

                SkillParticle.SetActive(true);

                SkillParticle.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 1.0f, Target.transform.position.z);

                SkillParticle.transform.SetParent(Target.transform, true);
            }

            if (GainedPassive)
            {
                if (Random.value * 100 <= InstantKnockOutValue)
                {
                    if(!Target.GetComponent<Puck>() && !Target.GetComponent<VineGolem>() && !Target.GetComponent<SylvanDiety>())
                    {
                        Target.GetHealth.ModifyHealth(-Target.GetCharacter.MaxHealth);
                    }
                    else
                    {
                        if (Target.GetComponent<Puck>())
                        {
                            if (Target.GetPuckAI.GetPlayerTarget == null)
                            {
                                Target.GetPuckAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
                                Target.GetPuckAI.GetStates = BossStates.Chase;
                            }
                            else return;
                        }
                    }
                }
                else
                {
                    if (Target.GetComponent<Puck>())
                    {
                        if (Target.GetPuckAI.GetPlayerTarget == null)
                        {
                            Target.GetPuckAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
                            Target.GetPuckAI.GetStates = BossStates.Chase;
                        }
                        else return;
                    }
                    else
                    {
                        if (Target.GetAI.GetPlayerTarget == null)
                        {
                            Target.GetAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
                            Target.GetAI.GetStates = States.Chase;
                        }
                        else return;
                    }
                }
            }
            else
            {
                if (Target.GetComponent<Puck>())
                {
                    if (Target.GetPuckAI.GetPlayerTarget == null)
                    {
                        Target.GetPuckAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
                        Target.GetPuckAI.GetStates = BossStates.Chase;
                    }
                    else return;
                }
                else
                {
                    if (Target.GetAI.GetPlayerTarget == null)
                    {
                        Target.GetAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
                        Target.GetAI.GetStates = States.Chase;
                    }
                    else return;
                }
            }
        }
    }

    public void Alleviate()
    {
        SkillsManager.Instance.GetActivatedSkill = true;

        if (settings.UseParticleEffects)
        {
            SkillParticle = ObjectPooler.Instance.GetAlleviateParticle();

            SkillParticle.SetActive(true);

            SkillParticle.transform.position = new Vector3(GetCharacter.transform.position.x, GetCharacter.transform.position.y, GetCharacter.transform.position.z);

            SkillParticle.transform.SetParent(GetCharacter.transform, true);
        }
        Invoke("InvokeAlleviate", ApplySkill);

        SoundManager.Instance.Heal();
    }

    public void InvokeAlleviate()
    {
        SkillsManager.Instance.GetActivatedSkill = false;

        this.CoolDownImage.fillAmount = 1;

        AlleviateHealSkillText();

        if (GameManager.Instance.GetDebuffStatusIconHolder.childCount > 0)
        {
            foreach (StatusIcon esi in GameManager.Instance.GetDebuffStatusIconHolder.GetComponentsInChildren<StatusIcon>())
            {
                esi.RemoveEffect();
            }
        }
        else return;
    }

    public void DiabolicLightning()
    {
        Invoke("InvokeDiabolocLightning", ApplySkill);
    }

    private void InvokeDiabolicLightning()
    {
        SetUpDamagePerimiter(SkillsManager.Instance.GetCharacter.transform.position, AreaOfEffectRange);
    }

    public void Heal()
    {
        SkillsManager.Instance.GetActivatedSkill = true;

        if (skillbar.GetSkillBar.fillAmount < 1)
        {
            skillbar.gameObject.SetActive(true);

            skillbar.GetSkill = this.button.GetComponent<Skills>();

            GetCharacter.GetComponent<PlayerAnimations>().PlaySpellCastAnimation();
        }
        if (skillbar.GetSkillBar.fillAmount >= 1)
        {
            SkillsManager.Instance.GetActivatedSkill = false;

            SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

            if(settings.UseParticleEffects)
            {
                SkillParticle = ObjectPooler.Instance.GetHealParticle();

                SkillParticle.SetActive(true);

                SkillParticle.transform.position = new Vector3(GetCharacter.transform.position.x, GetCharacter.transform.position.y + 1.0f, GetCharacter.transform.position.z);

                SkillParticle.transform.SetParent(GetCharacter.transform, true);
            }
            SoundManager.Instance.Heal();

            GetCharacter.GetComponent<Mana>().ModifyMana(-ManaCost);

            GetCharacter.GetComponent<PlayerAnimations>().EndSpellCastingAnimation();

            Invoke("HealSkillText", ApplySkill);
        }
    }

    public void Tenacity()
    {
        if(settings.UseParticleEffects)
        {
            SkillParticle = ObjectPooler.Instance.GetStrengthUpParticle();

            SkillParticle.SetActive(true);

            SkillParticle.transform.position = new Vector3(SkillParticleParent.position.x, SkillParticleParent.position.y + 1.0f, SkillParticleParent.position.z);

            SkillParticle.transform.SetParent(GetCharacter.transform);
        }

        SkillsManager.Instance.GetActivatedSkill = true;

        SkillCast();
    }

    public void Contract()
    {
        if(GetStatusIcon.activeInHierarchy)
        {
            GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
        }
        else
        {
            Invoke("InvokeContract", ApplySkill);

            SkillsManager.Instance.GetActivatedSkill = true;

            SkillCast();
        }
    }

    private void InvokeContract()
    {
        if (settings.UseParticleEffects)
        {
            SkillParticle = ObjectPooler.Instance.GetContractParticle();

            SkillParticle.SetActive(true);

            SkillParticle.transform.position = new Vector3(SkillParticleParent.position.x, SkillParticleParent.position.y, SkillParticleParent.position.z);

            SkillParticle.transform.SetParent(GetCharacter.transform);
        }
    }

    public void Illumination()
    {
        if (settings.UseParticleEffects)
        {
            SkillParticle = ObjectPooler.Instance.GetIlluminationEffectParticle();

            SkillParticle.SetActive(true);

            SkillParticle.transform.position = new Vector3(SkillParticleParent.position.x, SkillParticleParent.position.y + 1.0f, SkillParticleParent.position.z);

            SkillParticle.transform.SetParent(GetCharacter.transform);
        }

        SkillsManager.Instance.GetActivatedSkill = true;

        SkillCast();
    }

    private void SkillCast()
    {
        GetCharacter.GetComponent<PlayerAnimations>().SkillCastAnimation();

        Invoke("PlayerStatusEffectSkill", ApplySkill);
    }

    private void PlayerStatusEffectSkill()
    {
        SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

        SkillsManager.Instance.GetStatusIcon.PlayerInput();

        PlayerStatus();  
    }

    public void SwiftStrike()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        if(Target != null)
        { 
            if(DistanceToAttack() <= AttackRange)
            {
                FacingEnemy = true;

                TextHolder = Target.GetUI;

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

    public void BraveLight()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        if (Target != null)
        {
            if (DistanceToAttack() <= AttackRange)
            {
                FacingEnemy = true;

                TextHolder = Target.GetUI;

                SkillsManager.Instance.GetActivatedSkill = true;

                this.CoolDownImage.fillAmount = 1;

                SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>()); 

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

    public void SetBraveLightTextHolder()
    {
        TextHolder = GameManager.Instance.GetStatusEffectTransform;

        PlayerStatusEffectSkill();
    }

    private void FaceEnemy()
    {
        if(GetCharacter.GetComponent<BasicAttack>().GetTarget != null)
        {
            Vector3 Distance = new Vector3(GetCharacter.GetComponent<BasicAttack>().GetTarget.transform.position.x - GetCharacter.transform.position.x, 0,
                                       GetCharacter.GetComponent<BasicAttack>().GetTarget.transform.position.z - GetCharacter.transform.position.z).normalized;

            Quaternion Look = Quaternion.LookRotation(Distance);

            GetCharacter.transform.rotation = Quaternion.Slerp(GetCharacter.transform.rotation, Look, 10 * Time.deltaTime);
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
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        if(Target == null)
        {
            GameManager.Instance.InvalidTargetText();
        }
        else if(DistanceToAttack() < AttackRange)
        {
            GameManager.Instance.ShowTargetOutOfRangeText();
        }
        else if (DistanceToAttack() >= AttackRange)
        {
            StormThrustActivated = true;

            TextHolder = Target.GetUI;

            SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = false;

            this.CoolDownImage.fillAmount = 1;

            SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

            GetCharacter.GetComponent<Mana>().ModifyMana(-ManaCost);

            SkillsManager.Instance.GetActivatedSkill = true;
        }
    }

    private void StormThrustHit()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        GetCharacter.GetComponent<PlayerAnimations>().StormThrustAnimation();

        if(Target != null)
        {
            Vector3 Distance = new Vector3(Target.transform.position.x - GetCharacter.transform.position.x, 0,
                                           Target.transform.position.z - GetCharacter.transform.position.z).normalized;

            Quaternion Look = Quaternion.LookRotation(Distance);

            GetCharacter.transform.rotation = Quaternion.Slerp(GetCharacter.transform.rotation, Look, 10 * Time.deltaTime);
            GetCharacter.GetRigidbody.transform.position += Distance * 30 * Time.deltaTime;

            if (DistanceToAttack() <= 2)
            {
                GetCharacter.GetComponent<BasicAttack>().HitParticleEffect();

                DamageSkillText(Target);

                GetStatusEffectIconTrans = Target.GetDebuffTransform;

                EnemyStatus();

                StormThrustActivated = false;

                SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = true;

                SkillsManager.Instance.GetActivatedSkill = false;

                GetCharacter.GetComponent<PlayerAnimations>().EndStormThrustAnimation();
            }
        }
        else
        {
            StormThrustActivated = false;

            SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = true;

            SkillsManager.Instance.GetActivatedSkill = false;

            GetCharacter.GetComponent<PlayerAnimations>().EndStormThrustAnimation();
        }
    }

    public void WhirlwindSlash()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        rot = GetCharacter.transform.rotation;

        if (Target != null)
        {
            if(DistanceToAttack() <= AttackRange)
            {
                SkillParticle = ObjectPooler.Instance.GetWhirlwindSlashParticle();

                SkillParticle.SetActive(true);

                SkillParticle.transform.position = new Vector3(SkillParticleParent.position.x, SkillParticleParent.position.y, SkillParticleParent.position.z);

                SkillParticle.transform.SetParent(SkillParticleParent);

                GetCharacter.GetComponent<PlayerAnimations>().WhirlwindSlashAnimation();

                SkillsManager.Instance.GetActivatedSkill = true;

                this.CoolDownImage.fillAmount = 1;
            }
            else
            {
                GameManager.Instance.ShowTargetOutOfRangeText();
            }
        }
        else
        {
            GameManager.Instance.InvalidTargetText();
        }
    }

    private void WhirlwindSlashHit()
    {
        GetCharacter.transform.Rotate(0, 420 * Time.deltaTime, 0);
    }

    public void SetUpDamagePerimiter(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Enemy>())
            {
                if(settings.UseParticleEffects)
                {
                    SkillParticle = ObjectPooler.Instance.GetHitParticle();

                    SkillParticle.SetActive(true);

                    SkillParticle.transform.position = new Vector3(hitColliders[i].transform.position.x, hitColliders[i].transform.position.y + 0.5f,
                                                                   hitColliders[i].transform.position.z);

                    SkillParticle.transform.SetParent(hitColliders[i].transform);
                }
                DamageSkillText(hitColliders[i].GetComponent<Enemy>());
            }
        }
        GetCharacter.transform.rotation = rot;
    }

    public void EvilsEnd()
    {
        var Target = GetCharacter.GetComponent<BasicAttack>().GetTarget;

        if (Target != null)
        {
            int TargetCurrentHP = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.CurrentHealth;
            int TargetMaxHP = GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.MaxHealth;

            float HpCap = ((float)TargetCurrentHP / (float)TargetMaxHP) * 100f;

            if (DistanceToAttack() <= AttackRange)
            {
                if(HpCap <= StatusEffectPotency)
                {
                    GetCharacter.GetComponent<PlayerAnimations>().EvilsEndAnimation();

                    FacingEnemy = true;

                    TextHolder = Target.GetUI;

                    SkillsManager.Instance.GetActivatedSkill = true;

                    this.CoolDownImage.fillAmount = 1;

                    SkillsManager.Instance.CheckForSameSkills(this.GetComponent<Skills>());

                    GetCharacter.GetComponent<Mana>().ModifyMana(-ManaCost);
                }
                else
                {
                    GameManager.Instance.CannotExecuteText();
                }
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

    private TextMeshProUGUI AlleviateHealSkillText()
    {
        var HealTxt = ObjectPooler.Instance.GetPlayerHealText();

        HealTxt.SetActive(true);

        HealTxt.transform.SetParent(TextHolder.transform, false);

        #region CriticalHealChance
        if (GetCharacter.CurrentHealth > 0)
        {
            float AlleviatePercentage = 0.20f * GetCharacter.MaxHealth;

            Mathf.RoundToInt(AlleviatePercentage);

            GetCharacter.GetComponent<Health>().IncreaseHealth((int)AlleviatePercentage);

            HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + SkillName + " " + (int)AlleviatePercentage;
        }
        #endregion

        return HealTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI HealSkillText()
    {
        var HealTxt = ObjectPooler.Instance.GetPlayerHealText();

        HealTxt.SetActive(true);

        HealTxt.transform.SetParent(TextHolder.transform, false);

        var Critical = GetCharacter.GetCriticalChance;

        #region CriticalHealChance
        if (GetCharacter.CurrentHealth > 0)
        {
            if (Random.value * 100 <= Critical)
            {
                float CriticalValue = Potency * 1.5f;
                Mathf.Round(CriticalValue);

                GetCharacter.GetComponent<Health>().IncreaseHealth((int)CriticalValue + GetCharacter.CharacterIntelligence);

                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + SkillName + "</size>" + " " + "<size=35>" + 
                                                                         Mathf.Round(CriticalValue + GetCharacter.CharacterIntelligence) + "!";
            }
            else
            {
                GetCharacter.GetComponent<Health>().IncreaseHealth(Potency + GetCharacter.CharacterIntelligence);

                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + SkillName + " " + (Potency + GetCharacter.CharacterIntelligence);
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

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + GetStatusEffectName;

        StatusTxt.GetComponentInChildren<Image>().sprite = button.GetComponent<Image>().sprite;

        StatusEffectSkillText();

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI PlayerStatus()
    {
        var StatusTxt = ObjectPooler.Instance.GetPlayerStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + GetStatusEffectName;

        StatusTxt.GetComponentInChildren<Image>().sprite = button.GetComponent<Image>().sprite;

        StatusEffectSkillText();

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StatusEffectSkillText()
    {
        if (!GetStatusIcon.activeInHierarchy)
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
                if(GetStatusIcon.GetComponentInChildren<Image>().enabled)
                {
                    GetStatusIcon.GetComponent<EnemyStatusIcon>().PlayerInput();
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
        }
    }

    public TextMeshProUGUI DamageSkillText(Enemy Target)
    {
        var DamageTxt = ObjectPooler.Instance.GetEnemyDamageText();

        if(Target != null)
        {
            DamageTxt.SetActive(true);

            TextHolder = Target.GetUI;

            DamageTxt.transform.SetParent(TextHolder.transform, false);

            int DamageType = GetCharacter.GetCharacterData.CharacterName == "Knight" ? GetCharacter.CharacterStrength : GetCharacter.CharacterIntelligence;

            var Critical = GetCharacter.GetCriticalChance;

            #region CriticalHitChance
            if (Random.value * 100 <= Critical)
            {
                float CritValue = Potency * 1.25f;

                Mathf.RoundToInt(CritValue);

                if(CheckWeaknesses())
                {
                    int WeakDamage = (DamageType * 2) + (int)CritValue;

                    if(WeakDamage - Target.GetCharacter.CharacterDefense < 0)
                    {
                        Target.GetHealth.ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "<size=20>" + "1!" + "</size>" +
                                                                                   "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                    }
                    else
                    {
                        Target.GetHealth.ModifyHealth(-(WeakDamage - Target.GetCharacter.CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "<size=20>" +
                                                                                   (WeakDamage - Target.GetCharacter.CharacterDefense) + "!" + "</size>" +
                                                                                   "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                    }
                }
                else if(CheckResistances())
                {
                    float ResistValue = (DamageType + (int)CritValue) / 1.25f;

                    Mathf.RoundToInt(ResistValue);

                    if((int)ResistValue - Target.GetCharacter.CharacterDefense < 0)
                    {
                        Target.GetHealth.ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "<size=20>" + "1!" + "</size>" +
                                                                                   "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                    }
                    else
                    {
                        Target.GetHealth.ModifyHealth(-((int)ResistValue - Target.GetCharacter.CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "<size=20>" +
                                                                                   ((int)ResistValue - Target.GetCharacter.CharacterDefense) + "!" + "</size>" +
                                                                                   "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                    }
                }
                else if(CheckImmunities())
                {
                    DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "0" + "\n" + "<size=12> <#EFDFB8>" + "(IMMUNE!)" +
                                                                               "</color> </size>";
                }
                else if(CheckAbsorptions())
                {
                    if((DamageType + (int)CritValue) - Target.GetCharacter.CharacterDefense < 0)
                    {
                        Target.GetHealth.IncreaseHealth(1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "<size=20>" + "<#4CFFAD>" +
                                                                                   "1!" + "</size>" + "\n" + "</color>" + "<size=12> <#EFDFB8>" + "(ABSORBED!)";
                    }
                    else
                    {
                        Target.GetHealth.IncreaseHealth((GetCharacter.CharacterStrength + (int)CritValue) - Target.GetCharacter.CharacterDefense);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "<size=20>" + "<#4CFFAD>" +
                                                                                   ((DamageType + (int)CritValue) - Target.GetCharacter.CharacterDefense)
                                                                                   + "!" + "</size>" + "\n" + "</color>" + "<size=12> <#EFDFB8>" + "(ABSORBED!)";
                    }
                }
                else
                {
                    if((DamageType + (int)CritValue) - Target.GetCharacter.CharacterDefense < 0)
                    {
                        Target.GetHealth.ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = SkillName + " " + "<size=20>" + "1!";
                    }
                    else
                    {
                        Target.GetHealth.ModifyHealth(-((GetCharacter.CharacterStrength + (int)CritValue) - Target.GetCharacter.CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = SkillName + " " + "<size=20>" + Mathf.RoundToInt((CritValue + DamageType) -
                                                                                   Target.GetCharacter.CharacterDefense) + "!";
                    }
                }
            }
            else
            {
                if(CheckWeaknesses())
                {
                    int WeakDamage = (Potency + DamageType) * 2;

                    if(WeakDamage - Target.GetCharacter.CharacterDefense < 0)
                    {
                        Target.GetHealth.ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "1"
                                                                                   + "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                    }
                    else
                    {
                        Target.GetHealth.ModifyHealth(-(WeakDamage - Target.GetCharacter.CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + (WeakDamage - Target.GetCharacter.CharacterDefense)
                                                                                   + "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                    }
                }
                else if(CheckResistances())
                {
                    float ResistDamage = (Potency + DamageType) / 1.25f;

                    Mathf.RoundToInt(ResistDamage);

                    if(ResistDamage - Target.GetCharacter.CharacterDefense < 0)
                    {
                        Target.GetComponentInChildren<Health>().ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "1" + "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" +
                                                                                    "</color> </size>";
                    }
                    else
                    {
                        Target.GetComponentInChildren<Health>().ModifyHealth(-((int)ResistDamage - Target.GetCharacter.CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + Mathf.Round(ResistDamage -
                                                                                    Target.GetCharacter.CharacterDefense) + "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" +
                                                                                    "</color> </size>";
                    }
                }
                else if(CheckImmunities())
                {
                    DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "0" + "\n" + "<size=12> <#EFDFB8>" + "(IMMUNE!)" +
                                                                               "</color> </size>";
                }
                else if(CheckAbsorptions())
                {
                    if((Potency + DamageType) - Target.GetCharacter.CharacterDefense < 0)
                    {
                        Target.GetHealth.IncreaseHealth(1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "<#4CFFAD>" + "1" + "\n" + "</color> <size=12>" + "<#EFDFB8>" +
                                                                                   "(ABSORBED!)";
                    }
                    else
                    {
                        Target.GetHealth.IncreaseHealth((Potency + GetCharacter.CharacterStrength) - Target.GetCharacter.CharacterDefense);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + "<#4CFFAD>" + ((Potency + DamageType) -
                                                                                   Target.GetCharacter.CharacterDefense) + "\n" + "</color> <size=12>" + "<#EFDFB8>" +
                                                                                   "(ABSORBED!)";
                    }
                }
                else
                {
                    if((Potency + DamageType) - Target.GetCharacter.CharacterDefense < 0)
                    {
                        Target.GetHealth.ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " 1";
                    }
                    else
                    {
                        Target.GetHealth.ModifyHealth(-((Potency + GetCharacter.CharacterStrength) - Target.GetCharacter.CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + SkillName + " " + ((Potency + DamageType) -
                                                                                   Target.GetCharacter.CharacterDefense);
                    }
                }
            }
            #endregion

            if(Target.GetAI != null)
            {
                if(Target.GetAI.GetPlayerTarget == null)
                {
                    Target.GetAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
                }

                if (Target.GetAI.GetStates != States.Skill && Target.GetAI.GetStates != States.ApplyingAttack && Target.GetAI.GetStates != States.SkillAnimation &&
                !CheckAbsorptions())
                {
                    Target.GetAI.GetStates = States.Damaged;
                }
            }
            else
            {
                if(Target.GetPuckAI.GetPlayerTarget == null)
                {
                    Target.GetPuckAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
                    Target.GetPuckAI.GetStates = BossStates.Chase;
                    Target.GetPuckAI.EnableSpeech();
                }

                Target.GetPuckAI.CheckHP();
            }
        }
        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private bool CheckWeaknesses()
    {
        bool Weakness = false;

        for (int i = 0; i < GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.GetCharacterData.Weaknesses.Length; i++)
        {
            if (playerElement == (PlayerElement)GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.GetCharacterData.Weaknesses[i])
            {
                Weakness = true;
            }
        }
        return Weakness;
    }

    private bool CheckResistances()
    {
        bool Resistance = false;

        for (int i = 0; i < GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.GetCharacterData.Resistances.Length; i++)
        {
            if (playerElement == (PlayerElement)GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.GetCharacterData.Resistances[i])
            {
                Resistance = true;
            }
        }
        return Resistance;
    }

    private bool CheckImmunities()
    {
        bool Immunity = false;

        for (int i = 0; i < GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.GetCharacterData.Immunities.Length; i++)
        {
            if (playerElement == (PlayerElement)GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.GetCharacterData.Immunities[i])
            {
                Immunity = true;
            }
        }
        return Immunity;
    }

    private bool CheckAbsorptions()
    {
        bool Absorption = false;

        for (int i = 0; i < GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.GetCharacterData.Absorbtions.Length; i++)
        {
            if (playerElement == (PlayerElement)GetCharacter.GetComponent<BasicAttack>().GetTarget.GetCharacter.GetCharacterData.Absorbtions[i])
            {
                Absorption = true;
            }
        }
        return Absorption;
    }

    public void ShowSkillPanel(GameObject Panel)
    {
        if (gameObject.transform.parent == gameObject.GetComponent<DragUiObject>().GetDropZone.transform)
        {
            SkillNameText.text = SkillName;

            Panel.gameObject.SetActive(true);
        }

        if(CastTime <= 0 && Potency <= 0 && ManaCost <= 0)
        {
            if(GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "Cast Time: Instant" + "\n" +
                                          "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" 
                                          + GetPlayerElement + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + CoolDown + "s";
                }
            }
            else
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                          GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "Cast Time: Instant" +
                                          "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + 
                                          "</color>" + GetStatusDuration + "s" + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + CoolDown + "s";
                }
            }
        }
        if(CastTime > 0 && Potency <= 0 && ManaCost <= 0)
        {
            if(GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "Cast Time: " + CastTime + "s" + "\n" +
                                          "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "Cast Time: " + CastTime + "s" + "\n" + "Cooldown: " + CoolDown + "s";
                }
            }
            else
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                          GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "Cast Time: " + CastTime + "s" +
                                          "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + 
                                          "</color>" + GetStatusDuration + "s" + "\n\n" + "Cast Time: " + CastTime + "s" + "\n" + "Cooldown: " + CoolDown + "s";
                }
            }
        }
        if(CastTime > 0 && Potency > 0 && ManaCost <= 0)
        {
            if (GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "Power: " + Potency + "\n\n" + "Cast Time: " +
                                          CastTime + "s" + "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "Power: " + Potency + "\n\n" + "Cast Time: " + CastTime + "s" + "\n" + "Cooldown: " + CoolDown + "s";
                }
            }
            else
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                          GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "Power: " + Potency + "\n\n" +
                                          "Cast Time: " + CastTime + "s" + "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + 
                                          "</color>" + GetStatusDuration + "s" + "\n\n" + "Power: " + Potency + "\n\n" + "Cast Time: " + CastTime + "s" + "\n" + "Cooldown: " + 
                                          CoolDown + "s";
                }
            }
        }
        if(CastTime > 0 && Potency > 0 && ManaCost > 0)
        {
            if (GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "Power: " + Potency + "\n" + "MP Cost: " + 
                                          ManaCost + "\n\n" + "Cast Time: " + CastTime + "s" + "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "Power: " + Potency + "\n" + "MP Cost: " + ManaCost + "\n\n" + "Cast Time: " + CastTime + "s" + "\n" + "Cooldown: " 
                                          + CoolDown + "s";
                }
            }
            else
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "Power: " + Potency + "\n\n" + "<#EFDFB8>" +
                                          "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n"
                                          + "MP Cost: " + ManaCost + "\n\n" + "Cast Time: " + CastTime + "s" + "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "Power: " + Potency + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" 
                                          + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "MP Cost: " + ManaCost + "\n\n" + "Cast Time: " + CastTime + "s" + 
                                          "\n" + "Cooldown: " + CoolDown + "s";
                }
            }
        }
        if(CastTime <= 0 && Potency > 0 && ManaCost <= 0)
        {
            if (GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "Power: " + Potency + "\n\n" + "Cast Time: " +
                                          "Instant" + "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "Power: " + Potency + "\n\n" + "Cast Time: " + "Instant" + "\n" + "Cooldown: " + CoolDown + "s";
                }
            }
            else
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                          GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "Power: " + Potency + "\n\n" +
                                          "Cast Time: Instant" + "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + 
                                          "</color>" + GetStatusDuration + "s" + "\n\n" + "Power: " + Potency + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + CoolDown + "s";
                }
            }
        }
        if(CastTime <= 0 && Potency > 0 && ManaCost > 0)
        {
            if (GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "Power: " + Potency + "\n" + "MP Cost: " + 
                                          ManaCost + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "Power: " + Potency + "\n" + "MP Cost: " + ManaCost + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + 
                                          CoolDown + "s";
                }
            }
            else
            {
                if(GetPlayerElement == PlayerElement.NONE)
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "Power: " + Potency + "\n\n" + "<#EFDFB8>" +
                                          "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" +
                                          "MP Cost: " + ManaCost + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + CoolDown + "s";
                }
                else
                {
                    SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                          GetPlayerElement + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + GetStatusEffectName + "\n" + 
                                          "<#EFDFB8>" + "Status Duration: " + "</color>" + GetStatusDuration + "s" + "\n\n" + "Power: " + Potency + "\n" + 
                                          "MP Cost: " + ManaCost + "\n\n" + "Cast Time: Instant" + "\n" + 
                                          "Cooldown: " + CoolDown + "s";
                }
            }
        }
        if(CastTime <= 0 && ManaCost <= 0 && CoolDown <= 0 && Potency <= 0)
        {
            if (GetPlayerStatusEffect == EffectStatus.NONE && GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillPanelText.text = SkillDescription + "\n\n" + "Cast Time: Instant";
            }
            else
            {
                SkillPanelText.text = SkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + GetStatusEffectName + "\n" + "<#EFDFB8>" +
                                      "Status Duration: " + "</color> Infinite" + "\n\n" + "Cast Time: Instant";
            }   
        }
    }

    public void ShowCoolDownText()
    {
        if(CoolDownImage.fillAmount > 0)
        {
            CoolDownText.enabled = true;
        }
        else
        {
            CoolDownText.enabled = false;
        }
    }

    public void SetSkillIndex()
    {
        SkillsManager.Instance.GetKeyInput = index;
    }
}