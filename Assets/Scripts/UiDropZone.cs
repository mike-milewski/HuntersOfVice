﻿using UnityEngine;
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
                if(dragObject.GetComponent<Equipment>())
                {
                    if(!dragObject.GetDropZone.GetComponent<EquippedCheck>().GetIsEquipped)
                    {
                        dragObject.GetComponent<Equipment>().Equip();
                    }
                    else
                    {
                        gameObject.GetComponentInChildren<Equipment>().UnEquip();
                        gameObject.GetComponentInChildren<DragUiObject>().transform.SetParent(gameObject.GetComponentInChildren<DragUiObject>().GetMenuParent.transform, true);

                            new Vector2(gameObject.GetComponentInChildren<DragUiObject>().GetMenuParent.transform.position.x,
                                        gameObject.GetComponentInChildren<DragUiObject>().GetMenuParent.transform.position.y);

                        dragObject.GetComponent<Equipment>().Equip();
                    }
                }
            }
        }
    }
}
