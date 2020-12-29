#pragma warning disable 0649
using UnityEngine;

public class EnableEnemyChase : MonoBehaviour
{
    [SerializeField]
    private EnemyConnection enemyConnection;

    private void OnEnable()
    {
        enemyConnection.EnableChase();
    }
}
