using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUiObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private UiDropZone zone;

    [SerializeField]
    private Transform ParentObj;

    private GameObject PlaceHolder = null;

    public Transform GetParentObj
    {
        get
        {
            return ParentObj;
        }
        set
        {
            ParentObj = value;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PlaceHolder = new GameObject();
        PlaceHolder.transform.SetParent(zone.transform, false);
        RectTransform RectTrans = PlaceHolder.AddComponent<RectTransform>();
        RectTrans.sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);

        PlaceHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        gameObject.GetComponent<Skills>().GetIsBeingDragged = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(ParentObj, false);
        transform.position = eventData.position;

        int NewSiblingIndex = zone.transform.childCount;

        for (int i = 0; i < zone.transform.childCount; i++)
        {
            if (transform.position.x < zone.transform.GetChild(i).position.x || transform.position.y <= zone.transform.GetChild(i).position.y - 30 
                || transform.position.y >= zone.transform.GetChild(i).position.y + 30)
            {
                NewSiblingIndex = i;

                if (PlaceHolder.transform.GetSiblingIndex() < NewSiblingIndex)
                {
                    NewSiblingIndex--;
                }
                break;
            }
        }
        PlaceHolder.transform.SetSiblingIndex(NewSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(gameObject.transform.parent != zone.transform)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.SetSiblingIndex(PlaceHolder.transform.GetSiblingIndex());
        }
        Destroy(PlaceHolder);
    }
}
