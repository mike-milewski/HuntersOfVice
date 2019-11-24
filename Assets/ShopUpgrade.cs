using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrade : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private Transform UpgradeTransform, MaterialTransform;

    public void AddMaterials()
    {
        if (inventory.GetShopMaterialTransform.childCount > 0)
        {
            foreach (Materials materials in inventory.GetShopMaterialTransform.GetComponentsInChildren<Materials>(true))
            {
                materials.transform.SetParent(UpgradeTransform, true);
                if (!materials.gameObject.activeInHierarchy)
                {
                    materials.GetComponent<Image>().raycastTarget = true;
                    materials.gameObject.SetActive(true);
                }
            }
        }
    }

    public void ToggleMaterials()
    {
        if(UpgradeTransform.childCount > 0)
        {
            foreach (Materials materials in UpgradeTransform.GetComponentsInChildren<Materials>(true))
            {
                materials.transform.SetParent(MaterialTransform, true);
                if(!inventory.GetInInventory)
                {
                    materials.gameObject.SetActive(false);
                }
                else
                {
                    materials.gameObject.SetActive(true);
                }
            }
        }
    }
}
