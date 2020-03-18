using UnityEngine;

public class ResetMonsterEntryText : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public void ResetMonsterText()
    {
        ObjectPooler.Instance.ReturnMonsterEntryTextToPool(Parent);
    }
}
