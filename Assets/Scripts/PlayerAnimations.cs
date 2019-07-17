using UnityEngine;

public class PlayerAnimations : MonoBehaviour
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

    public void IdleAnimation()
    {
        animator.SetFloat("Speed", 0);
        animator.SetBool("Attacking", false);
    }

    public void RunAnimation()
    {
        animator.SetFloat("Speed", 1);
    }

    public void AttackAnimation()
    {
        animator.SetBool("Attacking", true);

        animator.ResetTrigger("Damaged");
        animator.SetBool("Damaged", false);
    }

    public void EndAttackAnimation()
    {
        this.GetComponent<BasicAttack>().GetAutoAttackTime = 0;

        animator.ResetTrigger("Attacking");

        animator.SetBool("Attacking", false);
    }

    public void DamagedAnimation()
    {
        animator.SetBool("Damaged", true);
    }

    //Animation event placed at the end of the Damaged animation.
    public void EndDamagedAnimation()
    {
        animator.SetBool("Damaged", false);
    }

    public void DeathAnimation()
    {
        animator.SetBool("Attacking", false);
        animator.SetBool("Damaged", false);

        animator.SetBool("Dead", true);
    }

    public void PlayResurrectAnimation()
    {
        animator.SetBool("Dead", false);
        animator.SetBool("Ressurecting", true);
    }

    //Animation event placed at the end of the Resurrection animation.
    public void EndRessurectAnimation()
    {
        animator.SetBool("Ressurecting", false);
        this.GetComponent<PlayerController>().enabled = true;
        GameManager.Instance.GetIsDead = false;
        SkillsManager.Instance.ReactivateSkillButtons();
    }

    public void PlaySpellCastAnimation()
    {
        animator.SetBool("SpellCasting", true);

        animator.ResetTrigger("Damaged");
        animator.SetBool("Damaged", false);
    }

    public void PlayContinueCastAnimation()
    {
        if(this.gameObject.GetComponent<PlayerController>().GetMovement == Vector3.zero)
        {
            animator.SetBool("SpellCasting", false);
            animator.SetBool("ContinueCasting", true);

            animator.ResetTrigger("Damaged");
            animator.SetBool("Damaged", false);
        }
        else
        {
            animator.SetBool("SpellCasting", false);
            animator.SetBool("ContinueCasting", false);

            animator.ResetTrigger("Damaged");
            animator.SetBool("Damaged", false);
        }
    }

    public void EndSpellCastingAnimation()
    {
        animator.SetBool("ContinueCasting", false);
        animator.SetBool("Spellcasting", false);

        animator.ResetTrigger("Damaged");
        animator.SetBool("Damaged", false);
    }

    public void EndAllSpellcastingBools()
    {
        animator.ResetTrigger("SpellCasting");
        animator.ResetTrigger("ConinueCasting");

        animator.SetBool("SpellCasting", false);
        animator.SetBool("ContinueCasting", false);
    }

    public void PlaySkillAnimation()
    {
        animator.SetBool("Skill", true);
        animator.SetBool("Attacking", false);
        animator.ResetTrigger("Damaged");
        animator.SetBool("Damaged", false);
    }

    public void StormThrustAnimation()
    {
        animator.SetBool("StormThrust", true);
        animator.SetBool("Attacking", false);
        animator.ResetTrigger("Damaged");
        animator.SetBool("Damaged", false);
    }

    public void WhirlwindSlashAnimation()
    {
        animator.SetBool("WhirlwindSlash", true);
        animator.SetBool("Attacking", false);
        animator.ResetTrigger("Damaged");
        animator.SetBool("Damaged", false);
    }

    public void DealSkillDamage()
    {
        SkillsManager.Instance.GetSkills[SkillsManager.Instance.GetKeyInput].DamageSkillText();
    }

    public void EndSkillAnimation()
    {
        animator.SetBool("Skill", false);

        SkillsManager.Instance.GetActivatedSkill = false;
    }

    public void EndStormThrustAnimation()
    {
        animator.SetBool("StormThrust", false);
    }

    public void EndWhirlwindSlashAnimation()
    {
        animator.SetBool("WhirlwindSlash", false);

        SkillsManager.Instance.GetWhirlwind = false;
        SkillsManager.Instance.GetActivatedSkill = false;

        SkillsManager.Instance.GetSkills[SkillsManager.Instance.GetKeyInput].SetUpDamagePerimiter(SkillsManager.Instance.GetCharacter.transform.position, 2);
    }

    public void PlayFallSoundEffect()
    {
        SoundManager.Instance.FallSE();
    }
}
