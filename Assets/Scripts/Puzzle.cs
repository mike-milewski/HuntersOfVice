#pragma warning disable 0649, 0414
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField]
    private GameObject ParticleEffect;

    [SerializeField]
    private GameObject ObjectToSpawn = null;

    [SerializeField]
    private int MaxEnemyCountRequired;

    private int EnemyCountRequired;

    public int GetEnemyCount
    {
        get
        {
            return EnemyCountRequired;
        }
        set
        {
            EnemyCountRequired = value;
        }
    }

    public void EnemyPuzzleType()
    {
        EnemyCountRequired++;

        if (EnemyCountRequired >= MaxEnemyCountRequired)
        {
            ParticleEffect.SetActive(true);
            ObjectToSpawn.SetActive(true);
        }
        else return;
    }
}
