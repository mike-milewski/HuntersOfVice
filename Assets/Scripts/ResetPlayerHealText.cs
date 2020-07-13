#pragma warning disable 0649
using UnityEngine;

public class ResetPlayerHealText : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public void ResetText()
    {
        ObjectPooler.Instance.ReturnPlayerHealToPool(Parent);
    }
}
