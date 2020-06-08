#pragma warning disable 0649
#pragma warning disable 0414
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
    private Shop shop;

    [SerializeField]
    private ShopUpgrade shopUpgrade;

    [SerializeField]
    private EquipmentMenu equipmentMenu;

    [SerializeField]
    private LowHpAnimation lowHpAnimation;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private TextMeshProUGUI InvalidText;

    [SerializeField]
    private Animator animator, SkillPanelAnimator, CharacterPanelAnimator, EquipmentPanelAnimator, InventoryPanelAnimator, SettingsPanelAnimator, MonsterBookAnimator;

    [SerializeField]
    private GameObject Knight, ShadowPriest;

    [SerializeField]
    private GameObject EnemyObject = null;

    [SerializeField]
    private GameObject LastEnemyObject = null;

    [SerializeField]
    private Transform SpawnPoint, InventoryMaterialTransform, ItemMessageTransform, MonsterEntryTransform, StatusEffectTextHolder;

    [SerializeField]
    private GameObject CharacterPanel, SkillsPanel, EquipmentPanel, InventoryPanel, SettingsPanel, ShopUpgradePanel, ItemDescriptionPanel;

    [SerializeField]
    private GameObject[] KnightSkills, ShadowPriestSkills;

    [SerializeField]
    private float RespawnTime;

    private bool IsDead, SkillsToggle, CharacterToggle, EquipmentToggle, InventoryToggle, SettingsToggle, MonsterToggle, TipToggle, MenuAnimating;

    public bool IsInInventory;

    [SerializeField]
    private EventSystem eventsystem;

    public Shop GetShop
    {
        get
        {
            return shop;
        }
        set
        {
            shop = value;
        }
    }

    public ShopUpgrade GetShopupgrade
    {
        get
        {
            return shopUpgrade;
        }
        set
        {
            shopUpgrade = value;
        }
    }

    public EquipmentMenu GetEquipmentMenu
    {
        get
        {
            return equipmentMenu;
        }
        set
        {
            equipmentMenu = value;
        }
    }

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

    public Transform GetInventoryMaterialTransform
    {
        get
        {
            return InventoryMaterialTransform;
        }
        set
        {
            InventoryMaterialTransform = value;
        }
    }

    public Transform GetItemMessageTransform
    {
        get
        {
            return ItemMessageTransform;
        }
        set
        {
            ItemMessageTransform = value;
        }
    }

    public Transform GetStatusEffectTransform
    {
        get
        {
            return StatusEffectTextHolder;
        }
        set
        {
            StatusEffectTextHolder = value;
        }
    }

    public Transform GetMonsterEntryTransform
    {
        get
        {
            return MonsterEntryTransform;
        }
        set
        {
            MonsterEntryTransform = value;
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

    public GameObject GetInventoryPanel
    {
        get
        {
            return InventoryPanel;
        }
        set
        {
            InventoryPanel = value;
        }
    }

    public GameObject GetShopUpgradePanel
    {
        get
        {
            return ShopUpgradePanel;
        }
        set
        {
            ShopUpgradePanel = value;
        }
    }

    public GameObject GetItemDescriptionPanel
    {
        get
        {
            return ItemDescriptionPanel;
        }
        set
        {
            ItemDescriptionPanel = value;
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

    public bool GetIsInInventory
    {
        get
        {
            return IsInInventory;
        }
        set
        {
            IsInInventory = value;
        }
    }

    private void OnEnable()
    {
        if(Knight.activeInHierarchy)
        {
            for(int i = 0; i < KnightSkills.Length; i++)
            {
                KnightSkills[i].SetActive(true);
                lowHpAnimation.GetCharacter = Knight.GetComponent<Character>();
            }
        }
        else if(ShadowPriest.activeInHierarchy)
        {
            for (int i = 0; i < ShadowPriestSkills.Length; i++)
            {
                ShadowPriestSkills[i].SetActive(true);
                lowHpAnimation.GetCharacter = ShadowPriest.GetComponent<Character>();
            }
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
                    SoundManager.Instance.Menu();
                    ToggleInventoryPanel();
                }
                else
                {
                    SoundManager.Instance.ReverseMenu();
                    IsInInventory = false;
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
                    SoundManager.Instance.Menu();
                    ToggleEquipmentPanel();
                }
                else
                {
                    SoundManager.Instance.ReverseMenu();
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
                    SoundManager.Instance.Menu();
                    ToggleCharacterPanel();
                }
                else
                {
                    SoundManager.Instance.ReverseMenu();
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
                    SoundManager.Instance.Menu();
                    ToggleSkillsPanel();
                }
                else
                {
                    SoundManager.Instance.ReverseMenu();
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
                    SoundManager.Instance.Menu();
                    ToggleSettingsPanel();
                }
                else
                {
                    SoundManager.Instance.ReverseMenu();
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
        float SetTime = 0.57f;
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
                SoundManager.Instance.Menu();
                ToggleEquipmentPanel();
            }
            else
            {
                SoundManager.Instance.ReverseMenu();
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
                SoundManager.Instance.Menu();
                ToggleCharacterPanel();
            }
            else
            {
                SoundManager.Instance.ReverseMenu();
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
                SoundManager.Instance.Menu();
                ToggleSkillsPanel();
            }
            else
            {
                SoundManager.Instance.ReverseMenu();
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
                SoundManager.Instance.Menu();
                ToggleSettingsPanel();
            }
            else
            {
                SoundManager.Instance.ReverseMenu();
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
                SoundManager.Instance.Menu();
                ToggleInventoryPanel();
            }
            else
            {
                SoundManager.Instance.ReverseMenu();

                IsInInventory = false;
                InventoryToggle = false;
                MonsterToggle = false;

                MonsterBookAnimator.SetBool("FadeIn", false);
                InventoryPanelAnimator.SetBool("FadeIn", false);
            }
            StartCoroutine(SetMenuAnimationToFalse());
        }
    }

    private void OpenInventoryMenu()
    {
        if(!InventoryToggle)
        {
            ToggleInventoryPanel();
            StartCoroutine(SetMenuAnimationToFalse());
        }
    }

    public void CloseInventoryMenu()
    {
        if(IsInInventory)
        {
            IsInInventory = false;
            InventoryToggle = false;
            MonsterToggle = false;

            MonsterBookAnimator.SetBool("FadeIn", false);
            InventoryPanelAnimator.SetBool("FadeIn", false);

            StartCoroutine(SetMenuAnimationToFalse());
        }
    }

    public void OpenInventory()
    {
        if(!IsInInventory)
        {
            IsInInventory = true;
            if(!MenuAnimating)
            {
                MenuAnimating = true;
                OpenInventoryMenu();
            }
        }
    }

    private void ToggleCharacterPanel()
    {
        CharacterToggle = true;
        EquipmentToggle = false;
        InventoryToggle = false;
        SettingsToggle = false;
        MonsterToggle = false;
        TipToggle = false;
        IsInInventory = false;

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
        TipToggle = false;
        IsInInventory = false;

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
        TipToggle = false;
        IsInInventory = false;

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
        TipToggle = false;
        IsInInventory = true;

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
        TipToggle = false;
        IsInInventory = false;

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
        SoundManager.Instance.Menu();

        monsterbook.GetMonsterInfoTxt.text = "";

        MonsterBookAnimator.SetBool("FadeIn", true);
        MonsterToggle = true;
    }

    public void CloseMonsterBook()
    {
        SoundManager.Instance.ReverseMenu();

        MonsterBookAnimator.SetBool("FadeIn", false);
        MonsterToggle = false;
    }

    public void OpenTipTome()
    {
        TipToggle = true;
    }

    public void CloseTipTome()
    {
        TipToggle = false;
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

        if(Knight.activeInHierarchy)
        {
            Knight.GetComponent<BasicAttack>().GetAutoAttackTime = 0;
            Knight.GetComponent<BasicAttack>().enabled = false;

            Knight.GetComponent<CapsuleCollider>().enabled = false;

            Knight.GetComponent<PlayerController>().enabled = false;

            Knight.GetComponent<PlayerAnimations>().DeathAnimation();

            Knight.GetComponent<Character>().GetRigidbody.useGravity = false;
            Knight.GetComponent<Character>().GetRigidbody.isKinematic = true;
        }
        else if(ShadowPriest.activeInHierarchy)
        {
            ShadowPriest.GetComponent<BasicAttack>().GetAutoAttackTime = 0;
            ShadowPriest.GetComponent<BasicAttack>().enabled = false;

            ShadowPriest.GetComponent<CapsuleCollider>().enabled = false;

            ShadowPriest.GetComponent<PlayerController>().enabled = false;

            ShadowPriest.GetComponent<PlayerAnimations>().DeathAnimation();

            ShadowPriest.GetComponent<Character>().GetRigidbody.useGravity = false;
            ShadowPriest.GetComponent<Character>().GetRigidbody.isKinematic = true;
        }

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

        if(Knight.activeInHierarchy)
        {
            Knight.transform.position = SpawnPoint.position;

            Knight.transform.rotation = Quaternion.Euler(0, 0, 0);

            Knight.GetComponent<Health>().IncreaseHealth(Knight.GetComponent<Character>().MaxHealth);
            Knight.GetComponent<Mana>().IncreaseMana(Knight.GetComponent<Character>().MaxMana);

            Knight.GetComponent<BasicAttack>().enabled = true;

            if (Knight.GetComponent<BasicAttack>().GetTarget != null)
            {
                Knight.GetComponent<BasicAttack>().GetTarget.TurnOffHealthBar();
                Knight.GetComponent<BasicAttack>().GetTarget = null;
                EnemyObject = null;
                LastEnemyObject = null;
            }

            Knight.GetComponent<CapsuleCollider>().enabled = true;

            Knight.GetComponent<Character>().GetRigidbody.useGravity = true;
            Knight.GetComponent<Character>().GetRigidbody.isKinematic = false;

            Knight.GetComponent<PlayerAnimations>().PlayResurrectAnimation();
            Knight.GetComponent<PlayerAnimations>().GetAnimator.ResetTrigger("Damaged");
        }
        else if(ShadowPriest.activeInHierarchy)
        {
            ShadowPriest.transform.position = SpawnPoint.position;

            ShadowPriest.transform.rotation = Quaternion.Euler(0, 0, 0);

            ShadowPriest.GetComponent<Health>().IncreaseHealth(ShadowPriest.GetComponent<Character>().MaxHealth);
            ShadowPriest.GetComponent<Mana>().IncreaseMana(ShadowPriest.GetComponent<Character>().MaxMana);

            ShadowPriest.GetComponent<BasicAttack>().enabled = true;

            if (ShadowPriest.GetComponent<BasicAttack>().GetTarget != null)
            {
                ShadowPriest.GetComponent<BasicAttack>().GetTarget.TurnOffHealthBar();
                ShadowPriest.GetComponent<BasicAttack>().GetTarget = null;
                EnemyObject = null;
                LastEnemyObject = null;
            }

            ShadowPriest.GetComponent<CapsuleCollider>().enabled = true;

            ShadowPriest.GetComponent<Character>().GetRigidbody.useGravity = true;
            ShadowPriest.GetComponent<Character>().GetRigidbody.isKinematic = false;

            ShadowPriest.GetComponent<PlayerAnimations>().PlayResurrectAnimation();
            ShadowPriest.GetComponent<PlayerAnimations>().GetAnimator.ResetTrigger("Damaged");
        }
    }

    public TextMeshProUGUI ShowNotEnoughManaText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Not enough Mana";

        SoundManager.Instance.Error();

        return InvalidText;
    }

    public TextMeshProUGUI ShowTargetOutOfRangeText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Target out of range";

        SoundManager.Instance.Error();

        return InvalidText;
    }

    public TextMeshProUGUI CannotExecuteText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Cannot Execute At This Time";

        SoundManager.Instance.Error();

        return InvalidText;
    }

    public TextMeshProUGUI InvalidTargetText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Invalid Target";

        SoundManager.Instance.Error();

        return InvalidText;
    }

    public TextMeshProUGUI NotEnoughCoinsText()
    {
        InvalidText.gameObject.SetActive(true);

        animator.Play("InvalidText", -1, 0f);

        InvalidText.text = "Not enough coins";

        SoundManager.Instance.Error();

        return InvalidText;
    }
}