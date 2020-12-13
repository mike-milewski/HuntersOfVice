#pragma warning disable 0649
using UnityEngine;

public class ToadstoolWalk : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform TargetPositionToMoveTowards;

    [SerializeField]
    private GameObject ParticleEffect;

    [SerializeField]
    private float Speed;

    private Vector3 Distance;

    private void OnEnable()
    {
        animator.SetFloat("Speed", 1.0f);
    }

    private void Update()
    {
        if(animator.GetFloat("Speed") > 0)
        {
            Distance = new Vector3(TargetPositionToMoveTowards.position.x - transform.position.x, TargetPositionToMoveTowards.position.y - transform.position.y,
                               TargetPositionToMoveTowards.position.z - transform.position.z).normalized;

            transform.position += Distance * Speed * Time.deltaTime;
        }

        if(Vector3.Distance(transform.position, TargetPositionToMoveTowards.position) <= 0.2f)
        {
            animator.SetFloat("Speed", 0.0f);
            Invoke("InvokeJump", 1f);
        }
    }

    private void InvokeJump()
    {
        animator.SetBool("Jump", true);
        Invoke("InvokeParticleEffect", 0.4f);
    }

    private void InvokeParticleEffect()
    {
        SpawnParticleEffect();
        gameObject.SetActive(false);
    }

    private void SpawnParticleEffect()
    {
        ParticleEffect.SetActive(true);
    }
}
