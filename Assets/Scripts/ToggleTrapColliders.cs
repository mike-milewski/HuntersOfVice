#pragma warning disable 0649
using UnityEngine;

public class ToggleTrapColliders : MonoBehaviour
{
    [SerializeField]
    private GameObject[] CollidersToEnable, CollidersToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            CheckEnabledObjects();
            CheckDisabledObjects();
        }
    }

    private void CheckEnabledObjects()
    {
        for (int i = 0; i < CollidersToEnable.Length; i++)
        {
            CollidersToEnable[i].gameObject.SetActive(true);
        }
    }

    private void CheckDisabledObjects()
    {
        for(int i = 0; i < CollidersToDisable.Length; i++)
        {
            CollidersToDisable[i].gameObject.SetActive(false);
        }
    }
}
