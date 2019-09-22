using UnityEngine;
using TMPro;

public class StatUpgrade : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Experience experience;

    [SerializeField]
    private TextMeshProUGUI StatText;

    public void PlusStatHealth()
    {
        if(experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
        }
    }

    public void PlusStatMana()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
        }
    }

    public void PlusStatStrength()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
        }
    }

    public void PlusStatDefense()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
        }
    }

    public void PlusStatIntelligence()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
        }
    }

    public void MinusStatHealth()
    {
        if(experience.GetStatPoints < experience.GetMaxStatPoints)
        {
            experience.GetStatPoints++;
        }
    }

    public void MinusStatMana()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints)
        {
            experience.GetStatPoints++;
        }
    }

    public void MinusStatStrength()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints)
        {
            experience.GetStatPoints++;
        }
    }

    public void MinusStatDefense()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints)
        {
            experience.GetStatPoints++;
        }
    }

    public void MinusStatIntelligence()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints)
        {
            experience.GetStatPoints++;
        }
    }
}
