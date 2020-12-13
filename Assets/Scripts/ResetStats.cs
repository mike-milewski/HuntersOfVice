using UnityEngine;

public class ResetStats : MonoBehaviour
{
    public void ResetPlayerStats()
    {
        if(GameManager.Instance.GetKnight.activeInHierarchy)
        {
            GameManager.Instance.GetKnight.GetComponent<Experience>().SetStatPointsText();
        }
        if(GameManager.Instance.GetShadowPriest.activeInHierarchy)
        {
            GameManager.Instance.GetShadowPriest.GetComponent<Experience>().SetStatPointsText();
        }
        if (GameManager.Instance.GetToadstool.activeInHierarchy)
        {
            GameManager.Instance.GetToadstool.GetComponent<Experience>().SetStatPointsText();
        }
    }
}
