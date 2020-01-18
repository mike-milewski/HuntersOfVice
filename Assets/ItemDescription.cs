using UnityEngine;
using TMPro;

public class ItemDescription : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ItemDescriptionText;

    public void MonsterTomeDescription()
    {
        ItemDescriptionText.text = "<size=12>" + "<u>" + "Monster Tome" + "</u>" + "</size>" + "\n\n" + 
                                   "A mystic tome that records valuable information of all defeated monsters.";
    }

    public void TipTomeDescription()
    {
        ItemDescriptionText.text = "<size=12>" + "<u>" + "Tip Tome" + "</u>" + "</size>" + "\n\n" + "A mystic tome that highlights a list of helpful information.";
    }
}
