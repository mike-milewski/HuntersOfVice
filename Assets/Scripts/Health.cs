using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Image HealthBar, FillBarTwo;

    [SerializeField]
    private Character character;

    [SerializeField]
    private GameObject DamageTextParent;

    [SerializeField]
    private Text HealthText, DamageText;

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
        HealthText.text = character.CurrentHealth + "/" + character.MaxHealth;
    }

    private void LateUpdate()
    {
        FillBarTwo.fillAmount = Mathf.Lerp(FillBarTwo.fillAmount, HealthBar.fillAmount, 4 * Time.deltaTime);
    }

    public void ModifyHealth(int Value)
    {
        character.CurrentHealth += Value;

        character.CurrentHealth = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth);

        HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth) + "/" + character.MaxHealth;

        HealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }

    public void GetFilledBar()
    {
        HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth) + "/" + character.MaxHealth;

        HealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }

    public GameObject GetDamageTextParent
    {
        get
        {
            return DamageTextParent;
        }
        set
        {
            DamageTextParent = value;
        }
    }

    public Text GetDamageText
    {
        get
        {
            return DamageText;
        }
        set
        {
            DamageText = value;
        }
    }
}
