using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Image HealthBar, FillBarTwo;

    [SerializeField]
    private GameObject DamageTextParent;

    [SerializeField]
    private Text HealthText = null; 
    
    [SerializeField]
    private GameObject DamageTextHolder, HealTextHolder;

    [SerializeField]
    private bool TakingDamage;

    [SerializeField]
    private float FillValue;

    public bool GetTakingDamage
    {
        get
        {
            return TakingDamage;
        }
        set
        {
            TakingDamage = value;
        }
    }

    public Image GetHealthBar
    {
        get
        {
            return HealthBar;
        }
        set
        {
            HealthBar = value;
        }
    }

    public Image GetFillBarTwo
    {
        get
        {
            return FillBarTwo;
        }
        set
        {
            FillBarTwo = value;
        }
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

    public GameObject GetDamageText
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

    public GameObject GetHealText
    {
        get
        {
            return HealTextHolder;
        }
        set
        {
            HealTextHolder = value;
        }
    }

    private void Reset()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        character.GetComponent<Character>();

        if (HealthText != null)
        HealthText.text = character.CurrentHealth + "/" + character.MaxHealth;
    }

    private void FixedUpdate()
    {
        if(TakingDamage)
        {
            FillBarTwo.fillAmount = Mathf.Lerp(FillBarTwo.fillAmount, HealthBar.fillAmount, FillValue);
        }
        else
        {
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, FillBarTwo.fillAmount, FillValue);
        }
    }

    public void IncreaseHealth(int Value)
    {
        character.CurrentHealth += Value;

        character.CurrentHealth = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth);

        if(HealthText != null)
        {
            HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth) + "/" + character.MaxHealth;
        }
        if(character.GetComponentInChildren<LowEnemyHPAnimation>())
        {
            if(character.CurrentHealth > character.MaxHealth / 4)
            {
                character.GetComponentInChildren<LowEnemyHPAnimation>().ResetAnimator();
                character.GetComponentInChildren<LowEnemyHPAnimation>().DisableAnimator();
            }
        }
        FillBarTwo.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }

    public void ModifyHealth(int Value)
    {
        character.CurrentHealth += Value;

        character.CurrentHealth = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth);

        if(character.GetComponent<Enemy>())
        {
            character.GetComponent<Enemy>().GetLocalHealthInfo();
        }

        if(HealthText != null)
        {
            HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth) + "/" + character.MaxHealth;
        }
        if (character.GetComponentInChildren<LowEnemyHPAnimation>())
        {
            if (character.CurrentHealth <= character.MaxHealth / 4)
            {
                character.GetComponentInChildren<LowEnemyHPAnimation>().EnableAnimator();
            }
        }

        HealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;

        if(character.CurrentHealth <= 0)
        {
            if(character.GetComponent<PlayerController>())
            {
                GameManager.Instance.Dead();
            }
            else
            {
                character.GetComponent<EnemyAI>().Dead();
            }
        }
    }

    public void GetFilledBar()
    {
        HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth) + "/" + character.MaxHealth;

        HealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }
}
