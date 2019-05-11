using UnityEngine;
using UnityEngine.UI;

public enum Skill { FungiBump, HealingCap, PoisonSpore };

public class EnemySkills : MonoBehaviour
{
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

    [SerializeField]
    private Transform TextHolder = null;

    [SerializeField]
    private Text DamageTextObject, HealTextObject;

    [SerializeField]
    private string SkillName;

    private float CastTime;

    private float Radius;

    private int Potency;

    private int RandomValue;

    [SerializeField]
    private bool ActiveSkill;

    public Skill[] skill;

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

    public Skill[] GetSkill
    {
        get
        {
            return skill;
        }
        set
        {
            skill = value;
        }
    }

    public void GenerateValue()
    {
        RandomValue = 1;//Random.Range(0, skill.Length);
    }

    public void ChooseSkill(int value)
    {
        if(!ActiveSkill)
        {
            ActiveSkill = true;
            switch (skill[RandomValue])
            {
                case (Skill.FungiBump):
                    FungiBump(10, 3.5f, "Fungi Bump");
                    break;
                case (Skill.HealingCap):
                    HealingCap(15, 3, "Healing Cap");
                    break;
                case (Skill.PoisonSpore):
                    PoisonSpore(0, 4, 1f, "Poison Spore");
                    break;
            }
        }
    }

    public void FungiBump(int potency, float attackRange, string skillname)
    {
        Potency = potency;
        SkillName = skillname;

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

        CastTime = castTime;

        Potency = potency;

        SkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        if(skillBar.GetFillImage.fillAmount >= 1)
        {
            Invoke("InvokeHealingCap", 0.2f);
        }  
    }

    public void PoisonSpore(int potency, float castTime, float radius, string skillname)
    {
        SpellCastingAnimation();

        CastTime = castTime;

        Potency = potency;

        Radius = radius;

        SkillName = skillname;

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

    private void InvokeHealingCap()
    {
        TextHolder = health.GetDamageTextParent.transform;

        SkillHealText(Potency, SkillName);

        health.IncreaseHealth(Potency + character.CharacterIntelligence);
        health.GetTakingDamage = false;

        enemy.GetLocalHealthInfo();

        ActiveSkill = false;
    }

    private void InvokePoisonSpore()
    {
        damageRadius.TakeDamageSphereRadius(damageRadius.GetDamageShape.transform.position, Radius + 1);

        DisableRadius();

        ActiveSkill = false;
    }

    private void InvokePoisonMist2()
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

        damageRadius.GetRadius = Radius;

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

    public Text SkillDamageText(int potency, string skillName)
    {
        SkillName = skillName;

        var SkillObj = Instantiate(DamageTextObject);

        var Target = character.GetComponent<EnemyAI>().GetPlayerTarget;

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = SkillName + " " + (potency - Target.GetComponent<Character>().CharacterDefense).ToString();

        return SkillObj;
    }

    public Text SkillHealText(int potency, string skillName)
    {
        SkillName = skillName;

        var SkillObj = Instantiate(HealTextObject);

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = SkillName + " " + (potency + character.CharacterIntelligence).ToString();

        return SkillObj;
    }
}
