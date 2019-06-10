using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private FadeScreen fadeScreen;

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
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.GetFadeState = FadeState.FADEOUT;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnTime);
        fadeScreen.GetFadeState = FadeState.FADEIN;
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