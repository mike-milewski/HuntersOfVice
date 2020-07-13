#pragma warning disable 0649
using UnityEngine;

public class ResetCoinText : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    public void ResetCoin()
    {
        ObjectPooler.Instance.ReturnCoinTextToPool(parent);
    }
}
