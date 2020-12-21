#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Runtime.Serialization;

public class BasicAttack : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private CursorController cursorController;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Transform HealTextTransform;

    [SerializeField]
    private EquipmentMenu equipmentMenu;

    [SerializeField]
    private Equipment[] equipment = null;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private PlayerAnimations playerAnimations;

    [SerializeField] [Tooltip("Current targeted object. Keep this null!")]
    private Enemy Target = null;

    [SerializeField]
    private ParticleSystem HitParticle;

    [SerializeField]
    private GameObject StatusEffectIcon = null;

    [SerializeField]
    private Sprite BurningStatusEffectSprite = null, SlowStatusEffectSprite = null;

    private Transform StatusEffectIconTrans = null, TextHolder = null;

    [SerializeField]
    private PlayerElement playerElement;

    [SerializeField]
    private bool HasBurnStatusEffect, HasSlowStatusEffect, UsesIntelligenceForDamage, IgnoresDefense, IgnoresElements;

    [SerializeField]
    private float MouseRange, AttackRange, AttackDelay, AutoAttackTime, HideStatsDistance;

    private float AttackDistance;

    private Vector3 MousePos, TargetPosition;

    private Quaternion LookDir;

    private RaycastHit hit;

    public float GetAutoAttackTime
    {
        get
        {
            return AutoAttackTime;
        }
        set
        {
            AutoAttackTime = value;
        }
    }

    public float GetAttackDelay
    {
        get
        {
            return AttackDelay;
        }
        set
        {
            AttackDelay = value;
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

    public bool GetIgnoreDefense
    {
        get
        {
            return IgnoresDefense;
        }
        set
        {
            IgnoresDefense = value;
        }
    }

    public bool GetHasBurnStatus
    {
        get
        {
            return HasBurnStatusEffect;
        }
        set
        {
            HasBurnStatusEffect = value;
        }
    }

    public bool GetHasSlowStatus
    {
        get
        {
            return HasSlowStatusEffect;
        }
        set
        {
            HasSlowStatusEffect = value;
        }
    }

    public bool GetUsesIntelligenceForDamage
    {
        get
        {
            return UsesIntelligenceForDamage;
        }
        set
        {
            UsesIntelligenceForDamage = value;
        }
    }

    public bool GetIgnoreElements
    {
        get
        {
            return IgnoresElements;
        }
        set
        {
            IgnoresElements = value;
        }
    }

    public Enemy GetTarget
    {
        get
        {
            return Target;
        }
        set
        {
            Target = value;
        }
    }

    public Equipment[] GetEquipment
    {
        get
        {
            return equipment;
        }
        set
        {
            equipment = value;
        }
    }

    public PlayerElement GetPlayerElement
    {
        get
        {
            return playerElement;
        }
        set
        {
            playerElement = value;
        }
    }

    private void Awake()
    {
        cam.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePoint();
        }

        if (Target != null)
        {
            Attack();
        }
    }

    private void MousePoint()
    {
        MousePos = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(MousePos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                GameManager.Instance.GetIsTargeting = true;

                SoundManager.Instance.ButtonClick();

                if (hit.collider.GetComponent<Character>().CurrentHealth > 0)
                {
                    AutoAttackTime = 0;
                    Target = hit.collider.GetComponent<Enemy>();

                    GameManager.Instance.GetEnemyObject = Target.gameObject;
                    Target.GetEnemySkillBar.ToggleCastBar();
                    Target.ToggleHealthBar();

                    Target.GetFilledBar();

                    if (GameManager.Instance.GetLastEnemyObject != null)
                    {
                        GameManager.Instance.GetLastEnemyObject.GetComponent<Enemy>().ToggleHealthBar();
                        GameManager.Instance.GetLastEnemyObject.GetComponent<Enemy>().GetEnemySkillBar.ToggleCastBar();
                    }
                    GameManager.Instance.GetLastEnemyObject = Target.gameObject;
                }
            }
            else
            {
                if (!IsPointerOnUIObject())
                {
                    if(Target != null)
                    {
                        GameManager.Instance.GetIsTargeting = false;

                        GameManager.Instance.GetEnemyObject = null;
                        GameManager.Instance.GetLastEnemyObject = null;

                        SoundManager.Instance.ReverseMouseClick();

                        Target.GetEnemySkillBar.ToggleCastBar();
                        Target.ToggleHealthBar();
                        Target = null;
                        AutoAttackTime = 0;
                    }
                }
            }
        }
    }

    //Checks to see if the mouse is positioned over a UI element(s).
    private bool IsPointerOnUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
    public float DistanceToTarget()
    {
        if (Target != null)
        {
            AttackDistance = Vector3.Distance(this.transform.position, Target.transform.position);
        }
        return AttackDistance;
    }

    private void Attack()
    {
        if(DistanceToTarget() <= AttackRange)
        {
            if(Target.GetCharacter.CurrentHealth > 0)
            {
                if(!SkillsManager.Instance.GetActivatedSkill)
                {
                    AutoAttackTime += Time.deltaTime;
                }
                else if(SkillsManager.Instance.GetActivatedSkill)
                {
                    AutoAttackTime = 0;
                }
                if (AutoAttackTime >= AttackDelay && Target != null && !SkillsManager.Instance.GetActivatedSkill)
                {
                    TargetPosition = new Vector3(Target.transform.position.x - this.transform.position.x, 0, 
                                                         Target.transform.position.z - this.transform.position.z).normalized;

                    LookDir = Quaternion.LookRotation(TargetPosition);

                    if(this.GetComponent<Animator>().GetFloat("Speed") < 1)
                    {
                        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, 5 * Time.deltaTime);
                    }

                    playerAnimations.AttackAnimation();
                    if(Target.GetAI != null)
                    {
                        if (Target.GetAI.GetIsHostile == false)
                        {
                            Target.GetAI.GetSphereTrigger.gameObject.SetActive(true);
                            Target.GetAI.GetPlayerTarget = this.character;
                        }
                    }
                    if(Target.GetPuckAI != null)
                    {
                        Target.GetPuckAI.GetPlayerTarget = this.character;
                    }
                    if(Target.GetRuneGolemAI != null)
                    {
                        Target.GetRuneGolemAI.GetPlayerTarget = this.character;
                    }
                }
            }
            else
            {
                RemoveTarget();
            }
        }
        else
        {
            AutoAttackTime = 0;
        }
        if(Target != null)
        {
            if(Vector3.Distance(this.transform.position, Target.transform.position) >= HideStatsDistance)
            {
                cursorController.SetDefaultCursor();

                Target.GetSkills.DisableEnemySkillBar();
                Target.TurnOffHealthBar();
                Target = null;

                GameManager.Instance.GetEnemyObject = null;
                GameManager.Instance.GetLastEnemyObject = null;

                SoundManager.Instance.ReverseMouseClick();

                AutoAttackTime = 0;
            }
        }
    }

    public void RemoveTarget()
    {
        Target.GetSkills.DisableEnemySkillBar();
        Target.TurnOffHealthBar();
        Target = null;

        GameManager.Instance.GetEnemyObject = null;
        GameManager.Instance.GetLastEnemyObject = null;

        AutoAttackTime = 0;
    }

    private TextMeshProUGUI MpHealText()
    {
        var HealingText = ObjectPooler.Instance.GetPlayerHealText();

        HealingText.SetActive(true);

        HealingText.transform.SetParent(HealTextTransform, false);

        HealingText.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + character.GetComponent<Mana>().RestoreMana() + "</size>" + "<size=20>" + " MP";

        return HealingText.GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI HpHealText()
    {
        var HealingText = ObjectPooler.Instance.GetPlayerHealText();

        HealingText.SetActive(true);

        HealingText.transform.SetParent(HealTextTransform, false);

        HealingText.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + character.GetComponent<Health>().RestoreHealth();

        return HealingText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DealDamage()
    {
        TakeDamage(Target);
    }

    private TextMeshProUGUI TakeDamage(Enemy enemy)
    {
        int DamageType = character.CharacterStrength;

        if(UsesIntelligenceForDamage)
        {
            DamageType = character.CharacterIntelligence;
        }

        float Critical = character.GetCriticalChance;

        var Damagetext = ObjectPooler.Instance.GetEnemyDamageText();

        HitParticleEffect();

        if(character.GetComponent<Mana>().GetUnlockedPassive)
        {
            MpHealText();
        }
        if (character.GetComponent<Health>().GetUnlockedPassive)
        {
            HpHealText();
        }
        if (enemy != null)
        {
            Damagetext.SetActive(true);

            TextHolder = enemy.GetUI;

            Damagetext.transform.SetParent(TextHolder, false);

            #region CriticalHitCalculation
            if (Random.value * 100 <= Critical)
            {
                float CriticalValue = DamageType * 1.25f;

                Mathf.Round(CriticalValue);

                if (CheckWeaknesses(enemy))
                {
                    int WeakDamage = (DamageType * 2) + (int)CriticalValue;

                    if (IgnoresDefense)
                    {
                        enemy.GetComponentInChildren<Health>().ModifyHealth(-WeakDamage);

                        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(WeakDamage) + "!" +
                                                                                    "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                    }
                    else
                    {
                        if (WeakDamage - enemy.GetCharacter.CharacterDefense <= 0)
                        {
                            enemy.GetComponentInChildren<Health>().ModifyHealth(-1);

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "1!" +
                                                                                        "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                        }
                        else
                        {
                            enemy.GetComponentInChildren<Health>().ModifyHealth(-(WeakDamage - enemy.GetCharacter.CharacterDefense));

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(WeakDamage - enemy.GetCharacter.CharacterDefense) + "!" +
                                                                                        "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                        }
                    }
                }
                else if (CheckResistances(enemy))
                {
                    float ResistDamage = (DamageType / 1.25f) + (int)CriticalValue;

                    Mathf.RoundToInt(ResistDamage);

                    if (IgnoresDefense)
                    {
                        enemy.GetComponentInChildren<Health>().ModifyHealth(-(int)ResistDamage);

                        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(ResistDamage) + "!" +
                                                                                    "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                    }
                    else
                    {
                        if (ResistDamage - enemy.GetCharacter.CharacterDefense <= 0)
                        {
                            enemy.GetComponentInChildren<Health>().ModifyHealth(-1);

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "1!" +
                                                                                        "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                        }
                        else
                        {
                            enemy.GetComponentInChildren<Health>().ModifyHealth(-((int)ResistDamage - enemy.GetCharacter.CharacterDefense));

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(ResistDamage - enemy.GetCharacter.CharacterDefense) + "!" +
                                                                                        "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                        }
                    }
                }
                else if (CheckImmunities(enemy))
                {
                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "0" + "\n" + "<size=12> <#EFDFB8>" + "(IMMUNE!)" + "</color> </size>";
                }
                else if (CheckAbsorptions(enemy))
                {
                    if (IgnoresDefense)
                    {
                        enemy.GetHealth.IncreaseHealth((int)CriticalValue);

                        enemy.GetLocalHealthInfo();

                        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "<#4CFFAD>" + Mathf.Round(CriticalValue) + "!" + "</color>" + "\n" + "</size>" + "<size=12> " +
                                                                                    "<#EFDFB8>" + "(ABSORBED!)" + "</color> </size>";
                    }
                    else
                    {
                        if (CriticalValue - enemy.GetCharacter.CharacterDefense <= 0)
                        {
                            enemy.GetHealth.IncreaseHealth(1);

                            enemy.GetLocalHealthInfo();

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "<#4CFFAD>" + "1!" + "</color>" + "\n" + "</size>" + "<size=12> " +
                                                                                        "<#EFDFB8>" + "(ABSORBED!)" + "</color> </size>";
                        }
                        else
                        {
                            enemy.GetHealth.IncreaseHealth((int)CriticalValue - enemy.GetCharacter.CharacterDefense);

                            enemy.GetLocalHealthInfo();

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "<#4CFFAD>" + Mathf.Round(CriticalValue -
                                                                                        enemy.GetCharacter.CharacterDefense) + "!" + "</color>" + "\n" + "</size>" + "<size=12> " +
                                                                                        "<#EFDFB8>" + "(ABSORBED!)" + "</color> </size>";
                        }
                    }
                }
                else
                {
                    if (IgnoresDefense)
                    {
                        enemy.GetHealth.ModifyHealth(-(int)CriticalValue);

                        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(CriticalValue) + "!";
                    }
                    else
                    {
                        if (CriticalValue - enemy.GetCharacter.CharacterDefense <= 0)
                        {
                            enemy.GetHealth.ModifyHealth(-1);

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "1!";
                        }
                        else
                        {
                            enemy.GetHealth.ModifyHealth(-((int)CriticalValue - enemy.GetCharacter.CharacterDefense));

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(CriticalValue - enemy.GetCharacter.CharacterDefense) + "!";
                        }
                    }
                }
            }
            else
            {
                if (CheckWeaknesses(enemy))
                {
                    int WeakDamage = (DamageType * 2);

                    if (IgnoresDefense)
                    {
                        enemy.GetComponentInChildren<Health>().ModifyHealth(-WeakDamage);

                        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + Mathf.Round(WeakDamage) +
                                                                                    "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                    }
                    else
                    {
                        if (WeakDamage - enemy.GetCharacter.CharacterDefense <= 0)
                        {
                            enemy.GetComponentInChildren<Health>().ModifyHealth(-1);

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "1" +
                                                                                        "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                        }
                        else
                        {
                            enemy.GetComponentInChildren<Health>().ModifyHealth(-(WeakDamage - enemy.GetCharacter.CharacterDefense));

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + Mathf.Round(WeakDamage - enemy.GetCharacter.CharacterDefense) +
                                                                                        "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                        }
                    }
                }
                else if (CheckResistances(enemy))
                {
                    float ResistDamage = (DamageType / 1.25f);

                    Mathf.RoundToInt(ResistDamage);

                    if (IgnoresDefense)
                    {
                        enemy.GetComponentInChildren<Health>().ModifyHealth(-(int)ResistDamage);

                        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + Mathf.Round(ResistDamage) +
                                                                                    "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                    }
                    else
                    {
                        if (ResistDamage - enemy.GetCharacter.CharacterDefense <= 0)
                        {
                            enemy.GetComponentInChildren<Health>().ModifyHealth(-1);

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "1" +
                                                                                        "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                        }
                        else
                        {
                            enemy.GetComponentInChildren<Health>().ModifyHealth(-((int)ResistDamage - enemy.GetCharacter.CharacterDefense));

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + Mathf.Round(ResistDamage - enemy.GetCharacter.CharacterDefense) +
                                                                                        "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                        }
                    }
                }
                else if (CheckImmunities(enemy))
                {
                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "0" + "\n" + "<size=12> <#EFDFB8>" + "(IMMUNE!)" + "</color> </size>";
                }
                else if (CheckAbsorptions(enemy))
                {
                    if (IgnoresDefense)
                    {
                        enemy.GetHealth.IncreaseHealth(DamageType);

                        enemy.GetLocalHealthInfo();

                        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "<#4CFFAD>" + Mathf.Round(DamageType) + "</color>" + "\n" + "<size=12> <#EFDFB8>" +
                                                                                    "(ABSORBED!)";
                    }
                    else
                    {
                        if (DamageType - enemy.GetCharacter.CharacterDefense <= 0)
                        {
                            enemy.GetHealth.IncreaseHealth(1);

                            enemy.GetLocalHealthInfo();

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "<#4CFFAD>" + "1" + "</color>" + "\n" + "<size=12> <#EFDFB8>" +
                                                                                        "(ABSORBED!)";
                        }
                        else
                        {
                            enemy.GetHealth.IncreaseHealth((DamageType - enemy.GetCharacter.CharacterDefense));

                            enemy.GetLocalHealthInfo();

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "<#4CFFAD>" + Mathf.Round(DamageType -
                                                                                        enemy.GetCharacter.CharacterDefense) + "</color>" + "\n" + "<size=12> <#EFDFB8>" +
                                                                                        "(ABSORBED!)";
                        }
                    }
                }
                else
                {
                    if (IgnoresDefense)
                    {
                        enemy.GetHealth.ModifyHealth(-DamageType);

                        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + Mathf.Round(DamageType);
                    }
                    else
                    {
                        if (DamageType - enemy.GetCharacter.CharacterDefense <= 0)
                        {
                            enemy.GetHealth.ModifyHealth(-1);

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "1";
                        }
                        else
                        {
                            enemy.GetHealth.ModifyHealth(-(DamageType - enemy.GetCharacter.CharacterDefense));

                            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + Mathf.Round(DamageType - enemy.GetCharacter.CharacterDefense);
                        }
                    }
                }
            }
            #endregion

            if (HasBurnStatusEffect)
            {
                if (Random.value * 100 <= 10)
                {
                    if (!CheckBurnStatusEffect(enemy))
                    {
                        BurningStatus(enemy);
                    }
                }
            }
            if (HasSlowStatusEffect)
            {
                if (Random.value * 100 <= 10)
                {
                    if (!CheckSlowStatusEffect(enemy))
                    {
                        SlowStatus(enemy);
                    }
                }
            }

            if (enemy.GetAI != null)
            {
                if (enemy.GetAI.GetStates != States.Skill && enemy.GetAI.GetStates != States.ApplyingAttack && enemy.GetAI.GetStates != States.SkillAnimation &&
                    !CheckAbsorptions(enemy))
                {
                    enemy.GetAI.GetStates = States.Damaged;
                }
            }
            if (enemy.GetPuckAI != null)
            {
                enemy.GetPuckAI.CheckHP();
            }
            if (enemy.GetRuneGolemAI != null)
            {
                enemy.GetRuneGolemAI.CheckHP();
            }
        }
        return Damagetext.GetComponentInChildren<TextMeshProUGUI>();
    }

    private bool CheckWeaknesses(Enemy enemy)
    {
        bool Weakness = false;

        for (int i = 0; i < enemy.GetCharacter.GetCharacterData.Weaknesses.Length; i++)
        {
            if(playerElement == (PlayerElement)enemy.GetCharacter.GetCharacterData.Weaknesses[i])
            {
                Weakness = true;
            }
        }
        return Weakness;
    }

    private bool CheckResistances(Enemy enemy)
    {
        bool Resistance = false;

        if(IgnoresElements)
        {
        }
        else
        {
            for (int i = 0; i < enemy.GetCharacter.GetCharacterData.Resistances.Length; i++)
            {
                if (playerElement == (PlayerElement)enemy.GetCharacter.GetCharacterData.Resistances[i])
                {
                    Resistance = true;
                }
            }
        }
        return Resistance;
    }

    private bool CheckImmunities(Enemy enemy)
    {
        bool Immunity = false;

        if(IgnoresElements)
        {
        }
        else
        {
            for (int i = 0; i < enemy.GetCharacter.GetCharacterData.Immunities.Length; i++)
            {
                if (playerElement == (PlayerElement)enemy.GetCharacter.GetCharacterData.Immunities[i])
                {
                    Immunity = true;
                }
            }
        }
        return Immunity;
    }

    private bool CheckAbsorptions(Enemy enemy)
    {
        bool Absorption = false;

        if(IgnoresElements)
        {
        }
        else
        {
            for (int i = 0; i < enemy.GetCharacter.GetCharacterData.Absorbtions.Length; i++)
            {
                if (playerElement == (PlayerElement)enemy.GetCharacter.GetCharacterData.Absorbtions[i])
                {
                    Absorption = true;
                }
            }
        }
        return Absorption;
    }

    private TextMeshProUGUI BurningStatus(Enemy enemy)
    {
        TextHolder = enemy.GetUI;

        var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ Burning";

        StatusTxt.GetComponentInChildren<Image>().sprite = BurningStatusEffectSprite;

        ApplyBurningStatus(enemy);

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI SlowStatus(Enemy enemy)
    {
        TextHolder = enemy.GetUI;

        var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ Slowed";

        StatusTxt.GetComponentInChildren<Image>().sprite = SlowStatusEffectSprite;

        ApplySlowStatus(enemy);

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void ApplyBurningStatus(Enemy enemy)
    {
        StatusEffectIconTrans = enemy.GetDebuffTransform;

        StatusEffectIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

        StatusEffectIcon.SetActive(true);

        StatusEffectIcon.transform.SetParent(StatusEffectIconTrans, false);

        StatusEffectIcon.GetComponentInChildren<Image>().sprite = BurningStatusEffectSprite;

        StatusEffectIcon.GetComponent<EnemyStatusIcon>().GetHasBurnStatus = true;

        StatusEffectIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = StatusEffect.Burning;
        StatusEffectIcon.GetComponent<EnemyStatusIcon>().GetPlayer = character.GetComponent<PlayerController>();
        StatusEffectIcon.GetComponentInChildren<Image>().sprite = BurningStatusEffectSprite;
        StatusEffectIcon.GetComponent<EnemyStatusIcon>().BurnStatus();

        StatusEffectIcon.GetComponent<EnemyStatusIcon>().CreateBurningParticle();
    }

    private void ApplySlowStatus(Enemy enemy)
    {
        StatusEffectIconTrans = enemy.GetDebuffTransform;

        StatusEffectIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

        StatusEffectIcon.SetActive(true);

        StatusEffectIcon.transform.SetParent(StatusEffectIconTrans, false);

        StatusEffectIcon.GetComponentInChildren<Image>().sprite = BurningStatusEffectSprite;

        StatusEffectIcon.GetComponent<EnemyStatusIcon>().GetHasSlowStatus = true;

        StatusEffectIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = StatusEffect.Slow;
        StatusEffectIcon.GetComponent<EnemyStatusIcon>().GetPlayer = character.GetComponent<PlayerController>();
        StatusEffectIcon.GetComponentInChildren<Image>().sprite = SlowStatusEffectSprite;
        StatusEffectIcon.GetComponent<EnemyStatusIcon>().SlowStatus();
    }

    private bool CheckBurnStatusEffect(Enemy enemy)
    {
        bool BurnStatus = false;

        foreach(EnemyStatusIcon enemystatus in enemy.GetDebuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
        {
            if(enemystatus.GetStatusEffect == StatusEffect.Burning)
            {
                BurnStatus = true;
            }
        }

        return BurnStatus;
    }

    private bool CheckSlowStatusEffect(Enemy enemy)
    {
        bool SlowStatus = false;

        foreach (EnemyStatusIcon enemystatus in enemy.GetDebuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
        {
            if (enemystatus.GetStatusEffect == StatusEffect.Slow)
            {
                SlowStatus = true;
            }
        }

        return SlowStatus;
    }
    
    public void HitParticleEffect()
    {
        if(settings.UseParticleEffects)
        {
            var Hitparticle = ObjectPooler.Instance.GetHitParticle();

            Hitparticle.SetActive(true);

            if (Target != null)
            {
                Hitparticle.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 0.5f, Target.transform.position.z);

                Hitparticle.transform.SetParent(Target.transform, true);
            }
        }
    }
}