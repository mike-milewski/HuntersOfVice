using UnityEngine;

public class InvalidText : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void DisableText()
    {
        gameObject.SetActive(false);
    }
}
