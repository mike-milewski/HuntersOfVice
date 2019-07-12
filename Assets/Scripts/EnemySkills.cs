using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Skill { //MushroomMan Skills
                    FungiBump, HealingCap, PoisonSpore,
                    Regen };

public enum Status { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste, Doom };

public enum EnemyElement { NONE, Fire, Water, Wind, Earth, Light, Dark };

[System.Serializable]
public class enemySkillManager
{
    [SerializeField]
    private Skill skills;

    [SerializeField]
    private EnemyElement enemyElement;

    [SerializeField]
    private Status status;

    [SerializeField] [Tooltip("The shape the skill will form when being cast. Enemy targets within its range will be hit.")]
    private Shapes shapes;

    [SerializeField] [Tooltip("Image of the status effect inflicted. Only apply if the skill will have a status effect.")]
    private Sprite StatusSprite = null;

    [SerializeField]
    private GameObject StatusIcon = null;

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

    [SerializeField] [Tooltip("The values used for the shape size if its set to CIRCLE")]
    private float SizeDeltaX, SizeDeltaY;

    [SerializeField]
    private Vector3 ShapeSize;

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

    public float GetSizeDeltaX
    {
        get
        {
            return SizeDeltaX;
        }
        set
        {
            SizeDeltaX = value;
        }
    }

