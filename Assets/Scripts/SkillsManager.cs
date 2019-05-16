﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager Instance = null;

    [SerializeField]
    private Character character;

    [SerializeField]
    private StatusIcon statusIcon;

    [SerializeField]
    private EnemyStatusIcon enemystatusIcon;

    private ParticleSystem ParticleObj = null;

    [SerializeField]
    private List<Skills> skills;

    private bool ActivatedSkill;

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

    public ParticleSystem GetParticleObj
    {
        get
        {
            return ParticleObj;
        }
        set
        {
            ParticleObj = value;
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
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            KeyInput = 0;
            if (skills[0].GetButton.GetComponent<Image>().fillAmount >= 1 && skills[0].GetCharacter.CurrentHealth > 0
                                                   && skills[0].GetCharacter.CurrentMana >= skills[0].GetManaCost && skills[0].GetComponent<Button>().interactable)
            {
                skills[0].GetButton.onClick.Invoke();
            }
            else if (skills[0].GetCharacter.CurrentMana < skills[0].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            KeyInput = 1;
            if (skills[1].GetButton.GetComponent<Image>().fillAmount >= 1 && skills[1].GetCharacter.CurrentHealth > 0
                                                  && skills[1].GetCharacter.CurrentMana >= skills[1].GetManaCost && skills[1].GetComponent<Button>().interactable)
            {
                skills[1].GetButton.onClick.Invoke();
            }
            else if (skills[1].GetCharacter.CurrentMana < skills[1].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            KeyInput = 2;
            if (skills[2].GetButton.GetComponent<Image>().fillAmount >= 1 && skills[2].GetCharacter.CurrentHealth > 0
                                                  && skills[2].GetCharacter.CurrentMana >= skills[2].GetManaCost && skills[2].GetComponent<Button>().interactable)
            {
                skills[2].GetButton.onClick.Invoke();
            }
            else if (skills[2].GetCharacter.CurrentMana < skills[2].GetManaCost)
            {
                GameManager.Instance.ShowNotEnoughManaText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            KeyInput = 3;
            if (skills[3].GetButton.GetComponent<Image>().fillAmount >= 1 && skills[3].GetCharacter.CurrentHealth > 0
                                                  && skills[3].GetCharacter.CurrentMana >= skills[3].GetManaCost && skills[3].GetComponent<Button>().interactable)
            {
                skills[3].GetButton.onClick.Invoke();
            }
            else if (skills[3].GetCharacter.CurrentMana < skills[3].GetManaCost)
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

    public void Clicked(int index)
    {
        KeyInput = skills.IndexOf(skills[index]);
    }
}
