#pragma warning disable 0649
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    [SerializeField]
    private Image ManaBar, FillBarTwo;

    [SerializeField]
    private Character character;

    [SerializeField]
    private CharacterMenu characterMenu;

    [SerializeField]
    private TextMeshProUGUI ManaText;

    private Coroutine routine = null;

    [SerializeField]
    private float FillValue;

    [SerializeField]
    private bool SpendingMana, UnlockedPassive;

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

    public bool GetUnlockedPassive
    {
        get
        {
            return UnlockedPassive;
        }
        set
        {
            UnlockedPassive = value;
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
        ManaText.text = character.CurrentMana.ToString();
    }

    private IEnumerator HealMana()
    {
        float elapsedTime = 0;
        float time = 2f;

        while(elapsedTime < time)
        {
            ManaBar.fillAmount = Mathf.Lerp(ManaBar.fillAmount, FillBarTwo.fillAmount, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator LoseMana()
    {
        float elapsedTime = 0;
        float time = 2f;

        while (elapsedTime < time)
        {
            FillBarTwo.fillAmount = Mathf.Lerp(FillBarTwo.fillAmount, ManaBar.fillAmount, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void IncreaseMana(int Value)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }

        SpendingMana = false;

        character.CurrentMana += Value;

        character.CurrentMana = Mathf.Clamp(character.CurrentMana, 0, character.MaxMana);

        ManaText.text = Mathf.Clamp(character.CurrentMana, 0, character.MaxMana).ToString();

        FillBarTwo.fillAmount = (float)character.CurrentMana / (float)character.MaxMana;

        routine = StartCoroutine(HealMana());

        if (characterMenu != null)
        {
            characterMenu.SetCharacterInfoText();
        }
    }

    public void ModifyMana(int Value)
    {
        if(routine != null)
        {
            StopCoroutine(routine);
        }

        SpendingMana = true;

        character.CurrentMana += Value;

        character.CurrentMana = Mathf.Clamp(character.CurrentMana, 0, character.MaxMana);

        ManaText.text = Mathf.Clamp(character.CurrentMana, 0, character.MaxMana).ToString();

        ManaBar.fillAmount = (float)character.CurrentMana / (float)character.MaxMana;

        routine = StartCoroutine(LoseMana());

        if (characterMenu != null)
        {
            characterMenu.SetCharacterInfoText();
        }
    }

    public void GetFilledBar()
    {
        ManaText.text = Mathf.Clamp(character.CurrentMana, 0, character.MaxMana).ToString();

        ManaBar.fillAmount = (float)character.CurrentMana / (float)character.MaxMana;
    }

    public int RestoreMana()
    {
        if(character.CurrentMana >= character.MaxMana)
        {
            return 0;
        }
        else
        {
            float percent = Mathf.Round(0.01f * (float)character.MaxMana);

            int GetMana = (int)percent;

            Mathf.Round(GetMana);
            if(GetMana < 1)
            {
                GetMana = 1;
            }

            IncreaseMana(GetMana);

            return GetMana;
        }
    }
}
