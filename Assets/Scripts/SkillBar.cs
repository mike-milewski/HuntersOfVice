using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private PlayerController playerController;

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
        SkillsManager.Instance.GetParticleObj = Instantiate(CastParticle, new Vector3(playerController.transform.position.x, 
                                                                                      playerController.transform.position.y + 0.1f, 
                                                                                      playerController.transform.position.z),
                                                                                      CastParticle.transform.rotation);

        SkillsManager.Instance.GetParticleObj.transform.SetParent(playerController.transform, true);

        ParticleExists = true;

        return SkillsManager.Instance.GetParticleObj;
    }

    private void Update()
    {
        if(playerController.GetMovement == Vector3.zero && character.CurrentHealth > 0)
        {
            SkillBarImage.fillAmount += Time.deltaTime / skills.GetCastTime;
            CastTime -= Time.deltaTime;
            SkillName.text = skills.GetSkillName + " " + Mathf.Clamp(CastTime, 0, skills.GetCastTime).ToString("F2");
            if (SkillBarImage.fillAmount >= 1)
            {
                skills.GetButton.onClick.Invoke();
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
