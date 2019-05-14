using UnityEngine;
using UnityEngine.UI;

public enum States { Patrol, Chase, Attack, ApplyingAttack, Skill, Damaged }

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private States states;

    [SerializeField]
    private Character character;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private EnemySkills enemySkills;

    [SerializeField]
    private MushroomMon_Ani_Test Anim;

    [SerializeField]
    private Transform[] Waypoints;

    [SerializeField]
    private float MoveSpeed, AttackRange, AttackDelay, AutoAttackTime, LookSpeed;

    [SerializeField] [Tooltip("Current targeted Player. Keep this empty!")]
    private Character PlayerTarget = null;

    [SerializeField]
    private SphereCollider EnemyTriggerSphere;

    [SerializeField]
    private ParticleSystem HitParticle;

    private bool ParticleExists;

    [SerializeField]
    private Image ThreatPic;

    [SerializeField]
    private Sprite DocileSprite, ThreatSprite;

    private int WaypointIndex;

    [SerializeField]
    private bool IsHostile;

    public MushroomMon_Ani_Test GetAnimation
    {
        get
        {
            return Anim;
        }
        set
        {
            Anim = value;
        }
    }

    private void Awake()
    {
        states = States.Patrol;
    }

    private void OnEnable()
    {
        ResetStats();
    }

    private void Update()
    {
        if (this.character.CurrentHealth > 0)
        {
            switch (states)
            {
                case (States.Patrol):
                    Patrol();
                    break;
                case (States.Chase):
                    Chase();
                    break;
                case (States.Attack):
                    Attack();
                    break;
                case (States.ApplyingAttack):
                    ApplyingNormalAtk();
                    break;
                case (States.Skill):
                    Skill();
                    break;
                case (States.Damaged):
                    Damage();
                    break;
            }
        }
    }

    public States GetStates
    {
        get
        {
            return states;
        }
        set
        {
            states = value;
        }
    }

    public float GetAutoAttack
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

    public bool GetIsHostile
    {
        get
        {
            return IsHostile;
        }
        set
        {
            IsHostile = value;
        }
    }

    public Character GetPlayerTarget
    {
        get
        {
            return PlayerTarget;
        }
        set
        {
            PlayerTarget = value;
        }
    }

    public SphereCollider GetSphereTrigger
    {
        get
        {
            return EnemyTriggerSphere;
        }
        set
        {
            EnemyTriggerSphere = value;
        }
    }

    private void Patrol()
    {
        Anim.RunAni();

        Vector3 Distance = new Vector3(Waypoints[WaypointIndex].position.x - this.transform.position.x, 0,
                                       Waypoints[WaypointIndex].position.z - this.transform.position.z).normalized;

        Quaternion LookDir = Quaternion.LookRotation(Distance);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookSpeed * Time.deltaTime);

        this.transform.position += Distance * MoveSpeed * Time.deltaTime;

        if (Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z),
                            new Vector3(Waypoints[WaypointIndex].position.x, 0, Waypoints[WaypointIndex].position.z)) <= 0.1f)
        {
            WaypointIndex++;
            if (WaypointIndex >= Waypoints.Length)
            {
                WaypointIndex = 0;
            }
        }
    }

    private void Chase()
    {
        Anim.RunAni();

        enemySkills.GetSkillBar.gameObject.SetActive(false);

        if (PlayerTarget != null)
        {
            if (Vector3.Distance(this.transform.position, PlayerTarget.transform.position) >= AttackRange)
            {
                Vector3 Distance = new Vector3(PlayerTarget.transform.position.x - this.transform.position.x, 0,
                                               PlayerTarget.transform.position.z - this.transform.position.z).normalized;

                Quaternion LookDir = Quaternion.LookRotation(Distance);

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookSpeed * Time.deltaTime);

                this.transform.position += Distance * MoveSpeed * Time.deltaTime;
            }
            else
            {
                states = States.Attack;
            }
        }
        else
        {
            states = States.Patrol;
        }
    }

    private void Attack()
    {
        Anim.IdleAni();

        if (PlayerTarget != null && Vector3.Distance(this.transform.position, PlayerTarget.transform.position) <= AttackRange)
        {
            Vector3 Distance = new Vector3(PlayerTarget.transform.position.x - this.transform.position.x, 0,
                                           PlayerTarget.transform.position.z - this.transform.position.z).normalized;

            Quaternion LookDir = Quaternion.LookRotation(Distance);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookSpeed * Time.deltaTime);

            if (PlayerTarget.CurrentHealth > 0)
            {
                AutoAttackTime += Time.deltaTime;
                if (AutoAttackTime >= AttackDelay)
                {
                    enemySkills.GenerateValue();
                    states = States.Skill;
                    /*
                    if (Random.value * 100 <= 50)
                    {
                        enemySkills.GenerateValue();
                        states = States.Skill;
                    }
                    else
                    {
                        states = States.ApplyingAttack;
                    }
                    */
                }
            }
            else
            {
                PlayerTarget = null;
                AutoAttackTime = 0;
                states = States.Patrol;
            }
        }
        else
        {
            AutoAttackTime = 0;
            states = States.Chase;
        }
    }

    private void ApplyingNormalAtk()
    {
        Anim.AttackAni();
    }

    private void Skill()
    {
        enemySkills.ChooseSkill(enemySkills.GetRandomValue);
    }

    public void Damage()
    {
        Anim.DamageAni();
    }

    public void Dead()
    {
        PlayerTarget = null;
        AutoAttackTime = 0;
        EnemyTriggerSphere.enabled = false;

        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        enemy.GetHealth.gameObject.SetActive(false);

        enemy.GetLocalHealth.gameObject.SetActive(false);

        enemySkills.GetSkillBar.gameObject.SetActive(false);
        enemySkills.GetActiveSkill = false;

        character.GetRigidbody.useGravity = false;

        enemySkills.DisableRadiusImage();
        enemySkills.DisableRadius();

        GameManager.Instance.GetEventSystem.SetSelectedGameObject(null);
        GameManager.Instance.GetLastObject = null;

        enemy.ReturnExperience();

        Anim.DeathAni();
    }

    //Resets the enemy's stats when enabled in the scene.
    private void ResetStats()
    {
        if (IsHostile)
        {
            ThreatPic.sprite = ThreatSprite;
            EnemyTriggerSphere.enabled = true;
        }
        else
        {
            ThreatPic.sprite = DocileSprite;
            EnemyTriggerSphere.enabled = false;
        }

        character.CurrentHealth = character.MaxHealth;
        enemy.GetFilledBar();
        enemy.GetLocalHealth.gameObject.SetActive(true);
        
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        character.GetRigidbody.useGravity = true;

        states = States.Patrol;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            PlayerTarget = other.GetComponent<Character>();
            states = States.Chase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            PlayerTarget = null;
            states = States.Patrol;
            AutoAttackTime = 0;
            enemySkills.DisableRadiusImage();
            enemySkills.DisableRadius();
            enemySkills.GetActiveSkill = false;
            enemySkills.GetSkillBar.gameObject.SetActive(false);
        }
        if(!IsHostile)
        {
            PlayerTarget = null;
            states = States.Patrol;
            AutoAttackTime = 0;
            EnemyTriggerSphere.gameObject.SetActive(false);
            enemySkills.DisableRadiusImage();
            enemySkills.DisableRadius();
            enemySkills.GetActiveSkill = false;
            enemySkills.GetSkillBar.gameObject.SetActive(false);
        }
    }

    public Text TakeDamage()
    {
        Text DamageObject = null;

        float Critical = character.GetCriticalChance;

        if(PlayerTarget != null)
        {
            if(!ParticleExists)
            {
                SkillsManager.Instance.GetParticleObj = Instantiate(HitParticle, new Vector3(PlayerTarget.transform.position.x, 
                                                                                             PlayerTarget.transform.position.y + 0.5f, 
                                                                                             PlayerTarget.transform.position.z),
                                                                                             PlayerTarget.transform.rotation);

                SkillsManager.Instance.GetParticleObj.transform.SetParent(PlayerTarget.transform, true);

                ParticleExists = true;
            }
            else
            {
                SkillsManager.Instance.GetParticleObj.gameObject.SetActive(true);
            }

            #region CriticalHitCalculation
            if (Random.value * 100 <= Critical)
            {
                DamageObject = Instantiate(PlayerTarget.GetComponent<Health>().GetDamageText);

                DamageObject.transform.SetParent(PlayerTarget.GetComponent<Health>().GetDamageTextParent.transform, false);

                PlayerTarget.GetComponent<Health>().ModifyHealth((-character.CharacterStrength - 5) - -PlayerTarget.CharacterDefense);
                PlayerTarget.GetComponent<Health>().GetTakingDamage = true;

                DamageObject.fontSize = 40;

                DamageObject.text = ((character.CharacterStrength + 5) - PlayerTarget.CharacterDefense).ToString() + "!";
            }
            else
            {
                DamageObject = Instantiate(PlayerTarget.GetComponent<Health>().GetDamageText);

                DamageObject.transform.SetParent(PlayerTarget.GetComponent<Health>().GetDamageTextParent.transform, false);

                PlayerTarget.GetComponent<Health>().ModifyHealth(-character.CharacterStrength - -PlayerTarget.CharacterDefense);
                PlayerTarget.GetComponent<Health>().GetTakingDamage = true;

                DamageObject.fontSize = 30;

                DamageObject.text = (character.CharacterStrength - PlayerTarget.CharacterDefense).ToString();
            }
            #endregion

            PlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
        }
        return DamageObject;
    }
}