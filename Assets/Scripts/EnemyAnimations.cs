using System.Collections;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField]
    private Animation anim;

    [SerializeField]
    private EnemyAI AI;

    [SerializeField]
    private EnemySkills enemyskills;

    [SerializeField]
    private GameObject ParentObject;

	private const string IDLE	= "Idle";
	private const string MOVE	= "Move";
	private const string ATTACK	= "Attack";
    private const string SKILLATTACK = "SkillAttack";
    private const string SKILLATTACK2 = "SkillAttack2";
    private const string CASTING = "Casting";
    private const string DAMAGE	= "Damage";
	private const string DEATH	= "Death";

    [SerializeField]
    private SkinnedMeshRenderer rend;

	void Start ()
    {
        CheckSkinnedMesh();
    }

    private void OnEnable()
    {
        Color alpha = rend.material.color;
        rend.material.color = alpha;
        alpha.a = 1.0f;
        rend.material.color = alpha;
        CheckSkinnedMesh();
    }

    public void IdleAni ()
    {
		anim.CrossFade (IDLE);
	}

	public void RunAni ()
    {
		anim.CrossFade (MOVE);
	}

	public void AttackAni ()
    {
		anim.CrossFade (ATTACK);
	}

    public void CastingAni()
    {
        anim.CrossFade(CASTING);
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

    public void SkillDamage()
    {
        enemyskills.SkillDamageText(enemyskills.GetManager[enemyskills.GetRandomValue].GetPotency, enemyskills.GetManager[enemyskills.GetRandomValue].GetSkillName);
    }

    public void FungiBumpAnim()
    {
        anim.CrossFade(SKILLATTACK);
    }

    public void SkillAtk2()
    {
        anim.CrossFade(SKILLATTACK2);
    }

    public void ResetAutoAttackTime()
    {
        if(AI.GetStates != States.SkillAnimation)
        {
            AI.GetAutoAttack = 0;
            AI.GetStates = States.Attack;
        }
        else
        {
            AI.CheckTarget();
        }
    }

    public void EndDamaged()
    {
        if(!AI.GetIsHostile)
        {
            AI.GetSphereTrigger.enabled = true;
            AI.GetStates = States.Attack;
        }
        else
        {
            AI.GetStates = States.Attack;
        }
    }

    public IEnumerator Fade()
    {
        Color alpha = rend.material.color;
        rend.material.color = alpha;
        yield return new WaitForSeconds(3f);
        while(alpha.a > 0.1f)
        {
            alpha.a -= 11 * Time.deltaTime;
            rend.material.color = alpha;
            CheckSkinnedMesh();
            yield return new WaitForSeconds(0.1f);
            alpha.a -= 11 * Time.deltaTime;
            rend.material.color = alpha;
            CheckSkinnedMesh();
            yield return new WaitForSeconds(0.1f);
        }
        ParentObject.SetActive(false);
        yield return null;
    }

    private void CheckSkinnedMesh()
    {
        if (gameObject.GetComponent<SkinnedMeshRenderer>() == null)
        {
            this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = rend.material;
        }
        else
        {
            this.gameObject.GetComponent<SkinnedMeshRenderer>().material = rend.material;
        }
    }
}