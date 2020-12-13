#pragma warning disable 0649
using UnityEngine;

public class OpenToadstoolCage : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Settings settings;

    public void InvokeOpenCage()
    {
        settings.SecretCharacterUnlocked = true;
        if (GameManager.Instance.GetKnight.activeInHierarchy)
        {
            settings.CharacterSavior = "Knight";
        }
        if (GameManager.Instance.GetShadowPriest.activeInHierarchy)
        {
            settings.CharacterSavior = "Shadow Priest";
        }

        Invoke("OpenCage", 4f);
    }

    private void OpenCage()
    {
        animator.SetBool("OpenCage", true);
    }
}
