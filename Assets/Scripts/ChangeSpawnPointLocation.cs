#pragma warning disable 0649
using UnityEngine;

public class ChangeSpawnPointLocation : MonoBehaviour
{
    [SerializeField]
    private SpawnPoint spawnPoint;

    [SerializeField]
    private EnemyAI[] EnemiesToEnable, EnemiesToDisable;

    [SerializeField]
    private Transform CurrentSpawnPointLocation, DesiredSpawnPointLocation;

    [SerializeField]
    private ShopKeeper shopKeeper = null;

    [SerializeField]
    private Transform ShopKeeperPositionOnRespawn = null;

    [SerializeField]
    private Transform[] EnemyZonesToEnable, EnemyZonesToDisable;

    [SerializeField]
    private bool AddSpawnPointToGameManager;

    [SerializeField]
    private HazardWallTrigger[] hazardWallTrigger;

    [SerializeField]
    private ObstacleDamageRadius[] obstacleDamageRadius;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            CurrentSpawnPointLocation.position = DesiredSpawnPointLocation.position;

            if (AddSpawnPointToGameManager)
            {
                GameManager.Instance.GetChangeSpawnPointLocation = this;

                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                GameManager.Instance.GetChangeSpawnPointLocation = null;
                gameObject.SetActive(false);
            }

            if(shopKeeper != null)
            {
                GameManager.Instance.GetShopKeeperLastPosition = ShopKeeperPositionOnRespawn;
            }

            spawnPoint.GetSpawnPointLocation = this;
        }
    }

    public void EnableWallTrigger()
    {
        for (int i = 0; i < hazardWallTrigger.Length; i++)
        {
            hazardWallTrigger[i].gameObject.SetActive(true);
            hazardWallTrigger[i].DisableWalls();
        }
    }

    public void DisableDamageRadii()
    {
        for (int i = 0; i < obstacleDamageRadius.Length; i++)
        {
            obstacleDamageRadius[i].enabled = false;
        }
    }

    private void EnableEnemies()
    {
        if(EnemiesToEnable.Length > 0)
        {
            for (int i = 0; i < EnemiesToEnable.Length; i++)
            {
                EnemiesToEnable[i].gameObject.SetActive(true);

                EnemiesToEnable[i].GetIsDisabled = false;
            }
        }
    }

    private void DisableEnemies()
    {
        if(EnemiesToDisable.Length > 0)
        {
            for (int i = 0; i < EnemiesToDisable.Length; i++)
            {
                EnemiesToDisable[i].gameObject.SetActive(false);

                EnemiesToDisable[i].GetIsDisabled = true;
            }
        }
    }

    private void EnableEnemyZones()
    {
        if(EnemyZonesToEnable.Length > 0)
        {
            for(int i = 0; i < EnemyZonesToEnable.Length; i++)
            {
                EnemyZonesToEnable[i].gameObject.SetActive(true);
            }
        }
    }

    private void DisableEnemyZones()
    {
        if (EnemyZonesToDisable.Length > 0)
        {
            for (int i = 0; i < EnemyZonesToDisable.Length; i++)
            {
                EnemyZonesToDisable[i].gameObject.SetActive(false);
            }
        }
    }

    public void ZonesAndEnemies()
    {
        EnableEnemies();
        DisableEnemies();
        EnableEnemyZones();
        DisableEnemyZones();
    }
}