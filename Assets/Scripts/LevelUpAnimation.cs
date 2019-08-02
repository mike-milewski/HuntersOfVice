using UnityEngine;

public class LevelUpAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject SkillMenuParent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayLevelUpAnimation()
    {
        animator.SetBool("LevelUp", true);
    }

    public void ReverseTheLevelUp()
    {
        animator.SetBool("LevelUp", false);
    }
}
