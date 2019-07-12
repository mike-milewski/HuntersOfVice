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
    private CharacterMenu characterMenu;

    [SerializeField]
    private StatusIcon statusIcon;

    [SerializeField]
    private EnemyStatusIcon enemystatusIcon;

    [SerializeField]
    private List<Skills> skills = new List<Skills>();

    private bool ActivatedSkill, DisruptedSkill;

    private int KeyInput;

    private int input;

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

    public CharacterMenu GetCharacterMenu
    {
        get
        {
            return characterMenu;
        }
        set
        {
            characterMenu = value;
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
    }

    public void AddSkillsToList()
    {
        foreach(Skills s in gameObject.transform.GetComponentsInChildren<Skills>())
        {
            skills.Add(s);
            for(int i = 0; i < skills.Count; i++)
            {
                s.GetIndex = i;
            }
        }
    }

    public void ClearSkills()
    {
        skills.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            input = 1;
            if (input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[0]);
                if (skills[KeyInput].GetCoolDownImage.fillAmount <= 0
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            input = 2;
            if (input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[1]);
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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            input = 3;
            if (input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[2]);
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
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            input = 4;
            if (input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[3]);
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
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            input = 5;
            if (input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[4]);
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
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            input = 6;
            if(input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[5]);
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
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            input = 7;
            if (input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[6]);
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
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            input = 8;
            if (input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[7]);
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
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            input = 9;
            if (input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[8]);
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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            input = 10;
            if (input > skills.Count)
            {
                return;
            }
            else
            {
                KeyInput = skills.IndexOf(skills[9]);
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
    }

    public void CheckForSameSkills(Skills other)
    {
        foreach (Skills s in gameObject.transform.GetComponentsInChildren<Skills>())
        {
            if (s.GetSkillName == other.GetSkillName)
            {
                s.GetCoolDownImage.fillAmount = 1;
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

    public void AllSkillsBeingDragged()
    {
        foreach (Skills s in skills)
        {
            s.GetIsBeingDragged = true;
        }
    }

    public void AllSkillsNotBeingDragged()
    {
        foreach (Skills s in skills)
        {
            s.GetIsBeingDragged = false;
        }
    }

    public void DeactivateSkillButtons()
    {
        foreach (Skills s in skills)
        {
            s.GetButton.interactable = false;
        }
    }
}