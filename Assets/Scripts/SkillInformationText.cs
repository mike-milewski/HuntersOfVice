#pragma warning disable 0649
using UnityEngine;
using TMPro;

public class SkillInformationText : MonoBehaviour
{
    [SerializeField]
    private SelectedCharacter selectedCharacter;

    [SerializeField]
    private TextMeshProUGUI SkillNameText, SkillInfoText;

    [SerializeField]
    private string KnightSkillName, ShadowPriestSkillName, ToadstoolSkillName;

    [SerializeField][TextArea]
    private string KnightSkillInfo, ShadowPriestSkillInfo, ToadstoolSkillInfo;

    private void OnEnable()
    {
        SelectedCharacter SC = FindObjectOfType<SelectedCharacter>();

        selectedCharacter = SC;
    }

    public void ShowSkillNameAndInfo()
    {
        if(selectedCharacter.GetKnightSelected)
        {
            SkillNameText.text = KnightSkillName;
            SkillInfoText.text = KnightSkillInfo;
        }
        if(selectedCharacter.GetShadowPriestSelected)
        {
            SkillNameText.text = ShadowPriestSkillName;
            SkillInfoText.text = ShadowPriestSkillInfo;
        }
        if (selectedCharacter.GetToadstoolSelected)
        {
            SkillNameText.text = ToadstoolSkillName;
            SkillInfoText.text = ToadstoolSkillInfo;
        }
    }
}
