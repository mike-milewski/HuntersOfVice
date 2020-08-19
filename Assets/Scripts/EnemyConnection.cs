#pragma warning disable 0649
using UnityEngine;

public class EnemyConnection : MonoBehaviour
{
    [SerializeField]
    private EnemyAI enemyAI;

    public EnemyAI GetEnemyAI
    {
        get
        {
            return enemyAI;
        }
        set
        {
            enemyAI = value;
        }
    }
}
