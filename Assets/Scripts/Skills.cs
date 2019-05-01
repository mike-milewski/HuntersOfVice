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

    [SerializeField] [Tooltip("The transform that holds the damage/heal/buff text values. Keep this empty for damage type skills!")]
    private Transform TextHolder = null;

    [SerializeField]
    private Text SkillTextObject, SkillPanelText;

    [SerializeField]
    private ParticleSystem SkillParticle;

    [SerializeField]
    private float CoolDown, AttackRange, ApplySkill;

    [SerializeField]
    private int ManaCost, Potency;
    
    [SerializeField] [Tooltip("Skills that have a cast time greater than 0 will activate the skill casting bar.")]
    private int CastTime;

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

    public Character GetCharacter
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
        }
    }

    private void Update()
    {
        CheckCoolDownStatus();
    }

    private void CheckCoolDownStatus()
    {
        if (this.button.GetComponent<Image>().fillAmount >= 1 && character.CurrentHealth > 0 && character.CurrentMana >= ManaCost && !SkillsManager.Instance.GetActivatedSkill)
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

            var HealParticle = Instantiate(SkillParticle, new Vector3(character.transform.position.x, character.transform.position.y + 1.0f, character.transform.position.z),
                                           Quaternion.identity);

            HealParticle.transform.SetParent(character.transform, true);

            character.GetComponent<Mana>().ModifyMana(-ManaCost);

            Invoke("InvokeHealthRestore", ApplySkill);
        }
    }

    private void InvokeHealthRestore()
    {
        character.GetComponent<Health>().ModifyHealth(Potency + character.CharacterIntelligence);

        HealSkillText();
    }

    public void TestDamageSkill()
    {
        if (character.GetComponent<BasicAttack>().GetTarget != null)
        {
            TextHolder = character.GetComponent<BasicAttack>().GetTarget.GetComponentInChildren<NoRotationHealthBar>().transform;
            if(Vector3.Distance(character.transform.position, character.GetComponent<BasicAttack>().GetTarget.transform.position) <= AttackRange)
            {
                SkillsManager.Instance.GetActivatedSkill = true;

                character.GetComponent<PlayerAnimations>().PlaySkillAnimation();

                this.button.GetComponent<Image>().fillAmount = 0;

                character.GetComponent<Mana>().ModifyMana(-ManaCost);

                var Target = character.GetComponent<BasicAttack>().GetTarget;

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

    //Place this on an animation as an animation event.
    //This way damage will be dealt during a specific portion of the attack animation.
    //If used for spell damage, invoke this function.
    public void SkillDamage()
    {
        var Target = character.GetComponent<BasicAttack>().GetTarget;

        Target.GetComponent<EnemyHealth>().ModifyHealth(-Potency - -Target.GetComponent<Character>().CharacterDefense);

        Target.GetComponent<EnemyAI>().GetStates = States.Damaged;

        SkillsManager.Instance.GetActivatedSkill = false;

        DamageSkillText();
    }

    private Text HealSkillText()
    {
        var SkillObj = Instantiate(SkillTextObject);

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

        if(CastTime <= 0)
        {
            SkillPanelText.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Mana: " + ManaCost + "\n" + "Potency: " + Potency + "\n" + "Cooldown: " + CoolDown;
        }
        else
        {
            SkillPanelText.text = SkillName + "\n\n" + SkillDescription + "\n\n" + "Mana: " + ManaCost + "\n" + "Potency: " + Potency + "\n" + "Cooldown: " + CoolDown
                                    + "\n" + "Cast Time: " + CastTime;
        }
    }
}