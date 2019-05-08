using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private EnemyAI enemyAI;

    [SerializeField]
    private EnemyHealth enemyHealth;

    [SerializeField]
    private EnemySkills enemySkills;

    [SerializeField]
    private EnemySkillBar enemySkillBar;

    [SerializeField]
    private Experience EXP;

    [SerializeField]
    private GameObject HealthObject;

    [SerializeField]
    private int ExperiencePoints;

    public int GetExperiencePoints
    {
        get
        {
            return ExperiencePoints;
        }
        set
        {
            ExperiencePoints = value;
        }
    }

    public GameObject GetHealthObject
    {
        get
        {
            return HealthObject;
        }
        set
        {
            HealthObject = value;
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

    public EnemyAI GetAI
    {
        get
        {
            return enemyAI;
        }
        set
        {
            enemyAI = value;
        }
    }

    public EnemyHealth GetHealth
    {
        get
        {
            return enemyHealth;
        }
        set
        {
            enemyHealth = value;
        }
    }

    public EnemySkills GetSkills
    {
        get
        {
            return enemySkills;
        }
        set
        {
            enemySkills = value;
        }
    }

    public EnemySkillBar GetEnemySkillBar
    {
        get
        {
            return enemySkillBar;
        }
        set
        {
            enemySkillBar = value;
        }
    }

    public void ReturnExperience()
    {
        EXP.GainEXP(ExperiencePoints);
        EXP.GetShowExperienceText().text = ExperiencePoints + " EXP";
    }
}
