using UnityEngine;
using UnityEngine.EventSystems;

public class UiDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DragUiObject dragObject = eventData.pointerDrag.GetComponent<DragUiObject>();
        if(dragObject != null)
        {
            dragObject.transform.SetParent(this.transform, false);

            SkillsManager.Instance.ClearSkills();
        }
    }
}
