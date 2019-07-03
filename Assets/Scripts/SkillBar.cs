using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBar : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private PlayerAnimations playerAnimations;

    [SerializeField]
    private Skills skills = null;

    [SerializeField]
    private ParticleSystem CastParticle;

    [SerializeField]
    private TextMeshProUGUI SkillName;

    [SerializeField]
    private Image SkillBarImage, SkillImage;

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
            CastParticle.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        CastTime = skills.GetCastTime;

        SkillImage.sprite = skills.GetComponent<Image>().sprite;
    }

    private void CreateParticle()
    {
        CastParticle = Instantiate(CastParticle, new Vector3(playerController.transform.position.x, 
                                                             playerController.transform.position.y + 0.1f, 
                                                             playerController.transform.position.z),
                                                             CastParticle.transform.rotation);

        CastParticle.transform.SetParent(playerController.transform, true);

        ParticleExists = true;
    }

    private void Update()
    {
        if(playerController.GetMovement == Vector3.zero && character.CurrentHealth > 0 && !SkillsManager.Instance.GetDisruptedSkill && !skills.GetIsBeingDragged)
        {
            playerAnimations.EndAttackAnimation();
            SkillBarImage.fillAmount += Time.deltaTime / skills.GetCastTime;
            CastTime -= Time.deltaTime;
            SkillName.text = skills.GetSkillName + " " + Mathf.Clamp(CastTime, 0, skills.GetCastTime).ToString("F2");
            if (SkillBarImage.fillAmount >= 1)
            {
                skills.GetButton.onClick.Invoke();
                SkillBarImage.fillAmount = 0;
                CastTime = skills.GetCastTime;
                CastParticle.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        else
        {
            playerAnimations.EndAllSpellcastingBools();
            SkillsManager.Instance.ReactivateSkillButtons();
            SkillBarImage.fillAmount = 0;
            CastTime = skills.GetCastTime;
            SkillsManager.Instance.GetActivatedSkill = false;
            CastParticle.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
