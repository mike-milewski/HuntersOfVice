using UnityEngine;
using UnityEngine.EventSystems;

public class DragUiObject : MonoBehaviour, IDragHandler
{
    public GameObject ParentObj;

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(ParentObj.transform, false);
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }
}
