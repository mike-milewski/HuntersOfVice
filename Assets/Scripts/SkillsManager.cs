#pragma warning disable 0649
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
    private Character character, Knight, ShadowPriest, Toadstool;

    [SerializeField]
    private CharacterMenu characterMenu;

    [SerializeField]
    private StatusIcon statusIcon;

    [SerializeField]
    private EnemyStatusIcon enemystatusIcon;

    [SerializeField]
    private List<Skills> skills = new List<Skills>();

    [SerializeField]
    private Skills ContractSkill = null;

    [SerializeField]
    private bool UsesHpForSkillCast;

    [SerializeField]
    private int ContractStack, MaxContractStack;

    private bool ActivatedSkill, DisruptedSkill, WhirlWindSlashAbility, Spin;

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

    public bool GetWhirlwind
    {
        get
        {
            return WhirlWindSlashAbility;
        }
        set
        {
            WhirlWindSlashAbility = value;
        }
    }

    public bool GetSpin
    {
        get
        {
            return Spin;
        }
        set
        {
            Spin = value;
        }
    }

    public bool GetUsesHpForSkillCast
    {
        get
        {
            return UsesHpForSkillCast;
        }
        set
        {
            UsesHpForSkillCast = value;
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

    public int GetContractStack
    {
        get
        {
            return ContractStack;
        }
        set
        {
            ContractStack = value;
        }
    }

    public int GetMaxContractStack
    {
        get
        {
            return MaxContractStack;
        }
        set
        {
            MaxContractStack = value;
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

    public Skills GetContractSkill
    {
        get
        {
            return ContractSkill;
        }
        set
        {
            ContractSkill = value;
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

    private void OnEnable()
    {
        if(Knight.gameObject.activeInHierarchy)
        {
            character = Knight;
        }
        if(ShadowPriest.gameObject.activeInHierarchy)
        {
            character = ShadowPriest;
        }
        if (Toadstool.gameObject.activeInHierarchy)
        {
            character = Toadstool;
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && skills.Count > 0)
        {
            if(skills[0].GetButton.interactable)
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
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && skills.Count > 1)
        {
            if(skills[1].GetButton.interactable)
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
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && skills.Count > 2)
        {
            if(skills[2].GetButton.interactable)
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
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && skills.Count > 3)
        {
            if(skills[3].GetButton.interactable)
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
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && skills.Count > 4)
        {
            if(skills[4].GetButton.interactable)
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
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && skills.Count > 5)
        {
            if(skills[5].GetButton.interactable)
            {
                input = 6;
                if (input > skills.Count)
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
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && skills.Count > 6)
        {
            if(skills[6].GetButton.interactable)
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
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && skills.Count > 7)
        {
            if(skills[7].GetButton.interactable)
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
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && skills.Count > 8)
        {
            if(skills[8].GetButton.interactable)
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
            else
            {
                GameManager.Instance.CannotExecuteText();
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