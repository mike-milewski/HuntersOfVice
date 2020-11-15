using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private ChangeSpawnPointLocation CurrentSpawnPointLocation = null;

    public ChangeSpawnPointLocation GetSpawnPointLocation
    {
        get
        {
            return CurrentSpawnPointLocation;
        }
        set
        {
            CurrentSpawnPointLocation = value;
        }
    }

    public void ZonesAndEnemies()
    {
        CurrentSpawnPointLocation.ZonesAndEnemies();
    }
}
