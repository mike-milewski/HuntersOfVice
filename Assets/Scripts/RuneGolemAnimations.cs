#pragma warning disable 0649
#pragma warning disable 0414
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneGolemAnimations : MonoBehaviour
{
    [SerializeField]
    private Animation anim = null;

    [SerializeField]
    private Animator EnemyAnimator = null;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private RuneGolem AI;

    [SerializeField]
    private EnemySkills enemyskills;

    [SerializeField]
    private RuneGolemDamageRadius damageradius;

    [SerializeField]
    private ChangeEnemyMaterial[] changeEnemyMaterial;

    private const string IDLE = "Idle";
    private const string MOVE = "Move";
    private const string ATTACK = "Attack";
    private const string SKILLATTACK = "SkillAttack";
    private const string SKILLATTACK2 = "SkillAttack2";
    private const string CASTING = "Casting";
    private const string DAMAGE = "Damage";
    private const string DEATH = "Death";

    public void IdleAni()
    {
        anim.Play(IDLE);
    }

    public void RunAni()
    {
        anim.Play(MOVE);
    }

    public void AttackAni()
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
        EnemyAnimator.SetBool("Skill3", false);
        EnemyAnimator.SetBool("Skill4", false);
    }

    public void DamageAni()
    {
        anim.Play(DAMAGE);
    }

    public void DeathAni()
    {
        anim.Play(DEATH);
    }

    public void DamagePlayer()
    {
        AI.TakeDamage();
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

    public void VicePlanterCast()
    {
        EnemyAnimator.SetBool("Skill3", true);
    }

    public void WoodishSireAnimator()
    {
        EnemyAnimator.SetBool("Skill4", true);
    }

    public void SylvanStormAnim()
    {
        EnemyAnimator.SetBool("SylvanStorm", true);
    }

    public void ResetSkillAnimator()
    {
        EnemyAnimator.SetBool("Skill", false);
        EnemyAnimator.SetBool("Skill2", false);
        EnemyAnimator.SetBool("Skill3", false);
        EnemyAnimator.SetBool("Skill4", false);
        EnemyAnimator.SetBool("SylvanStorm", false);

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
        EnemyAnimator.SetBool("Skill3", false);
        EnemyAnimator.SetBool("Skill4", false);
    }

    public void IdleAnimator()
    {
        EnemyAnimator.SetBool("Moving", false);
    }

    public void RuneGolemSkillDamage()
    {
        if (AI.GetRuneGolemPhases[AI.GetPhaseIndex].GetRuneGolemAiStates[AI.GetStateArrayIndex].GetSkillIndex > -1)
        {
            enemyskills.PuckSkillDamageText(enemyskills.GetManager[AI.GetRuneGolemPhases[AI.GetPhaseIndex].GetRuneGolemAiStates[AI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                                                    enemyskills.GetManager[AI.GetRuneGolemPhases[AI.GetPhaseIndex].GetRuneGolemAiStates[AI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
        }
    }

    public void SkillRadiusDamage()
    {
        if (AI.GetRuneGolemPhases[AI.GetPhaseIndex].GetRuneGolemAiStates[AI.GetStateArrayIndex].GetSkillIndex > -1)
        {
            if (damageradius.GetIsInRadius)
            {
                damageradius.TakeRadiusDamage();
            }
            damageradius.GetIsInRadius = false;
        }
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

    public void ResetAutoAttackTime()
    {
        if (AI.GetPlayerTarget != null)
        {
            if (AI.GetStates != RuneGolemStates.SkillAnimation)
            {
                EnemyAnimator.SetBool("Attacking", false);
                ResetSkillAnimator();
                AI.GetAutoAttack = 0;
                AI.GetStates = RuneGolemStates.Attack;
            }
            else
            {
                AI.CheckTarget();
            }
        }
        else
        {
            AI.GetStates = RuneGolemStates.Idle;
            EnemyAnimator.SetBool("Attacking", false);
            ResetSkillAnimator();
        }
    }

    public void EndDamaged()
    {
        EnemyAnimator.SetBool("Damaged", false);

        AI.GetStates = AI.GetRuneGolemPhases[AI.GetPhaseIndex].GetRuneGolemAiStates[AI.GetStateArrayIndex].GetState;
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
        changeEnemyMaterial[0].ChangeToAlphaMaterial();

        StartCoroutine(changeEnemyMaterial[0].Fade());

        changeEnemyMaterial[1].ChangeToAlphaMaterial();

        StartCoroutine(changeEnemyMaterial[1].Fade());

        changeEnemyMaterial[2].ChangeEquipmentToAlphaMaterial();

        StartCoroutine(changeEnemyMaterial[2].EquipmentFade());

        changeEnemyMaterial[3].ChangeEquipmentToAlphaMaterial();

        StartCoroutine(changeEnemyMaterial[3].EquipmentFade());
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