    public float GetSizeDeltaY
    {
        get
        {
            return SizeDeltaY;
        }
        set
        {
            SizeDeltaY = value;
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

    public EnemyElement GetEnemyElement
    {
        get
        {
            return enemyElement;
        }
        set
        {
            enemyElement = value;
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

    public Shapes GetShapes
    {
        get
        {
            return shapes;
        }
        set
        {
            shapes = value;
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

    public Vector3 GetShapeSize
    {
        get
        {
            return ShapeSize;
        }
        set
        {
            ShapeSize = value;
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

    public GameObject GetStatusIcon
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

    [Tooltip("The amount of time it takes before the skill is applied.")]
    private float ApplySkill;

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
                    PoisonSpore(GetManager[RandomValue].GetPotency, GetManager[RandomValue].GetCastTime, 
                        new Vector2(GetManager[RandomValue].GetSizeDeltaX, GetManager[RandomValue].GetSizeDeltaY), GetManager[RandomValue].GetSkillName);
                    break;
                case (Skill.Regen):
                    Regen(GetManager[RandomValue].GetCastTime, GetManager[RandomValue].GetStatusDuration, GetManager[RandomValue].GetSkillName);
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
            Invoke("InvokeRegen", ApplySkill);
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

        float Distance = Vector3.Distance(character.transform.position, enemyAI.GetPlayerTarget.transform.position);

        if (enemyAI.GetPlayerTarget != null)
        {
            if(Distance <= attackRange)
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
            Invoke("SkillHealText", ApplySkill);
        }  
    }

    private void InvokeHealingCap()
    {
        SkillHealText(skills[RandomValue].GetPotency, skills[RandomValue].GetSkillName);
    }

    public void PoisonSpore(int potency, float castTime, Vector2 sizeDelta, string skillname)
    {
        SpellCastingAnimation();

        skills[RandomValue].GetCastTime = castTime;

        skills[RandomValue].GetPotency = potency;

        sizeDelta = new Vector2(skills[RandomValue].GetSizeDeltaX, skills[RandomValue].GetSizeDeltaY);

        skills[RandomValue].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        EnableRadius();
        EnableRadiusImage();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            MushroomSporeAnimation();
            DisableRadiusImage();
            Invoke("InvokePoisonSpore", ApplySkill);
        }
    }

    private void InvokePoisonSpore()
    {
        switch(skills[RandomValue].GetShapes)
        {
            case (Shapes.Circle):
                damageRadius.TakeDamageSphereRadius(damageRadius.GetDamageShape.transform.position, damageRadius.SetCircleColliderSize());
                break;
            case (Shapes.Rectangle):
                damageRadius.TakeDamageRectangleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.SetRectangleColliderSize() / 2);
                break;
        }
        DisableRadius();

        ActiveSkill = false;
    }

    private void UseSkillBar()
    {
        skillBar.gameObject.SetActive(true);
        skillBar.ToggleCastBar();
    }

    private void SpellCastingAnimation()
    {
        enemyAI.GetAnimation.CastingAni();
    }

    private void FungiBumpAnimation()
    {
        enemyAI.GetAnimation.FungiBumpAnim();
    }

    private void MushroomSporeAnimation()
    {
        enemyAI.GetAnimation.SkillAtk2();
    }

    public void DisableEnemySkillBar()
    {
        foreach (Image image in skillBar.GetComponentsInChildren<Image>())
        {
            image.enabled = false;
        }
        skillBar.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    public void EnableEnemySkillBar()
    {
        foreach (Image image in skillBar.GetComponentsInChildren<Image>())
        {
            image.enabled = true;
        }
        skillBar.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
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
        damageRadius.ResetSizeDelta();
        damageRadius.ResetLocalScale();
        damageRadius.enabled = false;
    }

    public void EnableRadius()
    {
        damageRadius.enabled = true;
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(character.GetComponentInChildren<DamageRadius>().GetDamageShape.transform.position,
                        character.GetComponentInChildren<DamageRadius>().SetRectangleColliderSize());
    }
    */

    public TextMeshProUGUI EnemyStatus()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(GetManager[RandomValue].GetTextHolder.transform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4> +" + GetManager[RandomValue].GetStatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = GetManager[RandomValue].GetStatusSprite;

        StatusEffectSkillTextTransform();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI PlayerStatus()
    {
        var StatusEffectText = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(GetManager[RandomValue].GetTextHolder.transform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4> +" + GetManager[RandomValue].GetStatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = GetManager[RandomValue].GetStatusSprite;

        StatusEffectSkillTextTransform();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StatusEffectSkillTextTransform()
    {
        if(!GetManager[RandomValue].GetStatusIcon.activeInHierarchy)
        {
            if (GetManager[RandomValue].GetStatusIcon.GetComponent<StatusIcon>())
            {
                GetManager[RandomValue].GetStatusIcon = ObjectPooler.Instance.GetPlayerStatusIcon();

                GetManager[RandomValue].GetStatusIcon.SetActive(true);

                GetManager[RandomValue].GetStatusIcon.transform.SetParent(this.GetManager[RandomValue].GetStatusIconTrans.transform, false);

                GetManager[RandomValue].GetStatusIcon.GetComponent<Image>().sprite = this.GetManager[RandomValue].GetStatusSprite;

                GetManager[RandomValue].GetStatusIcon.GetComponent<StatusIcon>().GetEffectStatus = (EffectStatus)this.GetManager[RandomValue].GetStatus;
                GetManager[RandomValue].GetStatusIcon.GetComponent<StatusIcon>().GetEnemyTarget = enemy;
                GetManager[RandomValue].GetStatusIcon.GetComponent<StatusIcon>().EnemyInput();
            }
            else
            {
                GetManager[RandomValue].GetStatusIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

                GetManager[RandomValue].GetStatusIcon.SetActive(true);

                GetManager[RandomValue].GetStatusIcon.transform.SetParent(this.GetManager[RandomValue].GetStatusIconTrans.transform, false);

                GetManager[RandomValue].GetStatusIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = (StatusEffect)this.GetManager[RandomValue].GetStatus;

                GetManager[RandomValue].GetStatusIcon.GetComponent<Image>().sprite = this.GetManager[RandomValue].GetStatusSprite;
            }
        }
        else
        {
            if (GetManager[RandomValue].GetStatusIcon.GetComponent<StatusIcon>())
            {
                GetManager[RandomValue].GetStatusIcon.GetComponent<StatusIcon>().EnemyInput();
            }
            else
            {
                GetManager[RandomValue].GetStatusIcon.GetComponent<StatusIcon>().EnemyInput();
            }
        }
    }

    public TextMeshProUGUI SkillDamageText(int potency, string skillName)
    {
        if(enemyAI.GetPlayerTarget == null)
        {
            return null;
        }

        skills[RandomValue].GetSkillName = skillName;

        var DamageTxt = ObjectPooler.Instance.GetPlayerDamageText();

        DamageTxt.SetActive(true);

        DamageTxt.transform.SetParent(GetManager[RandomValue].GetTextHolder.transform, false);

        var Target = enemyAI.GetPlayerTarget;

        float Critical = character.GetCriticalChance;

        #region CriticalHitCalculation
        if (Random.value * 100 <= Critical)
        {
            enemyAI.GetPlayerTarget.GetComponent<Health>().ModifyHealth
                                                         (-(potency + 5 - Target.GetComponent<Character>().CharacterDefense));

            DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = skillName + " " + "<size=20>" + ((potency + 5) - 
                                                                       Target.GetComponent<Character>().CharacterDefense).ToString() + "!";
        }
        else
        {
            Target.GetComponentInChildren<Health>().ModifyHealth(-(potency - Target.GetComponent<Character>().CharacterDefense));

            DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + skillName + " " + (potency - Target.GetComponent<Character>().CharacterDefense).ToString();
        }
        #endregion

        enemyAI.GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();

        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI SkillHealText(int potency, string skillName)
    {
        skills[RandomValue].GetSkillName = skillName;

        var HealTxt = ObjectPooler.Instance.GetEnemyHealText();

        HealTxt.SetActive(true);

        var Critical = character.GetCriticalChance;

        ActiveSkill = false;

        #region CritChance
        if(character.CurrentHealth > 0)
        {
            if (Random.value * 100 <= Critical)
            {
                health.IncreaseHealth((skills[RandomValue].GetPotency + 10) + character.CharacterIntelligence);

                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = skills[RandomValue].GetSkillName + " " + "<size=20>" +
                                                                        (potency + character.CharacterIntelligence).ToString() + "!";

                enemy.GetLocalHealthInfo();
            }
            else
            {
                health.IncreaseHealth(skills[RandomValue].GetPotency + character.CharacterIntelligence);

                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + skills[RandomValue].GetSkillName + " " + 
                                                                         (potency + character.CharacterIntelligence).ToString();

                enemy.GetLocalHealthInfo();
            }
        }
        #endregion

        HealTxt.GetComponentInChildren<TextMeshProUGUI>().transform.SetParent(GetManager[RandomValue].GetTextHolder.transform, false);

        return HealTxt.GetComponentInChildren<TextMeshProUGUI>();
    }
}