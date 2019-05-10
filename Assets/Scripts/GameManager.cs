using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private Text InvalidText;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Transform SpawnPoint;

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
    }

    public void Dead()
    {
        Player.GetComponent<BasicAttack>().GetAutoAttackTime = 0;
        Player.GetComponent<BasicAttack>().enabled = false;

        Player.GetComponent<BoxCollider>().enabled = false;

        Player.GetComponent<PlayerController>().enabled = false;

        Player.GetComponent<PlayerAnimations>().DeathAnimation();

        Player.GetComponent<Character>().GetRigidbody.useGravity = false;
        Player.GetComponent<Character>().GetRigidbody.isKinematic = true;

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnTime);
        Player.transform.position = SpawnPoint.position;

        Player.GetComponent<Character>().CurrentHealth = Player.GetComponent<Character>().MaxHealth;
        Player.GetComponent<Health>().GetFilledBar();

        Player.GetComponent<BasicAttack>().enabled = true;
        
        if(Player.GetComponent<BasicAttack>().GetTarget != null)
        {
            Player.GetComponent<BasicAttack>().GetTarget.GetComponent<Enemy>().GetHealthObject.SetActive(false);
            Player.GetComponent<BasicAttack>().GetTarget = null;
        }

        Player.GetComponent<BoxCollider>().enabled = true;

        Player.GetComponent<Character>().GetRigidbody.useGravity = true;
        Player.GetComponent<Character>().GetRigidbody.isKinematic = false;

        Player.GetComponent<PlayerAnimations>().PlayResurrectAnimation();
    }

    public Text ShowNotEnoughManaText()
    {
        InvalidText.gameObject.SetActive(true);

        InvalidText.text = "Not enough Mana";

        return InvalidText;
    }

    public Text ShowTargetOutOfRangeText()
    {
        InvalidText.gameObject.SetActive(true);

        InvalidText.text = "Target out of range";

        return InvalidText;
    }

    public Text SkillStillRechargingText()
    {
        InvalidText.gameObject.SetActive(true);

        InvalidText.text = "Skill still recharging";

        return InvalidText;
    }

    public Text InvalidTargetText()
    {
        InvalidText.gameObject.SetActive(true);

        InvalidText.text = "Invalid Target";

        return InvalidText;
    }
}
