#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Experience : MonoBehaviour
{
    [SerializeField]
    private LevelUpAnimation levelUpAnimation;

    [SerializeField]
    private GameObject skillMenu;

    [SerializeField]
    private Transform Player, Knight, ShadowPriest;

    [SerializeField]
    private Image ExperienceBar, FillBarTwo;

    [SerializeField]
    private Character character;

    private Coroutine routine = null;

    [SerializeField]
    private TextMeshProUGUI ExperienceText, CharacterLevelText, StatPointsTxt;

    [SerializeField]
    private GameObject ExperienceTextParent, ExperienceTextHolder, StatusButton, StatConfirmButton, Stats;

    [SerializeField]
    private Button RespecButton;

    [SerializeField]
    private ParticleSystem LevelUpParticle;

    [SerializeField]
    private int ExperiencePoints, MaxLevel, StatPoints, StatPointIncrease;

    [SerializeField]
    private int[] NextToLevel;

    private int MaxStatPoints, CumulativeStatPoints, ToNextLevelIndex;

    [SerializeField]
    private float FillValue;

    public Button GetRespecButton
    {
        get
        {
            return RespecButton;
        }
        set
        {
            RespecButton = value;
        }
    }

    public int GetStatPoints
    {
        get
        {
            return StatPoints;
        }
        set
        {
            StatPoints = value;
        }
    }

    public int GetStatPointIncrease
    {
        get
        {
            return StatPointIncrease;
        }
        set
        {
            StatPointIncrease = value;
        }
    }

    public int GetMaxStatPoints
    {
        get
        {
            return MaxStatPoints;
        }
        set
        {
            MaxStatPoints = value;
        }
    }

    public int GetCumulativeStatPoints
    {
        get
        {
            return CumulativeStatPoints;
        }
        set
        {
            CumulativeStatPoints = value;
        }
    }

    public int GetNextToLevelIndex
    {
        get
        {
            return ToNextLevelIndex;
        }
        set
        {
            ToNextLevelIndex = value;
        }
    }

    public int[] GetNextToLevel
    {
        get
        {
            return NextToLevel;
        }
        set
        {
            NextToLevel = value;
        }
    }

    public TextMeshProUGUI GetStatPointsTxt
    {
        get
        {
            return StatPointsTxt;
        }
        set
        {
            StatPointsTxt = value;
        }
    }

    private void OnEnable()
    {
        if(Knight.gameObject.activeInHierarchy)
        {
            Player = Knight;
        }
        if(ShadowPriest.gameObject.activeInHierarchy)
        {
            Player = ShadowPriest;
        }

        character = GetComponent<Character>();

        UpdateExperienceText();

        UpdateCharacterLevel();
    }

    private void Reset()
    {
        character = GetComponent<Character>();
    }

    private void Awake()
    {
        ExperienceText.gameObject.SetActive(false);
    }

    public IEnumerator FillExperienceBar()
    {
        float elapsedTime = 0;
        float time = 2f;

        while(elapsedTime < time)
        {
            ExperienceBar.fillAmount = Mathf.Lerp(ExperienceBar.fillAmount, FillBarTwo.fillAmount, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void GainEXP(int Value)
    {
        if(routine != null)
        {
            StopCoroutine(routine);
        }

        if(character.Level < MaxLevel)
        {
            ExperiencePoints += Value;

            //Creates a level up particle effect when the player levels up. 
            //We put this in a separate conditional so that multiple particles don't spawn.
            if(ExperiencePoints >= NextToLevel[ToNextLevelIndex])
            {
                var LvParticle = Instantiate(LevelUpParticle, new Vector3(Player.transform.position.x, Player.transform.position.y + 1.0f, Player.transform.position.z), 
                                             Quaternion.identity);

                LvParticle.transform.SetParent(Player.transform, true);

                SoundManager.Instance.LevelUp();
            }

            //Loops through to check if the player's experience is enough to level them up
            //and continues as long as that's true.
            while (ExperiencePoints >= NextToLevel[ToNextLevelIndex] && character.Level < MaxLevel)
            {
                LevelUp();
            }
        }
        FillBarTwo.fillAmount = (float)ExperiencePoints / (float)NextToLevel[ToNextLevelIndex];

        UpdateExperienceText();

        routine = StartCoroutine(FillExperienceBar());
    }

    public void UpdateExperienceText()
    {
        if(character.Level < MaxLevel)
        {
            ExperienceText.text = ExperiencePoints + " / " + NextToLevel[ToNextLevelIndex];
        }
        else
        {
            ExperienceText.text = "---" + "/" + "---";
        }
    }

    public void UpdateCharacterLevel()
    {
        CharacterLevelText.text = character.Level.ToString();
    }

    private void LevelUp()
    {
        levelUpAnimation.PlayLevelUpAnimation();

        ExperienceBar.fillAmount = 0;

        character.Level++;

        UpdateCharacterLevel();

        //RespecButton.interactable = false;

        Stats.gameObject.SetActive(true);

        StatPointsTxt.gameObject.SetActive(true);

        character.GetCharacterData.HpIncrease++;
        character.GetCharacterData.MpIncrease++;

        StatusButton.GetComponent<Animator>().SetBool("StatPoints", true);

        StatConfirmButton.SetActive(true);

        if(character.Level < MaxLevel)
        {
            //Adds the extra experience left over after leveling up onto the current experience points.
            int SurplusExperience = Mathf.Abs(ExperiencePoints - NextToLevel[ToNextLevelIndex]);

            ToNextLevelIndex++;

            ExperiencePoints = SurplusExperience;
        }

        foreach(SkillMenu s in skillMenu.GetComponentsInChildren<SkillMenu>())
        {
            s.CheckIfUnlockedNewSkill();
        }

        character.GetComponent<Health>().IncreaseHealth(character.MaxHealth);
        character.GetComponent<Mana>().IncreaseMana(character.MaxMana);

        StatPoints += StatPointIncrease;

        MaxStatPoints += StatPoints;

        CumulativeStatPoints = StatPoints;

        StatPointsTxt.text = StatPoints.ToString();
    }

    public void SetStatPointsText()
    {
        if(!Stats.gameObject.activeInHierarchy && CumulativeStatPoints > 0)
        {
            StatPoints = CumulativeStatPoints;

            MaxStatPoints = CumulativeStatPoints;

            StatPointsTxt.text = StatPoints.ToString();

            Stats.gameObject.SetActive(true);

            StatusButton.GetComponent<Animator>().SetBool("StatPoints", true);

            StatConfirmButton.SetActive(true);

            character.DefaultStats();

            character.CharacterStrength = character.GetCharacterData.Strength;
            character.CharacterDefense = character.GetCharacterData.Defense;
            character.CharacterIntelligence = character.GetCharacterData.Intelligence;

            GetEquipmentStatIncrease();

            if (character.CurrentHealth > character.MaxHealth)
            {
                character.CurrentHealth = character.MaxHealth;
            }
            if(character.CurrentMana > character.MaxMana)
            {
                character.CurrentMana = character.MaxMana;
            }

            character.GetComponent<Health>().GetFilledBar();
            character.GetComponent<Mana>().GetFilledBar();

            SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
        }
    }

    private void GetEquipmentStatIncrease()
    {
        if(character.GetComponent<BasicAttack>().GetEquipment[0] != null)
        {
            character.GetComponent<BasicAttack>().GetEquipment[0].GetEquipmentStats();
        }
        if(character.GetComponent<BasicAttack>().GetEquipment[1] != null)
        {
            character.GetComponent<BasicAttack>().GetEquipment[1].GetEquipmentStats();
        }
    }

    public TextMeshProUGUI GetShowExperienceText()
    {
        var ExpText = ObjectPooler.Instance.GetExperienceText();

        ExpText.SetActive(true);

        ExpText.transform.SetParent(ExperienceTextParent.transform, false);

        return ExpText.GetComponentInChildren<TextMeshProUGUI>();
    }
}