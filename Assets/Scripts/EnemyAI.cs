#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public enum States { Idle, Patrol, Chase, Attack, ApplyingAttack, Skill, SkillAnimation, Damaged, Immobile }

public enum EnemyType { Monster, Boss }

[System.Serializable]
public class AiStates
{
    [SerializeField]
    private States state;

    [SerializeField]
    private int SkillIndex;

    public States GetState
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }

    public int GetSkillIndex
    {
        get
        {
            return SkillIndex;
        }
        set
        {
            SkillIndex = value;
        }
    }
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private States states;

    [SerializeField]
    private EnemyType enemyType;

    [SerializeField]
    private AiStates[] aiStates;

    [SerializeField]
    private Character character;

    [SerializeField]
    private Character Knight, ShadowPriest;

    [SerializeField]
    private MonsterBook monsterBook;

    [SerializeField]
    private MonsterInformation monsterInformation;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private EnemySkills enemySkills;

    [SerializeField]
    private EnemyAnimations Anim;

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

    [SerializeField]
    private float TimeToMoveAgain, WayPointDistance, PlayerDistance, OuterAttackDistance; //A value that determines how long the enemy will stay at one waypoint before moving on to the next.

    private float TimeToMove, DistanceToTarget;

    private bool StandingStill, PlayerEntry;

    [SerializeField]
    private Image ThreatPic;

    [SerializeField]
    private Sprite DocileSprite, ThreatSprite;

    private int WaypointIndex;

    [SerializeField]
    private bool IsHostile, IsAnAdd, IsAPuzzleComponent;

    [SerializeField]
    private bool IsUsingAnimator;

    private int StateArrayIndex;

    public int GetStateArrayIndex
    {
        get
        {
            return StateArrayIndex;
        }
        set
        {
            StateArrayIndex = value;
        }
    }

    public EnemyAnimations GetAnimation
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

    public AiStates[] GetAiStates
    {
        get
        {
            return aiStates;
        }
        set
        {
            aiStates = value;
        }
    }

    public bool GetIsUsingAnimator
    {
        get
        {
            return IsUsingAnimator;
        }
        set
        {
            IsUsingAnimator = value;
        }
    }

    public bool GetIsAnAdd
    {
        get
        {
            return IsAnAdd;
        }
        set
        {
            IsAnAdd = value;
        }
    }

    public bool GetIsAPuzzleComponent
    {
        get
        {
            return IsAPuzzleComponent;
        }
        set
        {
            IsAPuzzleComponent = value;
        }
    }

    public void IncreaseArray()
    {
        StateArrayIndex++;
        if (StateArrayIndex >= aiStates.Length)
        {
            StateArrayIndex = 0;
        }
    }

    private void Awake()
    {
        if(IsAnAdd)
        {
            EnemyTriggerSphere.gameObject.SetActive(false);

            states = States.Idle;

            StartCoroutine("WaitToTargetPlayer");
        }
        else
        {
            states = States.Patrol;

            TimeToMove = TimeToMoveAgain;
        }
    }

    private void OnEnable()
    {
        ResetStats();

        if(IsAnAdd)
        {
            EnemyTriggerSphere.gameObject.SetActive(false);

            states = States.Idle;

            StartCoroutine("WaitToTargetPlayer");
        }
    }

    private void Update()
    {
        if (this.character.CurrentHealth > 0)
        {
            switch (states)
            {
                case (States.Idle):
                    Idle();
                    break;
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
                case (States.Immobile):
                    Immobile();
                    break;
                case (States.SkillAnimation):
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

    private void Idle()
    {
        if (!IsUsingAnimator)
        {
            Anim.IdleAni();
        }
        else
        {
            Anim.IdleAnimator();
        }
    }

    private void Patrol()
    {
        float DistanceToWayPoint = Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z),
                                                    new Vector3(Waypoints[WaypointIndex].position.x, 0, Waypoints[WaypointIndex].position.z));

        if (!StandingStill)
        {
            if(!IsUsingAnimator)
            {
                Anim.RunAni();
            }
            else
            {
                Anim.MoveAnimator();
            }

            Vector3 Distance = new Vector3(Waypoints[WaypointIndex].position.x - this.transform.position.x, 0,
                                           Waypoints[WaypointIndex].position.z - this.transform.position.z).normalized;

            Quaternion LookDir = Quaternion.LookRotation(Distance);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookSpeed * Time.deltaTime);

            this.transform.position += Distance * MoveSpeed * Time.deltaTime;
        }
        else
        {
            if(!IsUsingAnimator)
            {
                Anim.IdleAni();
            }
            else
            {
                Anim.IdleAnimator();
            }
        }

        if (DistanceToWayPoint <= 0.1f)
        {
            StandingStill = true;
            TimeToMove -= Time.deltaTime;
            if (TimeToMove <= 0)
            {
                WaypointIndex++;
                if (WaypointIndex >= Waypoints.Length)
                {
                    WaypointIndex = 0;
                }
                TimeToMove = TimeToMoveAgain;
                StandingStill = false;
            }
        }
    }

    private void Chase()
    {
        StandingStill = false;

        CheckDistanceBetweenPlayerOrWaypoints();

        if (!IsUsingAnimator)
        {
            Anim.RunAni();
        }
        else
        {
            Anim.MoveAnimator();
        }

        enemySkills.GetSkillBar.gameObject.SetActive(false);

        if (PlayerTarget != null)
        {
            if (PlayerTarget != null)
            {
                DistanceToTarget = Vector3.Distance(this.transform.position, PlayerTarget.transform.position);
            }

            if (DistanceToTarget >= AttackRange)
            {
                Vector3 Distance = new Vector3(PlayerTarget.transform.position.x - this.transform.position.x, 0,
                                               PlayerTarget.transform.position.z - this.transform.position.z).normalized;

                Quaternion LookDir = Quaternion.LookRotation(Distance);

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookSpeed * Time.deltaTime);

                this.transform.position += Distance * MoveSpeed * Time.deltaTime;

                if (PlayerTarget.CurrentHealth > 0)
                {
                    AutoAttackTime += Time.deltaTime;
                    if(DistanceToTarget <= OuterAttackDistance)
                    {
                        if (AutoAttackTime >= AttackDelay)
                        {
                            states = aiStates[StateArrayIndex].GetState;
                        }
                    }
                }
                else
                {
                    enemy.GetHealth.IncreaseHealth(character.MaxHealth);
                    enemy.GetLocalHealthInfo();

                    PlayerTarget = null;
                    AutoAttackTime = 0;
                    states = States.Patrol;
                }
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
        if(!IsUsingAnimator)
        {
            Anim.IdleAni();
        }
        else
        {
            Anim.IdleAnimator();
        }

        if (PlayerTarget != null)
        {
            DistanceToTarget = Vector3.Distance(this.transform.position, PlayerTarget.transform.position);
        }

        if (PlayerTarget != null && DistanceToTarget <= AttackRange)
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
                    states = aiStates[StateArrayIndex].GetState;
                }
            }
            else
            {
                enemy.GetHealth.IncreaseHealth(character.MaxHealth);
                enemy.GetLocalHealthInfo();

                PlayerTarget = null;
                AutoAttackTime = 0;
                states = States.Patrol;
            }
        }
        else
        {
            states = States.Chase;
        }
    }

    private void ApplyingNormalAtk()
    {
        if(!IsUsingAnimator)
        {
            Anim.AttackAni();
        }
        else
        {
            Anim.AttackAnimator();
        }
    }

    private void Skill()
    {
        enemySkills.ChooseSkill(aiStates[StateArrayIndex].GetSkillIndex);
    }

    //Sets the enemy to this state if they are inflicted with the stun/sleep status effect.
    private void Immobile()
    {
        if(!IsUsingAnimator)
        {
            Anim.IdleAni();
        }
        else
        {
            Anim.IdleAnimator();
        }
    }

    public void Damage()
    {
        if(!IsUsingAnimator)
        {
            Anim.DamageAni();
        }
        else
        {
            Anim.DamagedAnimator();
        }
    }

    public void Dead()
    {
        StandingStill = false;
        PlayerTarget = null;
        AutoAttackTime = 0;
        EnemyTriggerSphere.enabled = false;

        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        enemy.GetLocalHealth.gameObject.SetActive(false);

        enemySkills.GetSkillBar.gameObject.SetActive(false);
        enemySkills.GetActiveSkill = false;

        character.GetRigidbody.useGravity = false;

        if (enemySkills.GetManager.Length > 0)
        {
            enemySkills.DisableRadiusImage();
            enemySkills.DisableRadius();
        }

        if (!GameManager.Instance.GetIsTargeting)
        {
            GameManager.Instance.GetEnemyObject = null;
            GameManager.Instance.GetLastEnemyObject = null;

            GameManager.Instance.GetEventSystem.SetSelectedGameObject(null);
        }

        enemy.ToggleHealthBar();

        enemy.ReturnCoins();

        enemy.ReturnExperience();

        if(!IsUsingAnimator)
        {
            Anim.DeathAni();
        }
        else
        {
            Anim.DeadAnimator();
        }

        CheckForInformation();
    }

    private void CheckForInformation()
    {
        var monsterinfo = monsterInformation;

        if (monsterBook.GetMonsterTransform.childCount <= 0)
        {
            GetMonsterEntryText();
            monsterinfo = Instantiate(monsterInformation, EnemyTransform());
            if (!GameManager.Instance.GetMonsterToggle)
            {
                monsterinfo.gameObject.SetActive(false);
            }
            else
            {
                monsterinfo.gameObject.SetActive(true);
            }
            monsterinfo.transform.SetParent(EnemyTransform(), false);
            monsterinfo.GetCharacter = character;

            monsterinfo.GetMonsterName.text = character.GetCharacterData.CharacterName;

            monsterinfo.GetCharacterData.Add(character.GetCharacterData);
        }
        else
        {
            CheckForSameEnemyDataLevel();

            if (CheckForSameEnemyDataName())
            {
                return;
            }
            else
            {
                GetMonsterEntryText();
                monsterinfo = Instantiate(monsterInformation, EnemyTransform());
                if (!GameManager.Instance.GetMonsterToggle)
                {
                    monsterinfo.gameObject.SetActive(false);
                }
                else
                {
                    monsterinfo.gameObject.SetActive(true);
                }
                monsterinfo.transform.SetParent(EnemyTransform(), false);
                monsterinfo.GetCharacter = character;

                monsterinfo.GetMonsterName.text = character.GetCharacterData.CharacterName;

                monsterinfo.GetCharacterData.Add(character.GetCharacterData);
            }
        }
    }

    private IEnumerator WaitToTargetPlayer()
    {
        yield return new WaitForSeconds(1f);
        SelectPlayerTarget();
    }

    private void SelectPlayerTarget()
    {
        if (Knight.gameObject.activeInHierarchy)
        {
            PlayerTarget = Knight;
        }
        else if (ShadowPriest.gameObject.activeInHierarchy)
        {
            PlayerTarget = ShadowPriest;
        }
        states = States.Chase;
    }

    private Transform EnemyTransform()
    {
        Transform EnemyButtonTransform = null;

        switch(enemyType)
        {
            case (EnemyType.Monster):
                EnemyButtonTransform = monsterBook.GetMonsterTransform;
                break;
            case (EnemyType.Boss):
                EnemyButtonTransform = monsterBook.GetBossTransform;
                break;
        }

        return EnemyButtonTransform;
    }

    private bool CheckForSameEnemyDataName()
    {
        bool SameName = false;

        foreach (MonsterInformation mi in monsterBook.GetMonsterTransform.GetComponentsInChildren<MonsterInformation>(true))
        {
            if (mi.GetCharacter.GetCharacterData.CharacterName == character.GetCharacterData.CharacterName)
            {
                SameName = true;
            }
        }
        return SameName;
    }

    private void CheckForSameEnemyDataLevel()
    {
        foreach (MonsterInformation mi in monsterBook.GetMonsterTransform.GetComponentsInChildren<MonsterInformation>(true))
        {
            if (mi.GetCharacter.GetCharacterData.CharacterName == character.GetCharacterData.CharacterName &&
                mi.GetCharacter.GetCharacterData.CharacterLevel != character.GetCharacterData.CharacterLevel)
            {
                if(mi.GetCharacterData.Count < monsterBook.GetMonsterLevelButtons.Length)
                {
                    mi.GetCharacterData.Add(character.GetCharacterData);
                }
            }

            if (mi.GetIsSelected)
            {
                mi.ShowLevelButtons();
            }
        }
    }

    //Resets the enemy's stats when enabled in the scene.
    private void ResetStats()
    {
        StateArrayIndex = 0;

        if(!IsAnAdd)
        {
            WaypointIndex = 0;

            transform.position = Waypoints[WaypointIndex].position;

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
            enemy.GetLocalHealthInfo();

            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            character.GetRigidbody.useGravity = true;

            states = States.Patrol;
        }
        else
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
            enemy.GetLocalHealthInfo();

            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            character.GetRigidbody.useGravity = true;

            AutoAttackTime = 0;

            states = States.Idle;
        }
    }

    private void CheckDistanceBetweenPlayerOrWaypoints()
    { 
        if(!IsAnAdd)
        {
            if (Vector3.Distance(transform.position, Waypoints[WaypointIndex].position) >= WayPointDistance)
            {
                EndBattle();
            }
            if(PlayerTarget != null)
            {
                if (Vector3.Distance(transform.position, PlayerTarget.transform.position) >= PlayerDistance)
                {
                    EndBattle();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerEntry = true;
        if(other.gameObject.GetComponent<PlayerController>())
        {
            PlayerTarget = other.GetComponent<Character>();
            states = States.Chase;

            enemy.GetExperience = other.gameObject.GetComponent<Experience>();

            if (gameObject.GetComponent<EnemyConnection>())
            {
                EnemyConnection ec = gameObject.GetComponent<EnemyConnection>();

                EnemyTriggerSphere.gameObject.SetActive(false);

                ec.GetEnemyAI.enemy.GetExperience = other.gameObject.GetComponent<Experience>();

                ec.GetEnemyAI.GetSphereTrigger.gameObject.SetActive(false);
                ec.GetEnemyAI.GetPlayerTarget = this.PlayerTarget;
                ec.GetEnemyAI.GetStates = States.Chase;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (states != States.Patrol)
        {
            if (other.gameObject.GetComponent<PlayerController>() && IsHostile)
            {
                EndBattle();
            }
            if (other.gameObject.GetComponent<PlayerController>() && !IsHostile)
            {
                EndBattle();
            }
        }
    }

    private void EndBattle()
    {
        RemoveNegativeStatusEffects();

        if (gameObject.GetComponent<EnemyConnection>())
        {
            EnemyTriggerSphere.gameObject.SetActive(true);

            gameObject.GetComponent<EnemyConnection>().GetEnemyAI.GetSphereTrigger.gameObject.SetActive(true);
        }

        if (IsHostile)
        {
            PlayerEntry = false;
            if (states != States.SkillAnimation)
            {
                PlayerTarget = null;
                states = States.Patrol;
                AutoAttackTime = 0;
                if (enemySkills.GetManager.Length > 0)
                {
                    enemySkills.DisableRadiusImage();
                    enemySkills.DisableRadius();
                }
                enemySkills.GetActiveSkill = false;
                enemySkills.GetSkillBar.gameObject.SetActive(false);
            }
            enemy.GetHealth.IncreaseHealth(character.MaxHealth);
            enemy.GetLocalHealthInfo();
            StateArrayIndex = 0;
        }
        if (!IsHostile)
        {
            PlayerEntry = false;
            if (states != States.SkillAnimation)
            {
                PlayerTarget = null;
                states = States.Patrol;
                AutoAttackTime = 0;
                EnemyTriggerSphere.gameObject.SetActive(false);
                if (enemySkills.GetManager.Length > 0)
                {
                    enemySkills.DisableRadiusImage();
                    enemySkills.DisableRadius();
                }
                enemySkills.GetActiveSkill = false;
                enemySkills.GetSkillBar.gameObject.SetActive(false);
            }
            enemy.GetHealth.IncreaseHealth(character.MaxHealth);
            enemy.GetLocalHealthInfo();
            StateArrayIndex = 0;
        }
    }

    private void RemoveNegativeStatusEffects()
    {
        foreach(EnemyStatusIcon esi in enemy.GetDebuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
        {
            if(enemy.GetDebuffTransform.childCount > 0)
            {
                esi.RemoveEffect();
            }
        }
    }

    public void CheckTarget()
    {
        if(!PlayerEntry)
        {
            PlayerTarget = null;
            states = States.Patrol;
            AutoAttackTime = 0;
            enemySkills.DisableRadiusImage();
            enemySkills.DisableRadius();
            enemySkills.GetActiveSkill = false;
            enemySkills.GetSkillBar.gameObject.SetActive(false);
        }
        else
        {
            AutoAttackTime = 0;
            states = States.Attack;
        }
    }

    private TextMeshProUGUI ReflectedDamage()
    {
        float RelectedValue = 0.10f * PlayerTarget.GetComponent<Character>().MaxHealth;

        var Damagetext = ObjectPooler.Instance.GetEnemyDamageText();

        Damagetext.SetActive(true);

        Damagetext.transform.SetParent(GetComponentInChildren<Health>().GetDamageTextParent.transform, false);

        GetComponentInChildren<Health>().ModifyHealth(-(int)RelectedValue);

        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + Mathf.Round(RelectedValue);

        return Damagetext.GetComponentInChildren<TextMeshProUGUI>();
    }

    public TextMeshProUGUI TakeDamage()
    {
        if(PlayerTarget == null)
        {
            return null;
        }

        CreateParticle();

        float Critical = character.GetCriticalChance;

        var t = ObjectPooler.Instance.GetPlayerDamageText();

        if (PlayerTarget != null)
        {
            t.gameObject.SetActive(true);

            t.transform.SetParent(PlayerTarget.GetComponent<Health>().GetDamageTextParent.transform, false);

            if (PlayerTarget.GetComponent<Health>().GetIsImmune)
            {
                t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + "0";
            }
            else
            {
                #region CriticalHitCalculation
                if (Random.value * 100 <= Critical)
                {
                    float CritCalc = character.CharacterStrength * 1.25f;

                    Mathf.Round(CritCalc);

                    if ((int)CritCalc - PlayerTarget.GetComponent<Character>().CharacterDefense < 0)
                    {
                        PlayerTarget.GetComponent<Health>().ModifyHealth(-1);

                        t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=35>" + "1" + "!";
                    }
                    else
                    {
                        PlayerTarget.GetComponent<Health>().ModifyHealth(-((int)CritCalc - PlayerTarget.CharacterDefense));

                        t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=35>" + ((int)CritCalc - PlayerTarget.CharacterDefense).ToString() + "!";
                    }
                }
                else
                {
                    if (character.CharacterStrength - PlayerTarget.GetComponent<Character>().CharacterDefense < 0)
                    {
                        PlayerTarget.GetComponent<Health>().ModifyHealth(-1);

                        t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + "1";
                    }
                    else
                    {
                        PlayerTarget.GetComponent<Health>().ModifyHealth(-(character.CharacterStrength - PlayerTarget.CharacterDefense));

                        t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + (character.CharacterStrength - PlayerTarget.CharacterDefense).ToString();
                    }
                }
                #endregion

                if(PlayerTarget.GetComponent<Animator>().GetFloat("Speed") < 1 && !PlayerTarget.GetComponent<Animator>().GetBool("Attacking"))
                {
                    PlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
                }
            }

            if (PlayerTarget.GetComponent<Health>().GetReflectingDamage)
            {
                ReflectedDamage();
            }
        }
        return t.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void CreateParticle()
    {
        if(settings.UseParticleEffects)
        {
            var Hitparticle = ObjectPooler.Instance.GetHitParticle();

            Hitparticle.SetActive(true);

            Hitparticle.transform.position = new Vector3(PlayerTarget.transform.position.x, PlayerTarget.transform.position.y + 0.6f, PlayerTarget.transform.position.z);

            Hitparticle.transform.SetParent(PlayerTarget.transform, true);
        }
    }

    private void GetMonsterEntryText()
    {
        var MonsterEntryTxt = ObjectPooler.Instance.GetMonsterEntryText();

        MonsterEntryTxt.SetActive(true);

        MonsterEntryTxt.transform.SetParent(GameManager.Instance.GetMonsterEntryTransform, false);
    }
}