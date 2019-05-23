using UnityEngine;

public class LowEnemyHPAnimation : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Animator animator;

    public Animator GetAnimator
    {
        get
        {
            return animator;
        }
        set
        {
            animator = value;
        }
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();

        ResetAnimator();

        DisableAnimator();
    }

    public void ResetAnimator()
    {
        animator.Play("EnemyHealthLowHP", -1, 0f);
    }

    public void EnableAnimator()
    {
        animator.enabled = true;
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }
}
