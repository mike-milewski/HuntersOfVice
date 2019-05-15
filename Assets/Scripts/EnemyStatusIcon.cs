using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusIcon : MonoBehaviour
{
    [SerializeField]
    private Character character = null;

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

    private void Start()
    {
        character = GetComponentInParent<Character>();

        KeyInput = character.GetComponent<EnemySkills>().GetRandomValue;

        Duration = character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDuration;

        StatusDescriptionText.text = character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName + "\n" +
                                     character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusDescription;
    }

    private void LateUpdate()
    {
        ToggleStatusIcon();
    }

    public Text RemoveStatusEffectText()
    {
        var SkillObj = Instantiate(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetSkillTextObject);

        SkillObj.transform.SetParent(character.GetComponent<EnemySkills>().GetManager[KeyInput].GetTextHolder.transform, false);

        SkillObj.text = "-" + character.GetComponent<EnemySkills>().GetManager[KeyInput].GetStatusEffectName;

        SkillObj.GetComponentInChildren<Image>().sprite = this.GetComponent<Image>().sprite;

        return SkillObj;
    }

    private void OnDisable()
    {
        RemoveStatusEffectText();
    }

    private void ToggleStatusIcon()
    {
        if (GameManager.Instance.GetLastObject == character.GetComponent<Enemy>().gameObject)
        {
            this.GetComponentInChildren<Image>().enabled = true;
            DurationText.enabled = true;
        }
        else if (GameManager.Instance.GetLastObject != character.GetComponent<Enemy>().gameObject)
        {
            this.GetComponentInChildren<Image>().enabled = false;
            DurationText.enabled = false;
        }
    }

    private void Update()
    {
        DurationText.text = Duration.ToString("F0");
        Duration -= Time.deltaTime;
        if (Duration <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
