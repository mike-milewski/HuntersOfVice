#pragma warning disable 0649
using UnityEngine;
using TMPro;

public class LoreMonolith : MonoBehaviour
{
    [SerializeField]
    private string LoreNumberString;

    [SerializeField] [TextArea]
    private string LoreString;

    [SerializeField]
    private GameObject LorePanel;

    [SerializeField]
    private TextMeshProUGUI PanelText;

    public void OpenPanel()
    {
        LorePanel.GetComponent<Animator>().SetBool("OpenPanel", true);
        PanelText.text = LoreNumberString + "\n\n" + LoreString;
    }
}
