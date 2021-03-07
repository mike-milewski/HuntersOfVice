using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private CharacterMenu characterMenu = null;

    [SerializeField]
    private Image HealthBar, FillBarTwo;

    [SerializeField]
    private GameObject DamageTextParent;

    [SerializeField]
    private TextMeshProUGUI HealthText = null;

    [SerializeField]
    private GameObject DamageTextHolder, HealTextHolder, HealthAnimationObj;

    private Coroutine routine = null;

    private bool IsImmune, ReflectingDamage, DamageWasReduced;

    //This variable is used to check and uncheck a hit while under the effect of the sleep status.
    [SerializeField]
    private bool SleepHit, UnlockedPassive, HasStatusGiftPassive;

    private bool ReducedDamage;

    [SerializeField]
    private float FillValue;

    public GameObject GetHealthObj
    {
        get
        {
            return HealthAnimationObj;
        }
        set
        {
            HealthAnimationObj = value;
        }
    }

    public bool GetIsImmune
    {
        get
        {
            return IsImmune;
        }
        set
        {
            IsImmune = value;
        }
    }

    public bool GetReducedDamage
    {
        get
        {
            return ReducedDamage;
        }
        set
        {
            ReducedDamage = value;
        }
    }

    public bool GetDamageWasReduced
    {
        get
        {
            return DamageWasReduced;
        }
        set
        {
            DamageWasReduced = value;
        }
    }

    public bool GetReflectingDamage
    {
        get
        {
            return ReflectingDamage;
        }
        set
        {
            ReflectingDamage = value;
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

    public bool GetHasStatusGiftPassive
    {
        get
        {
            return HasStatusGiftPassive;
        }
        set
        {
            HasStatusGiftPassive = value;
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

    private IEnumerator Heal()
    {
        float elapsedTime = 0;
        float time = 2f;

        while (elapsedTime < time)
        {
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, FillBarTwo.fillAmount, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator DealTheDamage()
    {
        float elapsedTime = 0;
        float time = 2f;

        while (elapsedTime < time)
        {
            FillBarTwo.fillAmount = Mathf.Lerp(FillBarTwo.fillAmount, HealthBar.fillAmount, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void IncreaseHealth(int Value)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }

        character.CurrentHealth += Value;

        character.CurrentHealth = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth);

        if(HealthText != null)
        {
            HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth).ToString();
        }
        if(HealthAnimationObj.GetComponent<LowHpAnimation>())
        {
            if(character.CurrentHealth > character.MaxHealth / 4)
            {
                HealthAnimationObj.GetComponent<LowHpAnimation>().GetAnimator.SetBool("LowHealth", false);
            }
        }
        FillBarTwo.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;

        routine = StartCoroutine(Heal());

        if(characterMenu != null)
        {
            characterMenu.SetCharacterInfoText();
        }
    }

    public void ModifyHealth(int Value)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }

        if (ReducedDamage)
        {
            if (Random.value * 100 <= 15)
            {
                DamageWasReduced = true;

                float percentage = Mathf.Abs(0.10f * (float)Value);

                Mathf.Round(percentage);

                if (percentage <= 1)
                {
                    percentage = 1;
                }

                character.CurrentHealth += -Mathf.Abs(Value + (int)percentage);
            }
            else
            {
                DamageWasReduced = false;

                character.CurrentHealth += Value;
            }
        }
        else
        {
            character.CurrentHealth += Value;
        }

        character.CurrentHealth = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth);

        if (character.GetComponent<Enemy>())
        {
            character.GetComponent<Enemy>().GetLocalHealthInfo();
        }

        if (HealthText != null)
        {
            HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth).ToString();
        }
        if (HealthAnimationObj.GetComponent<LowHpAnimation>())
        {
            if (character.CurrentHealth <= character.MaxHealth / 4)
            {
                HealthAnimationObj.GetComponent<LowHpAnimation>().GetAnimator.SetBool("LowHealth", true);
            }
        }

        HealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;

        routine = StartCoroutine(DealTheDamage());

        if (character.CurrentHealth <= 0)
        {
            if (character.GetComponent<PlayerController>())
            {
                if(character.GetCanRegenerate)
                {
                    if(character.GetRegenerationCount < character.GetMaxRegenerationCount)
                    {
                        IncreaseHealth(character.MaxHealth);
                        character.GetRegenerationCount++;
                    }
                    else
                    {
                        GameManager.Instance.Dead();
                    }
                }
                else
                {
                    GameManager.Instance.Dead();
                }
            }
            else
            {
                if (character.GetComponent<EnemyAI>())
                {
                    SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().RemoveEnemyFromList(character.GetComponent<Enemy>());
                    character.GetComponent<EnemyAI>().Dead();
                }
                if (character.GetComponent<Puck>())
                {
                    SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().RemoveEnemyFromList(character.GetComponent<Enemy>());
                    character.GetComponent<Puck>().Dead();
                }
                if (character.GetComponent<RuneGolem>())
                {
                    SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().RemoveEnemyFromList(character.GetComponent<Enemy>());
                    character.GetComponent<RuneGolem>().Dead();
                }
                if(character.GetComponent<SylvanDiety>())
                {
                    SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().RemoveEnemyFromList(character.GetComponent<Enemy>());
                    character.GetComponent<SylvanDiety>().Dead();
                }
                if(character.GetComponent<EarthEffigy>())
                {
                    SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().RemoveEnemyFromList(character.GetComponent<Enemy>());
                    character.GetComponent<EarthEffigy>().Dead();
                }
            }
        }
        SleepHit = true;

        if (characterMenu != null)
        {
            characterMenu.SetCharacterInfoText();
        }
    }

    public int GetReducedDamageValue(int value)
    {
        float percentage = Mathf.Abs(0.10f * (float)value);

        Mathf.Round(percentage);

        if (percentage <= 1)
        {
            percentage = 1;
        }

        int Damage = (int)percentage;

        Damage = Mathf.Abs(value - (int)percentage);

        return Damage;
    }

    public void GetFilledBar()
    {
        HealthText.text = Mathf.Clamp(character.CurrentHealth, 0, character.MaxHealth).ToString();

        HealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;

        FillBarTwo.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }

    public void ResetHealthAnimation()
    {
        HealthAnimationObj.GetComponent<LowHpAnimation>().GetAnimator.SetBool("LowHealth", false);

        HealthAnimationObj.GetComponent<LowHpAnimation>().GetAnimator.Play("HighHealth", -1, 0f);
    }

    public int RestoreHealth()
    {
        if (character.CurrentHealth >= character.MaxHealth)
        {
            return 0;
        }
        else
        {
            float percent = Mathf.Round(0.02f * (float)character.MaxHealth);

            int GetHP = (int)percent;

            Mathf.Round(GetHP);
            if (GetHP < 1)
            {
                GetHP = 1;
            }

            IncreaseHealth(GetHP);

            return GetHP;
        }
    }
}