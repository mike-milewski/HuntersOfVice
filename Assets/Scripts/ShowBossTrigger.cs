#pragma warning disable 0649
using UnityEngine;

public class ShowBossTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject Boss;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            Boss.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
