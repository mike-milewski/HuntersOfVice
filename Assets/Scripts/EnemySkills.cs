using UnityEngine;
using UnityEngine.UI;

public enum Skill { //MushroomMan Skills
                    FungiBump, HealingCap, PoisonSpore,
                    Regen};

[System.Serializable]
public class enemySkillManager
{
    [SerializeField]
    private Skill skills;

    [SerializeField] [Tooltip("Image of the status effect inflicted. Only apply if the skill will have a status effect.")]
    private Image StatusIcon = null;

    [SerializeField]
    private Transform BuffIconTrans = null;
    
    [SerializeField]
    private Transform DebuffIconTrans = null;

    [SerializeField]
    private Text SkillTextObject;

    [SerializeField]
    private string StatusEffectName;

    private string SkillName;

    private float CastTime;

    private float Radius;

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

    public Transform GetBuffIconTrans
    {
        get
        {
            return BuffIconTrans;
        }
        set
        {
            BuffIconTrans = value;
        }
    }

    public Transform GetDebuffIconTrans
    {
        get
        {
            return DebuffIconTrans;
        }
        set
        {
            DebuffIconTrans = value;
        }
    }
}

public class EnemySkills : MonoBehaviour
{
    [SerializeField]
    private enemySkillManager[] skills;

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

    [SerializeField] [Tooltip("The transform that holds the damage/heal/status effect text values. Keep this empty for damage type skills!")]
    private Transform TextHolder = null;

    private int RandomValue;

    [SerializeField]
    private bool ActiveSkill;

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
                #region MushroomMan Skills
                case (Skill.FungiBump):
                    FungiBump(10, 3.5f, "Fungi Bump");
                    break;
                case (Skill.HealingCap):
                    HealingCap(15, 3, "Healing Cap");
                    break;
                case (Skill.PoisonSpore):
                    PoisonSpore(10, 4, 1f, "Poison Spore");
                    break;
                case (Skill.Regen):
                    Regen(5f, "Regen");
                    break;
                #endregion
            }
        }
    }

    public void Regen(float castTime, string skillname)
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

    }

    public void FungiBump(int potency, float attackRange, string skillname)
    {
        skills[RandomValue].GetPotency = potency;
        skills[RandomValue].GetSkillName = skillname;

        TextHolder = enemyAI.GetPlayerTarget.GetComponent<Health>().GetDamageTextParent.transform;
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
        TextHolder = health.GetDamageTextParent.transform;

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
            Invoke("InvokePoisonSpore", 0.3f);
        }
    }

    private void InvokePoisonSpore()
    {
        damageRadius.TakeDamageSphereRadius(damageRadius.GetDamageShape.transform.position, skills[RandomValue].GetRadius + 1);

        DisableRadius();

        ActiveSkill = false;
    }
    /*
    private void InvokePoisonMist2()
    {
        damageRadius.TakeDamageRectangleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.GetDamageShape.transform.localScale);

        DisableRadius();

        ActiveSkill = false;
    }
    */

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

        damageRadius.GetRadius = skills[RandomValue].GetRadius;

        damageRadius.GetShapes = Shapes.Circle;
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

    public Text BuffStatusEffectSkillText()
    {
        var SkillObj = Instantiate(GetManager[RandomValue].GetSkillTextObject);

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = "+" + GetManager[RandomValue].GetStatusEffectName;

        var StatusIcon = Instantiate(GetManager[RandomValue].GetStatusIcon);

        StatusIcon.transform.SetParent(GetManager[RandomValue].GetBuffIconTrans.transform, false);

        SkillObj.GetComponentInChildren<Image>().sprite = StatusIcon.sprite;

        return SkillObj;
    }

    public Text DebuffStatusEffectSkillText()
    {
        var SkillObj = Instantiate(GetManager[RandomValue].GetSkillTextObject);

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = "+" + GetManager[RandomValue].GetStatusEffectName;

        var StatusIcon = Instantiate(GetManager[RandomValue].GetStatusIcon);

        StatusIcon.transform.SetParent(GetManager[RandomValue].GetDebuffIconTrans.transform, false);

        SkillObj.GetComponentInChildren<Image>().sprite = StatusIcon.sprite;

        return SkillObj;
    }

    public Text SkillDamageText(int potency, string skillName)
    {
        skills[RandomValue].GetSkillName = skillName;

        var SkillObj = Instantiate(GetManager[RandomValue].GetSkillTextObject);

        var Target = character.GetComponent<EnemyAI>().GetPlayerTarget;

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = skills[RandomValue].GetSkillName + " " + (potency - Target.GetComponent<Character>().CharacterDefense).ToString();

        return SkillObj;
    }

    public Text SkillHealText(int potency, string skillName)
    {
        skills[RandomValue].GetSkillName = skillName;

        var SkillObj = Instantiate(GetManager[RandomValue].GetSkillTextObject);

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = skills[RandomValue].GetSkillName + " " + (potency + character.CharacterIntelligence).ToString();

        return SkillObj;
    }
}
