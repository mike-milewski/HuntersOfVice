using UnityEngine;
using UnityEngine.UI;

public enum Skill { //MushroomMan Skills
                    FungiBump, HealingCap, PoisonSpore,
                    Regen, Regen2 };

public enum Status { NONE, DamageOverTime, HealthRegen, Stun, Sleep };

[System.Serializable]
public class enemySkillManager
{
    [SerializeField]
    private Skill skills;

    [SerializeField]
    private Status status;

    [SerializeField] [Tooltip("Image of the status effect inflicted. Only apply if the skill will have a status effect.")]
    private Sprite StatusSprite = null;

    [SerializeField]
    private Image StatusIcon = null;

    [SerializeField]
    private Transform TextHolder;

    [SerializeField]
    private Transform StatusIconTrans = null;
    
    [SerializeField] [Tooltip("Text holder representing heal or damage.")]
    private GameObject DamageORHealText = null;

    [SerializeField] [Tooltip("The gameobject that will hold the status effect text.")]
    private GameObject StatusEffectHolder = null;

    [SerializeField]
    private string StatusEffectName;

    [SerializeField]
    private string StatusDescription;

    [SerializeField]
    private string SkillName;

    [SerializeField]
    private float StatusDuration;

    [SerializeField]
    private float CastTime;

    [SerializeField]
    private float Radius;

    [SerializeField]
    public float AttackRange;

    [SerializeField][Tooltip("The amount of recovery or damage a target takes based on a regenerating or damage over time status.")]
    private int StatusEffectPotency;

    [SerializeField]
    private int Potency;

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

    public float GetCastTime
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

    public float GetRadius
    {
        get
        {
            return Radius;
        }
        set
        {
            Radius = value;
        }
    }

