using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    [SerializeField]
    private Image ManaBar, FillBarTwo;

    [SerializeField]
    private Character character;

    [SerializeField]
    private Text ManaText;

    [SerializeField]
    private float FillValue;

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

    private void LateUpdate()
    {
        FillBarTwo.fillAmount = Mathf.Lerp(FillBarTwo.fillAmount, ManaBar.fillAmount, FillValue);
    }

    public void ModifyMana(int Value)
    {
        character.CurrentMana += Value;

        ManaText.text = Mathf.Clamp(character.CurrentMana, 0, character.MaxMana) + "/" + character.MaxMana.ToString();

        ManaBar.fillAmount = (float)character.CurrentMana / (float)character.MaxMana;
    }
}
