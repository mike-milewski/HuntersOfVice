#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public enum SkillType { Active, Passive };

public enum PassiveBonus { HP, MP, Strength, Defense, Intelligence, Critical, ItemHP, ItemMana, Heal, WhirlwindSlash, StatPointsBonus, Illumination, ManaSiphon,
                           EvilsEndBonus, DiabolicLightningBonus, ShatterBonus, DiabolicTour, DualDeal };

public class SkillMenu : MonoBehaviour
{
    public static SkillMenu skillmenu;

    [SerializeField]
    private GameObject levelUp, levelUpParent;

    [SerializeField]
    private Skills IlluminationSkill, HealSkill, EvilsEndSkill, DiabolicLightningSkill, ShatterSkill;

    [SerializeField]
    private Items[] items;

    [SerializeField]
    private Character character;

    private Transform DropZoneTransform;

    [SerializeField]
    private TextMeshProUGUI LevelRequirementText, SkillInfoText, SkillNameText;

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
        {
            DropZoneTransform = skill.GetComponent<DragUiObject>().GetDropZone.transform;
            skill.GetComponent<DragUiObject>().SetRectTransform();
        }
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

                skill.gameObject.SetActive(false);
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

                skill.GetComponent<DragUiObject>().SetRectTransform();

                skill.GetComponent<Mask>().showMaskGraphic = true;
                skill.GetComponent<Skills>().GetCoolDownImage.GetComponent<Mask>().showMaskGraphic = true;

                skill.GetComponent<Skills>().GetComponent<Image>().raycastTarget = true;

