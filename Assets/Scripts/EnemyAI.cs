﻿#pragma warning disable 0649
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
    private Character Knight, ShadowPriest, Toadstool;

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
    private EnemyConnection enemyConnection = null;

    [SerializeField]
    private Transform[] Waypoints;

    [SerializeField]
    private Transform OriginPoint = null;

    [SerializeField]
    private float MoveSpeed, AttackRange, AttackDelay, AutoAttackTime, LookSpeed;

    private float DefaultAttackDelay, DefaultMoveSpeed;

    [SerializeField] [Tooltip("Current targeted Player. Keep this empty!")]
    private Character PlayerTarget = null;

    [SerializeField]
    private SphereCollider EnemyTriggerSphere = null;

    [SerializeField]
    private ParticleSystem HitParticle;

    [SerializeField]
    private float TimeToMoveAgain, WayPointDistance, PlayerDistance, OuterAttackDistance; //A value that determines how long the enemy will stay at one waypoint before moving on to the next.

    [SerializeField]
    private float TimeToMove, DistanceToTarget, HealTime;

    private bool StandingStill, PlayerEntry, IsDead, IsDisabled;

    private int WaypointIndex;

    [SerializeField]
    private bool IsHostile, IsAnAdd, IsAPuzzleComponent, IsAbushPuzzleComponent, IsATreasurePuzzleComponent, IsAMagicWallPuzzle, IsAsecretCharacterPuzzle;

    [SerializeField]
    private bool IsUsingAnimator;

    private int StateArrayIndex;

    private Vector3 Distance;

    private Quaternion LookDir;

    private bool IsDoomed, Healing, GainedEXP;

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

    public Enemy GetEnemy
    {
        get
        {
            return enemy;
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

    public float GetMoveSpeed
    {
        get
        {
            return MoveSpeed;
        }
        set
        {
            MoveSpeed = value;
        }
    }

    public float GetDefaultMoveSpeed
    {
        get
        {
            return DefaultMoveSpeed;
        }
        set
        {
            DefaultMoveSpeed = value;
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

    public float GetDefaultAttackDelay
    {
        get
        {
            return DefaultAttackDelay;
        }
        set
        {
            DefaultAttackDelay = value;
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

    public bool GetPlayerEntry
    {
        get
        {
            return PlayerEntry;
        }
        set
        {
            PlayerEntry = value;
        }
    }

    public bool GetIsDead
    {
        get
        {
            return IsDead;
        }
        set
        {
            IsDead = value;
        }
    }

    public bool GetIsDisabled
    {
        get
        {
            return IsDisabled;
        }
        set
        {
            IsDisabled = value;
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

    public bool GetIsABushPuzzleComponent
    {
        get
        {
            return IsAbushPuzzleComponent;
        }
        set
        {
            IsAbushPuzzleComponent = value;
        }
    }

    public bool GetIsATreasurePuzzleComponent
    {
        get
        {
            return IsATreasurePuzzleComponent;
        }
        set
        {
            IsATreasurePuzzleComponent = value;
        }
    }

    public bool GetIsAMagicWallPuzzleComponent
    {
        get
        {
            return IsAMagicWallPuzzle;
        }
        set
        {
            IsAMagicWallPuzzle = value;
        }
    }

    public bool GetIsAsecretCharacterPuzzleComponent
    {
        get
        {
            return IsAsecretCharacterPuzzle;
        }
        set
        {
            IsAsecretCharacterPuzzle = value;
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

        DefaultMoveSpeed = MoveSpeed;
        DefaultAttackDelay = AttackDelay;
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

        if(PlayerTarget != null)
        {
            if(PlayerTarget.CurrentHealth <= 0)
            {
                RemoveStatusEffects();
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
        DistanceToTarget = Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z),
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

            Distance = new Vector3(Waypoints[WaypointIndex].position.x - this.transform.position.x, 0,
                                   Waypoints[WaypointIndex].position.z - this.transform.position.z).normalized;

            if(Distance != Vector3.zero)
            {
                LookDir = Quaternion.LookRotation(Distance).normalized;
            }

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

        if (DistanceToTarget <= 0.5f)
        {
            StandingStill = true;
            if(Waypoints.Length == 1)
            {
                return;
            }
            else
            {
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

        if (character.CurrentHealth >= character.MaxHealth)
        {
            return;
        }
        else
        {
            Hel();
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
                Distance = new Vector3(PlayerTarget.transform.position.x - this.transform.position.x, 0,
                                       PlayerTarget.transform.position.z - this.transform.position.z).normalized;

                LookDir = Quaternion.LookRotation(Distance);

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
            Distance = new Vector3(PlayerTarget.transform.position.x - this.transform.position.x, 0,
                                           PlayerTarget.transform.position.z - this.transform.position.z).normalized;

            LookDir = Quaternion.LookRotation(Distance);

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
                if(enemyConnection != null)
                {
                    enemyConnection.GetIsInsideCollider = false;
                }

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
        IsDead = true;
        IsDoomed = false;

        if(gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
        }

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

        if(!GainedEXP)
        {
            enemy.ReturnCoins();

            enemy.ReturnExperience();

            GainedEXP = true;
        }

        if (!IsUsingAnimator)
        {
            Anim.DeathAni();
        }
        else
        {
            Anim.DeadAnimator();
        }

        if(SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetTarget != null)
        {
            SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().RemoveTarget();
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

            character.GetCharacterData.CheckedData = true;
        }
        else
        {
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

                character.GetCharacterData.CheckedData = true;
            }
        }
    }

    private IEnumerator WaitToTargetPlayer()
    {
        yield return new WaitForSeconds(1f);
        SelectPlayerTarget();
    }

    private void Hel()
    {
        HealTime += Time.deltaTime;
        if(HealTime >= 1.5f)
        {
            InvokeHeal();
            HealTime = 0;
        }
    }

    private void InvokeHeal()
    {
        float HealthAmount = 0.10f * character.MaxHealth;

        Mathf.Round(HealthAmount);

        enemy.GetHealth.IncreaseHealth((int)HealthAmount);

        enemy.GetLocalHealthInfo();

        if(character.CurrentHealth >= character.MaxHealth)
        {
            HealTime = 0;
        }
    }

    private void SelectPlayerTarget()
    {
        if (Knight.gameObject.activeInHierarchy)
        {
            PlayerTarget = Knight;
        }
        if (ShadowPriest.gameObject.activeInHierarchy)
        {
            PlayerTarget = ShadowPriest;
        }
        if (Toadstool.gameObject.activeInHierarchy)
        {
            PlayerTarget = Toadstool;
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
            if (mi.GetCharacter.GetCharacterData.CharacterName == character.GetCharacterData.CharacterName && !character.GetCharacterData.CheckedData)
            {
                character.GetCharacterData.CheckedData = true;

                if (mi.GetCharacterData.Count < monsterBook.GetMonsterLevelButtons.Length)
                {
                    mi.GetCharacterData.Add(character.GetCharacterData);
                    GetMonsterEntryText();
                }

                if (mi.GetIsSelected)
                {
                    mi.ShowLevelButtons();
                }

                SameName = true;
            }
            else if(mi.GetCharacter.GetCharacterData.CharacterName != character.GetCharacterData.CharacterName && !character.GetCharacterData.CheckedData)
            {
                SameName = false;
            }
            else if(mi.GetCharacter.GetCharacterData.CharacterName == character.GetCharacterData.CharacterName && character.GetCharacterData.CheckedData)
            {
                SameName = true;
            }
        }
        return SameName;
    }

    //Resets the enemy's stats when enabled in the scene.
    private void ResetStats()
    {
        IsDead = false;
        GainedEXP = false;

        if(gameObject.GetComponent<AudioSource>() != null)
        {
            if(!settings.MuteAudio)
            {
                gameObject.GetComponent<AudioSource>().volume = 1;
            }
        }

        StateArrayIndex = 0;

        if(enemyConnection != null)
        {
            if(enemyConnection.GetIsInsideCollider)
            {
                PlayerEntry = true;
                PlayerTarget = enemyConnection.GetCharacter;
                states = States.Chase;
                enemy.GetExperience = enemyConnection.GetCharacter.GetComponent<Experience>();
            }
            else
            {
                states = States.Patrol;
            }
        }
        else
        {
            EnemyTriggerSphere.enabled = true;

            states = States.Patrol;
        }

        if(!IsAnAdd)
        {
            WaypointIndex = 0;

            transform.position = Waypoints[WaypointIndex].position;

            if (IsHostile)
            {
                EnemyTriggerSphere.enabled = true;
            }
            else
            {
                EnemyTriggerSphere.enabled = false;
            }

            enemy.GetLocalHealth.gameObject.SetActive(true);
            enemy.GetHealth.ResetHealthAnimation();
            enemy.GetHealth.IncreaseHealth(character.MaxHealth);
            enemy.GetLocalHealthInfo();
            enemy.GetEnemyInfo();

            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            character.GetRigidbody.useGravity = true;
        }
        else
        {
            enemy.GetHealth.IncreaseHealth(character.MaxHealth);
            enemy.GetLocalHealth.gameObject.SetActive(true);
            enemy.GetLocalHealthInfo();
            enemy.GetEnemyInfo();

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
            if(OriginPoint != null)
            {
                if (Vector3.Distance(transform.position, OriginPoint.position) >= WayPointDistance)
                {
                    EndBattle();
                }
                if (PlayerTarget != null)
                {
                    if (Vector3.Distance(transform.position, PlayerTarget.transform.position) >= PlayerDistance)
                    {
                        EndBattle();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerEntry = true;
        if (other.gameObject.GetComponent<PlayerController>())
        {
            PlayerTarget = other.GetComponent<Character>();
            states = States.Chase;

            HealTime = 0;

            enemy.GetExperience = other.gameObject.GetComponent<Experience>();
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if(CheckDoomedStatusEffect())
        {
            RemoveStatusEffects();
        }
        else
        {
            if (states != States.Patrol)
            {
                if (other.gameObject.GetComponent<PlayerController>())
                {
                    EndBattle();
                }
            }
        }
    }

    private bool CheckDoomedStatusEffect()
    {
        foreach (EnemyStatusIcon esi in enemy.GetDebuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
        {
            if (esi.GetStatusEffect == StatusEffect.Doom)
            {
                IsDoomed = true;
            }
        }

        return IsDoomed;
    }

    public void EndBattle()
    {
        RemoveStatusEffects();

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
            StateArrayIndex = 0;
        }
        HealTime = 0;
    }

    private void RemoveStatusEffects()
    {
        foreach(EnemyStatusIcon esi in enemy.GetDebuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
        {
            if(enemy.GetDebuffTransform.childCount > 0)
            {
                esi.RemoveEffect();
            }
        }

        if(enemy.GetBuffTransform != null)
        {
            foreach (EnemyStatusIcon esi in enemy.GetBuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
            {
                if (enemy.GetBuffTransform.childCount > 0)
                {
                    esi.RemoveEffect();
                }
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
        float ReflectedValue = 0;

        if(GameManager.Instance.GetKnight.activeInHierarchy)
        {
            ReflectedValue = 0.10f * PlayerTarget.GetComponent<Character>().MaxHealth;
        }
        else if(GameManager.Instance.GetToadstool.activeInHierarchy)
        {
            ReflectedValue = 0.05f * PlayerTarget.GetComponent<Character>().MaxHealth;
        }

        Mathf.Round(ReflectedValue);

        var Damagetext = ObjectPooler.Instance.GetEnemyDamageText();

        Damagetext.SetActive(true);

        Damagetext.transform.SetParent(GetComponentInChildren<Health>().GetDamageTextParent.transform, false);

        GetComponentInChildren<Health>().ModifyHealth(-(int)ReflectedValue);

        Damagetext.GetComponentInChildren<TextMeshProUGUI>().text = "<size=20>" + (int)ReflectedValue;

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
            if(PlayerTarget.GetComponent<Health>().GetHasStatusGiftPassive)
            {
                GiveStatusEffect();
            }

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

                    if ((int)CritCalc - PlayerTarget.GetComponent<Character>().CharacterDefense <= 0)
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
                    if (character.CharacterStrength - PlayerTarget.GetComponent<Character>().CharacterDefense <= 0)
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

                if(PlayerTarget.GetComponent<Animator>().GetFloat("Speed") < 1 && !PlayerTarget.GetComponent<Animator>().GetBool("Attacking") && 
                   !PlayerTarget.GetComponent<PlayerAnimations>().GetAnimator.GetBool("Damaged"))
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

    public void CheckItemDrop()
    {
        if(gameObject.GetComponent<ItemDrop>())
        {
            gameObject.GetComponent<ItemDrop>().DropItem();
        }
    }

    private void GiveStatusEffect()
    {
        if (Random.value * 100 <= 25)
        {
            if(PlayerTarget.GetComponent<BasicAttack>().GetInflictsDoomStatus)
            {
                if(!CheckDoomedStatusEffect())
                {
                    if (Random.value * 100 <= 5)
                    {
                        var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

                        StatusTxt.SetActive(true);

                        StatusTxt.transform.SetParent(enemy.GetUI.transform, false);

                        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ Doomed";

                        StatusTxt.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetDoomedSprite;

                        GameObject statuseffectIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

                        statuseffectIcon.SetActive(true);

                        statuseffectIcon.GetComponent<EnemyStatusIcon>().GetHasDoomedStatus = true;

                        statuseffectIcon.transform.SetParent(enemy.GetDebuffTransform, false);

                        statuseffectIcon.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetDoomedSprite;

                        statuseffectIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = StatusEffect.Doom;
                        statuseffectIcon.GetComponent<EnemyStatusIcon>().GetPlayer = PlayerTarget.GetComponent<PlayerController>();
                        statuseffectIcon.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetDoomedSprite;
                        statuseffectIcon.GetComponent<EnemyStatusIcon>().DoomedStatus();
                    }
                }
                else
                {
                    StatusGiftEffects(Random.Range(0, 3));
                }
            }
            else
            {
                StatusGiftEffects(Random.Range(0, 3));
            }
        }
    }

    private int StatusGiftEffects(int RandomNumber)
    {
        StatusEffect[] GiftEffects = { StatusEffect.Poison, StatusEffect.Slow, StatusEffect.Stun };

        if(RandomNumber == 0)
        {
            if(!CheckPoisonStatusEffect())
            {
                var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

                StatusTxt.SetActive(true);

                StatusTxt.transform.SetParent(enemy.GetUI.transform, false);

                StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ Poison";

                StatusTxt.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetPoisonSprite;

                GameObject statuseffectIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

                statuseffectIcon.SetActive(true);

                statuseffectIcon.transform.SetParent(enemy.GetDebuffTransform, false);

                statuseffectIcon.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetPoisonSprite;

                statuseffectIcon.GetComponent<EnemyStatusIcon>().GetHasPoisonStatus = true;

                statuseffectIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = GiftEffects[RandomNumber];
                statuseffectIcon.GetComponent<EnemyStatusIcon>().GetPlayer = PlayerTarget.GetComponent<PlayerController>();
                statuseffectIcon.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetPoisonSprite;
                statuseffectIcon.GetComponent<EnemyStatusIcon>().PoisonStatus();

                statuseffectIcon.GetComponent<EnemyStatusIcon>().CreatePoisonEffectParticle();
            }
        }
        if(RandomNumber == 1)
        {
            if(!CheckSlowStatusEffect())
            {
                var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

                StatusTxt.SetActive(true);

                StatusTxt.transform.SetParent(enemy.GetUI.transform, false);

                StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ Slowed";

                StatusTxt.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetSlowedSprite;

                GameObject statuseffectIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

                statuseffectIcon.SetActive(true);

                statuseffectIcon.transform.SetParent(enemy.GetDebuffTransform, false);

                statuseffectIcon.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetSlowedSprite;

                statuseffectIcon.GetComponent<EnemyStatusIcon>().GetHasSlowStatus = true;

                statuseffectIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = GiftEffects[RandomNumber];
                statuseffectIcon.GetComponent<EnemyStatusIcon>().GetPlayer = PlayerTarget.GetComponent<PlayerController>();
                statuseffectIcon.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetSlowedSprite;
                statuseffectIcon.GetComponent<EnemyStatusIcon>().SlowStatus();
            }
        }
        if(RandomNumber == 2)
        {
            if(!CheckStunStatusEffect())
            {
                var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

                StatusTxt.SetActive(true);

                StatusTxt.transform.SetParent(enemy.GetUI.transform, false);

                StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ Stun";

                StatusTxt.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetStunSprite;

                GameObject statuseffectIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

                statuseffectIcon.SetActive(true);

                statuseffectIcon.transform.SetParent(enemy.GetDebuffTransform, false);

                statuseffectIcon.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetStunSprite;

                statuseffectIcon.GetComponent<EnemyStatusIcon>().GetHasStunStatus = true;

                statuseffectIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = GiftEffects[RandomNumber];
                statuseffectIcon.GetComponent<EnemyStatusIcon>().GetPlayer = PlayerTarget.GetComponent<PlayerController>();
                statuseffectIcon.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetStunSprite;
                statuseffectIcon.GetComponent<EnemyStatusIcon>().StunStatus();

                statuseffectIcon.GetComponent<EnemyStatusIcon>().CreateStunEffectParticle();
            }
        }
        return RandomNumber;
    }

    private bool CheckPoisonStatusEffect()
    {
        bool PoisonStatus = false;

        foreach (EnemyStatusIcon enemystatus in enemy.GetDebuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
        {
            if (enemystatus.GetStatusEffect == StatusEffect.Poison)
            {
                PoisonStatus = true;
            }
        }
        return PoisonStatus;
    }

    private bool CheckSlowStatusEffect()
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

    private bool CheckStunStatusEffect()
    {
        bool StunStatus = false;

        foreach (EnemyStatusIcon enemystatus in enemy.GetDebuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
        {
            if (enemystatus.GetStatusEffect == StatusEffect.Stun)
            {
                StunStatus = true;
            }
        }
        return StunStatus;
    }
}