#pragma warning disable 0649
using UnityEngine;
using TMPro;
using System.Collections;

public class LoreMonolith : MonoBehaviour
{
    [SerializeField]
    private string LoreNumberString;

    [SerializeField] [TextArea]
    private string LoreString;

    [SerializeField]
    private GameObject LorePanel;

    [SerializeField]
    private TextMeshProUGUI PanelTitleText, PanelText;

    private bool ClosingPanel;

    public GameObject GetLorePanel
    {
        get
        {
            return LorePanel;
        }
        set
        {
            LorePanel = value;
        }
    }

    [SerializeField]
    private float LoreDistance;

    public float GetLoreDistance
    {
        get
        {
            return LoreDistance;
        }
        set
        {
            LoreDistance = value;
        }
    }

    public void OpenPanel()
    {
        if(!ClosingPanel)
        {
            LorePanel.GetComponent<Animator>().SetBool("OpenMenu", true);
            PanelTitleText.text = LoreNumberString;
            PanelText.text = LoreString;
        }
    }

    public void ClosePanel()
    {
        ClosingPanel = true;
        LorePanel.GetComponent<Animator>().SetBool("OpenMenu", false);
        SoundManager.Instance.ReverseMenu();
        Invoke("InvokeSetClosingPanelToFalse", 0.6f);
    }

    private void InvokeSetClosingPanelToFalse()
    {
        ClosingPanel = false;
    }
}
