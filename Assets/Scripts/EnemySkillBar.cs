using UnityEngine;
using UnityEngine.UI;

public class EnemySkillBar : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Enemy enemy;

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
        CastTime = character.GetComponent<EnemySkills>().GetCastTime;
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
        SkillName.text = character.GetComponent<EnemySkills>().GetSkillName;
        CastTime = character.GetComponent<EnemySkills>().GetCastTime;
    }

    public void ToggleCastBar()
    {
        if (GameManager.Instance.GetEventSystem.currentSelectedGameObject == enemy.gameObject)
        {
            GetEnemySkill();
            character.GetComponent<EnemySkills>().EnableEnemySkillBar();
        }
        else if (GameManager.Instance.GetEventSystem.currentSelectedGameObject != enemy.gameObject)
        {
            GetEnemySkill();
            character.GetComponent<EnemySkills>().DisableEnemySkillBar();
        }
    }

    private void Update()
    {
        ToggleCastBar();

        SkillBarFillImage.fillAmount += Time.deltaTime / character.GetComponent<EnemySkills>().GetCastTime;
        CastTime -= Time.deltaTime;
        SkillName.text = character.GetComponent<EnemySkills>().GetSkillName;
        if (SkillBarFillImage.fillAmount >= 1)
        {
            character.GetComponent<EnemySkills>().GetActiveSkill = false;

            character.GetComponent<EnemySkills>().ChooseSkill(character.GetComponent<EnemySkills>().GetRandomValue);

            character.GetComponent<EnemyAI>().GetAutoAttack = 0;
            character.GetComponent<EnemyAI>().GetStates = States.Attack;

            SkillBarFillImage.fillAmount = 0;
            CastTime = character.GetComponent<EnemySkills>().GetCastTime;

            gameObject.SetActive(false);
        }
    }
}
