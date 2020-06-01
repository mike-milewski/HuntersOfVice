﻿#pragma warning disable 0649
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
    private ParticleSystem LevelUpParticle;

    [SerializeField]
    private int ExperiencePoints, NextToLevel, MaxLevel, StatPoints, StatPointIncrease;

    private int MaxStatPoints;

    [SerializeField]
    private float FillValue;

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
        else if(ShadowPriest.gameObject.activeInHierarchy)
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
            if(ExperiencePoints >= NextToLevel)
            {
                var LvParticle = Instantiate(LevelUpParticle, new Vector3(Player.transform.position.x, Player.transform.position.y + 1.0f, Player.transform.position.z), 
                                             Quaternion.identity);

                LvParticle.transform.SetParent(Player.transform, true);

                SoundManager.Instance.LevelUp();
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

        routine = StartCoroutine(FillExperienceBar());
    }

    public void UpdateExperienceText()
    {
        if(character.Level < MaxLevel)
        {
            ExperienceText.text = ExperiencePoints + " / " + NextToLevel;
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
        levelUpAnimation.PlayLevelUpAnimation();

        ExperienceBar.fillAmount = 0;

        character.Level++;

        UpdateCharacterLevel();

        Stats.gameObject.SetActive(true);

        StatPointsTxt.gameObject.SetActive(true);

        character.GetCharacterData.HpIncrease += 3;
        character.GetCharacterData.MpIncrease++;

        StatusButton.GetComponent<Animator>().SetBool("StatPoints", true);

        StatConfirmButton.SetActive(true);

        if(character.Level < MaxLevel)
        {
            //Adds the extra experience left over after leveling up onto the current experience points.
            int SurplusExperience = Mathf.Abs(ExperiencePoints - NextToLevel);

            ExperiencePoints = SurplusExperience;

            NextToLevel += character.Level * 11;
        }

        foreach(SkillMenu s in skillMenu.GetComponentsInChildren<SkillMenu>())
        {
            s.CheckIfUnlockedNewSkill();
        }

        character.GetComponent<Health>().IncreaseHealth(character.MaxHealth);
        character.GetComponent<Mana>().IncreaseMana(character.MaxMana);

        StatPoints += StatPointIncrease;

        MaxStatPoints += StatPoints;

        StatPointsTxt.text = StatPoints.ToString();
    }

    public TextMeshProUGUI GetShowExperienceText()
    {
        var ExpText = ObjectPooler.Instance.GetExperienceText();

        ExpText.SetActive(true);

        ExpText.transform.SetParent(ExperienceTextParent.transform, false);

        return ExpText.GetComponentInChildren<TextMeshProUGUI>();
    }
}