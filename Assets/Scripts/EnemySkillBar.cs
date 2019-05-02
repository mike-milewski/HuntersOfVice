using UnityEngine;
using UnityEngine.UI;

public class EnemySkillBar : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Image SkillBarFillImage;

    private Text SkillName;

    private float CastTime;

    private void OnEnable()
    {
        CastTime = character.GetComponent<EnemySkills>().GetCastTime;
    }

    private void Update()
    {
        SkillBarFillImage.fillAmount += Time.deltaTime / character.GetComponent<EnemySkills>().GetCastTime;
        CastTime -= Time.deltaTime;
        SkillName.text = character.GetComponent<EnemySkills>().GetSkillName + " " + Mathf.Clamp(CastTime, 0, character.GetComponent<EnemySkills>().GetCastTime).ToString("F2");
        if (SkillBarFillImage.fillAmount >= 1)
        {
            SkillBarFillImage.fillAmount = 0;
            CastTime = character.GetComponent<EnemySkills>().GetCastTime;
            gameObject.SetActive(false);
        }
    }
}
