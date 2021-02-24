#pragma warning disable 0219
#pragma warning disable 0414
#pragma warning disable 0649
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
    //SylvanSpider Skills
    SilkyFang,
    //GolemFragment Skills
    Slam,
    GaiasProwess,
    //RockSpirit Skills
    Gnaw,
    Slag,
    //Puck Skills
    SylvanBlessing,
    SylvanFury,
    SylvanStorm,
    VicePlanter,
    WoodishSire,
    //RuneGolem Skills
    Uplift,
    EarthHammer,
    SmashWave,
    //SylvanDiety Skills
    LuxAmplificationOne,
    Light,
    LuxAmplificationTwo,
    LuxSecundus,
    LuxTertium,
    SummonLuxOrbs,
    //FungusLord Skills
    ConfusionBreath,
    Punch,
    Harrow,
    //Shared Beetle Skills
    Smash,
    //AmberBeetle Skills
    Shock,
    //RubyBeetle Skills
    Burn,
    //EmeraldBeetle Skills
    Wind,
    //AmythestBeetle Skills
    Dark
};

public enum Status { NONE, DamageOverTime, HealthRegen, Stun, Sleep, Haste, Doom, StrengthUP, DefenseUP, IntelligenceUP, StrengthDOWN, DefenseDOWN,
                     IntelligenceDOWN, StrengthAndCriticalUP, DefenseAndIntelligenceUP, T, TE, TES, TEST, Slowed, E, ES, EST, TS, ESS, SS, Confusion };

