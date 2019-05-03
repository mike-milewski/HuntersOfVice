using UnityEngine;
using UnityEngine.UI;

public class EnemySkillBar : MonoBehaviour
{
    [SerializeField]
    private Character character = null;

    [SerializeField]
    private Image SkillBarFillImage;

    [SerializeField]
    private Text SkillName;

    private float CastTime;

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

    private void OnEnable()
    {
        CastTime = character.GetComponent<EnemySkills>().GetCastTime;
        SkillBarFillImage.fillAmount = 0;
    }

    private void OnDisable()
    {
        SkillBarFillImage.fillAmount = 0;
    }

    private void Update()
    {
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
