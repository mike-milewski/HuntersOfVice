using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Image HealthBar, LocalHealthBar, FillBarTwo;

    [SerializeField]
    private GameObject LocalHealth;

    [SerializeField]
    private GameObject DamageTextHolder;

    [SerializeField]
    private Text EnemyInfo, LocalEnemyInfo, DamageText;

    private void Reset()
    {
        character = GetComponent<Character>();
    }

    private void Awake()
    {
        character.GetComponent<Character>();
    }

    private void Start()
    {
        GetEnemyInfo();
    }

    private void LateUpdate()
    {
        FillBarTwo.fillAmount = Mathf.Lerp(FillBarTwo.fillAmount, HealthBar.fillAmount, .08f);
    }

    public void ModifyHealth(int Value)
    {
        character.CurrentHealth += Value;

        HealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
        LocalHealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }

    public void GetFilledBar()
    {
        HealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
        FillBarTwo.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
        LocalHealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }

    public void GetEnemyInfo()
    {
        EnemyInfo.text = "LV: " + character.Level + " " + character.characterName;
        LocalEnemyInfo.text = "LV: " + character.Level + " " + character.characterName;
    }

    public GameObject GetDamageTextHolder
    {
        get
        {
            return DamageTextHolder;
        }
        set
        {
            DamageTextHolder = value;
        }
    }

    public GameObject GetLocalHealth
    {
        get
        {
            return LocalHealth;
        }
        set
        {
            LocalHealth = value;
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
