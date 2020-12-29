#pragma warning disable 0649
using System.Collections;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField]
    private Animation anim = null;

    [SerializeField]
    private Animator EnemyAnimator = null;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Puzzle puzzle = null;

    [SerializeField]
    private EnemyAI AI = null;

    [SerializeField]
    private Puck puckAI = null;

    [SerializeField]
    private EnemySkills enemyskills = null;

    [SerializeField]
    private DamageRadius damageradius = null;

    [SerializeField]
    private ChangeEnemyMaterial changeEnemyMaterial = null;

    private bool CheckedForPuzzle;

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

    public void AnimatorCasting()
    {
        EnemyAnimator.SetBool("Attacking", false);
        EnemyAnimator.SetBool("Moving", false);
        EnemyAnimator.SetBool("Damaged", false);
        EnemyAnimator.SetBool("Skill", false);
        EnemyAnimator.SetBool("Skill2", false);

        if (EnemyAnimator.GetBool("Skill3"))
        {
            EnemyAnimator.SetBool("Skill3", false);
        }
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

    public void PuckDamagePlayer()
    {
        puckAI.TakeDamage();
    }

    public void MoveAnimator()
    {
        EnemyAnimator.SetBool("Moving", true);
    }

    public void AttackAnimator()
    {
        EnemyAnimator.SetBool("Attacking", true);
    }

    public void DamagedAnimator()
    {
        EnemyAnimator.SetBool("Damaged", true);
    }

    public void SkillAnimator()
    {
        EnemyAnimator.SetBool("Skill", true);
    }

    public void Skill2Animator()
    {
        EnemyAnimator.SetBool("Skill2", true);
    }

    public void Skill3Animator()
    {
        EnemyAnimator.SetBool("Skill3", true);
    }

    public void ResetSkillAnimator()
    {
        EnemyAnimator.SetBool("Skill", false);
        EnemyAnimator.SetBool("Skill2", false);

        if (EnemyAnimator.GetBool("Skill3"))
        {
            EnemyAnimator.SetBool("Skill3", false);
        }

        enemyskills.GetActiveSkill = false;
    }

    public void DeadAnimator()
    {
        EnemyAnimator.SetBool("Dead", true);
        EnemyAnimator.SetBool("Attacking", false);
        EnemyAnimator.SetBool("Moving", false);
        EnemyAnimator.SetBool("Damaged", false);
        EnemyAnimator.SetBool("Skill", false);
        EnemyAnimator.SetBool("Skill2", false);

        if(EnemyAnimator.GetBool("Skill3"))
        {
            EnemyAnimator.SetBool("Skill3", false);
        }
    }

    public void IdleAnimator()
    {
        EnemyAnimator.SetBool("Moving", false);
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

    public void ResetSkillAnim()
    {
        ResetSkillAnimator();
    }

    public void PuckResetAutoAttackTime()
    {
        if (puckAI.GetStates != BossStates.SkillAnimation)
        {
            EnemyAnimator.SetBool("Attacking", false);
            ResetSkillAnimator();
            puckAI.GetAutoAttack = 0;
            puckAI.GetStates = BossStates.Attack;
        }
        else
        {
            puckAI.CheckTarget();
        }
    }

    public void PuckEndDamaged()
    {
        EnemyAnimator.SetBool("Damaged", false);

        puckAI.GetStates = BossStates.Attack;
    }

    public void ResetAutoAttackTime()
    {
        if (AI.GetStates != States.SkillAnimation || AI.GetIsAnAdd)
        {
            if(AI.GetIsUsingAnimator)
            {
                EnemyAnimator.SetBool("Attacking", false);
                ResetSkillAnimator();
            }
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
        if(AI.GetIsUsingAnimator)
        {
            EnemyAnimator.SetBool("Damaged", false);
        }
        
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

    public void IncrementEnemyPuzzleCount()
    {
        if(AI.GetIsAPuzzleComponent)
        {
            if (!CheckedForPuzzle)
            {
                puzzle.EnemyPuzzleType();
                CheckedForPuzzle = true;
            }
        }
        if(AI.GetIsABushPuzzleComponent)
        {
            if(!CheckedForPuzzle)
            {
                puzzle.BushPuzzle();
                CheckedForPuzzle = true;
            }
        }
        if (AI.GetIsAsecretCharacterPuzzleComponent)
        {
            puzzle.SecretCharacterSpawn();
        }
    }

    public void IncrementGatePuzzleCount()
    {
        if (AI.GetIsAPuzzleComponent)
        {
            if (!CheckedForPuzzle)
            {
                puzzle.GatePuzzleType();
                CheckedForPuzzle = true;
            }
        }
        if(AI.GetIsAMagicWallPuzzleComponent)
        {
            puzzle.MagicWallPuzzleType();
        }
    }

    public void SpawnEnemy()
    {
        if (AI.GetIsATreasurePuzzleComponent)
        {
            puzzle.SpawnNewEnemy();
        }  
    }

    public void CheckItem()
    {
        AI.CheckItemDrop();
    }
}