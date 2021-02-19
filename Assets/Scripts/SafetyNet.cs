#pragma warning disable 0649
using UnityEngine;

public class SafetyNet : MonoBehaviour
{
    [SerializeField]
    private SpawnPoint spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            other.transform.position = spawnPoint.transform.position;
        }
    }
}
