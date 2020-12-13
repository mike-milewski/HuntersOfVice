#pragma warning disable 0649
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private ObstacleDamageRadius obstacleDamageRadius;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            obstacleDamageRadius.enabled = true;
            Invoke("InvokeResetTrap", 0.4f);
        }
    }

    private void InvokeResetTrap()
    {
        animator.SetBool("PlaySpikes", true);
        Invoke("InvokeResetAnimator", 0.4f);
        obstacleDamageRadius.enabled = false;
    }

    private void InvokeResetAnimator()
    {
        animator.SetBool("PlaySpikes", false);
    }
}
