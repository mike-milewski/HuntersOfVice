#pragma warning disable 0649
using UnityEngine;

public class ResetPlayerStatusEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public void ResetText()
    {
        ObjectPooler.Instance.ReturnPlayerStatusTextToPool(Parent);
    }
}
