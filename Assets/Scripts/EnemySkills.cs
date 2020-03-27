﻿#pragma warning disable 0219
#pragma warning disable 0414
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Skill
{   //MushroomMan Skills
    FungiBump,
    HealingCap,
    PoisonSpore,
    Regen,
    //Bee Skills
    StunningStinger,
    //Bunnykins Skill
    Hop,
    //GolemFragment Skills
    Slam,
    GaiasProwess,
    //RockSpirit Skills
    Gnaw,
    Slag,
    //MiniBosses:
    //Puck Skills
    SylvanBlessing,
    SylvanFury,
    SylvanStorm,
    VicePlanter,
    Touche,
    //VineGolem Skills
    EarthHammer,
    //MainBoss:
    //SylvanDiety Skills
    MagicDreams,
    Light
};

public enum Status { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste, Doom, StrengthUP, DefenseUP, IntelligenceUP, StrengthDOWN, DefenseDOWN,
                     IntelligenceDOWN, StrengthAndCriticalUP };

public enum EnemyElement { NONE, Fire, Water, Wind, Earth, Light, Dark };

[System.Serializable]
public class enemySkillManager
{
    [SerializeField]
    private Skill skills;

    [SerializeField]
    private EnemyElement enemyElement;

    [SerializeField]
    private Status status;

    [SerializeField] [Tooltip("The shape the skill will form when being cast. Enemy targets within its range will be hit.")]
    private Shapes shapes;

    [SerializeField] [Tooltip("Image of the status effect inflicted. Only apply if the skill will have a status effect.")]
    private Sprite StatusSprite = null;

    [SerializeField]
    private GameObject StatusIcon = null;

    [SerializeField]
    private Transform TextHolder;

    [SerializeField]
    private Transform StatusIconTrans = null;
    
    [SerializeField] [Tooltip("Text holder representing heal or damage.")]
    private GameObject DamageORHealText = null;

    [SerializeField] [Tooltip("The gameobject that will hold the status effect text.")]
    private GameObject StatusEffectHolder = null;

    [SerializeField]
    private GameObject SkillParticle;

    [SerializeField]
    private string StatusEffectName;

    [SerializeField]
    private string StatusDescription;

    [SerializeField]
    private string SkillName;

    [SerializeField]
    private float StatusDuration;

    [SerializeField]
    private float CastTime;

    [SerializeField] [Tooltip("The values used for the shape size if its set to CIRCLE")]
    private float SizeDeltaX, SizeDeltaY, ApplySkill;

    [SerializeField]
    private Vector3 ShapeSize;

    [SerializeField]
    public float AttackRange;

    [SerializeField][Tooltip("The amount of recovery or damage a target takes based on a regenerating or damage over time status.")]
    private int StatusEffectPotency;

    [SerializeField]
    private int Potency;

    [SerializeField]
    private bool IsBuff;

    public string GetSkillName
    {
        get
        {
            return SkillName;
        }
        set
        {
            SkillName = value;
        }
    }

    public float GetCastTime
    {
        get
        {
            return CastTime;
        }
        set
        {
            CastTime = value;
        }
    }

    public float GetSizeDeltaX
    {
        get
        {
            return SizeDeltaX;
        }
        set
        {
            SizeDeltaX = value;
        }
    }

    public float GetSizeDeltaY
    {
        get
        {
            return SizeDeltaY;
        }
        set
        {
            SizeDeltaY = value;
        }
    }

    public float GetAttackRange
    {
        get
        {
            return AttackRange;
        }
        set
        {
            AttackRange = value;
        }
    }

    public int GetPotency
    {
        get
        {
            return Potency;
        }
        set
        {
            Potency = value;
        }
    }

    public int GetStatusEffectPotency
    {
        get
        {
            return StatusEffectPotency;
        }
        set
        {
            StatusEffectPotency = value;
        }
    }
    
    public string GetStatusEffectName
    {
        get
        {
            return StatusEffectName;
        }
        set
        {
            StatusEffectName = value;
        }
    }

    public Skill GetSkills
    {
        get
        {
            return skills;
        }
        set
        {
            skills = value;
        }
    }

    public EnemyElement GetEnemyElement
    {
        get
        {
            return enemyElement;
        }
        set
        {
            enemyElement = value;
        }
    }

    public Status GetStatus
    {
        get
        {
            return status;
        }
        set
        {
            status = value;
        }
    }

    public Shapes GetShapes
    {
        get
        {
            return shapes;
        }
        set
        {
            shapes = value;
        }
    }

    public string GetStatusDescription
    {
        get
        {
            return StatusDescription;
        }
        set
        {
            StatusDescription = value;
        }
    }

