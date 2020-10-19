#pragma warning disable 0649
using UnityEngine;

public class ChangeSpawnPointLocation : MonoBehaviour
{
    [SerializeField]
    private Transform CurrentSpawnPointLocation, DesiredSpawnPointLocation;

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
}
