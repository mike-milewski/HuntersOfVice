using UnityEngine;
using TMPro;

public class SkillInformationText : MonoBehaviour
{
    [SerializeField]
    private SelectedCharacter selectedCharacter;

    [SerializeField]
    private TextMeshProUGUI SkillNameText, SkillInfoText;

    [SerializeField]
    private string KnightSkillName, ShadowPriestSkillName;

    [SerializeField][TextArea]
    private string KnightSkillInfo, ShadowPriestSkillInfo;

    public void ShowSkillNameAndInfo()
    {
        if(selectedCharacter.GetKnightSelected)
        {
            SkillNameText.text = KnightSkillName;
            SkillInfoText.text = KnightSkillInfo;
        }
        else if(selectedCharacter.GetShadowPriestSelected)
        {
            SkillNameText.text = ShadowPriestSkillName;
            SkillInfoText.text = ShadowPriestSkillInfo;
        }
    }
}
