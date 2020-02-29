#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TreasureChest : MonoBehaviour
{
    [SerializeField]
    private Equipment equipment;

    [SerializeField]
    private Transform ItemTransform, ItemMessageTransform;

    [SerializeField]
    private Animator animator;

    public Animator GetAnimator
    {
        get
        {
            return animator;
        }
        set
        {
            animator = value;
        }
    }

    public void GetItem()
    {
        equipment.transform.SetParent(ItemTransform, true);
        equipment.transform.localScale = new Vector3(1, 1, 1);
        equipment.transform.rotation = Quaternion.Euler(0, 0, 0);

        ItemMessageComponents();

        if(GameManager.Instance.GetEquipmentToggle)
        {
            equipment.gameObject.SetActive(true);
        }
    }

    private void ItemMessageComponents()
    {
        var ItemMessage = ObjectPooler.Instance.GetItemMessage();

        ItemMessage.transform.SetParent(ItemMessageTransform, false);

        ItemMessage.SetActive(true);

        ItemMessage.GetComponent<Animator>().SetBool("Appear", true);
        ItemMessage.GetComponentInChildren<TextMeshProUGUI>().text = equipment.GetEquipmentData.EquipmentName;

        ItemMessage.GetComponentInChildren<RawImage>().texture = equipment.GetEquipmentSprite.texture;
    }
}
