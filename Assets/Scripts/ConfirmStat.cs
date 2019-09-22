using UnityEngine;

public class ConfirmStat : MonoBehaviour
{
    public void ConfirmStatus(Experience experience)
    {
        experience.GetMaxStatPoints = experience.GetStatPoints;
        if(experience.GetStatPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void AddStats()
    {

    }
}
