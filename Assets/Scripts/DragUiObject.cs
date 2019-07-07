using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUiObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private UiDropZone zone;

    [SerializeField]
    private Transform ParentObj, SkillMenuParent;

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

        SkillsManager.Instance.AllSkillsBeingDragged();
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
            if(gameObject.GetComponent<Skills>().GetStatusIcon != null)
            {
                if (gameObject.GetComponent<Skills>().GetStatusIcon.activeInHierarchy)
                {
                    if (gameObject.GetComponent<Skills>().GetStatusIcon.GetComponent<StatusIcon>())
                    {
                        gameObject.GetComponent<Skills>().GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();

                    }
                    else if (gameObject.GetComponent<Skills>().GetStatusIcon.GetComponent<EnemyStatusIcon>())
                    {
                        gameObject.GetComponent<Skills>().GetStatusIcon.GetComponent<EnemyStatusIcon>().RemoveEffect();
                    }
                }
            }
            gameObject.transform.SetParent(SkillMenuParent, false);
            gameObject.transform.position = new Vector2(SkillMenuParent.transform.position.x, SkillMenuParent.transform.position.y);

            SkillsManager.Instance.ClearSkills();
            SkillsManager.Instance.AddSkillsToList();
            SkillsManager.Instance.AllSkillsNotBeingDragged();
        }
        else
        {
            transform.SetSiblingIndex(PlaceHolder.transform.GetSiblingIndex());
            CheckForSameSkills(gameObject.GetComponent<Skills>());

            SkillsManager.Instance.ClearSkills();
            SkillsManager.Instance.AddSkillsToList();
        }
        SkillsManager.Instance.AllSkillsNotBeingDragged();

        Destroy(PlaceHolder);
    }

    public void CheckForSameSkills(Skills other)
    {
        foreach (Skills s in zone.transform.GetComponentsInChildren<Skills>())
        {
            if(s.GetSkillName == other.GetSkillName)
            {
                other.GetButton.GetComponent<Image>().fillAmount = s.GetButton.GetComponent<Image>().fillAmount;
            }
        }
    }
}
