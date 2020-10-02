#pragma warning disable 0649
using UnityEngine;

public class EnemyConnection : MonoBehaviour
{
    [SerializeField]
    private EnemyAI[] enemyAI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            foreach(EnemyAI enemyai in enemyAI)
            {
                enemyai.GetPlayerEntry = true;
                enemyai.GetPlayerTarget = other.GetComponent<Character>();
                enemyai.GetStates = States.Chase;
                enemyai.GetEnemy.GetExperience = other.GetComponent<Experience>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach(EnemyAI enemyai in enemyAI)
        {
            if(enemyai.GetEnemy.GetCharacter.CurrentHealth > 0)
            enemyai.EndBattle();
        }
    }
}
