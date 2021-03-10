#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SylvanDietyBossStates { Idle, Chase, Attack, ApplyingAttack, Skill, SkillAnimation, Damaged, Immobile }

[System.Serializable]
public class SylvanDietyPhases
{
    [SerializeField]
    SylvanDietyBossAiStates[] states;

    [SerializeField]
    [TextArea]
    private string SpeechText;

    [SerializeField]
    private bool DontCheckHP, PlaySpeech;

    public SylvanDietyBossAiStates[] GetSylvanDietyBossAiStates
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

    public bool GetPlaySpeech
    {
        get
        {
            return PlaySpeech;
        }
        set
        {
            PlaySpeech = value;
        }
    }
}

[System.Serializable]
public class SylvanDietyBossAiStates
{
    [SerializeField]
    private SylvanDietyBossStates state;

    [SerializeField]
    private int SkillIndex;

    public SylvanDietyBossStates GetSylvanDietyState
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

public class SylvanDiety : MonoBehaviour
{
    [SerializeField]
    private SylvanDietyBossStates states;

    [SerializeField]
    private EnemyType enemyType;

    [SerializeField]
    private SylvanDietyPhases[] phases;

    [SerializeField]
    private Character character;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private AudioChanger audioChanger;

    [SerializeField]
    private EnemySkills enemySkills;

    [SerializeField]
    private SylvanDietyAnimations sylvanDietyAnimations;

    [SerializeField]
    private MuteBossAudio muteBossAudio;

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
    private GameObject[] LuxOrbs;

    [SerializeField]
    private GameObject WallTrigger, BossParticle, BossObject;

    [SerializeField]
    private Quaternion BossRotation;

    private float DistanceToTarget, DefaultMoveSpeed, DefaultAttackDelay;

    private bool PlayerEntry, ChangingPhase, OnEnabled, IsDoomed;

    [SerializeField]
    private bool IsReseted;

    [SerializeField]
    private int StateArrayIndex;

    [SerializeField]
    private int PhaseIndex, HpPhaseIndex;

    [SerializeField]
    private int[] HpToChangePhase;

    public SylvanDietyPhases[] GetSylvanDietyPhases
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

    public float GetAttackRange
    {
        get
        {
            return AttackRange;
        }
        set
        {
            AttackRange = value;
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

    public SylvanDietyAnimations GetAnimation
    {
        get
        {
            return sylvanDietyAnimations;
        }
        set
        {
            sylvanDietyAnimations = value;
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
        if(PlayerTarget != null)
        {
            StateArrayIndex++;

            if (StateArrayIndex >= phases[PhaseIndex].GetSylvanDietyBossAiStates.Length || OnEnabled)
            {
                StateArrayIndex = 0;
            }

            if (phases[PhaseIndex].GetSylvanDietyBossAiStates[StateArrayIndex].GetSylvanDietyState == SylvanDietyBossStates.Skill)
            {
                AttackRange = enemySkills.GetManager[phases[PhaseIndex].GetSylvanDietyBossAiStates[StateArrayIndex].GetSkillIndex].GetAttackRange;
            }
            else
            {
                AttackRange = 2.5f;
            }
        }
    }

    private void Awake()
    {
        states = SylvanDietyBossStates.Idle;

        Idle();

        DefaultMoveSpeed = MoveSpeed;
        DefaultAttackDelay = AttackDelay;
    }

    private void OnEnable()
    {
        PlayBossParticle();
    }

    private void Update()
    {
        if (this.character.CurrentHealth > 0)
        {
            switch (states)
            {
                case (SylvanDietyBossStates.Chase):
                    Chase();
                    break;
                case (SylvanDietyBossStates.Attack):
                    Attack();
                    break;
                case (SylvanDietyBossStates.ApplyingAttack):
                    ApplyingNormalAtk();
                    break;
                case (SylvanDietyBossStates.Skill):
                    Skill();
                    break;
                case (SylvanDietyBossStates.Damaged):
                    Damage();
                    break;
                case (SylvanDietyBossStates.Immobile):
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

    public SylvanDietyBossStates GetSylvanDietyStates
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
        sylvanDietyAnimations.IdleAnimator();
    }

    private void Chase()
    {
        sylvanDietyAnimations.MoveAnimator();

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
                            states = phases[PhaseIndex].GetSylvanDietyBossAiStates[StateArrayIndex].GetSylvanDietyState;
                        }
                    }
                }
            }
            else
            {
                states = SylvanDietyBossStates.Attack;
            }
        }
    }

