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
    private Equipment[] equipments;

    [SerializeField]
    private Transform ItemTransform, ItemMessageTransform;

    [SerializeField]
    private MeshRenderer[] meshRenderer;

    [SerializeField]
    private Material AlphaMaterial;

    [SerializeField]
    private GameObject ParentObject;

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
        if(Knight.gameObject.activeInHierarchy)
        {
            equipments[0].transform.SetParent(ItemTransform, true);
            equipments[0].transform.localScale = new Vector3(1, 1, 1);
            equipments[0].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(ShadowPriest.gameObject.activeInHierarchy)
        {
            equipments[1].transform.SetParent(ItemTransform, true);
            equipments[1].transform.localScale = new Vector3(1, 1, 1);
            equipments[1].transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        ItemMessageComponents();

        if(GameManager.Instance.GetEquipmentToggle)
        {
            if(Knight.gameObject.activeInHierarchy)
            {
                equipments[0].gameObject.SetActive(true);
            }
            else if(ShadowPriest.gameObject.activeInHierarchy)
            {
                equipments[1].gameObject.SetActive(true);
            }
        }
    }

    private void ItemMessageComponents()
    {
        var ItemMessage = ObjectPooler.Instance.GetItemMessage();

        ItemMessage.transform.SetParent(ItemMessageTransform, false);

        ItemMessage.SetActive(true);

        ItemMessage.GetComponent<Animator>().SetBool("Appear", true);
        if(Knight.gameObject.activeInHierarchy)
        {
            ItemMessage.GetComponentInChildren<TextMeshProUGUI>().text = equipments[0].GetEquipmentData.EquipmentName;

            ItemMessage.GetComponentInChildren<RawImage>().texture = equipments[0].GetEquipmentSprite.texture;
        }
        else if(ShadowPriest.gameObject.activeInHierarchy)
        {
            ItemMessage.GetComponentInChildren<TextMeshProUGUI>().text = equipments[1].GetEquipmentData.EquipmentName;

            ItemMessage.GetComponentInChildren<RawImage>().texture = equipments[1].GetEquipmentSprite.texture;
        }
    }

    private void ChangeMaterialsToAlpha()
    {
        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            mr.material = AlphaMaterial;
        }
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