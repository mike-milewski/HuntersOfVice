﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private TextMeshProUGUI InvalidText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject EnemyObject = null;

    [SerializeField]
    private GameObject LastEnemyObject = null;

    [SerializeField]
    private Transform SpawnPoint;

    [SerializeField]
    private GameObject CharacterPanel, SkillsPanel, InventoryPanel, SettingsPanel;

    private bool IsDead;

    [SerializeField]
    private EventSystem eventsystem;

    public Camera GetCamera
    {
        get
        {
            return camera;
        }
        set
        {
            camera = value;
        }
    }

    public EventSystem GetEventSystem
    {
        get
        {
            return eventsystem;
        }
        set
        {
            eventsystem = value;
        }
    }

    public GameObject GetSkillPanel
    {
        get
        {
            return SkillsPanel;
        }
        set
        {
            SkillsPanel = value;
        }
    }

    public GameObject GetEnemyObject
    {
        get
        {
            return EnemyObject;
        }
        set
        {
            EnemyObject = value;
        }
    }

    public GameObject GetLastEnemyObject
    {
        get
        {
            return LastEnemyObject;
        }
        set
        {
            LastEnemyObject = value;
        }
    }

    [SerializeField]
    private float RespawnTime;

    public bool GetIsDead
    {
        get
        {
            return IsDead;
        }
        set
        {
            IsDead = value;
        }
    }

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        InvalidText.gameObject.SetActive(false);

        eventsystem.GetComponent<EventSystem>();

        MaskSkillsPanel();

        camera.gameObject.SetActive(false);

        settings.UseParticleEffects = true;
    }

    private void Start()
    {
        camera.gameObject.SetActive(true);
    }

    private void Update()
    {
        #region UIPanels
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(!InventoryPanel.activeInHierarchy)
            {
                ToggleIventoryPanel();
            }
            else
            {
                InventoryPanel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!CharacterPanel.activeInHierarchy)
            {
                ToggleCharacterPanel();
            }
            else
            {
                CharacterPanel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!SkillsPanel.GetComponent<Image>().enabled)
            {
                ToggleSkillsPanel();
            }
            else
            {
                MaskSkillsPanel();
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!SettingsPanel.activeInHierarchy)
            {
                ToggleSettingsPanel();
            }
            else
            {
                SettingsPanel.SetActive(false);
            }
        }
        #endregion
    }

    private void ToggleCharacterPanel()
    {
        CharacterPanel.SetActive(true);
        MaskSkillsPanel();
        InventoryPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    private void ToggleSkillsPanel()
    {
        UnmaskSkillsPanel();
        CharacterPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    private void ToggleIventoryPanel()
    {
        InventoryPanel.SetActive(true);
        CharacterPanel.SetActive(false);
        MaskSkillsPanel();
        SettingsPanel.SetActive(false);
    }

    private void ToggleSettingsPanel()
    {
        SettingsPanel.SetActive(true);
        CharacterPanel.SetActive(false);
        MaskSkillsPanel();
        InventoryPanel.SetActive(false);
    }

    public void MaskSkillsPanel()
    {
        SkillsPanel.GetComponent<Image>().enabled = false;
        foreach(Mask m in SkillsPanel.GetComponentsInChildren<Mask>())
        {
            m.showMaskGraphic = false;
        }
        foreach(Image i in SkillsPanel.GetComponentsInChildren<Image>())
        {
            i.raycastTarget = false;
        }
    }

    public void UnmaskSkillsPanel()
    {
        SkillsPanel.GetComponent<Image>().enabled = true;
        foreach (Mask m in SkillsPanel.GetComponentsInChildren<Mask>())
        {
            m.showMaskGraphic = true;
        }
        foreach (Image i in SkillsPanel.GetComponentsInChildren<Image>())
        {
            i.raycastTarget = true;
        }
    }

    public void Dead()
    {
        IsDead = true;
        SkillsManager.Instance.DeactivateSkillButtons();

        SkillsManager.Instance.GetActivatedSkill = false;

        Player.GetComponent<BasicAttack>().GetAutoAttackTime = 0;
        Player.GetComponent<BasicAttack>().enabled = false;

        Player.GetComponent<CapsuleCollider>().enabled = false;

        Player.GetComponent<PlayerController>().enabled = false;

        Player.GetComponent<PlayerAnimations>().DeathAnimation();

        Player.GetComponent<Character>().GetRigidbody.useGravity = false;
        Player.GetComponent<Character>().GetRigidbody.isKinematic = true;

        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(2);
        FadeScreen.Instance.GetFadeState = FadeState.FADEOUT;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnTime);
        FadeScreen.Instance.GetFadeState = FadeState.FADEIN;

        Player.transform.position = SpawnPoint.position;

        Player.GetComponent<Health>().IncreaseHealth(Player.GetComponent<Character>().MaxHealth);
        Player.GetComponent<Mana>().IncreaseMana(Player.GetComponent<Character>().MaxMana);

        Player.GetComponent<BasicAttack>().enabled = true;
        
        if(Player.GetComponent<BasicAttack>().GetTarget != null)
        {
            Player.GetComponent<BasicAttack>().GetTarget.TurnOffHealthBar();
            Player.GetComponent<BasicAttack>().GetTarget = null;
            EnemyObject = null;
            LastEnemyObject = null;
        }

        Player.GetComponent<CapsuleCollider>().enabled = true;

        Player.GetComponent<Character>().GetRigidbody.useGravity = true;
        Player.GetComponent<Character>().GetRigidbody.isKinematic = false;

        Player.GetComponent<PlayerAnimations>().PlayResurrectAnimation();
        Player.GetComponent<PlayerAnimations>().GetAnimator.ResetTrigger("Damaged");
    }

    public TextMeshProUGUI ShowNotEnoughManaText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Not enough Mana";

        return InvalidText;
    }

    public TextMeshProUGUI ShowTargetOutOfRangeText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Target out of range";

        return InvalidText;
    }

    public TextMeshProUGUI CannotExecuteText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Cannot execute";

        return InvalidText;
    }

    public TextMeshProUGUI SkillStillRechargingText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Skill still recharging";

        return InvalidText;
    }

    public TextMeshProUGUI InvalidTargetText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Invalid Target";

        return InvalidText;
    }
}