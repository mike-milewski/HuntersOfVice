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

            SkillsManager.Instance.ArrangeSkills();
        }

        Skills skills = eventData.pointerDrag.GetComponent<Skills>();
        if(skills != null)
        {
            skills.GetIsBeingDragged = false;
        }
    }
}
