using System.Collections;
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
    private MonsterBook monsterbook;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private TextMeshProUGUI InvalidText;

    [SerializeField]
    private Animator animator, SkillPanelAnimator, CharacterPanelAnimator, EquipmentPanelAnimator, InventoryPanelAnimator, SettingsPanelAnimator, MonsterBookAnimator;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject EnemyObject = null;

    [SerializeField]
    private GameObject LastEnemyObject = null;

    [SerializeField]
    private Transform SpawnPoint;

    [SerializeField]
    private GameObject CharacterPanel, SkillsPanel, EquipmentPanel, InventoryPanel, SettingsPanel;

    private bool IsDead, SkillsToggle, CharacterToggle, EquipmentToggle, InventoryToggle, SettingsToggle, MonsterToggle, MenuAnimating;

    [SerializeField]
    private EventSystem eventsystem;

    public Camera GetCamera
    {
        get
        {
            return cam;
        }
        set
        {
            cam = value;
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

    public bool GetEquipmentToggle
    {
        get
        {
            return EquipmentToggle;
        }
        set
        {
            EquipmentToggle = value;
        }
    }

    public bool GetMonsterToggle
    {
        get
        {
            return MonsterToggle;
        }
        set
        {
            MonsterToggle = value;
        }
    }

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

    public bool GetMenuAnimating
    {
        get
        {
            return MenuAnimating;
        }
        set
        {
            MenuAnimating = value;
        }
    }

    [SerializeField]
    private float RespawnTime;

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

        cam.gameObject.SetActive(false);

        settings.UseParticleEffects = true;
    }

    private void Start()
    {
        cam.gameObject.SetActive(true);
    }

    public void SetMenuAnimatingToTrue()
    {
        MenuAnimating = true;
    }

    public void SetMenuAnimatingToFalse()
    {
        MenuAnimating = false;
    }

    private void Update()
    {
        #region UIPanels
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(!MenuAnimating)
            {
                MenuAnimating = true;
                if (!InventoryToggle)
                {
                    ToggleInventoryPanel();
                }
                else
                {
                    InventoryToggle = false;
                    InventoryPanelAnimator.SetBool("FadeIn", false);
                }
                StartCoroutine(SetMenuAnimationToFalse());
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!MenuAnimating)
            {
                MenuAnimating = true;
                if (!EquipmentToggle)
                {
                    ToggleEquipmentPanel();
                }
                else
                {
                    EquipmentToggle = false;
                    EquipmentPanelAnimator.SetBool("FadeIn", false);
                }
                StartCoroutine(SetMenuAnimationToFalse());
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(!MenuAnimating)
            {
                MenuAnimating = true;
                if (!CharacterToggle)
                {
                    ToggleCharacterPanel();
                }
                else
                {
                    CharacterToggle = false;
                    CharacterPanelAnimator.SetBool("FadeIn", false);
                }
                StartCoroutine(SetMenuAnimationToFalse());
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(!MenuAnimating)
            {
                MenuAnimating = true;
                if (!SkillsToggle)
                {
                    ToggleSkillsPanel();
                }
                else
                {
                    MaskSkillsPanel();
                }
                StartCoroutine(SetMenuAnimationToFalse());
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(!MenuAnimating)
            {
                MenuAnimating = true;
                if (!SettingsToggle)
                {
                    ToggleSettingsPanel();
                }
                else
                {
                    SettingsToggle = false;
                    SettingsPanelAnimator.SetBool("FadeIn", false);
                }
                StartCoroutine(SetMenuAnimationToFalse());
            }
        }
        #endregion
    }

    private IEnumerator SetMenuAnimationToFalse()
    {
        float SetTime = 0.6f;
        yield return new WaitForSeconds(SetTime);
        MenuAnimating = false;
    }

    public void ButtonEquipmentPanel()
    {
        if(!MenuAnimating)
        {
            MenuAnimating = true;
            if (!EquipmentToggle)
            {
                ToggleEquipmentPanel();
            }
            else
            {
                EquipmentToggle = false;
                EquipmentPanelAnimator.SetBool("FadeIn", false);
            }
            StartCoroutine(SetMenuAnimationToFalse());
        }
    }

    public void ButtonCharacterPanel()
    {
        if(!MenuAnimating)
        {
            MenuAnimating = true;
            if (!CharacterToggle)
            {
                ToggleCharacterPanel();
            }
            else
            {
                CharacterToggle = false;
                CharacterPanelAnimator.SetBool("FadeIn", false);
            }
            StartCoroutine(SetMenuAnimationToFalse());
        }
    }

    public void ButtonSkillsPanel()
    {
        if(!MenuAnimating)
        {
            MenuAnimating = true;
            if (!SkillsToggle)
            {
                ToggleSkillsPanel();
            }
            else
            {
                MaskSkillsPanel();
            }
            StartCoroutine(SetMenuAnimationToFalse());
        }
    }

    public void ButtonSettingsPanel()
    {
        if(!MenuAnimating)
        {
            MenuAnimating = true;
            if (!SettingsToggle)
            {
                ToggleSettingsPanel();
            }
            else
            {
                SettingsToggle = false;
                SettingsPanelAnimator.SetBool("FadeIn", false);
            }
            StartCoroutine(SetMenuAnimationToFalse());
        }
    }

    public void ButtonInventoryPanel()
    {
        if(!MenuAnimating)
        {
            MenuAnimating = true;
            if (!InventoryToggle)
            {
                ToggleInventoryPanel();
            }
            else
            {
                InventoryToggle = false;
                MonsterToggle = false;

                MonsterBookAnimator.SetBool("FadeIn", false);
                InventoryPanelAnimator.SetBool("FadeIn", false);
            }
            StartCoroutine(SetMenuAnimationToFalse());
        }
    }

    private void ToggleCharacterPanel()
    {
        CharacterToggle = true;
        EquipmentToggle = false;
        InventoryToggle = false;
        SettingsToggle = false;
        MonsterToggle = false;

        CharacterPanelAnimator.SetBool("FadeIn", true);
        MaskSkillsPanel();
        EquipmentPanelAnimator.SetBool("FadeIn", false);
        InventoryPanelAnimator.SetBool("FadeIn", false);
        SettingsPanelAnimator.SetBool("FadeIn", false);
        MonsterBookAnimator.SetBool("FadeIn", false);
    }

    private void ToggleSkillsPanel()
    {
        CharacterToggle = false;
        EquipmentToggle = false;
        InventoryToggle = false;
        SettingsToggle = false;
        MonsterToggle = false;

        UnmaskSkillsPanel();
        CharacterPanelAnimator.SetBool("FadeIn", false);
        EquipmentPanelAnimator.SetBool("FadeIn", false);
        InventoryPanelAnimator.SetBool("FadeIn", false);
        SettingsPanelAnimator.SetBool("FadeIn", false);
        MonsterBookAnimator.SetBool("FadeIn", false);
    }

    private void ToggleEquipmentPanel()
    {
        CharacterToggle = false;
        EquipmentToggle = true;
        InventoryToggle = false;
        SettingsToggle = false;
        MonsterToggle = false;

        EquipmentPanelAnimator.SetBool("FadeIn", true);
        CharacterPanelAnimator.SetBool("FadeIn", false);
        InventoryPanelAnimator.SetBool("FadeIn", false);
        MaskSkillsPanel();
        SettingsPanelAnimator.SetBool("FadeIn", false);
        MonsterBookAnimator.SetBool("FadeIn", false);
    }

    private void ToggleInventoryPanel()
    {
        CharacterToggle = false;
        EquipmentToggle = false;
        InventoryToggle = true;
        SettingsToggle = false;
        MonsterToggle = false;

        EquipmentPanelAnimator.SetBool("FadeIn", false);
        InventoryPanelAnimator.SetBool("FadeIn", true);
        CharacterPanelAnimator.SetBool("FadeIn", false);
        MaskSkillsPanel();
        SettingsPanelAnimator.SetBool("FadeIn", false);
        MonsterBookAnimator.SetBool("FadeIn", false);
    }

    private void ToggleSettingsPanel()
    {
        CharacterToggle = false;
        EquipmentToggle = false;
        InventoryToggle = false;
        SettingsToggle = true;
        MonsterToggle = false;

        CharacterPanelAnimator.SetBool("FadeIn", false);
        InventoryPanelAnimator.SetBool("FadeIn", false);
        SettingsPanelAnimator.SetBool("FadeIn", true);
        MaskSkillsPanel();
        EquipmentPanelAnimator.SetBool("FadeIn", false);
        MonsterBookAnimator.SetBool("FadeIn", false);
    }

    public void MaskSkillsPanel()
    {
        SkillsToggle = false;
        SkillPanelAnimator.SetBool("FadeIn", false);
        foreach(Mask m in SkillsPanel.GetComponentsInChildren<Mask>())
        {
            m.showMaskGraphic = false;
        }
        foreach(Image i in SkillsPanel.GetComponentsInChildren<Image>())
        {
            i.raycastTarget = false;
        }
    }

    public void OpenMonsterBook()
    {
        monsterbook.GetMonsterInfoTxt.text = "";

        MonsterBookAnimator.SetBool("FadeIn", true);
        MonsterToggle = true;
    }

    public void CloseMonsterBook()
    {
        MonsterBookAnimator.SetBool("FadeIn", false);
        MonsterToggle = false;
    }

    public void UnmaskSkillsPanel()
    {
        SkillsToggle = true;
        SkillPanelAnimator.SetBool("FadeIn", true);
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

        InvalidText.text = "Cannot Execute At This Time";

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