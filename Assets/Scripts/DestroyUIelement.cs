using UnityEngine;

public class DestroyUIelement : MonoBehaviour
{
    [SerializeField]
    private GameObject Parent;

    public void DestroyUI()
    {
        Parent.SetActive(false);
    }
}
