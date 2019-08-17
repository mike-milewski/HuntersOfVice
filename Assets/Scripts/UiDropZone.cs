using UnityEngine;
using UnityEngine.EventSystems;

public enum DropType { Skill, Weapon, Armor}

public class UiDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private DropType dropType;

    public void OnDrop(PointerEventData eventData)
    {
        DragUiObject dragObject = eventData.pointerDrag.GetComponent<DragUiObject>();
        if (dragObject != null)
        {
            if(dragObject.GetObjectType == (ObjectType)dropType)
            {
                dragObject.transform.SetParent(this.transform, true);

                if(dragObject.GetObjectType == ObjectType.Skill)
                {
                    SkillsManager.Instance.ClearSkills();
                }
            }
        }
    }
}
