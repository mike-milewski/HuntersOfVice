using UnityEngine;
using TMPro;

public class ItemDescription : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ItemDescriptionText;

    public void MonsterBookDescription()
    {
        ItemDescriptionText.text = "<size=12>" + "<u>" + "Monster Book" + "</u>" + "</size>" + "\n\n" + "A tome recording the information of all defeated monsters.";
    }

    public void TutorialBookDescription()
    {
        ItemDescriptionText.text = "<size=12>" + "<u>" + "Tutorial Book" + "</u>" + "</size>" + "\n\n" + "A tome that keeps track of helpful information.";
    }
}
