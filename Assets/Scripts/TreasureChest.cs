#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TreasureChest : MonoBehaviour
{
    [SerializeField]
    private PlayerController Knight;

    [SerializeField]
    private PlayerController ShadowPriest;

    [SerializeField]
    private PlayerController Toadstool;

    [SerializeField]
    private Equipment[] equipments;

    [SerializeField]
    private Transform WeaponTransform, ArmorTransform, ItemMessageTransform;

    [SerializeField]
    private MeshRenderer[] meshRenderer;

    [SerializeField]
    private Material AlphaMaterial;

    [SerializeField]
    private GameObject ParentObject;

    [SerializeField]
    private Animator animator;

    public Equipment[] GetEquipment
    {
        get
        {
            return equipments;
        }
        set
        {
            equipments = value;
        }
    }

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
        SoundManager.Instance.RecievedItem();

        if(Knight.gameObject.activeInHierarchy)
        {
            switch(equipments[0].GetEquipmentType)
            {
                case (EquipmentType.Weapon):
                    equipments[0].transform.SetParent(WeaponTransform, true);
                    break;
                default:
                    equipments[0].transform.SetParent(ArmorTransform, true);
                    break;
            }
            equipments[0].transform.localScale = new Vector3(1, 1, 1);
            equipments[0].transform.rotation = Quaternion.Euler(0, 0, 0);

            equipments[0].transform.position = new Vector3(equipments[0].transform.position.x, equipments[0].transform.position.y, 0);
        }
        if(ShadowPriest.gameObject.activeInHierarchy)
        {
            switch (equipments[1].GetEquipmentType)
            {
                case (EquipmentType.Weapon):
                    equipments[1].transform.SetParent(WeaponTransform, true);
                    break;
                default:
                    equipments[1].transform.SetParent(ArmorTransform, true);
                    break;
            }
            equipments[1].transform.localScale = new Vector3(1, 1, 1);
            equipments[1].transform.rotation = Quaternion.Euler(0, 0, 0);

            equipments[1].transform.position = new Vector3(equipments[1].transform.position.x, equipments[1].transform.position.y, 0);
        }
        if (Toadstool.gameObject.activeInHierarchy)
        {
            switch (equipments[2].GetEquipmentType)
            {
                case (EquipmentType.Weapon):
                    equipments[2].transform.SetParent(WeaponTransform, true);
                    break;
                default:
                    equipments[2].transform.SetParent(ArmorTransform, true);
                    break;
            }
            equipments[2].transform.localScale = new Vector3(1, 1, 1);
            equipments[2].transform.rotation = Quaternion.Euler(0, 0, 0);

            equipments[2].transform.position = new Vector3(equipments[2].transform.position.x, equipments[2].transform.position.y, 0);
        }

        ItemMessageComponents();

        if(GameManager.Instance.GetEquipmentToggle)
        {
            if(Knight.gameObject.activeInHierarchy)
            {
                equipments[0].gameObject.SetActive(true);
            }
            if(ShadowPriest.gameObject.activeInHierarchy)
            {
                equipments[1].gameObject.SetActive(true);
            }
            if (Toadstool.gameObject.activeInHierarchy)
            {
                equipments[2].gameObject.SetActive(true);
            }
        }
    }

    private void ItemMessageComponents()
    {
        var ItemMessage = ObjectPooler.Instance.GetItemMessage();

        ItemMessage.transform.SetParent(ItemMessageTransform, false);

        ItemMessage.SetActive(true);

        ItemMessage.GetComponent<Animator>().SetBool("Appear", true);
        ItemMessage.GetComponent<ResetItemMessage>().StartCoroutine("ReverseMessage");
        if (Knight.gameObject.activeInHierarchy)
        {
            ItemMessage.GetComponentInChildren<TextMeshProUGUI>().text = equipments[0].GetEquipmentData.EquipmentName;

            ItemMessage.GetComponentInChildren<RawImage>().texture = equipments[0].GetEquipmentSprite.texture;
        }
        if(ShadowPriest.gameObject.activeInHierarchy)
        {
            ItemMessage.GetComponentInChildren<TextMeshProUGUI>().text = equipments[1].GetEquipmentData.EquipmentName;

            ItemMessage.GetComponentInChildren<RawImage>().texture = equipments[1].GetEquipmentSprite.texture;
        }
        if (Toadstool.gameObject.activeInHierarchy)
        {
            ItemMessage.GetComponentInChildren<TextMeshProUGUI>().text = equipments[2].GetEquipmentData.EquipmentName;

            ItemMessage.GetComponentInChildren<RawImage>().texture = equipments[2].GetEquipmentSprite.texture;
        }
    }

    private void ChangeMaterialsToAlpha()
    {
        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            mr.material = AlphaMaterial;
        }
    }

    public void PlayTreasureChestSE()
    {
        SoundManager.Instance.TreasureChest();
    }

    public void FadeMaterials()
    {
        ChangeMaterialsToAlpha();

        StartCoroutine("Fade");
        StartCoroutine("Fade2");
        StartCoroutine("Fade3");
        StartCoroutine("Fade4");
        StartCoroutine("Fade5");
    }

    private IEnumerator Fade()
    {
        Color alpha = meshRenderer[0].material.color;
        meshRenderer[0].material.color = alpha;
        yield return new WaitForSeconds(3f);
        while (alpha.a > 0.1f)
        {
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[0].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[0].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
        ParentObject.SetActive(false);
        yield return null;
    }

    private IEnumerator Fade2()
    {
        Color alpha = meshRenderer[1].material.color;
        meshRenderer[1].material.color = alpha;
        yield return new WaitForSeconds(3f);
        while (alpha.a > 0.1f)
        {
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[1].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[1].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
        ParentObject.SetActive(false);
        yield return null;
    }

    private IEnumerator Fade3()
    {
        Color alpha = meshRenderer[2].material.color;
        meshRenderer[2].material.color = alpha;
        yield return new WaitForSeconds(3f);
        while (alpha.a > 0.1f)
        {
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[2].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[2].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
        ParentObject.SetActive(false);
        yield return null;
    }

    private IEnumerator Fade4()
    {
        Color alpha = meshRenderer[3].material.color;
        meshRenderer[3].material.color = alpha;
        yield return new WaitForSeconds(3f);
        while (alpha.a > 0.1f)
        {
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[3].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[3].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
        ParentObject.SetActive(false);
        yield return null;
    }

    private IEnumerator Fade5()
    {
        Color alpha = meshRenderer[4].material.color;
        meshRenderer[4].material.color = alpha;
        yield return new WaitForSeconds(3f);
        while (alpha.a > 0.1f)
        {
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[4].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer[4].material.color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
        ParentObject.SetActive(false);
        yield return null;
    }
}