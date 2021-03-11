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
        animator.Play("LevelupText", -1, 0f);
    }

    public void ReverseTheLevelUp()
    {
        gameObject.SetActive(false);
    }
}
