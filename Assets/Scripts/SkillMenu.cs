using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public enum SkillType { Active, Passive };

public enum PassiveBonus { HP, MP, Strength, Defense, Intelligence, Critical, StormThrust, Item };

public class SkillMenu : MonoBehaviour
{
    public static SkillMenu skillmenu;

    [SerializeField]
    private GameObject levelUp, levelUpParent;

    [SerializeField]
    private Character character;

    private Transform DropZoneTransform;

    [SerializeField]
    private TextMeshProUGUI LevelRequirementText, ActiveSkillInfo;

    [SerializeField]
    private string PassiveSkillName;

    [SerializeField] [Tooltip("The value used for passive abilities that increases stats via percentage.")]
    private int PassiveBonusValue;

    [SerializeField]
    private int LevelRequirement;

    [SerializeField]
    private Image SkillImage;

    [SerializeField]
    private GameObject skill;

    [SerializeField]
    private SkillType skilltype;

    [SerializeField]
    private PassiveBonus passivebonus;

    private void Awake()
    {
        skillmenu = this;

        if(skilltype == SkillType.Active)
        DropZoneTransform = skill.GetComponent<DragUiObject>().GetDropZone.transform;
    }

    private void Start()
    {
        CheckCharacterLevel();
    }

    private void CheckCharacterLevel()
    {
        LevelRequirementText.text = "<size=11>" + "<b>" + "Lv " + "</b>" +"</size>" + LevelRequirement.ToString();

        if(character.Level < LevelRequirement)
        {
            if(skilltype == SkillType.Active)
            {
                SkillImage.GetComponent<Button>().interactable = false;
                skill.GetComponent<Skills>().GetComponent<Image>().enabled = false;
                skill.GetComponent<Skills>().GetComponent<Button>().interactable = false;

                skill.GetComponent<DragUiObject>().enabled = false;
            }
            else
            {
                skill.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            if(skilltype == SkillType.Active)
            {
                SkillImage.GetComponent<Button>().interactable = true;
                skill.GetComponent<Button>().enabled = true;

                skill.GetComponent<DragUiObject>().enabled = true;

                skill.GetComponent<Skills>().enabled = true;

                skill.transform.SetParent(DropZoneTransform, false);

                skill.GetComponent<Mask>().showMaskGraphic = true;
                skill.GetComponent<Skills>().GetCoolDownImage.GetComponent<Mask>().showMaskGraphic = true;

                skill.GetComponent<Skills>().GetComponent<Image>().raycastTarget = true;

                SkillsManager.Instance.ClearSkills();
                SkillsManager.Instance.AddSkillsToList();
            }
            else
            {
                skill.GetComponent<Button>().interactable = true;
            }
        }
    }

    //Function used for when the player levels up.
    public void CheckIfUnlockedNewSkill()
    {
        if (character.Level >= LevelRequirement)
        {
            if(skilltype == SkillType.Active)
            {
                if(!skill.GetComponent<DragUiObject>().enabled)
                {
                    SetActiveSkillText();

                    SkillImage.GetComponent<Button>().interactable = true;
                    skill.GetComponent<Image>().enabled = true;
                    skill.GetComponent<Button>().enabled = true;

                    skill.GetComponent<DragUiObject>().enabled = true;
                    skill.GetComponent<Skills>().enabled = true;

                    skill.transform.SetParent(DropZoneTransform, false);

                    skill.GetComponent<Mask>().showMaskGraphic = true;
                    skill.GetComponent<Skills>().GetCoolDownImage.GetComponent<Mask>().showMaskGraphic = true;

                    skill.GetComponent<Image>().raycastTarget = true;

                    SkillsManager.Instance.ClearSkills();
                    SkillsManager.Instance.AddSkillsToList();
                }
            }
            else
            {
                if(!skill.GetComponent<Button>().interactable)
                {
                    SetPassiveSkillText();

                    skill.GetComponent<Button>().interactable = true;
                    GetPassiveBonus();
                }
            }
        }
    }

    private void SetActiveSkillText()
    {
        CreateLevelUpText();

        levelUp.GetComponentInChildren<LevelUp>().GetSkillImage.sprite = skill.GetComponent<Image>().sprite;
        levelUp.GetComponentInChildren<LevelUp>().GetSkillText.text = "<size=15>Active Skill Learned</size>" + "\n" + skill.GetComponent<Skills>().GetSkillName;
    }

    private void SetPassiveSkillText()
    {
        CreateLevelUpText();

        levelUp.GetComponentInChildren<LevelUp>().GetSkillImage.sprite = gameObject.GetComponent<Image>().sprite;
        levelUp.GetComponentInChildren<LevelUp>().GetSkillText.text = "<size=15>Passive Skill Learned</size>" + "\n" + PassiveSkillName;
    }

    private void CreateLevelUpText()
    {
        levelUp = ObjectPooler.Instance.GetLevelUpText();

        levelUp.SetActive(true);

        levelUp.transform.position = new Vector3(levelUpParent.transform.position.x, levelUpParent.transform.position.y,
                                                 levelUpParent.transform.position.z);

        levelUp.transform.SetParent(levelUpParent.transform);

        levelUp.transform.localScale = new Vector3(.7f, .7f, .7f);

        StartCoroutine("GetNewSkillText");
    }

    private IEnumerator GetNewSkillText()
    {
        yield return new WaitForSeconds(1.0f);
        ShowNewSkill();
    }

    private void ShowNewSkill()
    {
        if(character.Level >= LevelRequirement)
        levelUp.GetComponentInChildren<LevelUp>().PlaySkillLearned();
    }

    private void ShowSkillInfo(GameObject Panel)
    {
        Panel.SetActive(true);

        #region SkillInformation
        if(skilltype == SkillType.Active)
        {
            if (skill.GetComponent<Skills>().GetCastTime <= 0 && skill.GetComponent<Skills>().GetPotency <= 0 && skill.GetComponent<Skills>().GetManaCost <= 0)
            {
                if (skill.GetComponent<Skills>().GetPlayerStatusEffect == EffectStatus.NONE && skill.GetComponent<Skills>().GetEnemyStatusEffect == StatusEffect.NONE)
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Cast Time: Instant" +
                                           "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s" + "\n";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Cast Time: Instant" +
                                           "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s" + "\n";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Cast Time: Instant" +
                                           "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Cast Time: Instant" +
                                           "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
            }
            if (skill.GetComponent<Skills>().GetCastTime > 0 && skill.GetComponent<Skills>().GetPotency <= 0 && skill.GetComponent<Skills>().GetManaCost <= 0)
            {
                if (skill.GetComponent<Skills>().GetPlayerStatusEffect == EffectStatus.NONE && skill.GetComponent<Skills>().GetEnemyStatusEffect == StatusEffect.NONE)
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" +
                                           "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s" + "\n";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" +
                                           "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s" + "\n";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s"
                                           + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s"
                                           + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
            }
            if (skill.GetComponent<Skills>().GetCastTime > 0 && skill.GetComponent<Skills>().GetPotency > 0 && skill.GetComponent<Skills>().GetManaCost <= 0)
            {
                if (skill.GetComponent<Skills>().GetPlayerStatusEffect == EffectStatus.NONE && skill.GetComponent<Skills>().GetEnemyStatusEffect == StatusEffect.NONE)
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: " +
                                           skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: " +
                                           skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Power: " +
                                           skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " +
                                           skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Power: " +
                                           skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " +
                                           skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
            }
            if (skill.GetComponent<Skills>().GetCastTime > 0 && skill.GetComponent<Skills>().GetPotency > 0 && skill.GetComponent<Skills>().GetManaCost > 0)
            {
                if (skill.GetComponent<Skills>().GetPlayerStatusEffect == EffectStatus.NONE && skill.GetComponent<Skills>().GetEnemyStatusEffect == StatusEffect.NONE)
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n" + "MP Cost: " +
                                           skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " +
                                           skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" +"Power: " + skill.GetComponent<Skills>().GetPotency + "\n" + "MP Cost: " +
                                           skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " +
                                           skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "<#EFDFB8>" +
                                           "Added effect: " + "</color>" + skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " +
                                           "</color>" + skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "MP Cost: " + skill.GetComponent<Skills>().GetManaCost +
                                           "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "<#EFDFB8>" +
                                           "Added effect: " + "</color>" + skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " +
                                           "</color>" + skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "MP Cost: " + skill.GetComponent<Skills>().GetManaCost +
                                           "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
            }
            if (skill.GetComponent<Skills>().GetCastTime <= 0 && skill.GetComponent<Skills>().GetPotency > 0 && skill.GetComponent<Skills>().GetManaCost <= 0)
            {
                if (skill.GetComponent<Skills>().GetPlayerStatusEffect == EffectStatus.NONE && skill.GetComponent<Skills>().GetEnemyStatusEffect == StatusEffect.NONE)
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: Instant"
                                           + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: Instant"
                                           + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency +
                                           "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency +
                                           "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
            }
            if (skill.GetComponent<Skills>().GetCastTime <= 0 && skill.GetComponent<Skills>().GetPotency > 0 && skill.GetComponent<Skills>().GetManaCost > 0)
            {
                if (skill.GetComponent<Skills>().GetPlayerStatusEffect == EffectStatus.NONE && skill.GetComponent<Skills>().GetEnemyStatusEffect == StatusEffect.NONE)
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n" + "MP Cost: " +
                                           skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n" + "MP Cost: " +
                                           skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "<#EFDFB8>" +
                                           "Added effect: " + "</color>" + skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "MP Cost: " + skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: Instant"
                                           + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        ActiveSkillInfo.text = "<size=12>" + "<u>" + skill.GetComponent<Skills>().GetSkillName + "</u>" + "</size>" + "\n\n" +
                                           skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                           skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "<#EFDFB8>" + 
                                           "Added effect: " + "</color>" + skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                           skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "MP Cost: " + skill.GetComponent<Skills>().GetManaCost + "\n\n" + 
                                           "Cast Time: Instant" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
            }
        }
        else
        {
            switch (passivebonus)
            {
                case (PassiveBonus.HP):
                    HpBonusPassiveText();
                    break;
                case (PassiveBonus.MP):
                    MpBonusPassiveText();
                    break;
                case (PassiveBonus.Strength):
                    StrengthBonusPassiveText();
                    break;
                case (PassiveBonus.Defense):
                    DefenseBonusPassiveText();
                    break;
                case (PassiveBonus.Intelligence):
                    IntelligenceBonusPassiveText();
                    break;
                case (PassiveBonus.Critical):
                    CriticalBonusPassiveText();
                    break;
                case (PassiveBonus.StormThrust):
                    StormThrustBonusPassiveText();
                    break;
            }
        }
        #endregion
    }

    private void HpBonusPassiveText()
    {
        ActiveSkillInfo.text = "<size=12>" + "<u>" + PassiveSkillName + "</u>" + "</size>" + "\n\n" + "Increases Maximum HP by " + PassiveBonusValue + "%";
    }

    private void MpBonusPassiveText()
    {
        ActiveSkillInfo.text = "<size=12>" + PassiveSkillName + "</size>" + "\n\n" + "Increases Maximum MP by " + PassiveBonusValue + "%";
    }

    private void StrengthBonusPassiveText()
    {
        ActiveSkillInfo.text = "<size=12>" + PassiveSkillName + "</size>" + "\n\n" + "Increases Strength by " + PassiveBonusValue + "%";
    }

    private void DefenseBonusPassiveText()
    {
        ActiveSkillInfo.text = "<size=12>" + PassiveSkillName + "</size>" + "\n\n" + "Increases Defense by " + PassiveBonusValue + "%";
    }

    private void IntelligenceBonusPassiveText()
    {
        ActiveSkillInfo.text = "<size=12>" + PassiveSkillName + "</size>" + "\n\n" + "Increases Intelligence by " + PassiveBonusValue + "%";
    }

    private void CriticalBonusPassiveText()
    {
        ActiveSkillInfo.text = "<size=12>" + PassiveSkillName + "</size>" + "\n\n" + "Increases critical hit chance by " + PassiveBonusValue + "%";
    }

    private void StormThrustBonusPassiveText()
    {
        ActiveSkillInfo.text = "<size=12>" + "<u>" + PassiveSkillName + "</u>" + "</size>" + "\n\n" + "Storm Thrust receives the following bonuses:" + 
                               "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + "Wind" + "\n\n" + "<#EFDFB8>" + "Added Effect: " + "</color>" + "Stun" + "\n" + "<#EFDFB8>" + 
                               "Duration: " + "</color>" + "4s";
    }

    private void GetPassiveBonus()
    {
        switch(passivebonus)
        {
            case (PassiveBonus.HP):
                HpBonus();
                break;
            case (PassiveBonus.MP):
                MpBonus();
                break;
            case (PassiveBonus.Strength):
                StrengthBonus();
                break;
            case (PassiveBonus.Defense):
                DefenseBonus();
                break;
            case (PassiveBonus.Intelligence):
                IntelligenceBonus();
                break;
            case (PassiveBonus.Critical):
                CriticalBonus();
                break;
            case (PassiveBonus.StormThrust):
                StormThrustBonus();
                break;
            case (PassiveBonus.Item):
                ItemBonus();
                break;
        }
    }

    private int HpBonus()
    {
        float Percentage = (float)PassiveBonusValue / 100;

        float Hpbonus = character.MaxHealth;

        Hpbonus += character.MaxHealth * Percentage;

        Mathf.Round(Hpbonus);

        character.MaxHealth = (int)Hpbonus;

        return character.MaxHealth;
    }

    private int MpBonus()
    {
        float Percentage = (float)PassiveBonusValue / 100;

        float Mpbonus = character.MaxMana;

        Mpbonus += character.MaxMana * Percentage;

        Mathf.Round(Mpbonus);

        character.MaxMana = (int)Mpbonus;

        return character.MaxMana;
    }

    private int StrengthBonus()
    {
        float Percentage = (float)PassiveBonusValue / 100;

        float Strengthbonus = character.CharacterStrength;

        Strengthbonus += character.CharacterStrength * Percentage;

        Mathf.Round(Strengthbonus);

        character.CharacterStrength = (int)Strengthbonus;

        return character.CharacterStrength;
    }

    private int DefenseBonus()
    {
        float Percentage = (float)PassiveBonusValue / 100;

        float Defensebonus = character.CharacterDefense;

        Defensebonus += character.CharacterDefense * Percentage;

        Mathf.Round(Defensebonus);

        character.CharacterDefense = (int)Defensebonus;

        return character.CharacterDefense;
    }

    private int IntelligenceBonus()
    {
        float Percentage = (float)PassiveBonusValue / 100;

        float Intelligencebonus = character.CharacterIntelligence;

        Intelligencebonus += character.CharacterIntelligence * Percentage;

        Mathf.Round(Intelligencebonus);

        character.CharacterIntelligence = (int)Intelligencebonus;

        return character.CharacterIntelligence;
    }

    private int CriticalBonus()
    {
        float Percentage = (float)PassiveBonusValue / 100;

        float Criticalbonus = character.GetCriticalChance;

        Criticalbonus += character.GetCriticalChance * Percentage;

        Mathf.Round(Criticalbonus);

        character.GetCriticalChance = (int)Criticalbonus;

        return character.GetCriticalChance;
    }

    private void StormThrustBonus()
    {

    }

    private void ItemBonus()
    {

    }
}
