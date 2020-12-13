#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

public class CheckSecretCharacter : MonoBehaviour
{
    [SerializeField]
    private GameObject SecretCharacter, SecretCharacterParticle;

    [SerializeField]
    private CharacterSelector Knight, ShadowPriest;

    [SerializeField]
    private Button ReturnToMenu;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private GameObject Mushroom;

    private void OnEnable()
    {
        if(!settings.AlreadyCheckedForCharacter)
        {
            if (settings.SecretCharacterUnlocked)
            {
                Invoke("InvokeCharacterUnlock", 1f);
                Knight.GetComponent<BoxCollider>().enabled = false;
                ShadowPriest.GetComponent<BoxCollider>().enabled = false;
                ReturnToMenu.interactable = false;
                settings.AlreadyCheckedForCharacter = true;
            }
        }
        else
        {
            SecretCharacter.SetActive(true);
            animator.SetBool("CharacterSelection", false);
            Mushroom.SetActive(false);
        }
    }

    private void InvokeCharacterUnlock()
    {
        SecretCharacterParticle.SetActive(true);
        Mushroom.SetActive(false);
        SecretCharacter.SetActive(true);
        animator.SetBool("CharacterSelection", true);
        Invoke("ReenableCharacterCollidersAndMenuButton", 1f);
    }

    private void ReenableCharacterCollidersAndMenuButton()
    {
        Knight.GetComponent<BoxCollider>().enabled = true;
        ShadowPriest.GetComponent<BoxCollider>().enabled = true;
        ReturnToMenu.interactable = true;
    }
}
