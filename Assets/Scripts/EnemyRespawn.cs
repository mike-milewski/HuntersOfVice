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
            EnemyToRespawn.gameObject.SetActive(true);

            this.enabled = false;
        }
    }
}
