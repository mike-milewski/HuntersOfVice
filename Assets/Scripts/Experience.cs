using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experience : MonoBehaviour
{
    [SerializeField]
    private Transform Player;

    [SerializeField]
    private Image ExperienceBar, FillBarTwo;

    [SerializeField]
    private Character character;

    [SerializeField]
    private TextMeshProUGUI ExperienceText, CharacterLevelText;

    [SerializeField]
    private GameObject ExperienceTextParent, ExperienceTextHolder;

    [SerializeField]
    private ParticleSystem LevelUpParticle;

    [SerializeField]
    private int ExperiencePoints, NextToLevel, MaxLevel;

    [SerializeField]
    private float FillValue;

    public int GetNextToLevel
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

    private void Reset()
    {
        character = GetComponent<Character>();
    }

    private void Awake()
    {
        character = GetComponent<Character>();

        ExperienceText.gameObject.SetActive(false);

        UpdateExperienceText();

        UpdateCharacterLevel();
    }

    private void LateUpdate()
    {
        ExperienceBar.fillAmount = Mathf.Lerp(ExperienceBar.fillAmount, FillBarTwo.fillAmount, FillValue);
    }

    public void GainEXP(int Value)
    {
        if(character.Level < MaxLevel)
        {
            ExperiencePoints += Value;

            //Creates a level up particle effect when the player levels up. 
            //We put this in a separate conditional so that multiple particles
            //won't spawn.
            if(ExperiencePoints >= NextToLevel)
            {
                var LvParticle = Instantiate(LevelUpParticle, new Vector3(Player.transform.position.x, Player.transform.position.y + 1.0f, Player.transform.position.z), 
                                             Quaternion.identity);

                LvParticle.transform.SetParent(Player.transform, true);
            }
            //Loops through to check if the player's experience is enough to level them up
            //and continues as long as that's true.
            while (ExperiencePoints >= NextToLevel && character.Level < MaxLevel)
            {
                LevelUp();
            }
        }
        FillBarTwo.fillAmount = (float)ExperiencePoints / (float)NextToLevel;

        UpdateExperienceText();
    }

    public void UpdateExperienceText()
    {
        if(character.Level < MaxLevel)
        {
            ExperienceText.text = ExperiencePoints + "/" + NextToLevel;
        }
        else
        {
            ExperienceText.text = "---" + "/" + "---";
        }
    }

    private void UpdateCharacterLevel()
    {
        CharacterLevelText.text = character.Level.ToString();
    }

    private void LevelUp()
    {
        ExperienceBar.fillAmount = 0;

        character.Level++;
        character.MaxHealth += 50;
        character.MaxMana += 5;
        character.CharacterStrength += 3;
        character.CharacterDefense += 2;
        character.CharacterIntelligence += 1;

        UpdateCharacterLevel();

        if(character.Level < MaxLevel)
        {
            //Adds the extra experience left over after leveling up onto the current experience points.
            int SurplusExperience = Mathf.Abs(ExperiencePoints - NextToLevel);

            ExperiencePoints = SurplusExperience;

            NextToLevel += character.Level * 11;
        }

        character.CurrentHealth = character.MaxHealth;
        character.CurrentMana = character.MaxMana;

        character.GetComponent<Health>().GetFilledBar();
        character.GetComponent<Mana>().GetFilledBar();
    }

    public TextMeshProUGUI GetShowExperienceText()
    {
        var ExpText = ObjectPooler.Instance.GetExperienceText();

        ExpText.SetActive(true);

        ExpText.transform.SetParent(ExperienceTextParent.transform, false);

        return ExpText.GetComponentInChildren<TextMeshProUGUI>();
    }
}