#pragma warning disable 0649
using UnityEngine;

public class ChangeShopKeeperPosition : MonoBehaviour
{
    [SerializeField]
    private ShopKeeper shopKeeper;

    [SerializeField]
    private Transform shopKeeperPosition, PositionToSpawnWhenDead;

    [SerializeField]
    private GameObject Trigger = null;

    [SerializeField]
    private float shopPositionY;

    public Transform GetPosition
    {
        get
        {
            return PositionToSpawnWhenDead;
        }
        set
        {
            PositionToSpawnWhenDead = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            GameManager.Instance.GetShopKeeperLastPosition = PositionToSpawnWhenDead;

            shopKeeper.GetCurrentPosition = shopKeeperPosition;

            shopKeeper.gameObject.transform.position = new Vector3(shopKeeperPosition.position.x, shopPositionY, shopKeeperPosition.position.z);

            shopKeeper.transform.rotation = Quaternion.Euler(0, shopKeeperPosition.GetComponent<ShopKeeperRotation>().GetShopKeeperRotationY, 0);

            if(Trigger != null)
            {
                Trigger.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
