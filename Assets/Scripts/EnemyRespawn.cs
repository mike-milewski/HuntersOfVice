#pragma warning disable 0649
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    [SerializeField]
    private EnemyAI EnemyToRespawn;

    [SerializeField]
    private float RespawnTime;

    [SerializeField]
    private float RespawnTimer;

    private void OnEnable()
    {
        RespawnTimer = RespawnTime;
    }

    private void Update()
    {
        RespawnTimer -= Time.deltaTime;
        if(RespawnTimer <= 0)
        {
            if(!EnemyToRespawn.GetIsDisabled)
            {
                EnemyToRespawn.gameObject.SetActive(true);
            }
            EnemyToRespawn.GetIsDead = false;

            this.enabled = false;
        }
    }
}
