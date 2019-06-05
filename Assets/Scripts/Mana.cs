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

    [SerializeField]
    private bool SpendingMana;

    public bool GetSpendingMana
    {
        get
        {
            return SpendingMana;
        }
        set
        {
            SpendingMana = value;
        }
    }

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
        if(SpendingMana)
        {
            FillBarTwo.fillAmount = Mathf.Lerp(FillBarTwo.fillAmount, ManaBar.fillAmount, FillValue);
        }
        else
        {
            ManaBar.fillAmount = Mathf.Lerp(ManaBar.fillAmount, FillBarTwo.fillAmount, FillValue);
        }
    }

    public void IncreaseMana(int Value)
    {
        SpendingMana = false;

        character.CurrentMana += Value;

        ManaText.text = Mathf.Clamp(character.CurrentMana, 0, character.MaxMana) + "/" + character.MaxMana.ToString();

        FillBarTwo.fillAmount = (float)character.CurrentMana / (float)character.MaxMana;
    }

    public void ModifyMana(int Value)
    {
        SpendingMana = true;

        character.CurrentMana += Value;

        ManaText.text = Mathf.Clamp(character.CurrentMana, 0, character.MaxMana) + "/" + character.MaxMana.ToString();

        ManaBar.fillAmount = (float)character.CurrentMana / (float)character.MaxMana;
    }
}
