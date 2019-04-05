using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Transform SpawnPoint;

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

        Player.GetComponent<BoxCollider>().enabled = true;

        Player.GetComponent<Character>().GetRigidbody.useGravity = true;
        Player.GetComponent<Character>().GetRigidbody.isKinematic = false;

        Player.GetComponent<PlayerAnimations>().PlayResurrectAnimation();
    }
}
