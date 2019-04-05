using System.Collections;
using UnityEngine;

public class MushroomMon_Ani_Test : MonoBehaviour
{
    [SerializeField]
    private EnemyAI AI;

    [SerializeField]
    private GameObject ParentObject;

	private const string IDLE	= "Idle";
	private const string RUN	= "Run";
	private const string ATTACK	= "Attack";
	private const string DAMAGE	= "Damage";
	private const string DEATH	= "Death";

	private Animation anim;

    [SerializeField]
    private SkinnedMeshRenderer rend;

	void Start ()
    {
		anim = GetComponent<Animation>();

        rend.material = GetComponent<SkinnedMeshRenderer>().material;
    }

    private void OnEnable()
    {
        Color alpha = rend.material.color;
        rend.material.color = alpha;
        alpha.a = 1.0f;
        rend.material.color = alpha;
        this.gameObject.GetComponent<SkinnedMeshRenderer>().material = rend.material;
    }

    public void IdleAni ()
    {
		anim.CrossFade (IDLE);
	}

	public void RunAni ()
    {
		anim.CrossFade (RUN);
	}

	public void AttackAni ()
    {
		anim.CrossFade (ATTACK);
	}

	public void DamageAni ()
    {
		anim.CrossFade (DAMAGE);
	}

	public void DeathAni ()
    {
		anim.CrossFade (DEATH);
	}

    public void DamagePlayer()
    {
        AI.TakeDamage();
    }

    public void ResetAutoAttackTime()
    {
        AI.GetAutoAttack = 0;
    }

    public void EndDamaged()
    {
        AI.GetStates = States.Attack;
    }

    public IEnumerator Fade()
    {
        Color alpha = rend.material.color;
        rend.material.color = alpha;
        while(alpha.a > 0.2f)
        {
            alpha.a -= Mathf.Clamp01(alpha.a) * 6 * Time.deltaTime;
            rend.material.color = alpha;
            this.gameObject.GetComponent<SkinnedMeshRenderer>().material = rend.material;
            yield return new WaitForSeconds(0.1f);
            alpha.a -= Mathf.Clamp01(alpha.a) * 6 * Time.deltaTime;
            rend.material.color = alpha;
            this.gameObject.GetComponent<SkinnedMeshRenderer>().material = rend.material;
            yield return new WaitForSeconds(0.1f);
        }
        ParentObject.SetActive(false);
        yield return null;
    }
}
