#pragma warning disable 0649
using UnityEngine;

public class SetAllMaterials : MonoBehaviour
{
    public void AddAllMaterials()
    {
        if(GameManager.Instance.GetInventoryMaterialTransform.childCount > 0 && GameManager.Instance.GetIsInUpgrade)
        {
            foreach (Materials m in GameManager.Instance.GetInventoryMaterialTransform.GetComponentsInChildren<Materials>())
            {
                for(int i = 0; i < m.GetQuantity; i++)
                {
                    m.SetAllMaterialsToShop(i);
                }
                m.GetQuantity = 0;
                m.CheckQuantity();
            }
        }
    }
}
