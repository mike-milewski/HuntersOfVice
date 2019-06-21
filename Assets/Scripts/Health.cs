using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Image HealthBar, FillBarTwo;

    [SerializeField]
    private GameObject DamageTextParent;

    [SerializeField]
    private TextMeshProUGUI HealthText = null;

    [SerializeField]
    private GameObject DamageTextHolder, HealTextHolder, HealthAnimationObj;

    [SerializeField]
    private bool TakingDamage;

    //This variable is used to check and uncheck a hit while under the effects of the sleep status.
    [SerializeField]
    private bool SleepHit;

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

    public bool GetSleepHit
    {
        get
        {
            return SleepHit;
        }
        set
        {
            SleepHit = value;
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
            HealthText.text = character.CurrentHealth.ToString();
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
        TakingDamage = false;

        character.CurrentHealth += Value;

        character.CurrentHealth = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth);

        if(HealthText != null)
        {
            HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth).ToString();
        }
        if(HealthAnimationObj.GetComponent<LowEnemyHPAnimation>())
        {
            if(character.CurrentHealth > character.MaxHealth / 4)
            {
                HealthAnimationObj.GetComponent<LowEnemyHPAnimation>().GetAnimator.SetBool("LowHealth", false);
            }
        }
        FillBarTwo.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }

    public void ModifyHealth(int Value)
    {
        TakingDamage = true;

        character.CurrentHealth += Value;

        character.CurrentHealth = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth);

        if(character.GetComponent<Enemy>())
        {
            character.GetComponent<Enemy>().GetLocalHealthInfo();
        }

        if(HealthText != null)
        {
            HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth).ToString();
        }
        if (HealthAnimationObj.GetComponent<LowEnemyHPAnimation>())
        {
            if (character.CurrentHealth <= character.MaxHealth / 4)
            {
                HealthAnimationObj.GetComponent<LowEnemyHPAnimation>().GetAnimator.SetBool("LowHealth", true);
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
        SleepHit = true;
    }

    public void GetFilledBar()
    {
        HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth).ToString();

        HealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }
}
