using UnityEngine;

public class LowEnemyHPAnimation : MonoBehaviour
{
    [SerializeField]
    private Character character;

    private const string LOWHEALTH = "EnemyHealthLowHP";

    [SerializeField]
    private Animation _animation;

    public Animation GetAnimation
    {
        get
        {
            return _animation;
        }
        set
        {
            _animation = value;
        }
    }

    private void OnEnable()
    {
        _animation = GetComponent<Animation>();

        ResetAnimator();

        DisableAnimator();
    }

    public void ResetAnimator()
    {
        _animation.Rewind(LOWHEALTH);
    }

    public void EnableAnimator()
    {
        _animation.enabled = true;
        _animation.CrossFade(LOWHEALTH);
    }

    public void DisableAnimator()
    {
        _animation.enabled = false;
    }
}
