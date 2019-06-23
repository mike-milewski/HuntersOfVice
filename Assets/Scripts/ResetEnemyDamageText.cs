using UnityEngine;

public class ResetEnemyDamageText : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public void DisableText()
    {
        ObjectPooler.Instance.ReturnEnemyDamageToPool(Parent);
    }
}
