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
    private Camera cam;

    [SerializeField]
    private PlayerAnimations playerAnimations;

    [SerializeField] [Tooltip("Current targeted object. Keep this empty!")]
    private Enemy Target = null;

    [SerializeField]
    private ParticleSystem HitParticle;

    private bool ParticleExists;

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

        SearchForEnemy();
    }

    private void SearchForEnemy()
    {
        Vector3 MousePos = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(MousePos);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, MouseRange))
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                CursorController.Instance.SetAttackCursor();
            }
            else
            {
                CursorController.Instance.SetDefaultCursor();
            }
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
                    if(Target.GetAI.GetIsHostile == false)
                    {
                        Target.GetAI.GetSphereTrigger.gameObject.SetActive(true);
                        Target.GetAI.GetPlayerTarget = this.character;
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

        float Critical = character.GetCriticalChance;

        var Damagetext = ObjectPooler.Instance.GetEnemyDamageText();

        HitParticleEffect();

        Damagetext.SetActive(true);

        Damagetext.transform.SetParent(Target.GetComponentInChildren<Health>().GetDamageTextParent.transform, false);

        #region CriticalHitCalculation
        if (Random.value * 100 <= Critical)
        {
            float CriticalValue = character.CharacterStrength * 1.5f;

            Mathf.Round(CriticalValue);

            Target.GetComponentInChildren<Health>().ModifyHealth(-((int)CriticalValue - Target.GetCharacter.CharacterDefense));

            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(CriticalValue - Target.GetCharacter.CharacterDefense) + "!";
        }
        else
        {
            Target.GetComponentInChildren<Health>().ModifyHealth(-(character.CharacterStrength - Target.GetCharacter.CharacterDefense));

            Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + (character.CharacterStrength - Target.GetCharacter.CharacterDefense);
        }
        #endregion

        if (Target.GetAI.GetStates != States.Skill && Target.GetAI.GetStates != States.ApplyingAttack && Target.GetAI.GetStates != States.SkillAnimation)
            Target.GetAI.GetStates = States.Damaged;

        return Damagetext.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void HitParticleEffect()
    {
        var Hitparticle = ObjectPooler.Instance.GetHitParticle();

        Hitparticle.SetActive(true);

        Hitparticle.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 0.5f, Target.transform.position.z);

        Hitparticle.transform.SetParent(Target.transform, true);
    }
}