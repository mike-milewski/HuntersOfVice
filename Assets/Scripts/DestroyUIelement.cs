#pragma warning disable 0649
using UnityEngine;

public class DestroyUIelement : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public void DestroyUI()
    {
        ObjectPooler.Instance.ReturnPlayerDamageToPool(Parent);
    }
}
