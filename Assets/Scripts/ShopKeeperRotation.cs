using UnityEngine;

public class ShopKeeperRotation : MonoBehaviour
{
    [SerializeField]
    private float ShopKeeperRotationY;

    public float GetShopKeeperRotationY
    {
        get
        {
            return ShopKeeperRotationY;
        }
        set
        {
            ShopKeeperRotationY = value;
        }
    }
}
