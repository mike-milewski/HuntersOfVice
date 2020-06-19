using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    private SelectedCharacter selectedCharacter;

    [SerializeField]
    private string CharacterClass;

    private void OnEnable()
    {
        SelectedCharacter SC = FindObjectOfType<SelectedCharacter>();

        selectedCharacter = SC;
    }

    public SelectedCharacter GetSelectedCharacter
    {
        get
        {
            return selectedCharacter;
        }
        set
        {
            selectedCharacter = value;
        }
    }

    public string GetCharacterClass
    {
        get
        {
            return CharacterClass;
        }
        set
        {
            CharacterClass = value;
        }
    }
}
