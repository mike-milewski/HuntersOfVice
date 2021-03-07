#pragma warning disable 0649
using System;
using UnityEngine;

[System.Serializable]
class Enemies
{
    [SerializeField]
    private EnemyAI[] enemies;

    public EnemyAI[] GetEnemies
    {
        get
        {
            return enemies;
        }
        set
        {
            enemies = value;
        }
    }
}

public class EnemyZones : MonoBehaviour
{
    [SerializeField]
    private Enemies[] enemies;

    [SerializeField]
    private GameObject ZoneToEnable, ZoneToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            EnableEnemies();
            DisableEnemies();

            ZoneToEnable.SetActive(true);
            ZoneToDisable.SetActive(false);
        }
    }

    private void EnableEnemies()
    {
        for(int i = 0; i < enemies[0].GetEnemies.Length; i++)
        {
            if(enemies[0].GetEnemies[i] != null)
            {
                if (!enemies[0].GetEnemies[i].GetIsDead)
                {
                    enemies[0].GetEnemies[i].gameObject.SetActive(true);
                }
                enemies[0].GetEnemies[i].GetIsDisabled = false;
            }
        }
    }

    private void DisableEnemies()
    {
        for (int i = 0; i < enemies[1].GetEnemies.Length; i++)
        {
            if(enemies[1].GetEnemies[i] != null)
            {
                enemies[1].GetEnemies[i].gameObject.SetActive(false);
                enemies[1].GetEnemies[i].GetIsDisabled = true;
            }
        }
    }
}
