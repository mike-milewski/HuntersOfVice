#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BossStates { Patrol, Chase, Attack, ApplyingAttack, Skill, SkillAnimation, Damaged, Immobile }

public class BossAiStates
{
    [SerializeField]
    private BossStates state;

    [SerializeField]
    private int SkillIndex;

    public BossStates GetState
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

public class Puck : MonoBehaviour
{
    [SerializeField]
    private BossStates states;

    [SerializeField]
    private EnemyType enemyType;

    [SerializeField]
    private BossAiStates[] aiStates;

    [SerializeField]
    private Character character;

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
    private PuckAnimations puckAnimations;

    [SerializeField]
    private Transform[] Waypoints;

    [SerializeField]
    private float MoveSpeed, AttackRange, AttackDelay, AutoAttackTime, LookSpeed;

    [SerializeField]
    [Tooltip("Current targeted Player. Keep this empty!")]
    private Character PlayerTarget = null;

    [SerializeField]
    private SphereCollider EnemyTriggerSphere;

    [SerializeField]
    private ParticleSystem HitParticle;

    [SerializeField]
    private float TimeToMoveAgain; //A value that determines how long the enemy will stay at one waypoint before moving on to the next.

    private float TimeToMove, DistanceToTarget;

    private bool StandingStill, PlayerEntry;

    private int WaypointIndex;

    [SerializeField]
    private bool IsHostile;

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

    public PuckAnimations GetAnimation
    {
        get
        {
            return puckAnimations;
        }
        set
        {
            puckAnimations = value;
        }
    }

    public BossAiStates[] GetAiStates
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
        states = BossStates.Patrol;

        TimeToMove = TimeToMoveAgain;
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
                case (BossStates.Patrol):
                    Patrol();
                    break;
                case (BossStates.Chase):
                    Chase();
                    break;
                case (BossStates.Attack):
                    Attack();
                    break;
                case (BossStates.ApplyingAttack):
                    ApplyingNormalAtk();
                    break;
                case (BossStates.Skill):
                    Skill();
                    break;
                case (BossStates.Damaged):
                    Damage();
                    break;
                case (BossStates.Immobile):
                    Immobile();
                    break;
                case (BossStates.SkillAnimation):
                    break;
            }
        }
    }

    public BossStates GetStates
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
        float DistanceToWayPoint = Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z),
                                                    new Vector3(Waypoints[WaypointIndex].position.x, 0, Waypoints[WaypointIndex].position.z));

        if (!StandingStill)
        {
            if (!IsUsingAnimator)
            {
                puckAnimations.RunAni();
            }
            else
            {
                puckAnimations.MoveAnimator();
            }

            Vector3 Distance = new Vector3(Waypoints[WaypointIndex].position.x - this.transform.position.x, 0,
                                           Waypoints[WaypointIndex].position.z - this.transform.position.z).normalized;

            Quaternion LookDir = Quaternion.LookRotation(Distance);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookSpeed * Time.deltaTime);

            this.transform.position += Distance * MoveSpeed * Time.deltaTime;
        }
        else
        {
            if (!IsUsingAnimator)
            {
                puckAnimations.IdleAni();
            }
            else
            {
                puckAnimations.IdleAnimator();
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

        if (!IsUsingAnimator)
        {
            puckAnimations.RunAni();
        }
        else
        {
            puckAnimations.MoveAnimator();
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
                    states = BossStates.Patrol;
                }
            }
            else
            {
                states = BossStates.Attack;
            }
        }
        else
        {
            states = BossStates.Patrol;
        }
    }

    private void Attack()
    {
        if (!IsUsingAnimator)
        {
            puckAnimations.IdleAni();
        }
        else
        {
            puckAnimations.IdleAnimator();
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
                states = BossStates.Patrol;
            }
        }
        else
        {
            states = BossStates.Chase;
        }
    }

    private void ApplyingNormalAtk()
    {
        if (!IsUsingAnimator)
        {
            puckAnimations.AttackAni();
        }
        else
        {
            puckAnimations.AttackAnimator();
        }
    }

    private void Skill()
    {
        enemySkills.ChooseSkill(aiStates[StateArrayIndex].GetSkillIndex);
    }

    //Sets the enemy to this state if they are inflicted with the stun/sleep status effect.
    private void Immobile()
    {
        if (!IsUsingAnimator)
        {
            puckAnimations.IdleAni();
        }
        else
        {
            puckAnimations.IdleAnimator();
        }
    }

    public void Damage()
    {
        if (!IsUsingAnimator)
        {
            puckAnimations.DamageAni();
        }
        else
        {
            puckAnimations.DamagedAnimator();
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

        GameManager.Instance.GetEventSystem.SetSelectedGameObject(null);
        GameManager.Instance.GetEnemyObject = null;
        GameManager.Instance.GetLastEnemyObject = null;

        enemy.ToggleHealthBar();

        enemy.ReturnCoins();

        enemy.ReturnExperience();

        if (!IsUsingAnimator)
        {
            puckAnimations.DeathAni();
        }
        else
        {
            puckAnimations.DeadAnimator();
        }

        if (this.GetComponent<ItemDrop>() != null)
        {
            this.GetComponent<ItemDrop>().DropItem();
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

    private Transform EnemyTransform()
    {
        Transform EnemyButtonTransform = null;

        switch (enemyType)
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
                if (mi.GetCharacterData.Count < monsterBook.GetMonsterLevelButtons.Length)
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
        character.CurrentHealth = character.MaxHealth;
        enemy.GetFilledBar();
        enemy.GetLocalHealth.gameObject.SetActive(true);
        enemy.GetLocalHealthInfo();

        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        character.GetRigidbody.useGravity = true;

        states = BossStates.Patrol;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerEntry = true;
        if (other.gameObject.GetComponent<PlayerController>())
        {
            PlayerTarget = other.GetComponent<Character>();
            states = BossStates.Chase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && IsHostile)
        {
            PlayerEntry = false;
            if (states != BossStates.SkillAnimation)
            {
                PlayerTarget = null;
                states = BossStates.Patrol;
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
        if (other.gameObject.GetComponent<PlayerController>() && !IsHostile)
        {
            PlayerEntry = false;
            if (states != BossStates.SkillAnimation)
            {
                PlayerTarget = null;
                states = BossStates.Patrol;
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

    public void CheckTarget()
    {
        if (!PlayerEntry)
        {
            PlayerTarget = null;
            states = BossStates.Patrol;
            AutoAttackTime = 0;
            enemySkills.DisableRadiusImage();
            enemySkills.DisableRadius();
            enemySkills.GetActiveSkill = false;
            enemySkills.GetSkillBar.gameObject.SetActive(false);
        }
        else
        {
            AutoAttackTime = 0;
            states = BossStates.Attack;
        }
    }

    public TextMeshProUGUI TakeDamage()
    {
        if (PlayerTarget == null)
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

            PlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
        }
        return t.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void CreateParticle()
    {
        if (settings.UseParticleEffects)
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
