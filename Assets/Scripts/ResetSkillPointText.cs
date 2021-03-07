#pragma warning disable 0649
using UnityEngine;

public class ResetSkillPointText : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public void ResetText()
    {
        ObjectPooler.Instance.ReturnSkillPointTextToPool(Parent);
    }
}
