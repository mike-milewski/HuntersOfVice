using UnityEngine;

[System.Serializable]
public class ItemDrops
{
    [SerializeField]
    private Materials materials;

    [SerializeField]
    private MaterialData materialData;

    [SerializeField]
    private float DropChance;

    public Materials GetMaterials
    {
        get
        {
            return materials;
        }
        set
        {
            materials = value;
        }
    }

    public MaterialData GetMaterialData
    {
        get
        {
            return materialData;
        }
        set
        {
            materialData = value;
        }
    }

    public float GetDropChance
    {
        get
        {
            return DropChance;
        }
        set
        {
            DropChance = value;
        }
    }
}

public class ItemDrop : MonoBehaviour
{
    [SerializeField]
    private ItemDrops[] itemDrops;

    public void DropItem()
    {
        for(int i = 0; i < itemDrops.Length; i++)
        {
            if (Random.value * 100 <= itemDrops[i].GetDropChance)
            {
                Debug.Log("Dropped!");

                Materials m = itemDrops[i].GetMaterials;

                if (GameManager.Instance.GetInventoryMaterialTransform.childCount > 0)
                {
                    foreach (Materials mat in GameManager.Instance.GetInventoryMaterialTransform.GetComponentsInChildren<Materials>())
                    {
                        if (mat.GetMaterialData.name == m.GetMaterialData.name)
                        {
                            mat.GetQuantity++;
                        }
                        else
                        {
                            Instantiate(m, GameManager.Instance.GetInventoryMaterialTransform);

                            m.GetMaterialData = itemDrops[i].GetMaterialData;

                            m.GetQuantity++;

                            if (GameManager.Instance.GetIsInInventory)
                            {
                                m.gameObject.SetActive(true);
                            }
                            else
                            {
                                m.gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else
                {
                    Instantiate(m, GameManager.Instance.GetInventoryMaterialTransform);

                    itemDrops[i].GetMaterialData = m.GetMaterialData;

                    m.GetQuantity++;

                    if (GameManager.Instance.GetIsInInventory)
                    {
                        m.gameObject.SetActive(true);
                    }
                    else
                    {
                        m.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
