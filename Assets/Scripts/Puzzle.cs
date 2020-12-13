#pragma warning disable 0649, 0414
using UnityEngine;

[System.Serializable]
public class EnemiesToSpawn
{
    [SerializeField]
    private Enemy[] enemies;

    [SerializeField]
    private Transform[] EnemyParticleTransforms;

    [SerializeField]
    private int EnemyCount, MaxEnemyCount;

    public Enemy[] GetEnemies
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

    public Transform[] GetEnemyParticleTransforms
    {
        get
        {
            return EnemyParticleTransforms;
        }
        set
        {
            EnemyParticleTransforms = value;
        }
    }

    public int GetEnemyCount
    {
        get
        {
            return EnemyCount;
        }
        set
        {
            EnemyCount = value;
        }
    }

    public int GetMaxEnemyCount
    {
        get
        {
            return MaxEnemyCount;
        }
        set
        {
            MaxEnemyCount = value;
        }
    }
}

public class Puzzle : MonoBehaviour
{
    [SerializeField]
    private EnemiesToSpawn[] enemiesToSpawn;

    [SerializeField]
    private Enemy[] enemyToSpawn = null;

    [SerializeField]
    private GameObject ParticleEffect;

    [SerializeField]
    private GameObject ObjectToSpawn = null, ObjectToDespawn = null;

    [SerializeField]
    private BoxCollider BridgePathCollider = null;

    [SerializeField]
    private Transform EnemySpawnPoint = null;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private int MaxEnemyCountRequired, SpawnIndex, NumberOfEnemiesToSpawn;

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
            if(settings.UseParticleEffects)
            {
                ParticleEffect.SetActive(true);
            }
            ObjectToSpawn.SetActive(true);
            BridgePathCollider.enabled = false;
            gameObject.SetActive(false);
        }
        else return;
    }

    public void GatePuzzleType()
    {
        EnemyCountRequired++;

        if (EnemyCountRequired >= MaxEnemyCountRequired)
        {
            ObjectToDespawn.SetActive(false);
            gameObject.SetActive(false);
        }
        else return;
    }

    public void MagicWallPuzzleType()
    {
        ObjectToDespawn.SetActive(false);
    }

    public void BushPuzzle()
    {
        EnemyCountRequired++;

        if (EnemyCountRequired >= MaxEnemyCountRequired)
        {
            if (settings.UseParticleEffects)
            {
                ParticleEffect.SetActive(true);
            }
            ObjectToDespawn.SetActive(false);
            gameObject.SetActive(false);
        }
        else return;
    }

    public void SpawnNewEnemy()
    {
        SpawnIndex++;

        Invoke("ShouldSpawn", 4f);
    }

    private void ShouldSpawn()
    {
        if(SpawnIndex < NumberOfEnemiesToSpawn)
        {
            EnemySpawnParticle(EnemySpawnPoint.position);
            enemyToSpawn[SpawnIndex].gameObject.SetActive(true);
        }
        else
        {
            ParticleEffect.SetActive(true);
            ObjectToSpawn.SetActive(true);
            ObjectToDespawn.SetActive(false);
        }
    }

    public void SecretCharacterSpawn()
    {
        Invoke("SpawnSecretEnemies", 2.0f);
    }

    private void SpawnSecretEnemies()
    {
        enemiesToSpawn[SpawnIndex].GetEnemyCount++;

        if(enemiesToSpawn[SpawnIndex].GetEnemyCount >= enemiesToSpawn[SpawnIndex].GetMaxEnemyCount)
        {
            for (int i = 0; i < enemiesToSpawn[SpawnIndex].GetEnemies.Length; i++)
            {
                EnemySpawnParticle(enemiesToSpawn[SpawnIndex].GetEnemyParticleTransforms[i].position);

                enemiesToSpawn[SpawnIndex].GetEnemies[i].gameObject.SetActive(true);
            }
            SpawnIndex++;
        }
    }

    private void EnemySpawnParticle(Vector3 Position)
    {
        if (settings.UseParticleEffects)
        {
            var SpawnParticle = ObjectPooler.Instance.GetEnemyAppearParticle();

            SpawnParticle.SetActive(true);

            SpawnParticle.transform.position = new Vector3(Position.x, Position.y, Position.z);
        }
    }
}
