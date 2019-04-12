using UnityEngine;
using UnityEngine.UI;

public enum States { Patrol, Chase, Attack, SkillAttack, Damaged }

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private States states;

    [SerializeField]
    private Character character;

    [SerializeField]
    private MushroomMon_Ani_Test Anim;

    [SerializeField]
    private Transform[] Waypoints;

    [SerializeField]
    private float MoveSpeed, AttackRange, AttackDelay, AutoAttackTime, LookSpeed;

    [SerializeField] [Tooltip("Current targeted Player. Keep this empty!")]
    private GameObject PlayerTarget = null;

    [SerializeField]
    private SphereCollider EnemyTriggerSphere;

    [SerializeField]
    private ParticleSystem HitParticle;

    [SerializeField]
    private Image ThreatPic;

    [SerializeField]
    private Sprite DocileSprite, ThreatSprite;

    [SerializeField]
    private int WaypointIndex;

    [SerializeField]
    private bool IsHostile;

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

        if(Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z), 
                            new Vector3(Waypoints[WaypointIndex].position.x, 0, Waypoints[WaypointIndex].position.z)) <= 0.1f)
        {
            WaypointIndex++;
            if(WaypointIndex >= Waypoints.Length)
            {
                WaypointIndex = 0;
            }
        }
    }

    private void Chase()
    {
        Anim.RunAni();

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

    private void Attack()
    {
        Anim.IdleAni();

        if (PlayerTarget != null && Vector3.Distance(this.transform.position, PlayerTarget.transform.position) <= AttackRange)
        {
            Vector3 Distance = new Vector3(PlayerTarget.transform.position.x - this.transform.position.x, 0,
                                           PlayerTarget.transform.position.z - this.transform.position.z).normalized;

            Quaternion LookDir = Quaternion.LookRotation(Distance);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookSpeed * Time.deltaTime);

            if (PlayerTarget.GetComponent<Character>().CurrentHealth > 0)
            {
                AutoAttackTime += Time.deltaTime;
                if (AutoAttackTime >= AttackDelay)
                {
                    Anim.AttackAni();
                }
            }
            else
            {
                GameManager.Instance.Dead();
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

    private void SkillAttack()
    {

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
        this.GetComponent<Enemy>().GetHealthObject.SetActive(false);
        this.GetComponent<EnemyHealth>().GetLocalHealth.gameObject.SetActive(false);

        character.GetRigidbody.useGravity = false;

        this.GetComponent<Enemy>().ReturnExperience();

        Anim.DeathAni();
    }

    //Resets the enemy's stats when enabled in the scene.
    private void ResetStats()
    {
        if (IsHostile)
        {
            ThreatPic.GetComponent<Image>().sprite = ThreatSprite;
            EnemyTriggerSphere.enabled = true;
        }
        else
        {
            ThreatPic.GetComponent<Image>().sprite = DocileSprite;
            EnemyTriggerSphere.enabled = false;
        }

        character.CurrentHealth = character.MaxHealth;
        this.GetComponent<EnemyHealth>().GetFilledBar();
        this.GetComponent<EnemyHealth>().GetLocalHealth.gameObject.SetActive(true);
        
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        character.GetRigidbody.useGravity = true;

        states = States.Patrol;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Health>())
        {
            PlayerTarget = other.gameObject;
            states = States.Chase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<Health>())
        {
            PlayerTarget = null;
            states = States.Patrol;
            AutoAttackTime = 0;
        }
        if(!IsHostile)
        {
            PlayerTarget = null;
            states = States.Patrol;
            AutoAttackTime = 0;
            EnemyTriggerSphere.gameObject.SetActive(false);
        }
    }

    public Text TakeDamage()
    {
        Text DamageObject = null;

        float Critical = character.GetCriticalChance;

        var particleHit = HitParticle;

        if(PlayerTarget != null)
        {
            particleHit = Instantiate(HitParticle, new Vector3(PlayerTarget.transform.position.x, PlayerTarget.transform.position.y + 0.5f, PlayerTarget.transform.position.z), 
                                      PlayerTarget.transform.rotation);

            particleHit.transform.SetParent(PlayerTarget.transform, true);

            #region CriticalHitCalculation
            if (Random.value * 100 <= Critical)
            {
                DamageObject = Instantiate(PlayerTarget.GetComponent<Health>().GetDamageText);

                DamageObject.transform.SetParent(PlayerTarget.GetComponent<Health>().GetDamageTextParent.transform, false);

                PlayerTarget.GetComponent<Health>().ModifyHealth((-character.CharacterStrength - 5) - -PlayerTarget.GetComponent<Character>().CharacterDefense);

                DamageObject.fontSize = 40;

                DamageObject.text = ((character.CharacterStrength + 5) - PlayerTarget.GetComponent<Character>().CharacterDefense).ToString() + "!";
            }
            else
            {
                DamageObject = Instantiate(PlayerTarget.GetComponent<Health>().GetDamageText);

                DamageObject.transform.SetParent(PlayerTarget.GetComponent<Health>().GetDamageTextParent.transform, false);

                PlayerTarget.GetComponent<Health>().ModifyHealth(-character.CharacterStrength - -PlayerTarget.GetComponent<Character>().CharacterDefense);

                DamageObject.fontSize = 30;

                DamageObject.text = (character.CharacterStrength - PlayerTarget.GetComponent<Character>().CharacterDefense).ToString();
            }
            #endregion

            PlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
        }
        return DamageObject;
    }
}
