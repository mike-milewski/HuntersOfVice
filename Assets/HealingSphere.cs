#pragma warning disable 0649
using TMPro;
using UnityEngine;

public class HealingSphere : MonoBehaviour
{
    private Character character;

    [SerializeField]
    private Transform HealTextTransform;

    [SerializeField]
    private string SkillName;

    [SerializeField]
    private int HealAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            character = other.GetComponent<Character>();

            Heal();

            gameObject.SetActive(false);
        }
    }

    private TextMeshProUGUI Heal()
    {
        SoundManager.Instance.Heal();

        var HealTxt = ObjectPooler.Instance.GetPlayerHealText();

        HealTxt.SetActive(true);

        HealTxt.transform.SetParent(HealTextTransform.transform, false);

        #region CriticalHealChance
        if (character.CurrentHealth > 0)
        {
            HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + SkillName + " " + HealAmount;
        }
        #endregion

        character.GetComponent<Health>().IncreaseHealth(HealAmount);

        return HealTxt.GetComponentInChildren<TextMeshProUGUI>();
    }
}
