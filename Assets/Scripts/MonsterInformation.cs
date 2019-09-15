using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterInformation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI MonsterName;

    [SerializeField]
    private GameObject ParentObj;

    [SerializeField]
    private Button button;

    [SerializeField]
    private Character character = null;

    private void Awake()
    {
        ParentObj = transform.parent.parent.gameObject;
    }

    public Character GetCharacter
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
        }
    }

    public void ShowMonsterInfo()
    {
        ParentObj.GetComponent<MonsterBook>().GetMonsterInfoTxt.text = "<u>" + character.GetCharacterData.CharacterName + "</u>" + "\n\n" + "<size=12>" + "Level: " +
                                                                        character.GetCharacterData.CharacterLevel + "\n" + "HP: " + character.GetCharacterData.Health +
                                                                        "\n" + "Strength: " + character.GetCharacterData.Strength + "\n" + "Defense: " +
                                                                        character.GetCharacterData.Defense + "\n" + "Intelligence: " +
                                                                        character.GetCharacterData.Intelligence + "\n\n" + "EXP: " +
                                                                        character.GetComponent<Enemy>().GetExperiencePoints + "\n" + "Coins: " +
                                                                        character.GetComponent<Enemy>().GetCoins;
    }

    public TextMeshProUGUI GetMonsterName
    {
        get
        {
            return MonsterName;
        }
        set
        {
            MonsterName = value;
        }
    }
}