    public float GetStatusDuration
    {
        get
        {
            return StatusDuration;
        }
        set
        {
            StatusDuration = value;
        }
    }

    public float GetApplySkill
    {
        get
        {
            return ApplySkill;
        }
        set
        {
            ApplySkill = value;
        }
    }

    public bool GetIsBuff
    {
        get
        {
            return IsBuff;
        }
        set
        {
            IsBuff = value;
        }
    }

    public GameObject GetSkillParticle
    {
        get
        {
            return SkillParticle;
        }
        set
        {
            SkillParticle = value;
        }
    }

    public Vector3 GetShapeSize
    {
        get
        {
            return ShapeSize;
        }
        set
        {
            ShapeSize = value;
        }
    }

    public Sprite GetStatusSprite
    {
        get
        {
            return StatusSprite;
        }
        set
        {
            StatusSprite = value;
        }
    }

    public GameObject GetStatusIcon
    {
        get
        {
            return StatusIcon;
        }
        set
        {
            StatusIcon = value;
        }
    }

    public GameObject GetDamageOrHealText
    {
        get
        {
            return DamageORHealText;
        }
        set
        {
            DamageORHealText = value;
        }
    }

    public GameObject GetStatusEffectHolder
    {
        get
        {
            return StatusEffectHolder;
        }
        set
        {
            StatusEffectHolder = value;
        }
    }

    public Transform GetTextHolder
    {
        get
        {
            return TextHolder;
        }
        set
        {
            TextHolder = value;
        }
    }

    public Transform GetStatusIconTrans
    {
        get
        {
            return StatusIconTrans;
        }
        set
        {
            StatusIconTrans = value;
        }
    }
}

public class EnemySkills : MonoBehaviour
{
    [SerializeField]
    private enemySkillManager[] skills;

    [SerializeField]
    private Character character;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private EnemyAI enemyAI = null;

    [SerializeField]
    private Puck puckAI = null;

    [SerializeField]
    private EnemySkillBar skillBar;

    [SerializeField]
    private DamageRadius damageRadius = null;

    [SerializeField]
    private PuckDamageRadius puckDamageRadius = null;

    [SerializeField]
    private Health health;

    [SerializeField]
    private bool ActiveSkill, DisruptedSkill;

    public enemySkillManager[] GetManager
    {
        get
        {
            return skills;
        }
        set
        {
            skills = value;
        }
    }

    public EnemySkillBar GetSkillBar
    {
        get
        {
            return skillBar;
        }
        set
        {
            skillBar = value;
        }
    }

    public bool GetActiveSkill
    {
        get
        {
            return ActiveSkill;
        }
        set
        {
            ActiveSkill = value;
        }
    }

    public bool GetDisruptedSkill
    {
        get
        {
            return DisruptedSkill;
        }
        set
        {
            DisruptedSkill = value;
        }
    }

