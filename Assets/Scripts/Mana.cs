using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    [SerializeField]
    private Image ManaBar;

    [SerializeField]
    private Character character;

    [SerializeField]
    private Text ManaText;

    private void Reset()
    {
        character = GetComponent<Character>();
    }

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        ManaText.text = character.CurrentMana.ToString() + "/" + character.MaxMana.ToString();
    }

    public void ModifyMana(int Value)
    {
        character.CurrentMana += Value;

        ManaText.text = character.CurrentMana.ToString() + "/" + character.MaxMana.ToString();

        ManaBar.fillAmount = (float)character.CurrentMana / (float)character.MaxMana;
    }
}
