using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField]
    private Materials materials;

    private int i;

    public void DropItem()
    {
        for (i = 0; i < itemDrops.Length; i++)
        {
            if (Random.value * 100 <= itemDrops[i].GetDropChance)
            {
                var mat = materials;

                ItemMessageComponents();

                if (GameManager.Instance.GetInventoryMaterialTransform.childCount <= 0)
                {
                    mat = Instantiate(materials, GameManager.Instance.GetInventoryMaterialTransform);

                    mat.GetComponent<Materials>().GetMaterialData = itemDrops[i].GetMaterialData;

                    mat.GetComponent<Materials>().GetMaterialName = itemDrops[i].GetMaterialData.MaterialName;

                    mat.GetComponent<Materials>().GetQuantity++;

                    if (GameManager.Instance.GetIsInInventory)
                    {
                        mat.gameObject.SetActive(true);
                    }
                    else
                    {
                        mat.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (CheckForSameMaterialName())
                    {
                        return;
                    }
                    else
                    {
                        mat = Instantiate(materials, GameManager.Instance.GetInventoryMaterialTransform);

                        mat.GetComponent<Materials>().GetMaterialData = itemDrops[i].GetMaterialData;

                        mat.GetComponent<Materials>().GetMaterialName = itemDrops[i].GetMaterialData.MaterialName;

                        mat.GetComponent<Materials>().GetQuantity++;

                        if (GameManager.Instance.GetIsInInventory)
                        {
                            mat.gameObject.SetActive(true);
                        }
                        else
                        {
                            mat.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    private bool CheckForSameMaterialName()
    {
        bool SameName = false;

        foreach (Materials mat in GameManager.Instance.GetInventoryMaterialTransform.GetComponentsInChildren<Materials>(true))
        {
            if (mat.GetMaterialName == itemDrops[i].GetMaterialData.MaterialName)
            {
                SameName = true;

                mat.GetQuantity++;
            }
        }
        return SameName;
    }

    private void ItemMessageComponents()
    {
        var ItemMessage = ObjectPooler.Instance.GetItemMessage();

        ItemMessage.transform.SetParent(GameManager.Instance.GetItemMessageTransform, false);

        ItemMessage.SetActive(true);

        ItemMessage.GetComponent<Animator>().SetBool("Appear", true);
        ItemMessage.GetComponentInChildren<TextMeshProUGUI>().text = itemDrops[i].GetMaterialData.MaterialName;

        ItemMessage.GetComponentInChildren<RawImage>().texture = itemDrops[i].GetMaterialData.MaterialSprite.texture;
    }
}
