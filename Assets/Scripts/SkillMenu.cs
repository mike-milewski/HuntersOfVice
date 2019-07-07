using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillMenu : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private TextMeshProUGUI LevelRequirementText;

    [SerializeField]
    private Image SkillImage;

    [SerializeField]
    private Skills skill;

    [SerializeField]
    private int LevelRequirement;

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
            Debug.Log("Not the required level.");
        }
        else
        {
            SkillImage.GetComponent<Button>().interactable = true;
            Debug.Log("At the required level");
        }
    }
}
