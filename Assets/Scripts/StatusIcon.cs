using UnityEngine;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    [SerializeField]
    private Text DurationText, StatusDescriptionText;

    [SerializeField]
    private float Duration;

    private int KeyInput;

    [SerializeField]
    private GameObject StatusPanel;

    public int GetKeyInput
    {
        get
        {
            return KeyInput;
        }
        set
        {
            KeyInput = value;
        }
    }

    private void OnEnable()
    {
        KeyInput = SkillsManager.Instance.GetKeyInput;

        Duration = SkillsManager.Instance.GetSkills[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName + "\n" +
                                     SkillsManager.Instance.GetSkills[KeyInput].GetStatusDescription;
    }

    public Text RemoveStatusEffectText()
    {
        var SkillObj = Instantiate(SkillsManager.Instance.GetSkills[KeyInput].GetSkillTextObject);

        SkillObj.transform.SetParent(SkillsManager.Instance.GetSkills[KeyInput].GetTextHolder.transform, false);

        SkillObj.text = "-" + SkillsManager.Instance.GetSkills[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        return SkillObj;
    }

    private void OnDisable()
    {
        RemoveStatusEffectText();
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