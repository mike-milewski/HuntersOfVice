#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BossStates { Idle, Chase, Attack, ApplyingAttack, Skill, SkillAnimation, Damaged, Immobile, MovingToPosition, RotateToPosition, RotateToPositionTwo }

[System.Serializable]
public class Phases
{
    [SerializeField]
    BossAiStates[] states;

    [SerializeField][TextArea]
    private string SpeechText;

    [SerializeField]
    private bool DontCheckHP;

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

    public string GetSpeechText
    {
        get
        {
            return SpeechText;
        }
        set
        {
            SpeechText = value;
        }
    }

    public bool GetDontCheckHP
    {
        get
        {
            return DontCheckHP;
        }
        set
        {
            DontCheckHP = value;
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
    private AudioChanger audioChanger;

    [SerializeField]
    private EnemySkills enemySkills;

    [SerializeField]
    private PuckAnimations puckAnimations;

    [SerializeField]
    private float MoveSpeed, AttackRange, AttackDelay, AutoAttackTime, LookSpeed, OuterAttackDistance;

    [SerializeField]
    [Tooltip("Current targeted Player. Keep this empty!")]
    private Character PlayerTarget = null;

    [SerializeField]
    private TextMeshProUGUI SpeechText;

    [SerializeField]
    private Animator SpeechBox;

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
    private GameObject[] AddsToSpawn, MushroomObjs, PoisonMushrooms, SoothingSpheres;

    [SerializeField]
    private GameObject SwordObj, WallTrigger;

    [SerializeField]
    private Quaternion BossRotation;

    private float DistanceToTarget;

    private bool PlayerEntry, MovingToPosition, RotatingToPosition, ChangingPhase, PoisonMushroomsEnlarged, OnEnabled;

    [SerializeField]
    private bool IsReseted;

    private int StateArrayIndex;

    [SerializeField]
    private int PhaseIndex, HpPhaseIndex;

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

    public bool GetIsMovingToPosition
    {
        get
        {
            return MovingToPosition;
        }
        set
        {
            MovingToPosition = value;
        }
    }

    public bool GetIsRotatingToPosition
    {
        get
        {
            return RotatingToPosition;
        }
        set
        {
            RotatingToPosition = value;
        }
    }

    public bool GetChangingPhase
    {
        get
        {
            return ChangingPhase;
        }
        set
        {
            ChangingPhase = value;
        }
    }

    public bool GetIsReseted
    {
        get
        {
            return IsReseted;
        }
        set
        {
            IsReseted = value;
        }
    }

    public void IncreaseArray()
    {
        StateArrayIndex++;

        if (StateArrayIndex >= phases[PhaseIndex].GetBossAiStates.Length || OnEnabled)
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
        PlayParticle();
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
                case (BossStates.MovingToPosition):
                    MoveToPosition();
                    break;
                case (BossStates.RotateToPosition):
                    RotateToPosition();
                    break;
                case (BossStates.RotateToPositionTwo):
                    RotateToPositionTwo();
                    break;
            }
        }
        if(PlayerTarget != null)
        {
            if(PlayerTarget.CurrentHealth <= 0)
            {
                ResetStats();
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
                    if (DistanceToTarget <= OuterAttackDistance)
                    {
                        if (AutoAttackTime >= AttackDelay)
                        {
                            states = phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetState;
                        }
                    }
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
        if(!MovingToPosition || !RotatingToPosition)
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
            }
            else
            {
                states = BossStates.Chase;
            }
        }
    }

    private void MoveToPosition()
    {
        MovingToPosition = true;

        puckAnimations.MoveAnimator();

        this.gameObject.GetComponent<BoxCollider>().isTrigger = true;

        Vector3 Distance = new Vector3(BossPosition.position.x - this.transform.position.x, 0,
                                       BossPosition.position.z - this.transform.position.z).normalized;

        Quaternion LookDir = Quaternion.LookRotation(Distance);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookSpeed * Time.deltaTime);

