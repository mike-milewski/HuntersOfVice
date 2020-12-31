#pragma warning disable 0649
#pragma warning disable 0414
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private EnemyAnimations enemyAnimations = null;

    [SerializeField]
    private PuckAnimations puckAnimations = null;

    [SerializeField]
    private RuneGolemAnimations runeGolemAnimations = null;

    [SerializeField]
    private EnemyAI enemyAI = null;

    [SerializeField]
    private Puck puckAI = null;

    [SerializeField]
    private RuneGolem runeGolemAI = null;

    [SerializeField]
    private Health health;

    [SerializeField]
    private EnemySkills enemySkills = null;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private EnemySkillBar enemySkillBar;

    [SerializeField]
    private Experience EXP;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private GameObject LocalHealth;

    [SerializeField]
    private Transform UI, DebuffTransform, BuffTransform = null;

    [SerializeField]
    private Image LocalHealthBar, TargetedImage;

    [SerializeField]
    private TextMeshProUGUI EnemyInfo, LocalEnemyInfo;

    [SerializeField]
    private int ExperiencePoints, CoinAmount;

    [SerializeField]
    private bool IsInanimateEnemy;

    private bool CheckedForTarget;

    public EnemyAnimations GetEnemyAnimations
    {
        get
        {
            return enemyAnimations;
        }
        set
        {
            enemyAnimations = value;
        }
    }

    public bool GetIsInanimateEnemy
    {
        get
        {
            return IsInanimateEnemy;
        }
        set
        {
            IsInanimateEnemy = value;
        }
    }

    public bool GetCheckedForTarget
    {
        get
        {
            return CheckedForTarget;
        }
        set
        {
            CheckedForTarget = value;
        }
    }

    public int GetExperiencePoints
    {
        get
        {
            return ExperiencePoints;
        }
        set
        {
            ExperiencePoints = value;
        }
    }

    public int GetCoins
    {
        get
        {
            return CoinAmount;
        }
        set
        {
            CoinAmount = value;
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

    public EnemyAI GetAI
    {
        get
        {
            return enemyAI;
        }
        set
        {
            enemyAI = value;
        }
    }

    public Puck GetPuckAI
    {
        get
        {
            return puckAI;
        }
        set
        {
            puckAI = value;
        }
    }

    public RuneGolem GetRuneGolemAI
    {
        get
        {
            return runeGolemAI;
        }
        set
        {
            runeGolemAI = value;
        }
    }

    public EnemySkills GetSkills
    {
        get
        {
            return enemySkills;
        }
        set
        {
            enemySkills = value;
        }
    }

    public EnemySkillBar GetEnemySkillBar
    {
        get
        {
            return enemySkillBar;
        }
        set
        {
            enemySkillBar = value;
        }
    }

    public Health GetHealth
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public Experience GetExperience
    {
        get
        {
            return EXP;
        }
        set
        {
            EXP = value;
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

    public Transform GetUI
    {
        get
        {
            return UI;
        }
        set
        {
            UI = value;
        }
    }

    public Transform GetDebuffTransform
    {
        get
        {
            return DebuffTransform;
        }
        set
        {
            DebuffTransform = value;
        }
    }

    public Transform GetBuffTransform
    {
        get
        {
            return BuffTransform;
        }
        set
        {
            BuffTransform = value;
        }
    }

    private void Awake()
    {
        character = GetComponent<Character>();

        TargetedImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        GetEnemyInfo();
    }

    public void GetFilledBar()
    {
        health.GetHealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
        health.GetFillBarTwo.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }

    public void ToggleHealthBar()
    {
        if (GameManager.Instance.GetEnemyObject == enemy.gameObject)
        {
            if (enemyAnimations != null)
            {
                enemyAnimations.PlayHealthFade();
            }
            if(puckAnimations != null)
            {
                puckAnimations.PlayHealthFade();
            }
            if(runeGolemAnimations != null)
            {
                runeGolemAnimations.PlayHealthFade();
            }
            TargetedImage.gameObject.SetActive(true);
        }
        else
        {
            if (enemyAnimations != null)
            {
                enemyAnimations.ReverseFadeHealth();
            }
            if(puckAnimations != null)
            {
                puckAnimations.ReverseFadeHealth();
            }
            if (runeGolemAnimations != null)
            {
                runeGolemAnimations.ReverseFadeHealth();
            }
            TargetedImage.gameObject.SetActive(false);
        }
    }

    public void TurnOffHealthBar()
    {
        if (enemyAnimations != null)
        {
            enemyAnimations.ReverseFadeHealth();
        }
        if(puckAnimations != null)
        {
            puckAnimations.ReverseFadeHealth();
        }
        TargetedImage.gameObject.SetActive(false);
    }

    public void GetLocalHealthInfo()
    {
        LocalHealthBar.fillAmount = (float)character.CurrentHealth / (float)character.MaxHealth;
    }

    public void GetEnemyInfo()
    {
        EnemyInfo.text = "LV: " + character.GetCharacterData.CharacterLevel + " " + character.characterName;
        LocalEnemyInfo.text = "LV: " + character.GetCharacterData.CharacterLevel + " " + character.characterName;
    }

    public void ReturnCoins()
    {
        if(CoinAmount > 0 && GameManager.Instance.GetCharacter.CurrentHealth > 0)
        {
            if(EXP.GetGetsExtraExp)
            {
                float ExtraCoins = CoinAmount * 0.25f;

                Mathf.Round(ExtraCoins);

                inventory.AddCoins(CoinAmount + (int)ExtraCoins);
                inventory.ReturnCoinText().text = Mathf.Round(CoinAmount + ExtraCoins) + "<size=20>" + " Coins";
            }
            else
            {
                inventory.AddCoins(CoinAmount);
                inventory.ReturnCoinText().text = CoinAmount + "<size=20>" + " Coins";
            }
        }
    }

    public void ReturnExperience()
    {
        if(ExperiencePoints > 0 && GameManager.Instance.GetCharacter.CurrentHealth > 0)
        {
            CheckExperienceHolder();

            EXP.GainEXP(ExperiencePoints);
            if(EXP.GetGetsExtraExp)
            {
                float ExtraExp = ExperiencePoints * 0.25f;

                Mathf.Round(ExtraExp);

                EXP.GetShowExperienceText().text = Mathf.Round(ExperiencePoints + ExtraExp) + "<size=20>" + " EXP";
            }
            else
            {
                EXP.GetShowExperienceText().text = ExperiencePoints + "<size=20>" + " EXP";
            }
        }
    }

    private void CheckExperienceHolder()
    {
        if(GameManager.Instance.GetKnight.activeInHierarchy)
        {
            EXP = GameManager.Instance.GetKnight.GetComponent<Experience>();
        }
        if(GameManager.Instance.GetShadowPriest.activeInHierarchy)
        {
            EXP = GameManager.Instance.GetShadowPriest.GetComponent<Experience>();
        }
        if (GameManager.Instance.GetToadstool.activeInHierarchy)
        {
            EXP = GameManager.Instance.GetToadstool.GetComponent<Experience>();
        }
    }
}