using UnityEngine;

public class LowHpAnimation : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Animator _animator;

    public Animator GetAnimator
    {
        get
        {
            return _animator;
        }
        set
        {
            _animator = value;
        }
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void ResetAnimator()
    {
        _animator.Play("LowHP", -1, 0f);
    }
}
