using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    [SerializeField]
    private List<Skills> skills;

    private void Awake()
    {
        /*
        Object[] SkillButton = Resources.LoadAll("Prefabs", typeof(Skills));

        foreach(Skills s in SkillButton)
        {
            skills.Add(s);
        }
        */
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(skills[0].GetComponent<Button>().GetComponent<Image>().fillAmount >= 1 && skills[0].GetCharacter.CurrentHealth > 0 
                                                && skills[0].GetCharacter.CurrentMana >= skills[0].GetManaCost)
            {
                skills[0].GetComponent<Button>().onClick.Invoke();
            }
            else if(skills[0].GetCharacter.CurrentMana < skills[0].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (skills[1].GetComponent<Button>().GetComponent<Image>().fillAmount >= 1 && skills[1].GetCharacter.CurrentHealth > 0
                                                 && skills[1].GetCharacter.CurrentMana >= skills[1].GetManaCost)
            {
                skills[1].GetComponent<Button>().onClick.Invoke();
            }
            else if (skills[1].GetCharacter.CurrentMana < skills[1].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
    }
}
