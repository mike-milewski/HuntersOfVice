using UnityEngine;
using TMPro;

public class MonsterButton : MonoBehaviour
{
    [SerializeField]
    private MonsterInformation monsterInformation = null;

    [SerializeField]
    private TextMeshProUGUI MonsterInfoText;

    [SerializeField]
    private int Index;

    public MonsterInformation GetMonsterInformation
    {
        get
        {
            return monsterInformation;
        }
        set
        {
            monsterInformation = value;
        }
    }

    public int GetIndex
    {
        get
        {
            return Index;
        }
        set
        {
            Index = value;
        }
    }

    public void ShowMonsterInfo()
    {
        MonsterInfoText.text = "<u>" + monsterInformation.GetCharacterData[Index].CharacterName + "</u>" + "\n\n" + "<size=12>" + "Level: " +
                                                                        monsterInformation.GetCharacterData[Index].CharacterLevel + "\n" + "HP: " +
                                                                        monsterInformation.GetCharacterData[Index].Health +
                                                                        "\n" + "Strength: " + monsterInformation.GetCharacterData[Index].Strength + "\n" + "Defense: " +
                                                                        monsterInformation.GetCharacterData[Index].Defense + "\n" + "Intelligence: " +
                                                                        monsterInformation.GetCharacterData[Index].Intelligence + "\n\n" + monsterInformation.GetWeaknesses()
                                                                        + monsterInformation.GetResistances() + "\n\n"
                                                                        + "EXP: " + monsterInformation.GetCharacter.GetComponent<Enemy>().GetExperiencePoints + "\n" + 
                                                                        "Coins: " + monsterInformation.GetCharacter.GetComponent<Enemy>().GetCoins;
    }
}
