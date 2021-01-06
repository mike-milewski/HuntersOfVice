#pragma warning disable 0649
using UnityEngine;

public class EnemyConnection : MonoBehaviour
{
    [SerializeField]
    private EnemyAI[] enemyAI;

    [SerializeField]
    private Character character = null;

    [SerializeField]
    private bool IsInsideCollider;

    public Character GetCharacter
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
        }
    }

    public bool GetIsInsideCollider
    {
        get
        {
            return IsInsideCollider;
        }
        set
        {
            IsInsideCollider = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            IsInsideCollider = true;

            character = other.GetComponent<Character>();

            foreach(EnemyAI enemyai in enemyAI)
            {
                enemyai.GetPlayerEntry = true;
                enemyai.GetPlayerTarget = other.GetComponent<Character>();
                enemyai.GetStates = States.Chase;
                enemyai.GetEnemy.GetExperience = other.GetComponent<Experience>();
            }
        }
    }

    public void EnableChase()
    {
        Invoke("InvokeChase", 1f);
    }

    private void InvokeChase()
    {
        if(IsInsideCollider)
        {
            foreach (EnemyAI enemyai in enemyAI)
            {
                enemyai.GetPlayerEntry = true;
                enemyai.GetPlayerTarget = character;
                enemyai.GetStates = States.Chase;
                enemyai.GetEnemy.GetExperience = character.GetComponent<Experience>();
            }
        }
        else
        {
            foreach (EnemyAI enemyai in enemyAI)
            {
                enemyai.GetStates = States.Patrol;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            IsInsideCollider = false;
        }

        foreach(EnemyAI enemyai in enemyAI)
        {
            if(enemyai.GetEnemy.GetCharacter.CurrentHealth > 0)
            enemyai.EndBattle();
        }
    }
}
