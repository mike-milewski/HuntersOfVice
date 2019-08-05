using System.Collections;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField]
    private Animation anim;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private EnemyAI AI;

    [SerializeField]
    private EnemySkills enemyskills;

    [SerializeField]
    private DamageRadius damageradius;

    [SerializeField]
    private ChangeEnemyMaterial changeEnemyMaterial;

	private const string IDLE	= "Idle";
	private const string MOVE	= "Move";
	private const string ATTACK	= "Attack";
    private const string SKILLATTACK = "SkillAttack";
    private const string SKILLATTACK2 = "SkillAttack2";
    private const string CASTING = "Casting";
    private const string DAMAGE	= "Damage";
	private const string DEATH	= "Death";

    public void IdleAni ()
    {
        anim.Play(IDLE);
	}

	public void RunAni ()
    {
        anim.Play(MOVE);
	}

	public void AttackAni ()
    {
        anim.Play(ATTACK);
	}

    public void CastingAni()
    {
        anim.Play(CASTING);
    }

    public void DamageAni ()
    {
        anim.Play(DAMAGE);
	}

	public void DeathAni ()
    {
        anim.Play(DEATH);
	}

    public void DamagePlayer()
    {
        AI.TakeDamage();
    }

    public void SkillDamage()
    {
        enemyskills.SkillDamageText(enemyskills.GetManager[AI.GetAiStates[AI.GetStateArrayIndex].GetSkillIndex].GetPotency, 
                                    enemyskills.GetManager[AI.GetAiStates[AI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
    }

    public void SkillRadiusDamage()
    {
        if(damageradius.GetIsInRadius)
        {
            damageradius.TakeRadiusDamage();
        }
        damageradius.GetIsInRadius = false;
    }

    public void FungiBumpAnim()
    {
        anim.Play(SKILLATTACK);
    }

    public void SkillAtk2()
    {
        anim.Play(SKILLATTACK2);
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

    public void PlayHealthFade()
    {
        animator.SetBool("FadeHealth", true);
    }

    public void ReverseFadeHealth()
    {
        animator.SetBool("FadeHealth", false);
    }

    public void Fading()
    {
        changeEnemyMaterial.ChangeToAlphaMaterial();

        StartCoroutine(changeEnemyMaterial.Fade());
    }

    public void IncreaseAiArray()
    {
        AI.IncreaseArray();
    }

    public void PlayHitSE()
    {
        SoundManager.Instance.EnemyHit();
    }
}