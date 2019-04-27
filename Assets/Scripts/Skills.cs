using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private SkillBar skillbar;

    [SerializeField]
    private Button button;

    [SerializeField] [Tooltip("The transform that holds the damage/heal/buff text values. Keep this empty!")]
    private Transform TextHolder = null;

    [SerializeField]
    private Text SkillTextObject, SkillPanelObject;

    [SerializeField]
    private ParticleSystem SkillParticle;

    [SerializeField]
    private float CoolDown, AttackRange;

    [SerializeField]
    private int ManaCost, Potency, CastTime;

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

    private void Update()
    {
        BeginCoolDown();

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(this.button.GetComponent<Image>().fillAmount >= 1 && character.CurrentHealth > 0 && character.CurrentMana >= ManaCost)
            {
                TestHealSkill();
            }
            else if(character.CurrentMana <= ManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (this.button.GetComponent<Image>().fillAmount >= 1 && character.CurrentHealth > 0 && character.CurrentMana >= ManaCost)
            {
                TestDamageSkill();
            }
            else if (character.CurrentMana <= ManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
    }

    private void BeginCoolDown()
    {
        if (this.button.GetComponent<Image>().fillAmount >= 1 && character.CurrentHealth > 0 && character.CurrentMana >= ManaCost)
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
        if(skillbar.GetSkillBar.fillAmount < 1)
        {
            this.button.GetComponent<Image>().fillAmount = 0;

            skillbar.gameObject.SetActive(true);

            skillbar.GetSkill = this.button.GetComponent<Skills>();
        }

        if(skillbar.GetSkillBar.fillAmount >= 1)
        {
            character.GetComponent<Mana>().ModifyMana(-ManaCost);

            character.GetComponent<Health>().ModifyHealth(Potency + character.CharacterIntelligence);

            HealSkillText();
        }
    }

    public void TestDamageSkill()
    {
        if (character.GetComponent<BasicAttack>().GetTarget != null)
        {
            TextHolder = character.GetComponent<BasicAttack>().GetTarget.GetComponentInChildren<NoRotationHealthBar>().transform;
            if(Vector3.Distance(character.transform.position, character.GetComponent<BasicAttack>().GetTarget.transform.position) <= AttackRange)
            {
                this.button.GetComponent<Image>().fillAmount = 0;

                character.GetComponent<Mana>().ModifyMana(-ManaCost);

                var Target = character.GetComponent<BasicAttack>().GetTarget;
                /*
                var DamageParticle = Instantiate(SkillParticle, new Vector3(Target.transform.position.x, Target.transform.position.y + 1.0f, Target.transform.position.z),
                                       Quaternion.identity);

                DamageParticle.transform.SetParent(Target.transform, true);
                */

                Target.GetComponent<EnemyHealth>().ModifyHealth(-Potency - -Target.GetComponent<Character>().CharacterDefense);

                Target.GetComponent<EnemyAI>().GetStates = States.Damaged;

                DamageSkillText();
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

    private Text HealSkillText()
    {
        var SkillObj = Instantiate(SkillTextObject);
        /*
        var HealParticle = Instantiate(SkillParticle, new Vector3(character.transform.position.x, character.transform.position.y + 1.0f, character.transform.position.z),
                                       Quaternion.identity);

        HealParticle.transform.SetParent(character.transform, true);
        */

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = SkillName + " " + (Potency + character.CharacterIntelligence).ToString();

        return SkillObj;
    }

    private Text DamageSkillText()
    {
        var SkillObj = Instantiate(SkillTextObject);

        var Target = character.GetComponent<BasicAttack>();

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = SkillName + " " + (Potency - Target.GetTarget.GetComponent<Character>().CharacterDefense).ToString();

        return SkillObj;
    }

    public void ShowSkillPanel(GameObject Panel)
    {
        Panel.gameObject.SetActive(true);

        if(CastTime == 0)
        {
            SkillPanelObject.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Mana: " + ManaCost + "\n" + "Potency: " + Potency + "\n" + "Cooldown: " + CoolDown;
        }
        else
        {
            SkillPanelObject.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Mana: " + ManaCost + "\n" + "Potency: " + Potency + "\n" + "Cooldown: " + CoolDown
                                    + "\n" + "Cast Time: " + CastTime;
        }
    }
}