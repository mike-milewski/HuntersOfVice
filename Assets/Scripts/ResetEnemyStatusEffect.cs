using UnityEngine;

public class ResetEnemyStatusEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public void ResetText()
    {
        ObjectPooler.Instance.ReturnEnemyStatusTextToPool(Parent);
    }
}
