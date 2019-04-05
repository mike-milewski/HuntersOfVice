using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Character character;

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

    public void ReturnExperience()
    {
        EXP.GainEXP(ExperiencePoints);
        EXP.GetShowExperienceText().text = ExperiencePoints + " EXP";
    }
}
