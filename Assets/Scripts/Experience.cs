using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    [SerializeField]
    private Transform Player;

    [SerializeField]
    private Image ExperienceBar, FillBarTwo;

    [SerializeField]
    private Character character;

    [SerializeField]
    private Text ExperienceText, CharacterLevelText, ShowExperienceText;

    [SerializeField]
    private GameObject ExperienceTextParent;

    [SerializeField]
    private ParticleSystem LevelUpParticle;

    [SerializeField]
    private float FillValue;

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
        if(character.Level <= 99)
        {
            character.Experience += Value;

            //Creates a level up particle effect when the player levels up. 
            //We put this in a separate conditional so that multiple particles
            //won't spawn.
            if(character.Experience >= character.NextTo && character.Level <= 99)
            {
                var LvParticle = Instantiate(LevelUpParticle, new Vector3(Player.transform.position.x, Player.transform.position.y + 1.0f, Player.transform.position.z), 
                                             Quaternion.identity);

                LvParticle.transform.SetParent(Player.transform, true);
            }
            //Loops through to check if the player's experience is enough to level them up
            //and continues as long as that's true.
            while (character.Experience >= character.NextTo && character.Level <= 99)
            {
                LevelUp();
            }
        }
        FillBarTwo.fillAmount = (float)character.Experience / (float)character.NextTo;

        UpdateExperienceText();
    }

    public void UpdateExperienceText()
    {
        float Percent = ((float)character.Experience / (float)character.NextTo) * 100;

        ExperienceText.text = character.Experience + "/" + character.NextTo + " (" + Percent.ToString("F0") + "%)";
    }

    private void UpdateCharacterLevel()
    {
        CharacterLevelText.text = character.Level.ToString();
    }

    private void LevelUp()
    {
        character.Level++;
        //character.MaxHealth += 50;
        //character.MaxMana += 5;
        character.CharacterStrength += 3;
        character.CharacterDefense += 2;
        character.CharacterIntelligence += 1;

        UpdateCharacterLevel();

        //Adds the extra experience left over after leveling up onto the current experience points.
        int SurplusExperience = Mathf.Abs(character.Experience - character.NextTo);

        character.Experience = SurplusExperience;

        character.NextTo += character.Level * 11;

        character.CurrentHealth = character.MaxHealth;
        character.CurrentMana = character.MaxMana;

        this.gameObject.GetComponent<Health>().GetFilledBar();
    }

    public Text GetShowExperienceText()
    {
        var ExpText = Instantiate(ShowExperienceText);

        ExpText.transform.SetParent(ExperienceTextParent.transform, false);

        return ExpText;
    }
}