                SkillsManager.Instance.ClearSkills();
                SkillsManager.Instance.AddSkillsToList();
            }
            else
            {
                skill.GetComponent<Button>().interactable = true;
                GetPassiveBonus();
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

                    skill.GetComponent<DragUiObject>().SetRectTransform();

                    skill.GetComponent<Mask>().showMaskGraphic = true;
                    skill.GetComponent<Skills>().GetCoolDownImage.GetComponent<Mask>().showMaskGraphic = true;

                    skill.GetComponent<Image>().raycastTarget = true;

                    skill.gameObject.SetActive(true);

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
        yield return new WaitForSeconds(0.5f);
        levelUp.GetComponentInChildren<LevelUp>().PlaySkillLearnedSoundEffect();
    }

    private void ShowNewSkill()
    {
        if(character.Level >= LevelRequirement)
        {
            levelUp.GetComponentInChildren<LevelUp>().PlaySkillLearned();
        }
    }

    private void ShowSkillInfo(GameObject Panel)
    {
        Panel.SetActive(true);

        #region SkillInformation
        if (skilltype == SkillType.Active)
        {
            SkillNameText.text = skill.GetComponent<Skills>().GetSkillName;

            if (skill.GetComponent<Skills>().GetCastTime <= 0 && skill.GetComponent<Skills>().GetPotency <= 0 && skill.GetComponent<Skills>().GetManaCost <= 0)
            {
                if (skill.GetComponent<Skills>().GetPlayerStatusEffect == EffectStatus.NONE && skill.GetComponent<Skills>().GetEnemyStatusEffect == StatusEffect.NONE)
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Cast Time: Instant" +
                                             "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s" + "\n";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                             skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Cast Time: Instant" +
                                             "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s" + "\n";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Cast Time: Instant" +
                                             "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
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
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" +
                                             "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s" + "\n";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                             skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" +
                                             "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s" + "\n";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s"
                                             + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
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
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: " +
                                             skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                             skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: " +
                                             skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Power: " +
                                             skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " +
                                             skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
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
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n" + "MP Cost: " +
                                             skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " +
                                             skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                             skill.GetComponent<Skills>().GetPlayerElement + "\n\n" +"Power: " + skill.GetComponent<Skills>().GetPotency + "\n" + "MP Cost: " +
                                             skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " +
                                             skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "<#EFDFB8>" +
                                             "Added effect: " + "</color>" + skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " +
                                             "</color>" + skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "MP Cost: " + skill.GetComponent<Skills>().GetManaCost +
                                             "\n\n" + "Cast Time: " + skill.GetComponent<Skills>().GetCastTime + "s" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
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
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: Instant"
                                             + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                             skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "Cast Time: Instant"
                                             + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency +
                                             "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
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
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n" + "MP Cost: " +
                                             skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" +
                                             skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n" + "MP Cost: " +
                                             skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: Instant" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
                else
                {
                    if(skill.GetComponent<Skills>().GetPlayerElement == PlayerElement.NONE)
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "<#EFDFB8>" +
                                             "Added effect: " + "</color>" + skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "MP Cost: " + skill.GetComponent<Skills>().GetManaCost + "\n\n" + "Cast Time: Instant"
                                             + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                    else
                    {
                        SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + 
                                             skill.GetComponent<Skills>().GetPlayerElement + "\n\n" + "Power: " + skill.GetComponent<Skills>().GetPotency + "\n\n" + "<#EFDFB8>" + 
                                             "Added effect: " + "</color>" + skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + "Status Duration: " + "</color>" +
                                             skill.GetComponent<Skills>().GetStatusDuration + "s" + "\n\n" + "MP Cost: " + skill.GetComponent<Skills>().GetManaCost + "\n\n" + 
                                             "Cast Time: Instant" + "\n" + "Cooldown: " + skill.GetComponent<Skills>().GetCoolDown + "s";
                    }
                }
            }
            if (skill.GetComponent<Skills>().GetCastTime <= 0 && skill.GetComponent<Skills>().GetManaCost <= 0 && skill.GetComponent<Skills>().GetCoolDown <= 0 && 
                skill.GetComponent<Skills>().GetPotency <= 0)
            {
                SkillInfoText.text = skill.GetComponent<Skills>().GetSkillDescription + "\n\n" + "<#EFDFB8>" + "Added effect: " + "</color>" + 
                                     skill.GetComponent<Skills>().GetStatusEffectName + "\n" + "<#EFDFB8>" + 
                                     "Status Duration: " + "</color> Infinite" + "\n\n" + "Cast Time: Instant";
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
                case (PassiveBonus.ItemHP):
                    ItemHPBonusPassiveText();
                    break;
                case (PassiveBonus.ItemMana):
                    ItemManaBonusPassiveText();
                    break;
                case (PassiveBonus.Heal):
                    HealBonusPassiveText();
                    break;
                case (PassiveBonus.StatPointsBonus):
                    StatPointBonusPassiveText();
                    break;
                case (PassiveBonus.Illumination):
                    IlluminationBonusPassiveText();
                    break;
                case (PassiveBonus.ManaSiphon):
                    ManaSiphonPassiveText();
                    break;
                case (PassiveBonus.EvilsEndBonus):
                    EvilsEndBonusPassiveText();
                    break;
                case (PassiveBonus.DiabolicLightningBonus):
                    DiabolicLightningBonusPassiveText();
                    break;
                case (PassiveBonus.ShatterBonus):
                    ShatterBonusPassiveText();
                    break;
            }
        }
        #endregion
    }

    private void HpBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases Maximum HP by " + PassiveBonusValue + "%";
    }

    private void MpBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases Maximum MP by " + PassiveBonusValue + "%";
    }

    private void StrengthBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases Strength by " + PassiveBonusValue + "%";
    }

    private void DefenseBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases Defense by " + PassiveBonusValue + "%";
    }

    private void IntelligenceBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases Intelligence by " + PassiveBonusValue + "%";
    }

    private void CriticalBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases critical hit chance by " + PassiveBonusValue + "%";
    }

    private void StormThrustBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Storm Thrust receives the following bonuses:" + 
                             "\n\n" + "<#EFDFB8>" + "Element: " + "</color>" + "Wind" + "\n\n" + "<#EFDFB8>" + "Added Effect: " + "</color>" + "Stun" + "\n" + "<#EFDFB8>" + 
                             "Duration: " + "</color>" + "4s";
    }

    private void ItemHPBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases Potion's potency by " + PassiveBonusValue + "% " +
                             "and reduces the cooldown by half.";
    }

    private void ItemManaBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases Ether's potency by " + PassiveBonusValue + "% " +
                             "and reduces the cooldown by half";
    }

    private void HealBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Heal's cast time is reduced by 1 and power is increased by 50.";
    }

    private void StatPointBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases the amount of stat points received upon level up by 1.";
    }

    private void IlluminationBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "HP regeneration from Illumination recovers faster and its potency is increased by 5%.";
    }

    private void ManaSiphonPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Mana is recovered by 5% every time damage is dealt to a target by auto-attacks";
    }

    private void EvilsEndBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "The enemy HP condition for Evil's End is increased to 35% and halves the cooldown.";
    }

    private void DiabolicLightningBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Increases the damage area of Diabolic Lightning and halves the cooldown.";
    }

    private void ShatterBonusPassiveText()
    {
        SkillNameText.text = PassiveSkillName;

        SkillInfoText.text = "Shatter has a 5% chance of instantly defeating a target. \n <#EFDFB8>Does not work on bosses.</color>";
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
            case (PassiveBonus.ItemHP):
                ItemBonusHP();
                break;
            case (PassiveBonus.ItemMana):
                ItemBonusMana();
                break;
            case (PassiveBonus.Heal):
                HealBonus();
                break;
            case (PassiveBonus.ManaSiphon):
                ManaSiphon();
                break;
            case (PassiveBonus.StatPointsBonus):
                StatPointBonus();
                break;
            case (PassiveBonus.Illumination):
                IlluminationBonus();
                break;
            case (PassiveBonus.EvilsEndBonus):
                EvilsEndBonus();
                break;
            case (PassiveBonus.DiabolicLightningBonus):
                DiabolicLightningBonus();
                break;
            case (PassiveBonus.ShatterBonus):
                ShatterBonus();
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

    private void HealBonus()
    {
        HealSkill.GetCastTime -= 1;
        HealSkill.GetPotency += 50;
    }

    private void ManaSiphon()
    {
        character.GetComponent<Mana>().GetUnlockedPassive = true;
    }

    private void EvilsEndBonus()
    {
        EvilsEndSkill.GetStatusEffectPotency = 35;
        EvilsEndSkill.GetCoolDown = EvilsEndSkill.GetCoolDown / 2;

        EvilsEndSkill.GetSkillDescription = "Delivers a punishing blow to the target. <#EFDFB8>Can only be executed while the target is at 35% HP or below.</color>";
    }

    private void IlluminationBonus()
    {
        IlluminationSkill.GetStatusEffectPotency = 2;
        IlluminationSkill.GetHpAndDamageOverTimeTick += 5;

        IlluminationSkill.GetSkillDescription = "Gradually restores HP by 15%.";
    }

    private void DiabolicLightningBonus()
    {
        DiabolicLightningSkill.GetCoolDown -= DiabolicLightningSkill.GetCoolDown / 2;
        DiabolicLightningSkill.GetAreaOfEffectRange += 2;
    }

    private void ShatterBonus()
    {
        ShatterSkill.GetGainedPassive = true;
    }

    private void DiabolicTourBonus()
    {

    }

    private void DualDealBonus()
    {

    }

    private void StatPointBonus()
    {
        character.GetComponent<Experience>().GetStatPointIncrease++;
    }

    private void ItemBonusHP()
    {
        items[0].GetHealAmount += PassiveBonusValue;
        items[0].GetCoolDown = items[0].GetCoolDown / 2;
        items[0].GetCoolDownImage.fillAmount = 0;
    }

    private void ItemBonusMana()
    {
        items[1].GetHealAmount += PassiveBonusValue;
        items[1].GetCoolDown = items[1].GetCoolDown / 2;
        items[1].GetCoolDownImage.fillAmount = 0;
    }
}
