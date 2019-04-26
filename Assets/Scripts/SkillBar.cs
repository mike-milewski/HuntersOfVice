using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Skills skills = null;

    [SerializeField]
    private Text SkillName;

    [SerializeField]
    private Image SkillBarImage;

    private float CastTime;

    public Skills GetSkill
    {
        get
        {
            return skills;
        }
        set
        {
            skills = value;
        }
    }

    public Image GetSkillBar
    {
        get
        {
            return SkillBarImage;
        }
        set
        {
            SkillBarImage = value;
        }
    }

    private void Start()
    {
        CastTime = skills.GetCastTime;
    }

    private void Update()
    {
        if(character.GetComponent<PlayerController>().GetMovement == Vector3.zero)
        {
            SkillBarImage.fillAmount += Time.deltaTime / skills.GetCastTime;
            CastTime -= Time.deltaTime;
            SkillName.text = skills.GetSkillName + " " + Mathf.Clamp(CastTime, 0, skills.GetCastTime).ToString("F2");
            if (SkillBarImage.fillAmount >= 1)
            {
                SkillBarImage.fillAmount = 0;
                CastTime = skills.GetCastTime;
                gameObject.SetActive(false);
            }
        }
        else
        {
            SkillBarImage.fillAmount = 0;
            CastTime = skills.GetCastTime;
            gameObject.SetActive(false);
        }
    }
}
