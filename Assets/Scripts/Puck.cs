﻿#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BossStates { Idle, Chase, Attack, ApplyingAttack, Skill, SkillAnimation, Damaged, Immobile, MovingToPosition, RotateToPosition }

[System.Serializable]
public class Phases
{
    [SerializeField]
    BossAiStates[] states;

    public BossAiStates[] GetBossAiStates
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
}

[System.Serializable]
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
    private Phases[] phases;

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
    private float MoveSpeed, AttackRange, AttackDelay, AutoAttackTime, LookSpeed;

    [SerializeField]
    [Tooltip("Current targeted Player. Keep this empty!")]
    private Character PlayerTarget = null;

    [SerializeField]
    private SphereCollider EnemyTriggerSphere;

    [SerializeField]
    private ParticleSystem HitParticle;

    [SerializeField]
    private Transform BossPosition;

    [SerializeField]
    private ParticleSystem[] Walls;

    [SerializeField]
    private GameObject treasureChest, ChestSpawnParticle;

    [SerializeField]
    private GameObject[] AddsToSpawn, MushroomObjs;

    [SerializeField]
    private GameObject SwordObj;

    [SerializeField]
    private Quaternion BossRotation;

    private float DistanceToTarget;

    private bool PlayerEntry;

    private int StateArrayIndex;

    [SerializeField]
    private int PhaseIndex;

    [SerializeField]
    private int[] HpToChangePhase;

    public Phases[] GetPhases
    {
        get
        {
            return phases;
        }
        set
        {
            phases = value;
        }
    }

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

    public int GetPhaseIndex
    {
        get
        {
            return PhaseIndex;
        }
        set
        {
            PhaseIndex = value;
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

    public GameObject GetSwordObj
    {
        get
        {
            return SwordObj;
        }
        set
        {
            SwordObj = value;
        }
    }

    public void IncreaseArray()
    {
        StateArrayIndex++;
        if (StateArrayIndex >= phases[PhaseIndex].GetBossAiStates.Length)
        {
            StateArrayIndex = 0;
        }
    }

    private void Awake()
    {
        states = BossStates.Idle;

        Idle();
    }

    private void OnEnable()
    {
        ResetStats();
    }

    private void Update()
    {
        if(enemySkills.GetIsRotating)
        {
            enemySkills.SylvanStormRotation();
        }

        if (this.character.CurrentHealth > 0)
        {
            switch (states)
            {
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
                case (BossStates.MovingToPosition):
                    MoveToPosition();
                    break;
                case (BossStates.RotateToPosition):
                    RotateToPosition();
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
        puckAnimations.IdleAnimator();
    }

    private void Chase()
    {
        puckAnimations.MoveAnimator();

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
                        states = phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetState;
                    }
                }
                else
                {
                    enemy.GetHealth.IncreaseHealth(character.MaxHealth);
                    enemy.GetLocalHealthInfo();

                    PlayerTarget = null;
                    AutoAttackTime = 0;
                }
            }
            else
            {
                states = BossStates.Attack;
            }
        }
    }

    private void Attack()
    {
        puckAnimations.IdleAnimator();

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
                    states = phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetState;
                }
            }
            else
            {
                ResetStats();
            }
        }
        else
        {
            states = BossStates.Chase;
        }
    }

    private void MoveToPosition()
    {
        puckAnimations.MoveAnimator();

        Vector3 Distance = new Vector3(BossPosition.position.x - this.transform.position.x, 0,
                                       BossPosition.position.z - this.transform.position.z).normalized;

        Quaternion LookDir = Quaternion.LookRotation(Distance);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookSpeed * Time.deltaTime);

        this.transform.position += Distance * MoveSpeed * Time.deltaTime;

        if(Vector3.Distance(this.transform.position, BossPosition.position) <= 0.1f)
        {
            StateArrayIndex = 0;
            PhaseIndex++;

            states = BossStates.RotateToPosition;
        }
    }

    private void RotateToPosition()
    {
        Quaternion Rot = Quaternion.Euler(0, 180, 0);

        transform.rotation = Quaternion.Slerp(this.transform.rotation, Rot, LookSpeed * Time.deltaTime).normalized;

        if (Quaternion.Angle(transform.rotation, Rot) <= 3.5f)
        {
            StateArrayIndex = 0;
            PhaseIndex = 2;

            states = phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetState;
        }
    }

    public void CheckHP()
    {
        float HpCap = ((float)character.CurrentHealth / (float)character.MaxHealth) * 100f;

        if(PhaseIndex < HpToChangePhase.Length)
        {
            if (HpCap <= HpToChangePhase[PhaseIndex])
            {
                StateArrayIndex = 0;
                PhaseIndex++;

                states = phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetState;
            }
        }
        else
        {
            return;
        }
    }

    public void IncrementPhase()
    {
        StateArrayIndex = 0;
        PhaseIndex++;
    }

    private void ApplyingNormalAtk()
    {
        puckAnimations.AttackAnimator();
    }

    private void Skill()
    {
        enemySkills.ChooseSkill(phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetSkillIndex);
    }

    //Sets the enemy to this state if they are inflicted with the stun/sleep status effect.
    private void Immobile()
    {
        puckAnimations.IdleAnimator();
    }

    public void Damage()
    {
        puckAnimations.DamagedAnimator();
    }

    public void DisableWalls()
    {
        Walls[0].gameObject.SetActive(false);
        Walls[1].gameObject.SetActive(false);
    }

    public void EnableChestSpawnParticle()
    {
        ChestSpawnParticle.SetActive(true);
    }

    public void DisableChestSpawnParticle()
    {
        ChestSpawnParticle.SetActive(false);
    }

    public void SpawnTreasureChests()
    {
        treasureChest.SetActive(true);
    }

    private void DisableWall1()
    {
        var main = Walls[0].main;
        main.loop = false;
    }

    private void DisableWall2()
    {
        var main2 = Walls[1].main;
        main2.loop = false;
    }

    public void Dead()
    {
        DisableWall1();
        DisableWall2();

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
            enemySkills.DisablePuckRadiusImage();
            enemySkills.DisablePuckRadius();
        }

        GameManager.Instance.GetEventSystem.SetSelectedGameObject(null);
        GameManager.Instance.GetEnemyObject = null;
        GameManager.Instance.GetLastEnemyObject = null;

        enemy.ToggleHealthBar();

        puckAnimations.DeadAnimator();

        if (this.GetComponent<ItemDrop>() != null)
        {
            this.GetComponent<ItemDrop>().DropItem();
        }

        CheckForInformation();
    }

    private void CheckForInformation()
    {
        var monsterinfo = monsterInformation;

        if (monsterBook.GetBossTransform.childCount <= 0)
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
        PhaseIndex = 0;

        transform.position = BossPosition.position;

        EnemyTriggerSphere.gameObject.SetActive(true);

        character.CurrentHealth = character.MaxHealth;
        enemy.GetFilledBar();
        enemy.GetLocalHealth.gameObject.SetActive(true);
        enemy.GetLocalHealthInfo();

        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        character.GetRigidbody.useGravity = true;

        states = BossStates.Idle;
        Idle();

        transform.rotation = BossRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerEntry = true;
        if (other.gameObject.GetComponent<PlayerController>())
        {
            PlayerTarget = other.GetComponent<Character>();
            states = BossStates.Chase;
            EnemyTriggerSphere.gameObject.SetActive(false);
        }
    }

    public void CheckTarget()
    {
        if (!PlayerEntry)
        {
            PlayerTarget = null;
            states = BossStates.Idle;
            Idle();
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

            if(!SkillsManager.Instance.GetActivatedSkill)
            {
                PlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
            }
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

    public void SpawnAdds()
    {
        for(int i = 0; i < AddsToSpawn.Length; i++)
        {
            AddsToSpawn[i].SetActive(true);
        }
    }

    public void DisableMushroomObjs()
    {
        for (int i = 0; i < MushroomObjs.Length; i++)
        {
            MushroomObjs[i].SetActive(false);
        }
    }

    public void PuckHitSE()
    {
        SoundManager.Instance.PuckHit();
    }

    public void RightMoveSE()
    {
        SoundManager.Instance.PuckRightFootStep();
    }

    public void LeftMoveSE()
    {
        SoundManager.Instance.PuckLeftFootStep();
    }

    public void PuckFall()
    {
        SoundManager.Instance.PuckFall();
    }
}
