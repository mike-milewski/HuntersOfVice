using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillMenu : MonoBehaviour
{
    public static SkillMenu skillmenu;

    [SerializeField]
    private Character character;

    private Transform DropZoneTransform;

    [SerializeField]
    private TextMeshProUGUI LevelRequirementText, SkillInfo;

    [SerializeField]
    private Image SkillImage;

    [SerializeField]
    private Skills skill;

    [SerializeField]
    private int LevelRequirement;

    private void Awake()
    {
        skillmenu = this;

        DropZoneTransform = skill.GetComponent<DragUiObject>().GetDropZone.transform;
    }

    private void Start()
    {
        CheckCharacterLevel();
    }

    private void CheckCharacterLevel()
    {
        LevelRequirementText.text = LevelRequirement.ToString();

        if(character.Level < LevelRequirement)
        {
            SkillImage.GetComponent<Button>().interactable = false;
            skill.GetComponent<Image>().enabled = false;
            skill.GetComponent<Button>().interactable = false;

            skill.GetComponent<DragUiObject>().enabled = false;
        }
        else
        {
            SkillImage.GetComponent<Button>().interactable = true;
            skill.GetComponent<Button>().enabled = true;

            skill.GetComponent<DragUiObject>().enabled = true;

            skill.enabled = true;

            skill.transform.SetParent(DropZoneTransform, false);

            skill.GetComponent<Mask>().showMaskGraphic = true;
            skill.GetCoolDownImage.GetComponent<Mask>().showMaskGraphic = true;

            skill.GetComponent<Image>().raycastTarget = true;

            SkillsManager.Instance.ClearSkills();
            SkillsManager.Instance.AddSkillsToList();
        }
    }

    //Function used for when the player levels up.
    public void CheckIfUnlockedNewSkill()
    {
        if (character.Level >= LevelRequirement && !skill.GetComponent<DragUiObject>().enabled)
        {
            SkillImage.GetComponent<Button>().interactable = true;
            skill.GetComponent<Image>().enabled = true;
            skill.GetComponent<Button>().enabled = true;
            
            skill.GetComponent<DragUiObject>().enabled = true;
            skill.enabled = true;

            skill.transform.SetParent(DropZoneTransform, false);

            skill.GetComponent<Mask>().showMaskGraphic = true;
            skill.GetCoolDownImage.GetComponent<Mask>().showMaskGraphic = true;

            skill.GetComponent<Image>().raycastTarget = true;

            SkillsManager.Instance.ClearSkills();
            SkillsManager.Instance.AddSkillsToList();
        }
    }

    public void ShowSkillInfo(GameObject Panel)
    {
        Panel.SetActive(true);

        #region SkillInformation
        if (skill.GetCastTime <= 0 && skill.GetPotency <= 0 && skill.GetManaCost <= 0)
        {
            if (skill.GetPlayerStatusEffect == EffectStatus.NONE && skill.GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "Cooldown: " + skill.GetCoolDown + "s" + "\n" + 
                                 "Cast Time: Instant";
            }
            else
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "<#EBF500>" + "Added effect: " + "</color>" + 
                                 skill.GetStatusEffectName + "\n" + "<#EBF500>" + "Status Duration: " + "</color>" + skill.GetStatusDuration + "s" + "\n\n" + "Cooldown: " + 
                                 skill.GetCoolDown + "s" + "\n" + "Cast Time: Instant";
            }
        }
        if (skill.GetCastTime > 0 && skill.GetPotency <= 0 && skill.GetManaCost <= 0)
        {
            if (skill.GetPlayerStatusEffect == EffectStatus.NONE && skill.GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "Cooldown: " + skill.GetCoolDown + " Seconds" + "\n" + 
                                 "Cast Time: " + skill.GetCastTime + "s";
            }
            else
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "<#EBF500>" + "Added effect: " + "</color>" + 
                                 skill.GetStatusEffectName + "\n" + "<#EBF500>" + "Status Duration: " + "</color>" + skill.GetStatusDuration + "s" + "\n\n" + "Cooldown: " + 
                                 skill.GetCoolDown + "s" + "\n" + "Cast Time: " + skill.GetCastTime + "s";
            }
        }
        if (skill.GetCastTime > 0 && skill.GetPotency > 0 && skill.GetManaCost <= 0)
        {
            if (skill.GetPlayerStatusEffect == EffectStatus.NONE && skill.GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "Power: " + skill.GetPotency + "\n" + "Cooldown: " + 
                                 skill.GetCoolDown + "s" + "\n" + "Cast Time: " + skill.GetCastTime + "s";
            }
            else
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "<#EBF500>" + "Added effect: " + "</color>" + 
                                 skill.GetStatusEffectName + "\n" + "<#EBF500>" + "Status Duration: " + "</color>" + skill.GetStatusDuration + "s" + "\n\n" + "Power: " + 
                                 skill.GetPotency + "\n" + "Cooldown: " + skill.GetCoolDown + "s" + "\n" + "Cast Time: " + skill.GetCastTime + "s";
            }
        }
        if (skill.GetCastTime > 0 && skill.GetPotency > 0 && skill.GetManaCost > 0)
        {
            if (skill.GetPlayerStatusEffect == EffectStatus.NONE && skill.GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "Mana: " + skill.GetManaCost + "\n" + "Power: " + 
                                 skill.GetPotency + "\n" + "Cooldown: " + skill.GetCoolDown + "s" + "\n" + "Cast Time: " + skill.GetCastTime + "s";
            }
            else
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n" + "Mana: " + skill.GetManaCost + "\n\n" + "<#EBF500>" + 
                                 "Added effect: " + "</color>" + skill.GetStatusEffectName + "\n" + "<#EBF500>" + "Status Duration: " + "</color>" + skill.GetStatusDuration + "s" + 
                                 "\n\n" + "Power: " + skill.GetPotency + "\n" + "Cooldown: " + skill.GetCoolDown + "s" + "\n" + "Cast Time: " + skill.GetCastTime + "s";
            }
        }
        if (skill.GetCastTime <= 0 && skill.GetPotency > 0 && skill.GetManaCost <= 0)
        {
            if (skill.GetPlayerStatusEffect == EffectStatus.NONE && skill.GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "Power: " + skill.GetPotency + "\n" + "Cooldown: " + 
                                 skill.GetCoolDown + "s" + "\n" + "Cast Time: Instant";
            }
            else
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "<#EBF500>" + "Added effect: " + "</color>" + 
                                 skill.GetStatusEffectName + "\n" + "<#EBF500>" + "Status Duration: " + "</color>" + skill.GetStatusDuration + "s" + "\n\n" + "Power: " + 
                                 skill.GetPotency + "\n" + "Cooldown: " + skill.GetCoolDown + "s" + "\n" + "Cast Time: Instant";
            }
        }
        if (skill.GetCastTime <= 0 && skill.GetPotency > 0 && skill.GetManaCost > 0)
        {
            if (skill.GetPlayerStatusEffect == EffectStatus.NONE && skill.GetEnemyStatusEffect == StatusEffect.NONE)
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "Mana: " + skill.GetManaCost + "\n" + "Power: " + skill.GetPotency + "\n" +
                                      "Cooldown: " + skill.GetCoolDown + "s" + "\n" + "Cast Time: Instant";
            }
            else
            {
                SkillInfo.text = "<size=12>" + skill.GetSkillName + "</size>" + "\n\n" + skill.GetSkillDescription + "\n\n" + "Mana: " + skill.GetManaCost + "\n\n" + "<#EBF500>" + 
                                 "Added effect: " + "</color>" + skill.GetStatusEffectName + "\n" + "<#EBF500>" + "Status Duration: " + "</color>" + skill.GetStatusDuration + "s" + 
                                 "\n\n" + "Power: " + skill.GetPotency + "\n" + "Cooldown: " + skill.GetCoolDown + "s" + "\n" + "Cast Time: Instant";
            }
        }
        #endregion
    }
}
