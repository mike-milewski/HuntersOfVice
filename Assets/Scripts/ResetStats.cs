using UnityEngine;

public class ResetStats : MonoBehaviour
{
    public void ResetPlayerStats()
    {
        if(GameManager.Instance.GetKnight.activeInHierarchy)
        {
            GameManager.Instance.GetKnight.GetComponent<Experience>().SetStatPointsText();
        }
        else if(GameManager.Instance.GetShadowPriest.activeInHierarchy)
        {
            GameManager.Instance.GetShadowPriest.GetComponent<Experience>().SetStatPointsText();
        }
    }
}
