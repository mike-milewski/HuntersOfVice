using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Skills skills = null;

    [SerializeField]
    private ParticleSystem CastParticle;

    [SerializeField]
    private Text SkillName;

    [SerializeField]
    private Image SkillBarImage;

    [SerializeField]
    private bool ParticleExists;

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

    private void OnEnable()
    {
        if (!ParticleExists)
        {
            CreateParticle();
        }
        else
        {
            SkillsManager.Instance.GetParticleObj.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        CastTime = skills.GetCastTime;
    }

    private ParticleSystem CreateParticle()
    {
        SkillsManager.Instance.GetParticleObj = Instantiate(CastParticle, new Vector3(character.transform.position.x, character.transform.position.y + 0.1f, character.transform.position.z),
                                     CastParticle.transform.rotation);

        SkillsManager.Instance.GetParticleObj.transform.SetParent(character.transform, true);

        ParticleExists = true;

        return SkillsManager.Instance.GetParticleObj;
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
                skills.GetComponent<Button>().onClick.Invoke();
                SkillBarImage.fillAmount = 0;
                CastTime = skills.GetCastTime;
                SkillsManager.Instance.GetParticleObj.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        else
        {
            SkillsManager.Instance.ReactivateSkillButtons();
            SkillBarImage.fillAmount = 0;
            CastTime = skills.GetCastTime;
            SkillsManager.Instance.GetActivatedSkill = false;
            SkillsManager.Instance.GetParticleObj.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