        this.transform.position += Distance * MoveSpeed * Time.deltaTime;

        if(Vector3.Distance(this.transform.position, BossPosition.position) <= 0.1f)
        {
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;

            StateArrayIndex = 0;
            PhaseIndex++;

            if(PhaseIndex == 2)
            {
                states = BossStates.RotateToPosition;
            }
            else
            {
                states = BossStates.RotateToPositionTwo;
            }
        }
    }

    private void RotateToPosition()
    {
        RotatingToPosition = true;

        Quaternion Rot = Quaternion.Euler(0, 180, 0);

        transform.rotation = Quaternion.Slerp(this.transform.rotation, Rot, LookSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, Rot) <= 3.5f)
        {
            StateArrayIndex = 0;
            PhaseIndex = 2;

            states = phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetState;

            EnableSpeech();

            ChangingPhase = false;
            MovingToPosition = false;
            RotatingToPosition = false;
        }
    }

    private void RotateToPositionTwo()
    {
        RotatingToPosition = true;

        Quaternion Rot = Quaternion.Euler(0, 180, 0);

        transform.rotation = Quaternion.Slerp(this.transform.rotation, Rot, LookSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, Rot) <= 3.5f)
        {
            StateArrayIndex = 0;
            PhaseIndex = 5;

            states = phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetState;

            EnableSpeech();

            ChangingPhase = false;
            MovingToPosition = false;
            RotatingToPosition = false;
        }
    }

    private void CheckPlayerTarget()
    {
        if(PlayerTarget != null)
        {
            if(PlayerTarget.CurrentHealth <= 0)
            {
                IsReseted = true;
            }
        }
    }

    public void CheckHP()
    {
        float HpCap = ((float)character.CurrentHealth / (float)character.MaxHealth) * 100f;

        if(!phases[PhaseIndex].GetDontCheckHP)
        {
            if (HpPhaseIndex < HpToChangePhase.Length)
            {
                if (HpCap <= HpToChangePhase[HpPhaseIndex])
                {
                    ChangingPhase = true;

                    if (HpPhaseIndex < HpToChangePhase.Length)
                    {
                        HpPhaseIndex++;
                    }

                    IncrementPhase();

                    SpawnSoothingSpheres();

                    states = phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetState;
                }
            }
            else
            {
                ChangingPhase = false;

                return;
            }
        }
    }

    private void SpawnSoothingSpheres()
    {
        for(int i = 0; i < SoothingSpheres.Length; i++)
        {
            SoothingSpheres[i].SetActive(true);
        }
    }

    private void DespawnSoothingSpheres()
    {
        for (int i = 0; i < SoothingSpheres.Length; i++)
        {
            SoothingSpheres[i].SetActive(false);
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
        if (PlayerTarget.CurrentHealth <= 0)
        {
            states = BossStates.Idle;
        }
        else
        {
            if (phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetSkillIndex > -1)
            {
                enemySkills.ChooseSkill(phases[PhaseIndex].GetBossAiStates[StateArrayIndex].GetSkillIndex);
            }
            else return;
        }
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

    private void EnableWall()
    {
        var main = Walls[0].main;
        Walls[0].gameObject.SetActive(false);
        main.loop = true;

        WallTrigger.SetActive(true);
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
        enemySkills.SetRotationToFalse();

        EnableAudioChanger();

        EnableSpeechDead();

        DisableWall1();
        DisableWall2();

        DespawnSoothingSpheres();

        KillAdds();
        DisablePoisonMushroomDamageRadius();

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

        enemy.ReturnCoins();

        enemy.ReturnExperience();

        CheckForInformation();
    }

    private void EnableAudioChanger()
    {
        audioChanger.gameObject.SetActive(true);

        audioChanger.GetChangeToMiniBossTheme = false;
        audioChanger.GetChangeToLevelTheme = true;
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
    public void ResetStats()
    {
        DespawnSoothingSpheres();

        DisableSpeech();

        PlayParticle();

        enemySkills.SetRotationToFalse();

        MovingToPosition = false;

        RotatingToPosition = false;

        OnEnabled = true;

        ChangingPhase = false;

        PlayerTarget = null;

        puckAnimations.ResetSkillAnimator();

        states = BossStates.Idle;
        Idle();

        PhaseIndex = 0;
        HpPhaseIndex = 0;
        StateArrayIndex = 0;
        AutoAttackTime = 0;

        transform.position = BossPosition.position;

        EnemyTriggerSphere.gameObject.SetActive(true);

        enemy.GetHealth.IncreaseHealth(character.MaxHealth);
        enemy.GetLocalHealth.gameObject.SetActive(true);
        enemy.GetLocalHealthInfo();

        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        character.GetRigidbody.useGravity = true;

        transform.rotation = BossRotation;

        EnableWall();

        ReturnAddsToPositionAndRotation();

        DespawnAdds();
        ShrinkPoisonMushrooms();
        EnableMushroomObjs();

        InvokeOnEnabledFalse();

        RemoveStatusEffects();
    }

    private void RemoveStatusEffects()
    {
        foreach (EnemyStatusIcon esi in enemy.GetDebuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
        {
            if (enemy.GetDebuffTransform.childCount > 0)
            {
                esi.RemoveEffect();
            }
        }

        if (enemy.GetBuffTransform != null)
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

    private void InvokeOnEnabledFalse()
    {
        Invoke("OnEnabledOff", 0.5f);
    }

    private void OnEnabledOff()
    {
        OnEnabled = false;
    }

    private void PlayParticle()
    {
        if(settings.UseParticleEffects)
        SpawnParticleEffect(new Vector3(BossPosition.position.x, BossPosition.position.y, BossPosition.position.z - 0.3f));
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerEntry = true;
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if(PhaseIndex == 0)
            {
                EnableSpeech();
            }

            PlayerTarget = other.GetComponent<Character>();
            states = BossStates.Chase;
            EnemyTriggerSphere.gameObject.SetActive(false);
            IsReseted = false;
        }
    }

    public void EnableSpeech()
    {
        if(character.CurrentHealth > 0)
        {
            if (phases[PhaseIndex].GetSpeechText != "")
            {
                SpeechBox.SetBool("Fade", true);

                SpeechText.text = phases[PhaseIndex].GetSpeechText;
            }
            Invoke("DisableSpeech", 3.0f);
        }
    }

    private void EnableSpeechDead()
    {
        SpeechBox.SetBool("Fade", true);

        SpeechText.text = "I...let everybody...down...";

        Invoke("DisableSpeech", 3.0f);
    }

    private void DisableSpeech()
    {
        SpeechBox.SetBool("Fade", false);
    }

    public void CheckTarget()
    {
        if (!PlayerEntry)
        {
            PlayerTarget = null;
            states = BossStates.Idle;
            Idle();
            AutoAttackTime = 0;
            enemySkills.DisablePuckRadiusImage();
            enemySkills.DisablePuckRadius();
            enemySkills.GetActiveSkill = false;
            enemySkills.GetSkillBar.gameObject.SetActive(false);
        }
        else
        {
            AutoAttackTime = 0;
            if(PlayerTarget != null)
            {
                if(PlayerTarget.CurrentHealth <= 0)
                {
                    ResetStats();
                }
                else
                {
                    states = BossStates.Attack;
                }
            }
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
                if (PlayerTarget.GetComponent<Animator>().GetFloat("Speed") < 1)
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

            AddsToSpawn[i].GetComponent<EnemyAI>().GetStates = States.Idle;

            SpawnParticleEffect(new Vector3(MushroomObjs[i].transform.position.x, MushroomObjs[i].transform.position.y, MushroomObjs[i].transform.position.z));
        }
    }

    private void ReturnAddsToPositionAndRotation()
    {
        for(int i = 0; i < AddsToSpawn.Length; i++)
        {
            AddsToSpawn[i].transform.position = MushroomObjs[i].transform.position;

            AddsToSpawn[i].transform.rotation = Quaternion.Euler(0, 180, 0);

            AddsToSpawn[i].GetComponent<EnemySkills>().DisableRadiusImage();
            AddsToSpawn[i].GetComponent<EnemySkills>().DisableRadius();
        }
    }

    public void DisableMushroomObjs()
    {
        for (int i = 0; i < MushroomObjs.Length; i++)
        {
            MushroomObjs[i].SetActive(false);
        }
    }

    private void EnableMushroomObjs()
    {
        for (int i = 0; i < MushroomObjs.Length; i++)
        {
            if (MushroomObjs[i].activeInHierarchy)
            {
                return;
            }
            else
            {
                MushroomObjs[i].SetActive(true);

                SpawnParticleEffect(new Vector3(MushroomObjs[i].transform.position.x, MushroomObjs[i].transform.position.y, MushroomObjs[i].transform.position.z));
            }
        }
    }

    public void EnlargePoisonMushrooms()
    {
        PoisonMushroomsEnlarged = true;

        for(int i = 0; i < PoisonMushrooms.Length; i++)
        {
            PoisonMushrooms[i].transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);

            SpawnParticleEffect(new Vector3(PoisonMushrooms[i].transform.position.x, PoisonMushrooms[i].transform.position.y, PoisonMushrooms[i].transform.position.z));

            PoisonMushrooms[i].GetComponentInChildren<ObstacleDamageRadius>().enabled = true;
        }
    }

    private void DisablePoisonMushroomDamageRadius()
    {
        for(int i = 0; i < PoisonMushrooms.Length; i++)
        {
            PoisonMushrooms[i].GetComponentInChildren<ObstacleDamageRadius>().enabled = false;
        }
    }

    private void ShrinkPoisonMushrooms()
    {
        if(PoisonMushroomsEnlarged)
        {
            for (int i = 0; i < PoisonMushrooms.Length; i++)
            {
                PoisonMushrooms[i].transform.localScale = new Vector3(1, 1, 1);

                SpawnParticleEffect(new Vector3(PoisonMushrooms[i].transform.position.x, PoisonMushrooms[i].transform.position.y, PoisonMushrooms[i].transform.position.z));

                PoisonMushrooms[i].GetComponentInChildren<ObstacleDamageRadius>().enabled = false;
            }
        }
        PoisonMushroomsEnlarged = false;
    }

    private void DespawnAdds()
    {
        for (int i = 0; i < AddsToSpawn.Length; i++)
        {
            if (AddsToSpawn[i].GetComponent<Enemy>().GetEnemySkillBar.gameObject.activeInHierarchy)
            {
                AddsToSpawn[i].GetComponent<Enemy>().GetEnemySkillBar.gameObject.SetActive(false);
                AddsToSpawn[i].GetComponent<EnemySkills>().GetActiveSkill = false;
            }

            AddsToSpawn[i].SetActive(false);
        }
    }

    //Kills all enemies spawned by the boss if the boss dies while the adds are still alive.
    private void KillAdds()
    {
        for(int i = 0; i < AddsToSpawn.Length; i++)
        {
            if (AddsToSpawn[i].activeInHierarchy)
            {
                AddsToSpawn[i].GetComponentInChildren<Health>().ModifyHealth(-AddsToSpawn[i].GetComponent<Character>().MaxHealth);
            }
            else return;
        }
    }

    private void SpawnParticleEffect(Vector3 Pos)
    {
        if(settings.UseParticleEffects)
        {
            var SpawnParticle = ObjectPooler.Instance.GetEnemyAppearParticle();

            SpawnParticle.SetActive(true);

            SpawnParticle.transform.position = new Vector3(Pos.x, Pos.y + 0.5f, Pos.z);
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