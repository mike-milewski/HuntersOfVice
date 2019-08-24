using UnityEngine;

public class TreasureChest : MonoBehaviour
{
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
}
