using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

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
    private GameObject CharacterPanel, InventoryPanel, SettingsPanel;

    private bool IsDead;

    [SerializeField]
    private EventSystem eventsystem;

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

    private void ToggleIventoryPanel()
    {
        InventoryPanel.SetActive(true);
        CharacterPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    private void ToggleCharacterPanel()
    {
        CharacterPanel.SetActive(true);
        InventoryPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    private void ToggleSettingsPanel()
    {
        SettingsPanel.SetActive(true);
        CharacterPanel.SetActive(false);
        InventoryPanel.SetActive(false);
    }

    public void Dead()
    {
        IsDead = true;
        SkillsManager.Instance.DeactivateSkillButtons();

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