public enum EnemyElement { NONE, Fire, Water, Wind, Earth, Light, Dark, Magic };

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
    private Transform StatusIconTrans = null, SkillParticleParent = null;
    
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

    public Transform GetSkillParticleParent
    {
        get
        {
            return SkillParticleParent;
        }
        set
        {
            SkillParticleParent = value;
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
    private RuneGolem runeGolemAI = null;

    [SerializeField]
    private SylvanDiety SylvanDietyAI = null;

    [SerializeField]
    private EnemySkillBar skillBar;

    [SerializeField]
    private DamageRadius damageRadius = null;

    [SerializeField]
    private PuckDamageRadius puckDamageRadius = null;

    [SerializeField]
    private RuneGolemDamageRadius runeGolemDamageRadius = null;

    [SerializeField]
    private SylvanDietyDamageRadius SylvanDietyDamageRadius = null;

    [SerializeField]
    private Health health;

    private Quaternion rotation;

    [SerializeField]
    private bool ActiveSkill, DisruptedSkill;

    private bool IsRotating;

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

    public bool GetIsRotating
    {
        get
        {
            return IsRotating;
        }
        set
        {
            IsRotating = value;
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
                    case (Skill.VicePlanter):
                        VicePlanter(GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetCastTime, 
                                    GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    case (Skill.SylvanStorm):
                        SylvanStorm(GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    case (Skill.WoodishSire):
                        WoodishSire(GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                                    GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                        #endregion
                }
            }
            else if(runeGolemAI != null)
            {
                switch (skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkills)
                {
                    #region Rune Golem Skills
                    case (Skill.Uplift):
                        Uplift(GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    case (Skill.SmashWave):
                        SmashWave(GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    case (Skill.EarthHammer):
                        EarthHammer(GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                        #endregion
                }
            }
            else if(SylvanDietyAI != null)
            {
                switch (skills[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkills)
                {
                    #region SylvanDiety Skills
                    case (Skill.Light):
                        Lux(GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    case (Skill.LuxAmplificationOne):
                        SylvanDietyLuxAmplify();
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

                    #region Sylvan Spider Skills
                    case (Skill.SilkyFang):
                        SilkyFang(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                                  GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetAttackRange,
                                  GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region Fungus Lord Skills
                    case (Skill.ConfusionBreath):
                        ConfusionBreath();
                        break;
                    case (Skill.Punch):
                        Punch(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                           GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                           new Vector2(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                           GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                           GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    case (Skill.Harrow):
                        Harrow(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                           GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                           new Vector2(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                           GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                           GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region Shared Beetle Skills
                    case (Skill.Smash):
                        Smash(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                                  GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetAttackRange,
                                  GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region AmberBeetle Skill
                    case (Skill.Shock):
                        Shock(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region EmeraldBeetle Skill
                    case (Skill.Wind):
                        Wind(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region RubyBeetle Skill
                    case (Skill.Burn):
                        Burn(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime,
                            new Vector2(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY),
                            GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
                        break;
                    #endregion

                    #region AmythestBeetle Skill
                    case (Skill.Dark):
                        Dark(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
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
                                                                transform.position.x, transform.position.y + 0.7f, transform.position.z);

            GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.SetParent(gameObject.transform);
        }
    }

    public void UseSylvanBlessing()
    {
        if (puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex > -1 && !puckAI.GetIsMovingToPosition)
        {
            SylvanBlessing(GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusDuration,
                           GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
        }
    }

    public void InvokeSylvanBlessing()
    {
        if (puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex > -1 && !puckAI.GetIsMovingToPosition)
        {
            BossStatus();

            ActiveSkill = false;
        }
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
        DisableRadius();

        ActiveSkill = false;
    }
    #endregion

    #region Fungi Bump
    public void FungiBump(int potency, float attackRange, string skillname)
    {
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        if (enemyAI.GetPlayerTarget != null)
        {
            FungiBumpAnimation();
        }
        ActiveSkill = false;
    }
    #endregion

    #region Silky Fang
    public void SilkyFang(int potency, float attackRange, string skillname)
    {
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        if (enemyAI.GetPlayerTarget != null)
        {
            AnimatorSkill1();
        }
        ActiveSkill = false;
    }
    #endregion

    #region Confusion Breath
    public void ConfusionBreath()
    {
        if (enemyAI.GetPlayerTarget != null)
        {
            AnimatorSkill1();
        }
        ActiveSkill = false;
    }
    #endregion

    #region Punch
    public void Punch(int potency, float castTime, Vector2 sizeDelta, string skillname)
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

            Invoke("InvokePunch", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokePunch()
    {
        DisableRadius();

        ActiveSkill = false;
    }
    #endregion

    #region Harrow
    public void Harrow(int potency, float castTime, Vector2 sizeDelta, string skillname)
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
            enemyAI.GetAnimation.Skill3Animator();

            damageRadius.CheckIfPlayerIsInCircleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.SetCircleColliderSize());

            DisableRadiusImage();

            if(settings.UseParticleEffects)
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetHarrowParticle();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                    transform.position.x, transform.position.y + 2f, transform.position.z);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.localScale = new Vector3(1, 1, 1);
            }

            Invoke("InvokeHarrow", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokeHarrow()
    {
        DisableRadius();

        ActiveSkill = false;
    }
    #endregion

    #region Vice Planter
    private void VicePlanter(float castTime, string skillname)
    {
        PuckSpellCastingAnimation();

        skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            PuckAnimatorSkill3();
        }
    }

    public void InvokeConfusionBreath()
    {
        if(settings.UseParticleEffects)
        {
            var ConfusionBreath = ObjectPooler.Instance.GetConfusionBreathParticle();

            ConfusionBreath.SetActive(true);

            ConfusionBreath.transform.SetParent(skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticleParent);

            ConfusionBreath.transform.position = new Vector3(skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticleParent.position.x,
                                                             skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticleParent.position.y,
                                                             skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticleParent.position.z);

            ConfusionBreath.transform.rotation = skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticleParent.rotation;

            ConfusionBreath.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void VicePlanterParticle()
    {
        if (settings.UseParticleEffects)
        {
            var VicePlanterParticles = ObjectPooler.Instance.GetVicePlanterParticle();

            VicePlanterParticles.SetActive(true);

            VicePlanterParticles.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z);
        }
    }

    public void InvokeVicePlanter()
    {
        if(puckAI.GetPhaseIndex == 2)
        {
            puckAI.SpawnAdds();
            puckAI.DisableMushroomObjs();
        }
        else
        {
            puckAI.EnlargePoisonMushrooms();
        }
    }
    #endregion

    #region Sylvan Storm
    public void SylvanStorm(int potency, float castTime, Vector2 sizeDelta, string skillname)
    {
        PuckSpellCastingAnimation();

        skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        sizeDelta = new Vector2(skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                                skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY);

        skillBar.GetCharacter = character;

        UseSkillBar();

        EnablePuckRadius();
        EnablePuckRadiusImage();

        rotation = this.transform.rotation;

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            if (puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex > -1)
            {
                SylvanStormAnimation();

                puckDamageRadius.CheckIfPlayerIsInCircleRadius(puckDamageRadius.GetDamageShape.transform.position, puckDamageRadius.SetCircleColliderSize());

                DisablePuckRadiusImage();

                if (settings.UseParticleEffects)
                {
                    GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle =
                                                                                                    ObjectPooler.Instance.GetSylvanStormParticle();

                    GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

                    GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position =
                                                                                new Vector3(puckAI.GetSwordObj.transform.position.x, puckAI.GetSwordObj.transform.position.y +
                                                                                            0.2f, puckAI.GetSwordObj.transform.position.z);

                    GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.SetParent(
                                                                                                                            puckAI.GetSwordObj.gameObject.transform);

                    GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.localScale =
                                                                                                                                                    new Vector3(1, 1, 1);
                }
                Invoke("InvokeSylvanStorm", skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
            }
        }
    }

    public void SetRotationToTrue()
    {
        IsRotating = true;
    }

    public void SetRotationToFalse()
    {
        IsRotating = false;
    }

    public void SylvanStormRotation()
    {
        this.transform.Rotate(0, 2500 * Time.deltaTime, 0);
    }

    public void EndRotation()
    {
        IsRotating = false;

        this.transform.rotation = rotation;
    }

    private void InvokeSylvanStorm()
    {
        DisablePuckRadius();

        ActiveSkill = false;
    }
    #endregion

    #region WoodishSire
    public void WoodishSire(int potency, string skillname)
    {
        skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;

        WoodishSireAnimation();
    }
    #endregion

    #region Uplift
    private void Uplift(int potency, float castTime, Vector2 sizeDelta, string skillname)
    {
        RuneGolemSpellCastingAnimation();

        skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        sizeDelta = new Vector2(skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                                skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY);

        EnableRuneGolemRadius();
        EnableRuneGolemRadiusImage();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            if (runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex > -1)
            {
                RuneGolemUpliftSkillAnimation();

                runeGolemDamageRadius.CheckIfPlayerIsInCircleRadius(runeGolemDamageRadius.GetDamageShape.transform.position, runeGolemDamageRadius.SetCircleColliderSize());

                DisableRuneGolemRadiusImage();
                DisableRuneGolemRadius();
            }
        }
    }

    public void InvokeUpliftAnimation()
    {
        if (settings.UseParticleEffects)
        {
            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle =
                                                                                                                              ObjectPooler.Instance.GetUpliftParticle();

            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = 
                                                       new Vector3(character.transform.position.x, character.transform.position.y + 0.3f, character.transform.position.z);
        }

        ActiveSkill = false;
    }
    #endregion

    #region EarthHammer
    private void EarthHammer(int potency, float castTime, string skillname)
    {
        RuneGolemSpellCastingAnimation();

        skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            if (runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex > -1)
            {
                RuneGolemEarthHammerSkillAnimation();
            }
        }
    }

    public void InvokeEarthHammerAnimation()
    {
        if (settings.UseParticleEffects)
        {
            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle =
                                                                                                                              ObjectPooler.Instance.GetEarthHammerParticle();

            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

            GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position =
                                                       new Vector3(runeGolemAI.GetBossPosition.position.x, runeGolemAI.GetBossPosition.position.y, runeGolemAI.GetBossPosition.position.z);
        }

        ActiveSkill = false;
    }
    #endregion

    #region SmashWave
    private void SmashWave(int potency, float castTime, Vector2 sizeDelta, string skillname)
    {
        RuneGolemSpellCastingAnimation();

        skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        sizeDelta = new Vector2(skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX,
                                skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY);

        EnableRuneGolemRadius();
        EnableRuneGolemRadiusImage();

        if (skillBar.GetFillImage.fillAmount >= 1)
        {
            RuneGolemSmashWaveSkillAnimation();

            runeGolemDamageRadius.CheckIfPlayerIsInRectangleRadius(runeGolemDamageRadius.GetDamageShape.transform.position, new Vector3(
                                                          GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetShapeSize.x,
                                                          GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetShapeSize.y, 3f),
                                                          character.transform.rotation);

            DisableRuneGolemRadiusImage();

            Invoke("InvokeSmashWave", skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    public void InvokeSmashWave()
    {
        if (settings.UseParticleEffects)
        {
            var p = Instantiate(skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle,
                                character.transform.position, character.transform.rotation);

            p.SetActive(true);

            Vector3 Trans = new Vector3(character.transform.position.x, character.transform.position.y + 0.5f, character.transform.position.z);

            p.transform.position = Trans + character.transform.forward * 2f;

            Destroy(p, 2f);
        }
        DisableRuneGolemRadius();

        ActiveSkill = false;
    }
    #endregion

    #region Shock
    public void Shock(int potency, float castTime, Vector2 sizeDelta, string skillname)
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

            damageRadius.CheckIfPlayerIsInCircleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.SetCircleColliderSize());

            DisableRadiusImage();

            if (settings.UseParticleEffects)
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetShockParticle();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                    transform.position.x, transform.position.y + 0.2f, transform.position.z);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.localScale = new Vector3(1, 1, 1);
            }

            Invoke("InvokeShock", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokeShock()
    {
        DisableRadius();

        ActiveSkill = false;
    }
    #endregion

    #region Wind
    public void Wind(int potency, float castTime, Vector2 sizeDelta, string skillname)
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

            damageRadius.CheckIfPlayerIsInCircleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.SetCircleColliderSize());

            DisableRadiusImage();

            if (settings.UseParticleEffects)
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetWindParticle();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                    transform.position.x, transform.position.y + 0.2f, transform.position.z);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.localScale = new Vector3(1, 1, 1);
            }

            Invoke("InvokeWind", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokeWind()
    {
        DisableRadius();

        ActiveSkill = false;
    }
    #endregion

    #region Burn
    public void Burn(int potency, float castTime, Vector2 sizeDelta, string skillname)
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

            damageRadius.CheckIfPlayerIsInCircleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.SetCircleColliderSize());

            DisableRadiusImage();

            if (settings.UseParticleEffects)
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetBurnParticle();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                    transform.position.x, transform.position.y + 0.2f, transform.position.z);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.localScale = new Vector3(1, 1, 1);
            }

            Invoke("InvokeBurn", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokeBurn()
    {
        DisableRadius();

        ActiveSkill = false;
    }
    #endregion

    #region Dark
    public void Dark(int potency, float castTime, Vector2 sizeDelta, string skillname)
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

            damageRadius.CheckIfPlayerIsInCircleRadius(damageRadius.GetDamageShape.transform.position, damageRadius.SetCircleColliderSize());

            DisableRadiusImage();

            if (settings.UseParticleEffects)
            {
                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetDarkParticle();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                    transform.position.x, transform.position.y + 0.2f, transform.position.z);

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.localScale = new Vector3(1, 1, 1);
            }

            Invoke("InvokeDark", skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetApplySkill);
        }
    }

    private void InvokeDark()
    {
        DisableRadius();

        ActiveSkill = false;
    }
    #endregion

    #region Smash
    public void Smash(int potency, float attackRange, string skillname)
    {
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency = potency;
        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        if (enemyAI.GetPlayerTarget != null)
        {
            AnimatorSkill1();
        }
        ActiveSkill = false;
    }
    #endregion

    #region LuxAmplification
    public void LuxAmplification(float Duration, string skillname)
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

    public void UseLuxAmplification()
    {
        LuxAmplification(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusDuration,
                         GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
    }

    public void InvokeLuxAmplification()
    {
        EnemyStatus();
    }
    #endregion

    #region Lux
    public void Lux(int potency, float castTime, string skillname)
    {
        SylvanDietySpellCastingAnimation();

        skills[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime = castTime;

        skills[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillname;

        skillBar.GetCharacter = character;

        UseSkillBar();

        if (skillBar.GetFillImage.fillAmount >= 1 && SylvanDietyAI.GetPlayerTarget != null)
        {
            SylvanDietySkillCast();

            if (settings.UseParticleEffects)
            {
                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle = ObjectPooler.Instance.GetLightParticle();

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.SetActive(true);

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.position = new Vector3(
                                                                                                                     SylvanDietyAI.GetPlayerTarget.transform.position.x,
                                                                                                                     SylvanDietyAI.GetPlayerTarget.transform.position.y + 0.7f,
                                                                                                                     SylvanDietyAI.GetPlayerTarget.transform.position.z);

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.SetParent(SylvanDietyAI.GetPlayerTarget.transform);

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillParticle.transform.localScale = new Vector3(1, 1, 1);
            }
            ActiveSkill = false;

            SylvanDietySkillDamageText(GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                            GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);
        }
    }
    #endregion

    private void UseSkillBar()
    {
        skillBar.gameObject.SetActive(true);
        skillBar.ToggleCastBar();
    }

    private void SpellCastingAnimation()
    {
        enemyAI.GetAnimation.CastingAni();
    }

    private void PuckSpellCastingAnimation()
    {
        puckAI.GetAnimation.AnimatorCasting();
    }

    private void RuneGolemSpellCastingAnimation()
    {
        runeGolemAI.GetAnimation.AnimatorCasting();
    }

    private void SylvanDietySpellCastingAnimation()
    {
        SylvanDietyAI.GetAnimation.AnimatorCasting();
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
        if(puckAI.GetPlayerTarget != null)
        puckAI.GetAnimation.SkillAnimator();
    }

    private void PuckAnimatorSkill3()
    {
        puckAI.GetAnimation.VicePlanterCast();
    }

    private void WoodishSireAnimation()
    {
        puckAI.GetAnimation.WoodishSireAnimator();
    }

    private void SylvanStormAnimation()
    {
        puckAI.GetAnimation.SylvanStormAnim();
    }

    private void RuneGolemUpliftSkillAnimation()
    {
        runeGolemAI.GetAnimation.SkillAnimator();
    }

    private void RuneGolemSmashWaveSkillAnimation()
    {
        runeGolemAI.GetAnimation.Skill2Animator();
    }

    private void RuneGolemEarthHammerSkillAnimation()
    {
        runeGolemAI.GetAnimation.EarthHammerSkillAnimator();
    }

    private void SylvanDietySkillCast()
    {
        SylvanDietyAI.GetAnimation.SkillAnimator();
    }

    private void SylvanDietyLuxAmplify()
    {
        SylvanDietyAI.GetAnimation.Skill2Animator();
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

    public void DisablePuckRadiusImage()
    {
        foreach (Image r in puckDamageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = false;
        }
    }

    public void EnablePuckRadiusImage()
    {
        foreach (Image r in puckDamageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = true;
        }
    }

    public void DisableRuneGolemRadiusImage()
    {
        foreach (Image r in runeGolemDamageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = false;
        }
    }

    public void EnableRuneGolemRadiusImage()
    {
        foreach (Image r in runeGolemDamageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = true;
        }
    }

    public void DisableSylvanDietyRadiusImage()
    {
        foreach (Image r in SylvanDietyDamageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = false;
        }
    }

    public void EnableSylvanDietyRadiusImage()
    {
        foreach (Image r in SylvanDietyDamageRadius.GetComponentsInChildren<Image>())
        {
            r.enabled = true;
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

    public void DisablePuckRadius()
    {
        puckDamageRadius.ResetLocalScale();
        puckDamageRadius.ResetSizeDelta();

        puckDamageRadius.enabled = false;
    }

    public void EnablePuckRadius()
    {
        puckDamageRadius.enabled = true;
    }

    public void DisableRuneGolemRadius()
    {
        runeGolemDamageRadius.ResetLocalScale();
        runeGolemDamageRadius.ResetSizeDelta();

        runeGolemDamageRadius.enabled = false;
    }

    public void EnableRuneGolemRadius()
    {
        runeGolemDamageRadius.enabled = true;
    }

    public void DisableSylvanDietyRadius()
    {
        SylvanDietyDamageRadius.ResetLocalScale();
        SylvanDietyDamageRadius.ResetSizeDelta();

        SylvanDietyDamageRadius.enabled = false;
    }

    public void EnableSylvanDietyRadius()
    {
        SylvanDietyDamageRadius.enabled = true;
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

    public TextMeshProUGUI SylvanDietyBossStatus()
    {
        var StatusEffectText = ObjectPooler.Instance.GetEnemyStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetTextHolder.transform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].
                                                                                          GetStatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

        BossStatusEffectSkillTextTransform();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI PlayerStatus()
    {
        var StatusEffectText = ObjectPooler.Instance.GetPlayerStatusText();

        if (enemyAI.GetPlayerTarget.GetComponent<Character>().GetIsImmuneToStatusEffects)
        {
            StatusEffectText.SetActive(true);

            StatusEffectText.transform.SetParent(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusEffectHolder.transform, false);

            StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].
                                                                                              GetStatusEffectName + "\n" + "<size=12> <#EFDFB8>" + "(IMMUNE!)";

            StatusEffectText.GetComponentInChildren<Image>().sprite = GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;
        }
        else
        {
            StatusEffectText.SetActive(true);

            StatusEffectText.transform.SetParent(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusEffectHolder.transform, false);

            StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].
                                                                                              GetStatusEffectName;

            StatusEffectText.GetComponentInChildren<Image>().sprite = GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

            StatusEffectSkillTextTransform();
        }

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

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().CheckStatusEffect();
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

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().GetKeyInput =
                enemyAI.GetAiStates[character.GetComponent<EnemyAI>().GetStateArrayIndex].GetSkillIndex;

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().EnemyInput();

                GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().CheckStatusEffects();
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

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().GetKeyInput =
                character.GetComponent<Puck>().GetPhases[character.GetComponent<Puck>().GetPhaseIndex].GetBossAiStates[character.GetComponent<Puck>().GetStateArrayIndex].GetSkillIndex;

                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().EnemyInput();
                GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().CheckStatusEffects();
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

    public void SylvanDietyBossStatusEffectSkillTextTransform()
    {
        if (!GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.activeInHierarchy)
        {
            if (GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>())
            {
                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon = ObjectPooler.Instance.GetPlayerStatusIcon();

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.SetActive(true);

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.transform.SetParent(
                    this.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIconTrans.transform, false);

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<Image>().sprite =
                    this.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().GetEffectStatus =
                    (EffectStatus)this.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatus;

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().GetEnemyTarget = enemy;
                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<StatusIcon>().EnemyInput();
            }
            else
            {
                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.SetActive(true);

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.transform.SetParent(
                    this.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIconTrans.transform, false);

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect =
                    (StatusEffect)this.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatus;

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<Image>().sprite =
                    this.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusSprite;

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().GetKeyInput =
                character.GetComponent<SylvanDiety>().GetSylvanDietyPhases[character.GetComponent<SylvanDiety>().GetPhaseIndex].GetSylvanDietyBossAiStates[character.GetComponent<SylvanDiety>().GetStateArrayIndex].GetSkillIndex;

                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().EnemyInput();
                GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetStatusIcon.GetComponent<EnemyStatusIcon>().CheckStatusEffects();
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

    private TextMeshProUGUI ReflectedDamage()
    {
        float ReflectedValue = 0;

        if(GameManager.Instance.GetKnight.activeInHierarchy)
        {
            if (puckAI != null)
            {
                ReflectedValue = 0.01f * puckAI.GetPlayerTarget.GetComponent<Character>().MaxHealth;
            }
            if (enemyAI != null)
            {
                ReflectedValue = 0.10f * enemyAI.GetPlayerTarget.GetComponent<Character>().MaxHealth;
            }
            if (runeGolemAI != null)
            {
                ReflectedValue = 0.01f * enemyAI.GetPlayerTarget.GetComponent<Character>().MaxHealth;
            }
        }
        else if(GameManager.Instance.GetToadstool.activeInHierarchy)
        {
            if (enemyAI != null)
            {
                ReflectedValue = 0.05f * enemyAI.GetPlayerTarget.GetComponent<Character>().MaxHealth;
            }
            if (puckAI != null)
            {
                ReflectedValue = 0.01f * puckAI.GetPlayerTarget.GetComponent<Character>().MaxHealth;
            }
            if (runeGolemAI != null)
            {
                ReflectedValue = 0.01f * enemyAI.GetPlayerTarget.GetComponent<Character>().MaxHealth;
            }
            if(SylvanDietyAI != null)
            {
                ReflectedValue = 0.01f * enemyAI.GetPlayerTarget.GetComponent<Character>().MaxHealth;
            }
        }

        Mathf.Round(ReflectedValue);

        var Damagetext = ObjectPooler.Instance.GetEnemyDamageText();

        Damagetext.SetActive(true);

        Damagetext.transform.SetParent(GetComponentInChildren<Health>().GetDamageTextParent.transform, false);

        GetComponentInChildren<Health>().ModifyHealth(-(int)ReflectedValue);

        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + (int)ReflectedValue;

        return Damagetext.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI SkillDamageText(int potency, string skillName)
    {
        var Target = enemyAI.GetPlayerTarget;

        if (Target == null)
        {
            return null;
        }

        skills[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillName;

        var DamageTxt = ObjectPooler.Instance.GetPlayerDamageText();

        DamageTxt.SetActive(true);

        DamageTxt.transform.SetParent(GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetTextHolder.transform, false);

        CreateHitParticleEffect();

        potency = GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetEnemyElement == EnemyElement.Magic ? 
                  character.CharacterIntelligence + GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency : 
                  character.CharacterStrength + GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency;

        float Critical = character.GetCriticalChance;

        if(Target.GetComponent<Health>().GetIsImmune)
        {
            DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=25>" + "0";
        }
        else
        {
            #region CriticalHitCalculation
            if (Random.value * 100 <= Critical)
            {
                float CritCalc = potency * 1.25f;

                Mathf.Round(CritCalc);

                if ((int)CritCalc - Target.GetComponent<Character>().CharacterDefense <= 0)
                {
                    Target.GetComponent<Health>().ModifyHealth(-1);

                    DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + "1";
                }
                else
                {
                    Target.GetComponent<Health>().ModifyHealth(-((int)CritCalc - Target.GetComponent<Character>().CharacterDefense));

                    DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + ((int)CritCalc -
                                                                               Target.GetComponent<Character>().CharacterDefense).ToString() + "!";
                }
            }
            else
            {
                if (potency - Target.GetComponent<Character>().CharacterDefense <= 0)
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

            if (!SkillsManager.Instance.GetActivatedSkill)
            {
                if (enemyAI.GetPlayerTarget.GetComponent<Animator>().GetFloat("Speed") < 1 && !enemyAI.GetPlayerTarget.GetComponent<Animator>().GetBool("Attacking") &&
                    !enemyAI.GetPlayerTarget.GetComponent<PlayerAnimations>().GetAnimator.GetBool("Damaged"))
                {
                    enemyAI.GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
                }
            }
        }

        if (Target.GetComponent<Health>().GetReflectingDamage)
        {
            ReflectedDamage();
        }

        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI PuckSkillDamageText(int potency, string skillName)
    {
        skills[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillName;

        var DamageTxt = ObjectPooler.Instance.GetPlayerDamageText();

        DamageTxt.SetActive(true);

        DamageTxt.transform.SetParent(GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetTextHolder.transform, false);

        var Target = puckAI.GetPlayerTarget;

        CreatePuckHitParticleEffect();

        float Critical = character.GetCriticalChance;

        if(Target == null)
        {
            return null;
        }
        else
        {
            if(Target.GetComponent<Health>().GetIsImmune)
            {
                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=25>" + "0";
            }
            else
            {
                #region CriticalHitCalculation
                if (Random.value * 100 <= Critical)
                {
                    float CritCalc = potency * 1.25f;

                    Mathf.Round(CritCalc);

                    if ((int)CritCalc - Target.GetComponent<Character>().CharacterDefense < 0)
                    {
                        Target.GetComponent<Health>().ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + "1";
                    }
                    else
                    {
                        Target.GetComponent<Health>().ModifyHealth
                                                                 (-((int)CritCalc - Target.GetComponent<Character>().CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + ((int)CritCalc -
                                                                                   Target.GetComponent<Character>().CharacterDefense).ToString() + "!";
                    }
                }
                else
                {
                    if (potency - Target.GetComponent<Character>().CharacterDefense < 0)
                    {
                        Target.GetComponent<Health>().ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " " + "1";
                    }
                    else
                    {
                        Target.GetComponent<Health>().ModifyHealth(-(potency - Target.GetComponent<Character>().CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " " +
                                                                                   (potency - Target.GetComponent<Character>().CharacterDefense).ToString();
                    }
                }
                #endregion
            }

            if (!SkillsManager.Instance.GetActivatedSkill)
            {
                if (puckAI.GetPlayerTarget.GetComponent<Animator>().GetFloat("Speed") < 1)
                {
                    puckAI.GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
                }
            }
        }

        if(Target.GetComponent<Health>().GetReflectingDamage)
        {
            ReflectedDamage();
        }

        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI RuneGolemSkillDamageText(int potency, string skillName)
    {
        skills[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillName;

        var DamageTxt = ObjectPooler.Instance.GetPlayerDamageText();

        DamageTxt.SetActive(true);

        DamageTxt.transform.SetParent(GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetTextHolder.transform, false);

        var Target = runeGolemAI.GetPlayerTarget;

        CreateRuneGolemHitParticleEffect();

        float Critical = character.GetCriticalChance;

        if (Target == null)
        {
            return null;
        }
        else
        {
            if (Target.GetComponent<Health>().GetIsImmune)
            {
                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=25>" + "0";

                foreach (StatusIcon si in GameManager.Instance.GetBuffStatusIconHolder.GetComponentsInChildren<StatusIcon>())
                {
                    if (si.GetEffectStatus == EffectStatus.EarthenProtection)
                    {
                        si.RemoveEarthenProtection();
                    }
                }
            }
            else
            {
                #region CriticalHitCalculation
                if (Random.value * 100 <= Critical)
                {
                    float CritCalc = potency * 1.25f;

                    Mathf.Round(CritCalc);

                    if ((int)CritCalc - Target.GetComponent<Character>().CharacterDefense < 0)
                    {
                        Target.GetComponent<Health>().ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + "1";
                    }
                    else
                    {
                        Target.GetComponent<Health>().ModifyHealth
                                                                 (-((int)CritCalc - Target.GetComponent<Character>().CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + ((int)CritCalc -
                                                                                   Target.GetComponent<Character>().CharacterDefense).ToString() + "!";
                    }
                }
                else
                {
                    if (potency - Target.GetComponent<Character>().CharacterDefense < 0)
                    {
                        Target.GetComponent<Health>().ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " " + "1";
                    }
                    else
                    {
                        Target.GetComponent<Health>().ModifyHealth(-(potency - Target.GetComponent<Character>().CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " " +
                                                                                   (potency - Target.GetComponent<Character>().CharacterDefense).ToString();
                    }
                }
                #endregion
            }

            if (!SkillsManager.Instance.GetActivatedSkill)
            {
                if (runeGolemAI.GetPlayerTarget.GetComponent<Animator>().GetFloat("Speed") < 1)
                {
                    runeGolemAI.GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
                }
            }
        }

        if (Target.GetComponent<Health>().GetReflectingDamage)
        {
            ReflectedDamage();
        }

        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI SylvanDietySkillDamageText(int potency, string skillName)
    {
        skills[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName = skillName;

        var DamageTxt = ObjectPooler.Instance.GetPlayerDamageText();

        DamageTxt.SetActive(true);

        DamageTxt.transform.SetParent(GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetTextHolder.transform, false);

        var Target = SylvanDietyAI.GetPlayerTarget;

        CreateSylvanDietyHitParticleEffect();

        float Critical = character.GetCriticalChance;

        if (Target == null)
        {
            return null;
        }
        else
        {
            if (Target.GetComponent<Health>().GetIsImmune)
            {
                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=25>" + "0";
            }
            else
            {
                #region CriticalHitCalculation
                if (Random.value * 100 <= Critical)
                {
                    float CritCalc = potency * 1.25f;

                    Mathf.Round(CritCalc);

                    if ((int)CritCalc - Target.GetComponent<Character>().CharacterDefense < 0)
                    {
                        Target.GetComponent<Health>().ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + "1";
                    }
                    else
                    {
                        Target.GetComponent<Health>().ModifyHealth
                                                                 (-((int)CritCalc - Target.GetComponent<Character>().CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " </size>" + " " + "<size=35>" + ((int)CritCalc -
                                                                                   Target.GetComponent<Character>().CharacterDefense).ToString() + "!";
                    }
                }
                else
                {
                    if (potency - Target.GetComponent<Character>().CharacterDefense < 0)
                    {
                        Target.GetComponent<Health>().ModifyHealth(-1);

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " " + "1";
                    }
                    else
                    {
                        Target.GetComponent<Health>().ModifyHealth(-(potency - Target.GetComponent<Character>().CharacterDefense));

                        DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + skillName + " " +
                                                                                   (potency - Target.GetComponent<Character>().CharacterDefense).ToString();
                    }
                }
                #endregion
            }

            if (!SkillsManager.Instance.GetActivatedSkill)
            {
                if (SylvanDietyAI.GetPlayerTarget.GetComponent<Animator>().GetFloat("Speed") < 1)
                {
                    SylvanDietyAI.GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
                }
            }
        }

        if (Target.GetComponent<Health>().GetReflectingDamage)
        {
            ReflectedDamage();
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

            var hitParticle = ObjectPooler.Instance.GetHitParticle();

            hitParticle.SetActive(true);

            hitParticle.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 0.6f, Target.transform.position.z);

            hitParticle.transform.SetParent(Target.transform);
        }
    }

    private void CreatePuckHitParticleEffect()
    {
        if (settings.UseParticleEffects)
        {
            var Target = puckAI.GetPlayerTarget;

            var hitParticle = ObjectPooler.Instance.GetHitParticle();

            hitParticle.SetActive(true);

            hitParticle.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 0.6f, Target.transform.position.z);

            hitParticle.transform.SetParent(Target.transform);
        }
    }

    private void CreateRuneGolemHitParticleEffect()
    {
        if (settings.UseParticleEffects)
        {
            var Target = runeGolemAI.GetPlayerTarget;

            var hitParticle = ObjectPooler.Instance.GetHitParticle();

            hitParticle.SetActive(true);

            hitParticle.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 0.6f, Target.transform.position.z);

            hitParticle.transform.SetParent(Target.transform);
        }
    }

    private void CreateSylvanDietyHitParticleEffect()
    {
        if (settings.UseParticleEffects)
        {
            var Target = SylvanDietyAI.GetPlayerTarget;

            var hitParticle = ObjectPooler.Instance.GetHitParticle();

            hitParticle.SetActive(true);

            hitParticle.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 0.6f, Target.transform.position.z);

            hitParticle.transform.SetParent(Target.transform);
        }
    }
}