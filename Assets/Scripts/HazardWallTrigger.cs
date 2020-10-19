#pragma warning disable 0649
using UnityEngine;

public class HazardWallTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject Wall, Wall2;

    [SerializeField]
    private ObstacleDamageRadius[] obstacleDamageRadius;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            EnableDamageRadius();

            Wall.SetActive(true);
            Wall2.SetActive(true);

            gameObject.SetActive(false);
        }
    }

    public void DisableWalls()
    {
        Wall.SetActive(false);
        Wall2.SetActive(false);
    }

    private void EnableDamageRadius()
    {
        for(int i = 0; i < obstacleDamageRadius.Length; i++)
        {
            obstacleDamageRadius[i].enabled = true;
        }
    }
}
