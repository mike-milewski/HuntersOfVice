using UnityEngine;
using UnityEngine.UI;

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

    private int Potency;

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
    }

    public void HealingCap(int potency, float castTime, string skillname)
    {
        SpellCastingAnimation();

        CastTime = castTime;

        Potency = potency;

        SkillName = skillname;

        UseSkillBar();

        Invoke("InvokeHealingCap", 0.5f);
    }

    private void InvokeHealingCap()
    {
        TextHolder = character.GetComponent<EnemyHealth>().GetDamageTextHolder.transform;

        SkillHealText(Potency, SkillName);

        character.GetComponent<EnemyHealth>().ModifyHealth(Potency + character.CharacterIntelligence);

        character.GetComponent<EnemyAI>().GetStates = States.Attack;
    }

    private void UseSkillBar()
    {
        skillBar.gameObject.SetActive(true);
    }

    private void SpellCastingAnimation()
    {
        character.GetComponent<MushroomMon_Ani_Test>().CastingAni();
    }

    private Text SkillDamageText(int potency, string skillName)
    {
        SkillName = skillName;

        var SkillObj = Instantiate(DamageTextObject);

        var Target = character.GetComponent<EnemyAI>().GetPlayerTarget;

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = SkillName + " " + (potency - Target.GetComponent<Character>().CharacterDefense).ToString();

        return SkillObj;
    }

    private Text SkillHealText(int potency, string skillName)
    {
        SkillName = skillName;

        var SkillObj = Instantiate(HealTextObject);

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = SkillName + " " + (potency + character.CharacterIntelligence).ToString();

        return SkillObj;
    }
}