    public float GetAttackRange
    {
        get
        {
            return AttackRange;
        }
        set
        {
            AttackRange = value;
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

    public int GetStatusEffectPotency
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
    
    public string GetStatusEffectName
    {
        get
        {
            return StatusEffectName;
        }
        set
        {
            StatusEffectName = value;
        }
    }

    public Skill GetSkills
    {
        get
        {
            return skills;
        }
        set
        {
            skills = value;
        }
    }

    public Status GetStatus
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

    public string GetStatusDescription
    {
        get
        {
            return StatusDescription;
        }
        set
        {
            StatusDescription = value;
        }
    }

    public float GetStatusDuration
    {
        get
        {
            return StatusDuration;
        }
        set
        {
            StatusDuration = value;
        }
    }

    public Sprite GetStatusSprite
    {
        get
        {
            return StatusSprite;
        }
        set
        {
            StatusSprite = value;
        }
    }

    public Image GetStatusIcon
    {
        get
        {
            return StatusIcon;
        }
        set
        {
            StatusIcon = value;
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

    public GameObject GetStatusEffectHolder
    {
        get
        {
            return StatusEffectHolder;
        }
        set
        {
            StatusEffectHolder = value;
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

    public Transform GetStatusIconTrans
    {
        get
        {
            return StatusIconTrans;
        }
        set
        {
            StatusIconTrans = value;
        }
    }
}

public class EnemySkills : MonoBehaviour
{
    [SerializeField]
    private enemySkillManager[] skills;

    [SerializeField]
    private Character character;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private EnemyAI enemyAI;

    [SerializeField]
    private EnemySkillBar skillBar;

    [SerializeField]
    private DamageRadius damageRadius;

    [SerializeField]
    private Health health;

    private int RandomValue;

    [SerializeField]
    private bool ActiveSkill, DisruptedSkill;

    public enemySkillManager[] GetManager
    {
        get
        {
            return skills;
        }
        set
        {
            skills = value;
        }
    }

    public EnemySkillBar GetSkillBar
    {
        get
        {
            return skillBar;
        }
        set
        {
            skillBar = value;
        }
    }

    public int GetRandomValue
    {
        get
        {
            return RandomValue;
        }
        set
        {
            RandomValue = value;

        }
    }

    public bool GetActiveSkill
    {
        get
        {
            return ActiveSkill;
        }
        set
        {
            ActiveSkill = value;
        }
    }

    public bool GetDisruptedSkill
    {
        get
        {
            return DisruptedSkill;
        }
        set
        {
            DisruptedSkill = value;
        }
    }

    public void GenerateValue()
    {
        RandomValue = Random.Range(0, skills.Length);
    }

    public void ChooseSkill(int value)
    {
        if(!ActiveSkill)
        {
            ActiveSkill = true;
            switch (skills[RandomValue].GetSkills)
            {
                #region Mushroom Man Skills
                case (Skill.FungiBump):
                    FungiBump(GetManager[RandomValue].GetPotency, GetManager[RandomValue].GetAttackRange, GetManager[RandomValue].GetSkillName);
                    break;
                case (Skill.HealingCap):
                    HealingCap(GetManager[RandomValue].GetPotency, GetManager[RandomValue].GetCastTime, GetManager[RandomValue].GetSkillName);
                    break;
                case (Skill.PoisonSpore):
                    PoisonSpore(GetManager[RandomValue].GetPotency, GetManager[RandomValue].GetCastTime, GetManager[RandomValue].GetRadius, GetManager[RandomValue].GetSkillName);
                    break;
                case (Skill.Regen):
                    Regen(GetManager[RandomValue].GetCastTime, GetManager[RandomValue].GetStatusDuration, GetManager[RandomValue].GetSkillName);
                    break;
                case (Skill.Regen2):
                    Regen2(GetManager[RandomValue].GetCastTime, GetManager[RandomValue].GetStatusDuration, GetManager[RandomValue].GetSkillName);
                    break;
                    #endregion
            }
        }
    }

    public void Regen(float castTime, float Duration, string skillname)
    {
        SpellCastingAnimation();

        skills[RandomValue].GetCastTime = castTime;

        skills[RandomValue].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            Invoke("InvokeRegen", 0.2f);
        }
    }

    public void Regen2(float castTime, float Duration, string skillname)
    {
        SpellCastingAnimation();

        skills[RandomValue].GetCastTime = castTime;

        skills[RandomValue].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            Invoke("InvokeRegen", 0.2f);
        }
    }

    public void InvokeRegen()
    {
        StatusEffectSkillTextTransform();

        ActiveSkill = false;
    }

    public void FungiBump(int potency, float attackRange, string skillname)
    {
        skills[RandomValue].GetPotency = potency;
        skills[RandomValue].GetSkillName = skillname;

        if (enemyAI.GetPlayerTarget != null)
        {
            if(Vector3.Distance(character.transform.position, enemyAI.GetPlayerTarget.transform.position) <= attackRange)
            {
                FungiBumpAnimation();
            }
        }
        ActiveSkill = false;
    }

    public void HealingCap(int potency, float castTime, string skillname)
    {
        SpellCastingAnimation();

        skills[RandomValue].GetCastTime = castTime;

        skills[RandomValue].GetPotency = potency;

        skills[RandomValue].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        if(skillBar.GetFillImage.fillAmount >= 1)
        {
            Invoke("InvokeHealingCap", 0.2f);
        }  
    }

    private void InvokeHealingCap()
    {
        SkillHealText(skills[RandomValue].GetPotency, skills[RandomValue].GetSkillName);

        health.IncreaseHealth(skills[RandomValue].GetPotency + character.CharacterIntelligence);
        health.GetTakingDamage = false;

        enemy.GetLocalHealthInfo();

        ActiveSkill = false;
    }

    public void PoisonSpore(int potency, float castTime, float radius, string skillname)
    {
        SpellCastingAnimation();

        skills[RandomValue].GetCastTime = castTime;

        skills[RandomValue].GetPotency = potency;

        skills[RandomValue].GetRadius = radius;

        skills[RandomValue].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        EnableRadius();
        EnableRadiusImage();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            DisableRadiusImage();
            Invoke("InvokePoisonSpore2", 0.3f);
        }
    }

    private void InvokePoisonSpore()
    {
        damageRadius.TakeDamageSphereRadius(damageRadius.GetDamageShape.transform.position, skills[RandomValue].GetRadius + 1);

        DisableRadius();

        ActiveSkill = false;
    }

    private void InvokePoisonSpore2()
    {
        damageRadius.TakeDamageRectangleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.GetDamageShape.transform.localScale);

        DisableRadius();

        ActiveSkill = false;
    }

    private void UseSkillBar()
    {
        skillBar.gameObject.SetActive(true);
    }

    private void SpellCastingAnimation()
    {
        enemyAI.GetAnimation.CastingAni();
    }

    private void FungiBumpAnimation()
    {
        enemyAI.GetAnimation.FungiBumpAnim();
    }

    public void DisableEnemySkillBar()
    {
        foreach (Image image in skillBar.GetComponentsInChildren<Image>())
        {
            image.enabled = false;
        }
        skillBar.GetComponentInChildren<Text>().enabled = false;
    }

    public void EnableEnemySkillBar()
    {
        foreach (Image image in skillBar.GetComponentsInChildren<Image>())
        {
            image.enabled = true;
        }
        skillBar.GetComponentInChildren<Text>().enabled = true;
    }

    public void DisableRadiusImage()
    {
        foreach (Image r in damageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = false;
        }
    }

    public void EnableRadiusImage()
    {
        foreach (Image r in damageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = true;
        }
    }

    public void DisableRadius()
    {
        damageRadius.ResetLocalScale();
        damageRadius.GetRadius = 0;
        damageRadius.enabled = false;
    }

    public void EnableRadius()
    {
        damageRadius.enabled = true;

        damageRadius.GetRadius = 1;//skills[RandomValue].GetRadius;

        damageRadius.GetShapes = Shapes.Rectangle;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(character.GetComponentInChildren<DamageRadius>().GetDamageShape.transform.position,
            character.GetComponentInChildren<DamageRadius>().GetDamageShape.transform.localScale * 2);
    }
    */

    public int TakeDamage(int potency, string skillname)
    {
        SkillDamageText(potency, skillname);

        character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Health>().ModifyHealth
                                         (-potency - -character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Character>().CharacterDefense);

        character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
        character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Health>().GetTakingDamage = true;

        return potency;
    }

    public Text StatusEffectSkillTextTransform()
    {
        var SkillObj = Instantiate(GetManager[RandomValue].GetStatusEffectHolder);

        SkillObj.transform.SetParent(GetManager[RandomValue].GetTextHolder.transform, false);

        SkillObj.GetComponentInChildren<Text>().text = "+" + GetManager[RandomValue].GetStatusEffectName;

        var StatIcon = Instantiate(GetManager[RandomValue].GetStatusIcon);

        SkillObj.GetComponentInChildren<Image>().sprite = GetManager[RandomValue].GetStatusSprite;

        StatIcon.sprite = GetManager[RandomValue].GetStatusSprite;

        StatIcon.transform.SetParent(GetManager[RandomValue].GetStatusIconTrans.transform, false);

        if(StatIcon.GetComponent<StatusIcon>())
        {
            StatIcon.GetComponent<StatusIcon>().GetEffectStatus = GetManager[RandomValue].GetStatus;
            StatIcon.GetComponent<StatusIcon>().GetEnemyTarget = enemy;
            StatIcon.GetComponent<StatusIcon>().EnemyInput();
        }
        return SkillObj.GetComponentInChildren<Text>();
    }

    public Text SkillDamageText(int potency, string skillName)
    {
        skills[RandomValue].GetSkillName = skillName;

        var SkillObj = Instantiate(GetManager[RandomValue].GetDamageOrHealText);

        var Target = character.GetComponent<EnemyAI>().GetPlayerTarget;

        SkillObj.transform.SetParent(GetManager[RandomValue].GetTextHolder.transform, false);

        SkillObj.GetComponentInChildren<Text>().text = skills[RandomValue].GetSkillName + " " + (potency - Target.GetComponent<Character>().CharacterDefense).ToString();

        return SkillObj.GetComponentInChildren<Text>();
    }

    public Text SkillHealText(int potency, string skillName)
    {
        skills[RandomValue].GetSkillName = skillName;

        var SkillObj = Instantiate(GetManager[RandomValue].GetDamageOrHealText);

        SkillObj.GetComponentInChildren<Text>().transform.SetParent(GetManager[RandomValue].GetTextHolder.transform, false);

        SkillObj.GetComponentInChildren<Text>().text = skills[RandomValue].GetSkillName + " " + (potency + character.CharacterIntelligence).ToString();

        return SkillObj.GetComponentInChildren<Text>();
    }
}