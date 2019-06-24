using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance = null;

    [SerializeField]
    private Character character;

    [SerializeField]
    private StatusIcon statusIcon;

    [SerializeField]
    private EnemyStatusIcon enemystatusIcon;

    [SerializeField]
    private List<Skills> skills = new List<Skills>();

    private bool ActivatedSkill, DisruptedSkill;

    private int KeyInput;

    public bool GetActivatedSkill
    {
        get
        {
            return ActivatedSkill;
        }
        set
        {
            ActivatedSkill = value;
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

    public int GetKeyInput
    {
        get
        {
            return KeyInput;
        }
        set
        {
            KeyInput = value;
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

    public List<Skills> GetSkills
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

    public StatusIcon GetStatusIcon
    {
        get
        {
            return statusIcon;
        }
        set
        {
            statusIcon = value;
        }
    }

    public EnemyStatusIcon GetEnemyStatusIcon
    {
        get
        {
            return enemystatusIcon;
        }
        set
        {
            enemystatusIcon = value;
        }
    }

    private void Awake()
    {
        Instance = this;

        AddSkillsToList();
        ArrangeSkills();
    }

    private void AddSkillsToList()
    {
        foreach(Skills s in gameObject.transform.GetComponentsInChildren<Skills>())
        {
            skills.Add(s);
        }
    }

    public void ArrangeSkills()
    {
        skills = skills.OrderBy(g => g.name).ToList();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            KeyInput = 0;
            if (skills[KeyInput].GetButton.GetComponent<Image>().fillAmount >= 1
                                                   && skills[KeyInput].GetCharacter.CurrentMana >= skills[KeyInput].GetManaCost && skills[KeyInput].GetComponent<Button>().interactable)
            {
                skills[KeyInput].GetButton.onClick.Invoke();
            }
            else if (skills[KeyInput].GetCharacter.CurrentMana < skills[KeyInput].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            KeyInput = 1;
            if (skills[KeyInput].GetButton.GetComponent<Image>().fillAmount >= 1
                                                   && skills[KeyInput].GetCharacter.CurrentMana >= skills[KeyInput].GetManaCost && skills[KeyInput].GetComponent<Button>().interactable)
            {
                skills[KeyInput].GetButton.onClick.Invoke();
            }
            else if (skills[KeyInput].GetCharacter.CurrentMana < skills[KeyInput].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            KeyInput = 2;
            if (skills[KeyInput].GetButton.GetComponent<Image>().fillAmount >= 1
                                                   && skills[KeyInput].GetCharacter.CurrentMana >= skills[KeyInput].GetManaCost && skills[KeyInput].GetComponent<Button>().interactable)
            {
                skills[KeyInput].GetButton.onClick.Invoke();
            }
            else if (skills[KeyInput].GetCharacter.CurrentMana < skills[KeyInput].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            KeyInput = 3;
            if (skills[KeyInput].GetButton.GetComponent<Image>().fillAmount >= 1 
                                                   && skills[KeyInput].GetCharacter.CurrentMana >= skills[KeyInput].GetManaCost && skills[KeyInput].GetComponent<Button>().interactable)
            {
                skills[KeyInput].GetButton.onClick.Invoke();
            }
            else if (skills[KeyInput].GetCharacter.CurrentMana < skills[KeyInput].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            KeyInput = 4;
            if (skills[KeyInput].GetButton.GetComponent<Image>().fillAmount >= 1
                                                   && skills[KeyInput].GetCharacter.CurrentMana >= skills[KeyInput].GetManaCost && skills[KeyInput].GetComponent<Button>().interactable)
            {
                skills[KeyInput].GetButton.onClick.Invoke();
            }
            else if (skills[KeyInput].GetCharacter.CurrentMana < skills[KeyInput].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
    }
 
    public void ReactivateSkillButtons()
    {
        foreach (Skills s in skills)
        {
            s.GetButton.interactable = true;
        }
    }

    public void DeactivateSkillButtons()
    {
        foreach (Skills s in skills)
        {
            s.GetButton.interactable = false;
        }
    }
 
    public void Clicked(int index)
    {
        KeyInput = skills.IndexOf(skills[index]);
    }
}