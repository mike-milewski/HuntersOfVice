using System.Collections.Generic;
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