    public void ChooseSkill(int value)
    {
        if(!ActiveSkill)
        {
            ActiveSkill = true;
            if(puckAI != null)
            {
                switch (skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkills)
                {
                    #region Puck Skills
                    case (Skill.SylvanBlessing):
                        PuckAnimatorSkill1();
                        break;
                        #endregion
                }
            }
            else
            {
                switch (skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkills)
                {
                    #region Mushroom Man Skills
                    case (Skill.PoisonSpore):
                        PoisonSpore(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    case (Skill.FungiBump):
                        FungiBump(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                                  GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetAttackRange,
                                  GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    case (Skill.HealingCap):
                        HealingCap(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    case (Skill.Regen):
                        Regen(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusDuration,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region Bee Skills
                    case (Skill.StunningStinger):
                        StunningStinger(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region Golem Fragment Skills
                    case (Skill.GaiasProwess):
                        AnimatorSkill1();
                        break;
                    case (Skill.Slam):
                        Slam(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region Rock Spirit Skills
                    case (Skill.Slag):
                        Slag(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region Bunnykins Skill
                    case (Skill.Hop):
                        Hop(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion
                }
            }
        }
    }

    #region HP Regen
    public void Regen(float castTime, float Duration, string skillname)
    {
        SpellCastingAnimation();

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            Invoke("InvokeRegen", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    public void InvokeRegen()
    {
        StatusEffectSkillTextTransform();

        ActiveSkill = false;
    }
    #endregion

    #region Sylvan Blessing
    public void SylvanBlessing(float Duration, string skillname)
    {
        skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        if (settings.UseParticleEffects)
        {
            GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetSylvanBlessingParticle();

            GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

            GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                transform.position.x, transform.position.y + 0.2f, transform.position.z);

            GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.SetParent(gameObject.transform);
        }
    }

    public void UseSylvanBlessing()
    {
        SylvanBlessing(GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusDuration,
                       GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
    }

    public void InvokeSylvanBlessing()
    {
        BossStatus();

        ActiveSkill = false;
    }
    #endregion

    #region Gaia's Prowess
    public void GaiasProwess(float Duration, string skillname)
    {
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        if (settings.UseParticleEffects)
        {
            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetGaiasProwessParticle();

            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                transform.position.x, transform.position.y + 0.2f, transform.position.z);

            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.SetParent(gameObject.transform);
        }
    }

    public void UseGaiasProwess()
    {
        GaiasProwess(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusDuration,
                     GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
    }

    public void InvokeGaiasProwess()
    {
        EnemyStatus();
    }
    #endregion

    #region Healing Spell
    public void HealingCap(int potency, float castTime, string skillname)
    {
        SpellCastingAnimation();

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        if(skillBar.GetFillImage.fillAmount >= 1)
        {
            Invoke("SkillHealText", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }  
    }

    private void InvokeHealingCap()
    {
        SkillHealText(skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency, 
                      skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
    }
    #endregion

    #region Poison Spore
    public void PoisonSpore(int potency, float castTime, Vector2 sizeDelta, string skillname)
    {
        SpellCastingAnimation();

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;

        sizeDelta = new Vector2(skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX, 
                                skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY);

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        EnableRadius();
        EnableRadiusImage();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            MushroomSporeAnimation();

            damageRadius.CheckIfPlayerIsInCircleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.SetCircleColliderSize());

            DisableRadiusImage();

            if(settings.UseParticleEffects)
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetPoisonSporeParticle();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                    transform.position.x, transform.position.y + 0.2f, transform.position.z);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.SetParent(gameObject.transform);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.localScale = new Vector3(1, 1, 1);
            }

            Invoke("InvokePoisonSpore", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokePoisonSpore()
    {
        DisableRadius();

        ActiveSkill = false;
    }
    #endregion

    #region Slam
    public void Slam(int potency, float castTime, Vector2 sizeDelta, string skillname)
    {
        AnimatorCastingAnimation();

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;

        sizeDelta = new Vector2(skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                                skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY);

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        EnableRadius();
        EnableRadiusImage();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            enemyAI.GetAnimation.Skill2Animator();

            damageRadius.CheckIfPlayerIsInRectangleRadius(damageRadius.GetDamageShape.transform.position, new Vector3(
                                                          GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetShapeSize.x,
                                                          GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetShapeSize.y, 1.7f),
                                                          character.transform.rotation);

            DisableRadiusImage();

            Invoke("InvokeSlam", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokeSlam()
    {
        if (settings.UseParticleEffects)
        {
            var SlamParticle = ObjectPooler.Instance.GetSlamParticle();

            SlamParticle.SetActive(true);

            Vector3 Trans = new Vector3(character.transform.position.x, character.transform.position.y + 0.3f, character.transform.position.z);

            SlamParticle.transform.position = Trans + character.transform.forward * 2.5f;

            Quaternion rot = SlamParticle.transform.rotation;

            rot.z = character.transform.rotation.y;
        }
        DisableRadius();
    }
    #endregion

    #region Slag
    public void Slag(int potency, float castTime, string skillname)
    {
        SpellCastingAnimation();

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        EnableRadius();
        EnableRadiusImage();

        if (skillBar.GetFillImage.fillAmount >= 1 && enemyAI.GetPlayerTarget != null)
        {
            enemyAI.GetAnimation.FungiBumpAnim();

            if (settings.UseParticleEffects)
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetSlagParticle();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                                                                     enemyAI.GetPlayerTarget.transform.position.x, 
                                                                                                                     enemyAI.GetPlayerTarget.transform.position.y + 0.7f, 
                                                                                                                     enemyAI.GetPlayerTarget.transform.position.z);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.SetParent(enemyAI.GetPlayerTarget.transform);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.localScale = new Vector3(1, 1, 1);
            }
            ActiveSkill = false;

            SkillDamageText(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
        }
    }
    #endregion

    #region Hop
    public void Hop(int potency, float castTime, Vector2 sizeDelta, string skillname)
    {
        SpellCastingAnimation();

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;

        sizeDelta = new Vector2(skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                                skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY);

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        EnableRadius();
        EnableRadiusImage();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            MushroomSporeAnimation();

            damageRadius.CheckIfPlayerIsInCircleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.SetCircleColliderSize());

            DisableRadiusImage();

            Invoke("InvokeHop", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokeHop()
    {
        DisableRadius();

        if (settings.UseParticleEffects)
        {
            var HopParticle = ObjectPooler.Instance.GetHopParticle();

            HopParticle.gameObject.SetActive(true);

            HopParticle.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        }

        ActiveSkill = false;
    }
    #endregion

    #region Stunning Stinger
    public void StunningStinger(int potency, float castTime, Vector2 sizeDelta, string skillname)
    {
        SpellCastingAnimation();

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;

        sizeDelta = new Vector2(skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                                skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY);

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        EnableRadius();
        EnableRadiusImage();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            enemyAI.GetAnimation.FungiBumpAnim();

            damageRadius.CheckIfPlayerIsInRectangleRadius(damageRadius.GetDamageShape.transform.position, new Vector3(
                                                          GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetShapeSize.x,
                                                          GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetShapeSize.y, 1.7f),
                                                          character.transform.rotation);

            DisableRadiusImage();

            Invoke("InvokeStunningStinger", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokeStunningStinger()
    {
        if (settings.UseParticleEffects)
        {
            var StingerParticle = ObjectPooler.Instance.GetHitParticle();

            StingerParticle.SetActive(true);

            Vector3 Trans = new Vector3(character.transform.position.x, character.transform.position.y + 0.5f, character.transform.position.z);

            StingerParticle.transform.position = Trans + character.transform.forward * 1f;

            StingerParticle.transform.SetParent(character.transform);
        }

        DisableRadius();

        ActiveSkill = false;
    }
    #endregion

    public void FungiBump(int potency, float attackRange, string skillname)
    {
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        float Distance = Vector3.Distance(character.transform.position, enemyAI.GetPlayerTarget.transform.position);

        if (enemyAI.GetPlayerTarget != null)
        {
            FungiBumpAnimation();
        }
        ActiveSkill = false;
    }

    private void UseSkillBar()
    {
        skillBar.gameObject.SetActive(true);
        skillBar.ToggleCastBar();
    }

    private void SpellCastingAnimation()
    {
        enemyAI.GetAnimation.CastingAni();
    }

    private void AnimatorCastingAnimation()
    {
        enemyAI.GetAnimation.AnimatorCasting();
    }

    private void FungiBumpAnimation()
    {
        enemyAI.GetAnimation.FungiBumpAnim();
    }

    private void MushroomSporeAnimation()
    {
        enemyAI.GetAnimation.SkillAtk2();
    }

    private void AnimatorSkill1()
    {
        enemyAI.GetAnimation.SkillAnimator();
    }

    private void AnimatorSkill2()
    {
        enemyAI.GetAnimation.Skill2Animator();
    }

    private void PuckAnimatorSkill1()
    {
        puckAI.GetAnimation.SkillAnimator();
    }

    public void DisableEnemySkillBar()
    {
        foreach (Image image in skillBar.GetComponentsInChildren<Image>())
        {
            image.enabled = false;
        }
        skillBar.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    public void EnableEnemySkillBar()
    {
        foreach (Image image in skillBar.GetComponentsInChildren<Image>())
        {
            image.enabled = true;
        }
        skillBar.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
    }

    public void DisableRadiusImage()
    {
        foreach (Image r in damageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = false;
        }
    }

    public void EnableRadiusImage()
    {
        foreach (Image r in damageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = true;
        }
    }

    public void DisableRadius()
    {
        switch(skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetShapes)
        {
            case (Shapes.Circle):
                damageRadius.ResetLocalScale();
                damageRadius.ResetSizeDelta();
                break;
            case (Shapes.Rectangle):
                damageRadius.ResetSizeDelta();
                break;
        }
        damageRadius.enabled = false;
    }

    public void EnableRadius()
    {
        damageRadius.enabled = true;
    }

    public TextMeshProUGUI EnemyStatus()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetTextHolder.transform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].
                                                                                          GetStatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

        StatusEffectSkillTextTransform();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI BossStatus()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetTextHolder.transform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].
                                                                                          GetStatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

        BossStatusEffectSkillTextTransform();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI PlayerStatus()
    {
        var StatusEffectText = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusEffectHolder.transform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].
                                                                                          GetStatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

        StatusEffectSkillTextTransform();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StatusEffectSkillTextTransform()
    {
        if(!GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.activeInHierarchy)
        {
            if (GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>())
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon = ObjectPooler.Instance.GetPlayerStatusIcon();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.SetActive(true);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.transform.SetParent(
                    this.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIconTrans.transform, false);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<Image>().sprite = 
                    this.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().GetEffectStatus = 
                    (EffectStatus)this.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatus;

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().GetEnemyTarget = enemy;
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().EnemyInput();
            }
            else
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.SetActive(true);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.transform.SetParent(
                    this.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIconTrans.transform, false);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = 
                    (StatusEffect)this.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatus;

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<Image>().sprite = 
                    this.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().EnemyInput();
            }
        }
        else
        {
            if (GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>())
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().EnemyInput();
            }
            else
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().EnemyInput();
            }
        }
    }

    public void BossStatusEffectSkillTextTransform()
    {
        if (!GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.activeInHierarchy)
        {
            if (GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>())
            {
                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon = ObjectPooler.Instance.GetPlayerStatusIcon();

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.SetActive(true);

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.transform.SetParent(
                    this.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIconTrans.transform, false);

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<Image>().sprite =
                    this.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().GetEffectStatus =
                    (EffectStatus)this.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatus;

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().GetEnemyTarget = enemy;
                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().EnemyInput();
            }
            else
            {
                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.SetActive(true);

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.transform.SetParent(
                    this.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIconTrans.transform, false);

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect =
                    (StatusEffect)this.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatus;

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<Image>().sprite =
                    this.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().EnemyInput();
            }
        }
        else
        {
            if (GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>())
            {
                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().EnemyInput();
            }
            else
            {
                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().EnemyInput();
            }
        }
    }

    public TextMeshProUGUI SkillDamageText(int potency, string skillName)
    {
        if(enemyAI.GetPlayerTarget == null)
        {
            return null;
        }

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillName;

        var DamageTxt = ObjectPooler.Instance.GetPlayerDamageText();

        DamageTxt.SetActive(true);

        DamageTxt.transform.SetParent(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetTextHolder.transform, false);

        var Target = enemyAI.GetPlayerTarget;

        CreateHitParticleEffect();

        float Critical = character.GetCriticalChance;

        #region CriticalHitCalculation
        if (Random.value * 100 <= Critical)
        {
            float CritCalc = potency * 1.25f;

            Mathf.Round(CritCalc);

            if((int)CritCalc - Target.GetComponent<Character>().CharacterDefense < 0)
            {
                Target.GetComponentInChildren<Health>().ModifyHealth(-1);

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + "1";
            }
            else
            {
                enemyAI.GetPlayerTarget.GetComponent<Health>().ModifyHealth
                                                         (-((int)CritCalc - Target.GetComponent<Character>().CharacterDefense));

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + ((int)CritCalc -
                                                                           Target.GetComponent<Character>().CharacterDefense).ToString() + "!";
            }
        }
        else
        {
            if (potency - Target.GetComponent<Character>().CharacterDefense < 0)
            {
                Target.GetComponentInChildren<Health>().ModifyHealth(-1);

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " " + "1";
            }
            else
            {
                Target.GetComponentInChildren<Health>().ModifyHealth(-(potency - Target.GetComponent<Character>().CharacterDefense));

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " " +
                                                                           (potency - Target.GetComponent<Character>().CharacterDefense).ToString();
            }    
        }
        #endregion

        if(!SkillsManager.Instance.GetActivatedSkill)
        {
            enemyAI.GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
        }

        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI SkillHealText(int potency, string skillName)
    {
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillName;

        var HealTxt = ObjectPooler.Instance.GetEnemyHealText();

        HealTxt.SetActive(true);

        var Critical = character.GetCriticalChance;

        ActiveSkill = false;

        #region CritChance
        if(character.CurrentHealth > 0)
        {
            if (Random.value * 100 <= Critical)
            {
                health.IncreaseHealth((skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency + 10) + character.CharacterIntelligence);

                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName + " " + "<size=20>" +
                                                                         (potency + character.CharacterIntelligence).ToString() + "!";

                enemy.GetLocalHealthInfo();
            }
            else
            {
                health.IncreaseHealth(skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency + character.CharacterIntelligence);

                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName + " " + 
                                                                         (potency + character.CharacterIntelligence).ToString();

                enemy.GetLocalHealthInfo();
            }
        }
        #endregion

        HealTxt.GetComponentInChildren<TextMeshProUGUI>().transform.SetParent(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetTextHolder.transform, false);

        return HealTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void CreateHitParticleEffect()
    {
        if(settings.UseParticleEffects)
        {
            var Target = enemyAI.GetPlayerTarget;

            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetHitParticle();

            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3();

            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                                Target.transform.position.x, Target.transform.position.y + 0.6f, Target.transform.position.z);

            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.SetParent(Target.transform);
        }
    }
}