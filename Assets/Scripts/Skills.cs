using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Button button;

    [SerializeField]
    private Transform TextHolder;

    [SerializeField]
    private Text SkillTextObject, SkillPanelObject;

    [SerializeField]
    private float CoolDown, AttackRange;

    [SerializeField]
    private int ManaCost;

    [SerializeField]
    private string SkillName;

    [SerializeField] [TextArea]
    private string SkillDescription;

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
        this.button.GetComponent<Image>().fillAmount = 0;

        character.GetComponent<Mana>().ModifyMana(-ManaCost);

        character.GetComponent<Health>().ModifyHealth(Potency);

        HealSkillText();
    }

    public void TestDamageSkill()
    {
        if (character.GetComponent<BasicAttack>().GetTarget != null)
        {
            if(Vector3.Distance(character.transform.position, character.GetComponent<BasicAttack>().GetTarget.transform.position) <= AttackRange)
            {
                this.button.GetComponent<Image>().fillAmount = 0;

                character.GetComponent<Mana>().ModifyMana(-ManaCost);

                var Target = character.GetComponent<BasicAttack>().GetTarget;

                Target.GetComponent<EnemyHealth>().ModifyHealth(-Potency - -Target.GetComponent<Character>().CharacterDefense);

                Target.GetComponent<EnemyAI>().GetStates = States.Damaged;

                DamageSkillText();
            }
            else
            {
                GameManager.Instance.ShowTargetOutOfRangeText();
            }
        }
    }

    private Text HealSkillText()
    {
        var SkillObj = Instantiate(SkillTextObject);

        SkillObj.color = Color.green;

        SkillObj.transform.SetParent(TextHolder.transform, false);

        SkillObj.text = SkillName + " " + Potency;

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

        SkillPanelObject.text = SkillName + "\n \n" + SkillDescription + "\n \n" + "Potency: " + Potency;
    }
}