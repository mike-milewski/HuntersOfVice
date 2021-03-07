#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum RuneGolemStates { Idle, Chase, Attack, ApplyingAttack, Skill, SkillAnimation, Damaged, Immobile, MovingToPosition, RotateToPosition, RotateToPositionTwo }

[System.Serializable]
public class RuneGolemPhases
{
    [SerializeField]
    RuneGolemAiStates[] states;

    [SerializeField]
    private int EarthEffigysToKill, EarthEffigyKillCount;

    [SerializeField]
    private bool DontCheckHP, SpawnEarthEffigy, SpawnWeakEarthEffigys;

    public RuneGolemAiStates[] GetRuneGolemAiStates
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

    public int GetEarthEffigysToKill
    {
        get
        {
            return EarthEffigysToKill;
        }
        set
        {
            EarthEffigysToKill = value;
        }
    }

    public int GetEarthEffigyKillCount
    {
        get
        {
            return EarthEffigyKillCount;
        }
        set
        {
            EarthEffigyKillCount = value;
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

    public bool GetSpawnEarthEffigy
    {
        get
        {
            return SpawnEarthEffigy;
        }
        set
        {
            SpawnEarthEffigy = value;
        }
    }

    public bool GetSpawnWeakEarthEffigys
    {
        get
        {
            return SpawnWeakEarthEffigys;
        }
        set
        {
            SpawnWeakEarthEffigys = value;
        }
    }
}

[System.Serializable]
public class RuneGolemAiStates
{
    [SerializeField]
    private RuneGolemStates state;

    [SerializeField]
    private int SkillIndex;

    public RuneGolemStates GetState
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

public class RuneGolem : MonoBehaviour
{
    [SerializeField]
    private RuneGolemStates states;

    [SerializeField]
    private EnemyType enemyType;

    [SerializeField]
    private RuneGolemPhases[] phases;

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
    private RuneGolemAnimations runeGolemAnimations;

    [SerializeField]
    private float MoveSpeed, AttackRange, AttackDelay, AutoAttackTime, LookSpeed, OuterAttackDistance;

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
    private GameObject treasureChest, ChestSpawnParticle, EarthEffigy;

    [SerializeField]
    private GameObject WallTrigger;

    [SerializeField]
    private GameObject[] SoothingSpheres, WeakEarthEffigys;

    [SerializeField]
    private Quaternion BossRotation;

    private float DistanceToTarget;

    private bool PlayerEntry, ChangingPhase, IsReseted, OnEnabled;

    private int StateArrayIndex;

    [SerializeField]
    private int PhaseIndex, HpPhaseIndex;

    [SerializeField]
    private int[] HpToChangePhase;

    public RuneGolemPhases[] GetRuneGolemPhases
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

    public RuneGolemAnimations GetAnimation
    {
        get
        {
            return runeGolemAnimations;
        }
        set
        {
            runeGolemAnimations = value;
        }
    }

    public Transform GetBossPosition
    {
        get
        {
            return BossPosition;
        }
        set
        {
            BossPosition = value;
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

        if (StateArrayIndex >= phases[PhaseIndex].GetRuneGolemAiStates.Length || OnEnabled)
        {
            StateArrayIndex = 0;
        }
    }

    private void Awake()
    {
        states = RuneGolemStates.Idle;

        Idle();
    }

    private void OnEnable()
    {
        PlayParticle();
    }

    private void Update()
    {
        if (this.character.CurrentHealth > 0)
        {
            switch (states)
            {
                case (RuneGolemStates.Chase):
                    Chase();
                    break;
                case (RuneGolemStates.Attack):
                    Attack();
                    break;
                case (RuneGolemStates.ApplyingAttack):
                    ApplyingNormalAtk();
                    break;
                case (RuneGolemStates.Skill):
                    Skill();
                    break;
                case (RuneGolemStates.Damaged):
                    Damage();
                    break;
                case (RuneGolemStates.Immobile):
                    Immobile();
                    break;
            }
        }
        if (PlayerTarget != null)
        {
            if (PlayerTarget.CurrentHealth <= 0)
            {
                ResetStats();
            }
        }
    }

    public RuneGolemStates GetStates
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
        runeGolemAnimations.IdleAnimator();
    }

    private void Chase()
    {
        runeGolemAnimations.MoveAnimator();

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
                            states = phases[PhaseIndex].GetRuneGolemAiStates[StateArrayIndex].GetState;
                        }
                    }
                }
            }
            else
            {
                states = RuneGolemStates.Attack;
            }
        }
    }

    private void Attack()
    {
        runeGolemAnimations.IdleAnimator();

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
                    states = phases[PhaseIndex].GetRuneGolemAiStates[StateArrayIndex].GetState;
                }
            }
        }
        else
        {
            states = RuneGolemStates.Chase;
        }
    }

    public void CheckHP()
    {
        float HpCap = ((float)character.CurrentHealth / (float)character.MaxHealth) * 100f;

        if (!phases[PhaseIndex].GetDontCheckHP)
        {
            if (HpPhaseIndex < HpToChangePhase.Length)
            {
                if (HpCap <= HpToChangePhase[HpPhaseIndex])
                {
                    if (HpPhaseIndex < HpToChangePhase.Length)
                    {
                        HpPhaseIndex++;
                    }

                    enemySkills.GetSkillBar.GetFillImage.fillAmount = 0;

                    if (enemySkills.GetManager.Length > 0)
                    {
                        enemySkills.DisableRuneGolemRadiusImage();
                        enemySkills.DisableRuneGolemRadius();
                    }

                    SpawnSoothingSpheres();

                    IncrementPhase();

                    states = phases[PhaseIndex].GetRuneGolemAiStates[StateArrayIndex].GetState;

                    phases[PhaseIndex].GetEarthEffigyKillCount = 0;

                    if (phases[PhaseIndex].GetSpawnEarthEffigy)
                    {
                        SpawnEarthEffigy();
                    }
                    if (phases[PhaseIndex].GetSpawnWeakEarthEffigys)
                    {
                        SpawnWeakEarthEffigys();
                    }
                }
            }
            else
            {
                ChangingPhase = false;

                return;
            }
        }
    }

    public void IncrementPhase()
    {
        StateArrayIndex = 0;
        PhaseIndex++;
    }

    private void ApplyingNormalAtk()
    {
        runeGolemAnimations.AttackAnimator();
    }

    private void SpawnSoothingSpheres()
    {
        for (int i = 0; i < SoothingSpheres.Length; i++)
        {
            SoothingSpheres[i].SetActive(true);
        }
    }

    private void DespawnSoothingSpheres()
    {
        for (int i = 0; i < SoothingSpheres.Length; i++)
        {
            if(SoothingSpheres[i].activeInHierarchy)
            {
                SoothingOrbAppearParticle(new Vector3(SoothingSpheres[i].transform.position.x, SoothingSpheres[i].transform.position.y, SoothingSpheres[i].transform.position.z));
            }
            SoothingSpheres[i].SetActive(false);
        }
    }

    private void SoothingOrbAppearParticle(Vector3 Position)
    {
        if (settings.UseParticleEffects)
        {
            var particle = ObjectPooler.Instance.GetSoothingOrbParticle();

            particle.SetActive(true);

            particle.transform.position = new Vector3(Position.x, Position.y, Position.z);
        }
    }

    private void Skill()
    {
        if (PlayerTarget.CurrentHealth <= 0)
        {
            states = RuneGolemStates.Idle;
        }
        else
        {
            if (phases[PhaseIndex].GetRuneGolemAiStates[StateArrayIndex].GetSkillIndex > -1)
            {
                enemySkills.ChooseSkill(phases[PhaseIndex].GetRuneGolemAiStates[StateArrayIndex].GetSkillIndex);
            }
            else return;
        }
    }

    //Sets the enemy to this state if they are inflicted with the stun/sleep status effect.
    private void Immobile()
    {
        runeGolemAnimations.IdleAnimator();
    }

    public void Damage()
    {
        runeGolemAnimations.DamagedAnimator();
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
        enemy.CheckExperienceHolder();

        EnableAudioChanger();

        DespawnEarthEffigy();
        DespawnWeakEarthEffigys();

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
            enemySkills.DisableRuneGolemRadiusImage();
            enemySkills.DisableRuneGolemRadius();
        }

        GameManager.Instance.GetEventSystem.SetSelectedGameObject(null);
        GameManager.Instance.GetEnemyObject = null;
        GameManager.Instance.GetLastEnemyObject = null;

        enemy.ToggleHealthBar();

        runeGolemAnimations.DeadAnimator();

        if (this.GetComponent<ItemDrop>() != null)
        {
            this.GetComponent<ItemDrop>().DropItem();
        }

        enemy.ReturnSkillPoints();

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

        DespawnEarthEffigy();
        DespawnWeakEarthEffigys();

        PlayParticle();

        enemySkills.SetRotationToFalse();

        OnEnabled = true;

        IsReseted = false;

        ChangingPhase = false;

        PlayerTarget = null;

        PlayerEntry = false;

        runeGolemAnimations.ResetSkillAnimator();

        states = RuneGolemStates.Idle;
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

        InvokeOnEnabledFalse();
    }

    private void InvokeOnEnabledFalse()
    {
        Invoke("OnEnabledOff", 0.5f);
    }

    private void SpawnEarthEffigy()
    {
        EarthEffigy.SetActive(true);

        SpawnParticleEffect(new Vector3(EarthEffigy.transform.position.x, EarthEffigy.transform.position.y, EarthEffigy.transform.position.z - 0.3f));
    }

    private void DespawnEarthEffigy()
    {
        if(EarthEffigy.activeInHierarchy)
        {
            StartCoroutine("WaitToDisableEffigy");
        }
    }

    private void SpawnWeakEarthEffigys()
    {
        for(int i = 0; i < WeakEarthEffigys.Length; i++)
        {
            WeakEarthEffigys[i].SetActive(true);

            SpawnParticleEffect(new Vector3(WeakEarthEffigys[i].transform.position.x, WeakEarthEffigys[i].transform.position.y, WeakEarthEffigys[i].transform.position.z - 0.3f));
        }
    }

    private void DespawnWeakEarthEffigys()
    {
        StartCoroutine("WaitToDisableWeakEffigys");
    }

    private IEnumerator WaitToDisableEffigy()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnParticleEffect(new Vector3(EarthEffigy.transform.position.x, EarthEffigy.transform.position.y, EarthEffigy.transform.position.z - 0.3f));

        EarthEffigy.SetActive(false);
    }

    private IEnumerator WaitToDisableWeakEffigys()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < WeakEarthEffigys.Length; i++)
        {
            if(WeakEarthEffigys[i].activeInHierarchy)
            {
                SpawnParticleEffect(new Vector3(WeakEarthEffigys[i].transform.position.x, WeakEarthEffigys[i].transform.position.y, WeakEarthEffigys[i].transform.position.z - 0.3f));
            }

            WeakEarthEffigys[i].SetActive(false);
        }
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
        if (other.gameObject.GetComponent<PlayerController>() && !PlayerEntry)
        {
            PlayerTarget = other.GetComponent<Character>();
            states = RuneGolemStates.Chase;
            EnemyTriggerSphere.gameObject.SetActive(false);
            IsReseted = false;
            PlayerEntry = true;
        }
    }

    public void CheckTarget()
    {
        if (!PlayerEntry)
        {
            PlayerTarget = null;
            states = RuneGolemStates.Idle;
            Idle();
            AutoAttackTime = 0;
            enemySkills.DisableRuneGolemRadiusImage();
            enemySkills.DisableRuneGolemRadius();
            enemySkills.GetActiveSkill = false;
            enemySkills.GetSkillBar.gameObject.SetActive(false);
        }
        else
        {
            AutoAttackTime = 0;
            if (PlayerTarget != null)
            {
                if (PlayerTarget.CurrentHealth <= 0)
                {
                    ResetStats();
                }
                else
                {
                    states = RuneGolemStates.Attack;
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

                    t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=35>" + "1!";
                }
                else
                {
                    PlayerTarget.GetComponent<Health>().ModifyHealth(-((int)CritCalc - PlayerTarget.CharacterDefense));

                    if (PlayerTarget.GetComponent<Health>().GetDamageWasReduced)
                    {
                        t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=35>" +
                        PlayerTarget.GetComponent<Health>().GetReducedDamageValue((int)CritCalc - PlayerTarget.CharacterDefense).ToString() + "!" + 
                        "\n" + "<size=16> <#EFDFB8>" + "(Reduced!)";
                    }
                    else
                    {
                        t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=35>" + ((int)CritCalc - PlayerTarget.CharacterDefense).ToString() + "!";
                    }
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

                    if (PlayerTarget.GetComponent<Health>().GetDamageWasReduced)
                    {
                        t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" +
                        PlayerTarget.GetComponent<Health>().GetReducedDamageValue(character.CharacterStrength - PlayerTarget.CharacterDefense).ToString() + 
                        "\n" + "<size=16> <#EFDFB8>" + "(Reduced!)";
                    }
                    else
                    {
                        t.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + (character.CharacterStrength - PlayerTarget.CharacterDefense).ToString();
                    }
                }
            }
            #endregion

            if (!SkillsManager.Instance.GetActivatedSkill)
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

    private void SpawnParticleEffect(Vector3 Pos)
    {
        if (settings.UseParticleEffects)
        {
            var SpawnParticle = ObjectPooler.Instance.GetEnemyAppearParticle();

            SpawnParticle.SetActive(true);

            SpawnParticle.transform.position = new Vector3(Pos.x, Pos.y + 0.5f, Pos.z);
        }
    }

    public void RuneGolemMove()
    {
        SoundManager.Instance.RuneGolemWalk();
    }

    public void RuneGolemFall()
    {
        SoundManager.Instance.PuckFall();
    }
}
