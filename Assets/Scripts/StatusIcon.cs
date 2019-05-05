using UnityEngine;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    [SerializeField]
    private Text DurationText, StatusDescriptionText;

    [SerializeField]
    private GameObject TestObject;

    [SerializeField]
    private float Duration;

    private int KeyInput;

    [SerializeField]
    private GameObject StatusPanel;

    private void OnEnable()
    {
        KeyInput = SkillsManager.Instance.GetKeyInput;

        Duration = SkillsManager.Instance.GetSkills[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "\n" +
                                     SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;
    }

    private void OnDisable()
    {
        SkillsManager.Instance.GetSkills[KeyInput].StatusEffectRemovedText();
    }

    private void Update()
    {
        DurationText.text = Duration.ToString("F0");
        Duration -= Time.deltaTime;
        if(Duration <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}