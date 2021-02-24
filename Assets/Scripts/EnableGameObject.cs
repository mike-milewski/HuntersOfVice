#pragma warning disable 0649
using UnityEngine;

public class EnableGameObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectToSpawn;

    [SerializeField]
    private float RespawnValue;

    private float RespawnTime;

    private void OnEnable()
    {
        RespawnTime = 0;
    }

    private void Update()
    {
        RespawnTime += Time.deltaTime;
        if(RespawnTime >= RespawnValue)
        {
            objectToSpawn[Random.Range(0, objectToSpawn.Length)].SetActive(true);
            this.enabled = false;
        }
    }
}
