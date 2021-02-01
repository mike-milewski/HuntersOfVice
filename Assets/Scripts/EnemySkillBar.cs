#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySkillBar : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private EnemyAI enemyAI = null;

    [SerializeField]
    private Puck puckAI = null;

    [SerializeField]
    private RuneGolem runeGolemAI = null;

    [SerializeField]
    private SylvanDiety SylvanDietyAI = null;

    [SerializeField]
    private EnemySkills enemySkills = null;

    [SerializeField]
    private Image SkillBarFillImage;

    [SerializeField]
    private TextMeshProUGUI SkillName;

    [SerializeField]
    private GameObject CastParticle;

    [SerializeField]
    private bool Casting;

    private float CastTime;

    [SerializeField]
    private float CastParticleSize;

    public Character GetCharacter
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
        }
    }

    public Image GetFillImage
    {
        get
        {
            return SkillBarFillImage;
        }
        set
        {
            SkillBarFillImage = value;
        }
    }

    public TextMeshProUGUI GetSkillName
    {
        get
        {
            return SkillName;
        }
        set
        {
            SkillName = value;
        }
    }

    public Enemy GetEnemy
    {
        get
        {
            return enemy;
        }
        set
        {
            enemy = value;
        }
    }

    public bool GetCasting
    {
        get
        {
            return Casting;
        }
        set
        {
            Casting = value;
        }
    }

    private void OnEnable()
    {
        if(settings.UseParticleEffects)
        {
            CastParticle = ObjectPooler.Instance.GetEnemyCastParticle();

            CastParticle.SetActive(true);

            CastParticle.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 0.6f, character.transform.position.z);

            CastParticle.transform.SetParent(character.transform);

            CastParticle.transform.localScale = new Vector3(1,1,1);

        }
        if(enemyAI != null)
        {
            CastTime = enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
            character.GetComponentInChildren<DamageRadius>().GetShapes = enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetShapes;
        }
        if(puckAI != null)
        {
            CastTime = enemySkills.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
            character.GetComponentInChildren<PuckDamageRadius>().GetShapes = enemySkills.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetShapes;
        }
        if(runeGolemAI != null)
        {
            CastTime = enemySkills.GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
            character.GetComponentInChildren<RuneGolemDamageRadius>().GetShapes = enemySkills.GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetShapes;
        }
        if (SylvanDietyAI != null)
        {
            CastTime = enemySkills.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
            character.GetComponentInChildren<SylvanDietyDamageRadius>().GetShapes = enemySkills.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetShapes;
        }
        Casting = true;
        SkillBarFillImage.fillAmount = 0;
    }

    private void OnDisable()
    {
        if(settings.UseParticleEffects || CastParticle.activeInHierarchy)
        {
            ObjectPooler.Instance.ReturnEnemyCastParticleToPool(CastParticle);
        }

        Casting = false;
        SkillBarFillImage.fillAmount = 0;
    }

    public void GetEnemySkill()
    {
        if(enemySkills != null)
        {
            if (enemySkills.GetManager.Length > 0)
            {
                if (enemyAI != null)
                {
                    SkillName.text = enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName;
                    CastTime = enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
                }
                if (puckAI != null)
                {
                    SkillName.text = enemySkills.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName;
                    CastTime = enemySkills.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
                }
                if (runeGolemAI != null)
                {
                    SkillName.text = enemySkills.GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillName;
                    CastTime = enemySkills.GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
                }
                if (SylvanDietyAI != null)
                {
                    SkillName.text = enemySkills.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName;
                    CastTime = enemySkills.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
                }
            }
        }
    }

    public void ToggleCastBar()
    {
        if (GameManager.Instance.GetEnemyObject == enemy.gameObject)
        {
            if(enemySkills != null)
            {
                GetEnemySkill();
                enemySkills.EnableEnemySkillBar();
            }
        }
        else if (GameManager.Instance.GetEnemyObject != enemy.gameObject)
        {
            if(enemySkills != null)
            {
                GetEnemySkill();
                enemySkills.DisableEnemySkillBar();
            }
        }
    }

    private void LateUpdate()
    {
        if(enemyAI != null)
        {
            SkillBarFillImage.fillAmount += Time.deltaTime / enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
            CastTime -= Time.deltaTime;
            SkillName.text = enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName;
            if (SkillBarFillImage.fillAmount >= 1)
            {
                enemySkills.GetActiveSkill = false;

                enemySkills.ChooseSkill(enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex);

                enemyAI.GetStates = States.SkillAnimation;

                SkillBarFillImage.fillAmount = 0;
                CastTime = enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;

                gameObject.SetActive(false);
            }
        }
        if(puckAI != null)
        {
            if(puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex != -1)
            {
                SkillBarFillImage.fillAmount += Time.deltaTime / enemySkills.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
                CastTime -= Time.deltaTime;
                SkillName.text = enemySkills.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetSkillName;
                if (SkillBarFillImage.fillAmount >= 1)
                {
                    enemySkills.GetActiveSkill = false;

                    if (!puckAI.GetChangingPhase)
                    {
                        enemySkills.ChooseSkill(puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex);

                        puckAI.GetStates = BossStates.SkillAnimation;
                    }

                    SkillBarFillImage.fillAmount = 0;
                    CastTime = enemySkills.GetManager[puckAI.GetPhases[puckAI.GetPhaseIndex].GetBossAiStates[puckAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;

                    gameObject.SetActive(false);
                }
            }
            else
            {
                enemySkills.GetActiveSkill = false;

                SkillBarFillImage.fillAmount = 0;

                gameObject.SetActive(false);
            }
        }
        if (runeGolemAI != null)
        {
            if (runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex != -1)
            {
                SkillBarFillImage.fillAmount += Time.deltaTime / enemySkills.GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
                CastTime -= Time.deltaTime;
                SkillName.text = enemySkills.GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetSkillName;
                if (SkillBarFillImage.fillAmount >= 1)
                {
                    enemySkills.GetActiveSkill = false;

                    if (!runeGolemAI.GetChangingPhase)
                    {
                        enemySkills.ChooseSkill(runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex);

                        runeGolemAI.GetStates = RuneGolemStates.SkillAnimation;
                    }

                    SkillBarFillImage.fillAmount = 0;
                    CastTime = enemySkills.GetManager[runeGolemAI.GetRuneGolemPhases[runeGolemAI.GetPhaseIndex].GetRuneGolemAiStates[runeGolemAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;

                    gameObject.SetActive(false);
                }
            }
            else
            {
                enemySkills.GetActiveSkill = false;

                SkillBarFillImage.fillAmount = 0;

                gameObject.SetActive(false);
            }
        }
        if (SylvanDietyAI != null)
        {
            if (SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex != -1)
            {
                SkillBarFillImage.fillAmount += Time.deltaTime / enemySkills.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;
                CastTime -= Time.deltaTime;
                SkillName.text = enemySkills.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName;
                if (SkillBarFillImage.fillAmount >= 1)
                {
                    enemySkills.GetActiveSkill = false;

                    enemySkills.ChooseSkill(SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex);

                    SylvanDietyAI.GetSylvanDietyStates = SylvanDietyBossStates.SkillAnimation;

                    SkillBarFillImage.fillAmount = 0;
                    CastTime = enemySkills.GetManager[SylvanDietyAI.GetSylvanDietyPhases[SylvanDietyAI.GetPhaseIndex].GetSylvanDietyBossAiStates[SylvanDietyAI.GetStateArrayIndex].GetSkillIndex].GetCastTime;

                    gameObject.SetActive(false);
                }
            }
            else
            {
                enemySkills.GetActiveSkill = false;

                SkillBarFillImage.fillAmount = 0;

                gameObject.SetActive(false);
            }
        }

        if (enemySkills.GetDisruptedSkill)
        {  
            enemySkills.GetActiveSkill = false;

            enemyAI.GetAutoAttack = 0;

            SkillBarFillImage.fillAmount = 0;

            gameObject.SetActive(false);
        }
    }
}