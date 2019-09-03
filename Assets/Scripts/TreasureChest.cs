using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TreasureChest : MonoBehaviour
{
    [SerializeField]
    private Equipment equipment;

    [SerializeField]
    private GameObject ItemMessage;

    [SerializeField]
    private Image ItemImage;

    [SerializeField]
    private TextMeshProUGUI ItemMessageText;

    [SerializeField]
    private Transform ItemTransform;

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
        ItemMessage.GetComponent<Animator>().SetBool("Appear", true);
        ItemMessageText.text = equipment.GetEquipmentData.EquipmentName;
        ItemImage.sprite = equipment.GetEquipmentSprite;
        StartCoroutine("ReverseItemMessage");
    }

    private IEnumerator ReverseItemMessage()
    {
        float timer = 2.0f;
        yield return new WaitForSeconds(timer);
        ItemMessage.GetComponent<Animator>().SetBool("Appear", false);
    }
}
