using UnityEngine;
using UnityEngine.UI;

public enum Skill { FungiBump, HealingCap, PoisonMist };

public class EnemySkills : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private EnemySkillBar skillBar;

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
        RandomValue = Random.Range(0, skill.Length);
    }

    public void ChooseSkill(int value)
    {
        if(!ActiveSkill)
        {
            ActiveSkill = true;
            switch (skill[RandomValue])
            {
                case (Skill.FungiBump):
                    FungiBump(15, 3.5f, "Fungi Bump");
                    break;
                case (Skill.HealingCap):
                    HealingCap(15, 3, "Healing Cap");
                    break;
                case (Skill.PoisonMist):
                    PoisonMist(15, 4, 1f, "Poison Mist");
                    break;
            }
        }
    }

    public void FungiBump(int potency, float attackRange, string skillname)
    {
        TextHolder = character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Health>().GetDamageTextParent.transform;
        if (character.GetComponent<EnemyAI>().GetPlayerTarget != null)
        {
            if(Vector3.Distance(character.transform.position, character.GetComponent<EnemyAI>().GetPlayerTarget.transform.position) <= attackRange)
            {
                SkillDamageText(potency, skillname);

                character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Health>().ModifyHealth
                                                 (-potency - -character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Character>().CharacterDefense);

                character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
            }
        }
        character.GetComponent<EnemyAI>().GetAutoAttack = 0;
        character.GetComponent<EnemyAI>().GetStates = States.Attack;
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

    public void PoisonMist(int potency, float castTime, float radius, string skillname)
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
            Invoke("InvokePoisonMist", 0.3f);
        }
    }

    private void InvokeHealingCap()
    {
        TextHolder = character.GetComponent<EnemyHealth>().GetDamageTextHolder.transform;

        SkillHealText(Potency, SkillName);

        character.GetComponent<EnemyHealth>().ModifyHealth(Potency + character.CharacterIntelligence);

        ActiveSkill = false;
    }

    private void InvokePoisonMist()
    {
        character.GetComponentInChildren<DamageRadius>().TakeDamage(character.GetComponentInChildren<DamageRadius>().GetDamageShape.transform.position, Radius + 1);

        DisableRadius();

        ActiveSkill = false;
    }

    private void UseSkillBar()
    {
        skillBar.gameObject.SetActive(true);
        if(character.GetComponent<EnemyAI>().GetPlayerTarget != null)
        {
            if (character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<BasicAttack>().GetTarget != null)
            {
                EnableEnemySkillBar();
            }
            else
            {
                DisableEnemySkillBar();
            }
        }
    }

    private void SpellCastingAnimation()
    {
        character.GetComponent<EnemyAI>().GetAnimation.CastingAni();
    }

    public void DisableEnemySkillBar()
    {
        if(skillBar.gameObject.activeInHierarchy)
        {
            foreach(Image image in skillBar.GetComponentsInChildren<Image>())
            {
                image.enabled = false;
            }
            skillBar.GetComponentInChildren<Text>().enabled = false;
        }
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
        foreach (DamageRadius r in character.GetComponentsInChildren<DamageRadius>())
        {
            Debug.Log("Disabled!");
            r.gameObject.GetComponent<Image>().enabled = false;
        }
    }

    public void EnableRadiusImage()
    {
        foreach (DamageRadius r in character.GetComponentsInChildren<DamageRadius>())
        {
            r.gameObject.GetComponent<Image>().enabled = true;
        }
    }

    public void DisableRadius()
    {
        foreach (DamageRadius r in character.GetComponentsInChildren<DamageRadius>())
        {
            r.ResetLocalScale();
            r.GetRadius = 0;
            r.enabled = false;
        }
    }

    public void EnableRadius()
    {
        foreach (DamageRadius r in character.GetComponentsInChildren<DamageRadius>())
        {
            r.enabled = true;

            r.GetRadius = Radius;

            r.GetShapes = Shapes.Circle;
        }
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
