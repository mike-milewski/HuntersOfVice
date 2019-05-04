using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicAttack : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField] [Tooltip("Current targeted object. Keep this empty!")]
    private GameObject Target = null;

    [SerializeField]
    private ParticleSystem HitParticle;

    private ParticleSystem Obj = null;

    private bool ParticleExists;

    [SerializeField]
    private float MouseRange, AttackRange, AttackDelay, AutoAttackTime, HideStatsDistance;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        MousePoint();

        if(Target != null)
        {
            Attack();
        }
    }

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

    public GameObject GetTarget
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

    private void MousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, MouseRange))
        {
            if(hit.collider.GetComponent<Enemy>())
            {
                if(hit.collider.GetComponent<Character>().CurrentHealth > 0)
                {
                    AutoAttackTime = 0;
                    Target = hit.collider.GetComponent<Enemy>().gameObject;
                    Target.GetComponent<EnemyHealth>().GetEnemyInfo();
                    Target.GetComponent<EnemyHealth>().GetFilledBar();
                    Target.GetComponent<Enemy>().GetHealthObject.SetActive(true);
                    Target.GetComponent<EnemySkills>().EnableEnemySkillBar();
                }
            }
            else
            {
                if(!IsPointerOnUIObject())
                {
                    if (Target != null)
                    {
                        Target.GetComponent<Enemy>().GetHealthObject.SetActive(false);
                        Target.GetComponent<EnemySkills>().DisableEnemySkillBar();
                    }
                    Target = null;
                    AutoAttackTime = 0;
                }
            }
        }
    }

    //Checks to see if the mouse is positioned over a UI element.
    private bool IsPointerOnUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

    private void Attack()
    {
        if(Vector3.Distance(this.transform.position, Target.transform.position) <= AttackRange)
        {
            if(Target.GetComponent<Character>().CurrentHealth > 0)
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

                    this.GetComponent<PlayerAnimations>().AttackAnimation();
                    if(Target.GetComponent<EnemyAI>().GetIsHostile == false)
                    {
                        Target.GetComponent<EnemyAI>().GetSphereTrigger.gameObject.SetActive(true);
                        Target.GetComponent<EnemyAI>().GetPlayerTarget = this.gameObject;
                    }
                }
            }
            else
            {
                this.GetComponent<PlayerAnimations>().EndAttackAnimation();
                Target.GetComponent<EnemyAI>().Dead();
                Target = null;
                AutoAttackTime = 0;
            }
        }
        else
        {
            this.GetComponent<PlayerAnimations>().EndAttackAnimation();
        }
        if(Target != null)
        {
            if(Vector3.Distance(this.transform.position, Target.transform.position) >= HideStatsDistance)
            {
                Target.GetComponent<Enemy>().GetHealthObject.SetActive(false);
                Target.GetComponent<EnemySkills>().DisableEnemySkillBar();
                Target = null;
                AutoAttackTime = 0;
            }
        }
    }

    public Text TakeDamage()
    {
        Text DamageObject = null;

        float Critical = character.GetCriticalChance;

        if (Target != null)
        {
            if(!ParticleExists)
            {
                Obj = Instantiate(HitParticle, new Vector3(Target.transform.position.x, Target.transform.position.y + 0.5f, Target.transform.position.z),
                                        Target.transform.rotation);

                Obj.transform.SetParent(Target.transform, true);

                ParticleExists = true;
            }
            else
            {
                Obj.gameObject.SetActive(true);
            }

            #region CriticalHitCalculation
            if (Random.value * 100 <= Critical)
            {
                DamageObject = Instantiate(Target.GetComponent<EnemyHealth>().GetDamageText);

                DamageObject.transform.SetParent(Target.GetComponent<EnemyHealth>().GetDamageTextHolder.transform, false);

                Target.GetComponent<EnemyHealth>().ModifyHealth((-character.CharacterStrength - 5) - -Target.GetComponent<Character>().CharacterDefense);

                DamageObject.fontSize = 30;

                DamageObject.text = ((character.CharacterStrength + 5) - Target.GetComponent<Character>().CharacterDefense).ToString() + "!";
            }
            else
            {
                DamageObject = Instantiate(Target.GetComponent<EnemyHealth>().GetDamageText);

                DamageObject.transform.SetParent(Target.GetComponent<EnemyHealth>().GetDamageTextHolder.transform, false);

                Target.GetComponent<EnemyHealth>().ModifyHealth(-character.CharacterStrength - -Target.GetComponent<Character>().CharacterDefense);

                DamageObject.fontSize = 20;

                DamageObject.text = (character.CharacterStrength - Target.GetComponent<Character>().CharacterDefense).ToString();
            }
            #endregion

            if(Target.GetComponent<EnemyAI>().GetStates != States.Skill)
            Target.GetComponent<EnemyAI>().GetStates = States.Damaged;
        }
        return DamageObject;
    }
}