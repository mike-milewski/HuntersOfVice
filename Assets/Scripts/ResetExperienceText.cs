#pragma warning disable 0649
using UnityEngine;

public class ResetExperienceText : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public void RestText()
    {
        ObjectPooler.Instance.ReturnExperienceTextToPool(Parent);
    }
}
