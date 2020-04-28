#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BasicAttack : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Settings settings;

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
    private PlayerElement playerElement;

    [SerializeField]
    private float MouseRange, AttackRange, AttackDelay, AutoAttackTime, HideStatsDistance;

    private float AttackDistance;

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
        Vector3 MousePos = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(MousePos);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, MouseRange))
        {
            if (hit.collider.GetComponent<Enemy>())
            {
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
                    Vector3 TargetPosition = new Vector3(Target.transform.position.x - this.transform.position.x, 0, 
                                                         Target.transform.position.z - this.transform.position.z).normalized;

                    Quaternion LookDir = Quaternion.LookRotation(TargetPosition);

                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, 5 * Time.deltaTime);

                    playerAnimations.AttackAnimation();
                    if(Target.GetAI != null)
                    {
                        if (Target.GetAI.GetIsHostile == false)
                        {
                            Target.GetAI.GetSphereTrigger.gameObject.SetActive(true);
                            Target.GetAI.GetPlayerTarget = this.character;
                        }
                    }
                    else
                    {
                        Target.GetPuckAI.GetPlayerTarget = this.character;
                    }
                }
            }
            else
            {
                playerAnimations.EndAttackAnimation();

                Target.GetSkills.DisableEnemySkillBar();
                Target.TurnOffHealthBar();
                Target = null;

                GameManager.Instance.GetEnemyObject = null;
                GameManager.Instance.GetLastEnemyObject = null;
                
                AutoAttackTime = 0;
            }
        }
        else
        {
            playerAnimations.EndAttackAnimation();
            AutoAttackTime = 0;
        }
        if(Target != null)
        {
            if(Vector3.Distance(this.transform.position, Target.transform.position) >= HideStatsDistance)
            {
                CursorController.Instance.SetDefaultCursor();

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

    public TextMeshProUGUI TakeDamage()
    {
        if(Target == null)
        {
            return null;
        }

        int DamageType = character.GetCharacterData.name == "Knight" ? character.CharacterStrength : character.CharacterIntelligence;

        float Critical = character.GetCriticalChance;

        var Damagetext = ObjectPooler.Instance.GetEnemyDamageText();

        HitParticleEffect();

        Damagetext.SetActive(true);

        Damagetext.transform.SetParent(Target.GetComponentInChildren<Health>().GetDamageTextParent.transform, false);

        if(character.GetComponent<Mana>().GetUnlockedPassive)
        {
            character.GetComponent<Mana>().RestoreMana();
        }

        #region CriticalHitCalculation
        if (Random.value * 100 <= Critical)
        {
            float CriticalValue = DamageType * 1.25f;

            Mathf.Round(CriticalValue);

            if(CheckWeaknesses())
            {
                int WeakDamage = (DamageType * 2) + (int)CriticalValue;

                if(WeakDamage - Target.GetCharacter.CharacterDefense < 0)
                {
                    Target.GetComponentInChildren<Health>().ModifyHealth(-1);

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "1!" +
                                                                                "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                }
                else
                {
                    Target.GetComponentInChildren<Health>().ModifyHealth(-(WeakDamage - Target.GetCharacter.CharacterDefense));

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(WeakDamage - Target.GetCharacter.CharacterDefense) + "!" +
                                                                                "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                }
            }
            else if(CheckResistances())
            {
                float ResistDamage = (DamageType / 1.25f) + (int)CriticalValue;

                Mathf.RoundToInt(ResistDamage);

                if (ResistDamage - Target.GetCharacter.CharacterDefense < 0)
                {
                    Target.GetComponentInChildren<Health>().ModifyHealth(-1);

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "1!" +
                                                                                "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                }
                else
                {
                    Target.GetComponentInChildren<Health>().ModifyHealth(-((int)ResistDamage - Target.GetCharacter.CharacterDefense));

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(ResistDamage - Target.GetCharacter.CharacterDefense) + "!" +
                                                                                "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                }
            }
            else if(CheckImmunities())
            {
                Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "0" + "\n" + "<size=12> <#EFDFB8>" + "(IMMUNE!)" + "</color> </size>";
            }
            else if(CheckAbsorptions())
            {
                if(CriticalValue - Target.GetCharacter.CharacterDefense < 0)
                {
                    Target.GetHealth.IncreaseHealth(1);

                    Target.GetLocalHealthInfo();

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "<#4CFFAD>" + "1!" + "</color>" + "\n" + "</size>" + "<size=12> " +
                                                                                "<#EFDFB8>" + "(ABSORBED!)" + "</color> </size>";
                }
                else
                {
                    Target.GetHealth.IncreaseHealth(((int)CriticalValue + DamageType) - Target.GetCharacter.CharacterDefense);

                    Target.GetLocalHealthInfo();

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "<#4CFFAD>" + Mathf.Round((CriticalValue + DamageType) -
                                                                                Target.GetCharacter.CharacterDefense) + "!" + "</color>" + "\n" + "</size>" + "<size=12> " +
                                                                                "<#EFDFB8>" + "(ABSORBED!)" + "</color> </size>";
                }
            }
            else
            {
                if(CriticalValue - Target.GetCharacter.CharacterDefense < 0)
                {
                    Target.GetHealth.ModifyHealth(-1);

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + "1!";
                }
                else
                {
                    Target.GetHealth.ModifyHealth(-(((int)CriticalValue + DamageType) - Target.GetCharacter.CharacterDefense));

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round((CriticalValue + DamageType) -
                                                                                Target.GetCharacter.CharacterDefense) + "!";
                }
            }
        }
        else
        {
            if (CheckWeaknesses())
            {
                int WeakDamage = (DamageType * 2);

                if(WeakDamage - Target.GetCharacter.CharacterDefense < 0)
                {
                    Target.GetComponentInChildren<Health>().ModifyHealth(-1);

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "1" +
                                                                                "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                }
                else
                {
                    Target.GetComponentInChildren<Health>().ModifyHealth(-(WeakDamage - Target.GetCharacter.CharacterDefense));

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + Mathf.Round(WeakDamage - Target.GetCharacter.CharacterDefense) +
                                                                                "\n" + "<size=12> <#EFDFB8>" + "(WEAKNESS!)" + "</color> </size>";
                }
            }
            else if (CheckResistances())
            {
                float ResistDamage = (DamageType / 1.25f);

                Mathf.RoundToInt(ResistDamage);

                if (ResistDamage - Target.GetCharacter.CharacterDefense < 0)
                {      
                    Target.GetComponentInChildren<Health>().ModifyHealth(-1);

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "1" +
                                                                                "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                }
                else
                {
                    Target.GetComponentInChildren<Health>().ModifyHealth(-((int)ResistDamage - Target.GetCharacter.CharacterDefense));

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + Mathf.Round(ResistDamage - Target.GetCharacter.CharacterDefense) +
                                                                                "\n" + "<size=12> <#EFDFB8>" + "(RESISTED!)" + "</color> </size>";
                }
            }
            else if (CheckImmunities())
            {
                Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "0" + "\n" + "<size=12> <#EFDFB8>" + "(IMMUNE!)" + "</color> </size>";
            }
            else if (CheckAbsorptions())
            {
                if(DamageType - Target.GetCharacter.CharacterDefense < 0)
                {
                    Target.GetHealth.IncreaseHealth(1);

                    Target.GetLocalHealthInfo();

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "<#4CFFAD>" + "1" + "</color>" + "\n" + "<size=12> <#EFDFB8>" +
                                                                                "(ABSORBED!)";
                }
                else
                {
                    Target.GetHealth.IncreaseHealth((DamageType - Target.GetCharacter.CharacterDefense));

                    Target.GetLocalHealthInfo();

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "<#4CFFAD>" + Mathf.Round(DamageType -
                                                                                Target.GetCharacter.CharacterDefense) + "</color>" + "\n" + "<size=12> <#EFDFB8>" +
                                                                                "(ABSORBED!)";
                }
            }
            else
            {
                if(DamageType - Target.GetCharacter.CharacterDefense < 0)
                {
                    Target.GetHealth.ModifyHealth(-1);

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + "1";
                }
                else
                {
                    Target.GetHealth.ModifyHealth(-(DamageType - Target.GetCharacter.CharacterDefense));

                    Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + (DamageType - Target.GetCharacter.CharacterDefense);
                }
            }
        }
        #endregion

        if(Target.GetAI != null)
        {
            if (Target.GetAI.GetStates != States.Skill && Target.GetAI.GetStates != States.ApplyingAttack && Target.GetAI.GetStates != States.SkillAnimation &&
                !CheckAbsorptions())
            {
                Target.GetAI.GetStates = States.Damaged;
            }
        }
        else
        {
            Target.GetPuckAI.CheckHP();

            if (Target.GetPuckAI.GetStates != BossStates.Skill && Target.GetPuckAI.GetStates != BossStates.ApplyingAttack && Target.GetPuckAI.GetStates 
                != BossStates.SkillAnimation && Target.GetPuckAI.GetStates != BossStates.MovingToPosition && Target.GetPuckAI.GetStates != BossStates.RotateToPosition 
                && !CheckAbsorptions())
            {
                Target.GetPuckAI.GetStates = BossStates.Damaged;
            }
        }

        return Damagetext.GetComponentInChildren<TextMeshProUGUI>();
    }

    private bool CheckWeaknesses()
    {
        bool Weakness = false;

        for (int i = 0; i < Target.GetCharacter.GetCharacterData.Weaknesses.Length; i++)
        {
            if(playerElement == (PlayerElement)Target.GetCharacter.GetCharacterData.Weaknesses[i])
            {
                Weakness = true;
            }
        }
        return Weakness;
    }

    private bool CheckResistances()
    {
        bool Resistance = false;

        for (int i = 0; i < Target.GetCharacter.GetCharacterData.Resistances.Length; i++)
        {
            if (playerElement == (PlayerElement)Target.GetCharacter.GetCharacterData.Resistances[i])
            {
                Resistance = true;
            }
        }
        return Resistance;
    }

    private bool CheckImmunities()
    {
        bool Immunity = false;

        for (int i = 0; i < Target.GetCharacter.GetCharacterData.Immunities.Length; i++)
        {
            if (playerElement == (PlayerElement)Target.GetCharacter.GetCharacterData.Immunities[i])
            {
                Immunity = true;
            }
        }
        return Immunity;
    }

    private bool CheckAbsorptions()
    {
        bool Absorption = false;

        for (int i = 0; i < Target.GetCharacter.GetCharacterData.Absorbtions.Length; i++)
        {
            if (playerElement == (PlayerElement)Target.GetCharacter.GetCharacterData.Absorbtions[i])
            {
                Absorption = true;
            }
        }
        return Absorption;
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