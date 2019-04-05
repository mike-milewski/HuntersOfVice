using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Button btn;

    [SerializeField]
    private float CoolDown;

    [SerializeField]
    private string SkillName;

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
    }

    private void BeginCoolDown()
    {
        if (btn.GetComponent<Image>().fillAmount >= 1 && character.CurrentHealth > 0)
        {
            btn.interactable = true;
            return;
        }
        else
        {
            btn.GetComponent<Image>().fillAmount += Time.deltaTime / CoolDown;
            btn.interactable = false;
        }
    }

    public void TestHealSkill()
    {
        btn.GetComponent<Image>().fillAmount = 0;

        character.GetComponent<Health>().ModifyHealth(Potency);
    }
}