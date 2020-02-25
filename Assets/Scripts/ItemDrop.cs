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

    private int i;

    public void DropItem()
    {
        for(i = 0; i < itemDrops.Length; i++)
        {
            if (Random.value * 100 <= itemDrops[i].GetDropChance)
            {
                Materials m = itemDrops[i].GetMaterials;

                GameObject go = m.gameObject;

                if (GameManager.Instance.GetInventoryMaterialTransform.childCount > 0)
                {
                    if (CheckForSameMaterialName())
                    {
                        return;
                    }
                    else
                    {
                        GameObject g = Instantiate(go, GameManager.Instance.GetInventoryMaterialTransform);

                        g.GetComponent<Materials>().GetMaterialData = itemDrops[i].GetMaterialData;

                        g.GetComponent<Materials>().GetQuantity++;

                        if (GameManager.Instance.GetIsInInventory)
                        {
                            g.gameObject.SetActive(true);
                        }
                        else
                        {
                            g.gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    GameObject g = Instantiate(go, GameManager.Instance.GetInventoryMaterialTransform);

                    g.GetComponent<Materials>().GetMaterialData = itemDrops[i].GetMaterialData;

                    g.GetComponent<Materials>().GetQuantity++;

                    if (GameManager.Instance.GetIsInInventory)
                    {
                        g.gameObject.SetActive(true);
                    }
                    else
                    {
                        g.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private bool CheckForSameMaterialName()
    {
        bool SameName = false;

        Materials m = itemDrops[i].GetMaterials;

        foreach (Materials mat in GameManager.Instance.GetInventoryMaterialTransform.GetComponentsInChildren<Materials>(true))
        {
            if (mat.GetMaterialName == m.GetMaterialName)
            {
                SameName = true;

                mat.GetQuantity++;
            }
        }

        return SameName;
    }
}