    private void Attack()
    {
        sylvanDietyAnimations.IdleAnimator();

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
                    states = phases[PhaseIndex].GetSylvanDietyBossAiStates[StateArrayIndex].GetSylvanDietyState;
                }
            }
        }
        else
        {
            states = SylvanDietyBossStates.Chase;
        }
    }

    private void CheckPlayerTarget()
    {
        if (PlayerTarget != null)
        {
            if (PlayerTarget.CurrentHealth <= 0)
            {
                IsReseted = true;
            }
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
                    ChangingPhase = true;

                    if (HpPhaseIndex < HpToChangePhase.Length)
                    {
                        HpPhaseIndex++;
                    }

                    IncrementPhase();

                    states = phases[PhaseIndex].GetSylvanDietyBossAiStates[StateArrayIndex].GetSylvanDietyState;
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

        if (phases[PhaseIndex].GetSylvanDietyBossAiStates[StateArrayIndex].GetSylvanDietyState == SylvanDietyBossStates.Skill)
        {
            AttackRange = enemySkills.GetManager[phases[PhaseIndex].GetSylvanDietyBossAiStates[StateArrayIndex].GetSkillIndex].GetAttackRange;
        }
        else
        {
            AttackRange = 2.5f;
        }

        if(phases[PhaseIndex].GetPlaySpeech)
        {
            EnableSpeech();
        }
    }

    private void ApplyingNormalAtk()
    {
        sylvanDietyAnimations.AttackAnimator();
    }

    private void Skill()
    {
        if (PlayerTarget.CurrentHealth <= 0)
        {
            states = SylvanDietyBossStates.Idle;
        }
        else
        {
            if (phases[PhaseIndex].GetSylvanDietyBossAiStates[StateArrayIndex].GetSkillIndex > -1)
            {
                enemySkills.ChooseSkill(phases[PhaseIndex].GetSylvanDietyBossAiStates[StateArrayIndex].GetSkillIndex);
            }
            else return;
        }
    }

    //Sets the enemy to this state if they are inflicted with the stun/sleep status effect.
    private void Immobile()
    {
        sylvanDietyAnimations.IdleAnimator();
    }

    public void Damage()
    {
        sylvanDietyAnimations.DamagedAnimator();
    }

    private void EnableWall()
    {
        var main = Walls[0].main;
        Walls[0].gameObject.SetActive(false);
        main.loop = true;

        WallTrigger.SetActive(true);
    }

    public void SpawnLuxOrbs()
    {
        for(int i = 0; i < LuxOrbs.Length; i++)
        {
            LuxOrbs[i].SetActive(true);
        }
    }

    public void DespawnLuxOrbs()
    {
        for (int i = 0; i < LuxOrbs.Length; i++)
        {
            LuxOrbs[i].GetComponent<EnableGameObject>().GetRespawnTime = 0;
            LuxOrbs[i].SetActive(false);
        }
    }

    public void Dead()
    {
        enemySkills.SetRotationToFalse();

        DespawnLuxOrbs();

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
            enemySkills.DisableSylvanDietyRadiusImage();
            enemySkills.DisableSylvanDietyRadius();
        }

        GameManager.Instance.GetEventSystem.SetSelectedGameObject(null);
        GameManager.Instance.GetEnemyObject = null;
        GameManager.Instance.GetLastEnemyObject = null;

        enemy.ToggleHealthBar();

        sylvanDietyAnimations.DeadAnimator();

        if (this.GetComponent<ItemDrop>() != null)
        {
            this.GetComponent<ItemDrop>().DropItem();
        }

        GameManager.Instance.GetCharacter.GetComponent<PlayerController>().SetMoveToFalse();
        GameManager.Instance.GetCharacter.GetComponent<PlayerController>().enabled = false;

        GameManager.Instance.GetBeatGame = true;

        GameManager.Instance.CheckAllTogggledMenus();

        audioChanger.gameObject.SetActive(true);

        muteBossAudio.gameObject.SetActive(true);

        StartCoroutine("WaitToEndGame");
    }

    public void PlayParticleAndRemoveBoss()
    {
        BossParticle.transform.position = transform.position;
        PlayBossParticle();
        BossObject.SetActive(false);
    }

    private IEnumerator WaitToEndGame()
    {
        yield return new WaitForSeconds(11);
        GameManager.Instance.ReturnToMenu();
    }

    private void EnableAudioChanger()
    {
        audioChanger.gameObject.SetActive(true);

        audioChanger.GetChangeToMiniBossTheme = false;
        audioChanger.GetChangeToLevelTheme = true;
    }

    //Resets the enemy's stats when enabled in the scene.
    public void ResetStats()
    {
        DisableSpeech();

        PlayParticle();

        DespawnLuxOrbs();

        enemySkills.SetRotationToFalse();

        OnEnabled = true;

        ChangingPhase = false;

        PlayerTarget = null;

        PlayerEntry = false;

        sylvanDietyAnimations.ResetSkillAnimator();

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

        states = SylvanDietyBossStates.Idle;
        Idle();

        AttackRange = 2.5f;

        EnableWall();

        DespawnLuxOrbs();

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
        if (settings.UseParticleEffects)
            SpawnParticleEffect(new Vector3(BossPosition.position.x, BossPosition.position.y, BossPosition.position.z - 0.3f));
    }

    private void PlayBossParticle()
    {
        SpawnBossParticleEffect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && !PlayerEntry)
        {
            if (PhaseIndex == 0)
            {
                EnableSpeech();
            }

            PlayerTarget = other.GetComponent<Character>();
            states = SylvanDietyBossStates.Chase;
            IsReseted = false;
            PlayerEntry = true;
            EnemyTriggerSphere.gameObject.SetActive(false);
        }
    }

    public void EnableSpeech()
    {
        if (character.CurrentHealth > 0)
        {
            if (phases[PhaseIndex].GetSpeechText != "")
            {
                SpeechBox.SetBool("Fade", true);

                SpeechText.text = phases[PhaseIndex].GetSpeechText;
            }
            Invoke("DisableSpeech", 4.0f);
        }
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
            states = SylvanDietyBossStates.Idle;
            Idle();
            AutoAttackTime = 0;
            enemySkills.DisableSylvanDietyRadiusImage();
            enemySkills.DisableSylvanDietyRadius();
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
                    states = SylvanDietyBossStates.Attack;
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
            if (PlayerTarget.GetComponent<Health>().GetHasStatusGiftPassive)
            {
                GiveStatusEffect();
            }

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

    private void SpawnParticleEffect(Vector3 Pos)
    {
        if (settings.UseParticleEffects)
        {
            var SpawnParticle = ObjectPooler.Instance.GetEnemyAppearParticle();

            SpawnParticle.SetActive(true);

            SpawnParticle.transform.position = new Vector3(Pos.x, Pos.y + 0.5f, Pos.z);
        }
    }

    private void GiveStatusEffect()
    {
        if (Random.value * 100 <= 25)
        {
            if (PlayerTarget.GetComponent<BasicAttack>().GetInflictsDoomStatus)
            {
                if (!CheckDoomedStatusEffect())
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
        StatusEffect[] GiftEffects = { StatusEffect.Poison, StatusEffect.Slow };

        if (RandomNumber == 0)
        {
            if (!CheckPoisonStatusEffect())
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
        if (RandomNumber == 1)
        {
            if (!CheckSlowStatusEffect())
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

    private void SpawnBossParticleEffect()
    {
        BossParticle.SetActive(true);
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
