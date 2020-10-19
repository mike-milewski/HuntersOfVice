#pragma warning disable 0649
using UnityEngine;

public class ChangeShopKeeperPosition : MonoBehaviour
{
    [SerializeField]
    private ShopKeeper shopKeeper;

    [SerializeField]
    private Transform shopKeeperPosition;

    [SerializeField]
    private GameObject Trigger;

    [SerializeField]
    private float shopPositionY;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            shopKeeper.gameObject.transform.position = new Vector3(shopKeeperPosition.position.x, shopPositionY, shopKeeperPosition.position.z);
            Trigger.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
