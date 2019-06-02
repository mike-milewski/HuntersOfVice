using UnityEngine;
using UnityEngine.UI;

public class EnemySkillBar : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private EnemyAI enemyAI;

    [SerializeField]
    private EnemySkills enemySkills;

    [SerializeField]
    private Image SkillBarFillImage;

    [SerializeField]
    private Text SkillName;

    private float CastTime;

    [SerializeField]
    private bool Casting;

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

    public Text GetSkillName
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
        CastTime = enemySkills.GetManager[enemySkills.GetRandomValue].GetCastTime;
        character.GetComponentInChildren<DamageRadius>().GetShapes = enemySkills.GetManager[enemySkills.GetRandomValue].GetShapes;
        Casting = true;
        SkillBarFillImage.fillAmount = 0;
    }

    private void OnDisable()
    {
        Casting = false;
        SkillBarFillImage.fillAmount = 0;
    }

    public void GetEnemySkill()
    {
        SkillName.text = enemySkills.GetManager[enemySkills.GetRandomValue].GetSkillName;
        CastTime = enemySkills.GetManager[enemySkills.GetRandomValue].GetCastTime;
    }

    public void ToggleCastBar()
    {
        if (GameManager.Instance.GetLastObject == enemy.gameObject)
        {
            GetEnemySkill();
            enemySkills.EnableEnemySkillBar();
        }
        else if (GameManager.Instance.GetLastObject != enemy.gameObject)
        {
            GetEnemySkill();
            enemySkills.DisableEnemySkillBar();
        }
    }

    private void LateUpdate()
    {
        ToggleCastBar();

        SkillBarFillImage.fillAmount += Time.deltaTime / enemySkills.GetManager[enemySkills.GetRandomValue].GetCastTime;
        CastTime -= Time.deltaTime;
        SkillName.text = enemySkills.GetManager[enemySkills.GetRandomValue].GetSkillName;
        if (SkillBarFillImage.fillAmount >= 1)
        {
            enemySkills.GetActiveSkill = false;

            enemySkills.ChooseSkill(enemySkills.GetRandomValue);

            enemyAI.GetAutoAttack = 0;
            enemyAI.GetStates = States.Attack;

            SkillBarFillImage.fillAmount = 0;
            CastTime = enemySkills.GetManager[enemySkills.GetRandomValue].GetCastTime;

            gameObject.SetActive(false);
        }
        else if(enemySkills.GetDisruptedSkill)
        {
            enemySkills.GetActiveSkill = false;

            enemyAI.GetAutoAttack = 0;

            SkillBarFillImage.fillAmount = 0;

            gameObject.SetActive(false);
        }
    }
}