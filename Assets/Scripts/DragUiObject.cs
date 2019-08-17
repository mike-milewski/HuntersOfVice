using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ObjectType { Skill, Weapon, Armor }

public class DragUiObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private UiDropZone zone;

    [SerializeField]
    private Transform ParentObj, SkillMenuParent;

    [SerializeField]
    private ObjectType objectType;

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

    public UiDropZone GetDropZone
    {
        get
        {
            return zone;
        }
        set
        {
            zone = value;
        }
    }

    public ObjectType GetObjectType
    {
        get
        {
            return objectType;
        }
        set
        {
            objectType = value;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(objectType == ObjectType.Skill)
        {
            PlaceHolder = new GameObject();
            PlaceHolder.transform.SetParent(zone.transform, true);

            RectTransform RectTrans = PlaceHolder.AddComponent<RectTransform>();

            RectTrans.sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);

            PlaceHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());

            SkillsManager.Instance.AllSkillsBeingDragged();
        }
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        foreach (Mask m in gameObject.GetComponentsInChildren<Mask>())
        {
            m.showMaskGraphic = true;
        }
        foreach (Image i in gameObject.GetComponentsInChildren<Image>())
        {
            i.raycastTarget = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(ParentObj, true);
        transform.position = eventData.position;

        if(objectType == ObjectType.Skill)
        {
            int NewSiblingIndex = zone.transform.childCount;

            for (int i = 0; i < zone.transform.childCount; i++)
            {
                if (transform.position.x < zone.transform.GetChild(i).position.x)
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(gameObject.transform.parent != zone.transform)
        {
            if(objectType == ObjectType.Skill)
            {
                if (gameObject.GetComponent<Skills>().GetStatusIcon != null)
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
                gameObject.GetComponent<Button>().enabled = false;
                gameObject.GetComponent<Mask>().showMaskGraphic = false;
                gameObject.GetComponent<Image>().raycastTarget = false;
                foreach (Mask m in gameObject.GetComponentsInChildren<Mask>())
                {
                    m.showMaskGraphic = false;
                }
            }
            gameObject.transform.SetParent(SkillMenuParent, true);
            gameObject.transform.position = new Vector2(SkillMenuParent.transform.position.x, SkillMenuParent.transform.position.y);

            ResetRectTransform();

            if (!GameManager.Instance.GetSkillPanel.GetComponent<Image>().enabled)
            {
                gameObject.GetComponent<Mask>().showMaskGraphic = false;
                gameObject.GetComponent<Skills>().GetCoolDownImage.GetComponent<Mask>().showMaskGraphic = false;
                gameObject.GetComponent<Image>().raycastTarget = false;
            }

            if(objectType == ObjectType.Skill)
            {
                SkillsManager.Instance.ClearSkills();
                SkillsManager.Instance.AddSkillsToList();
                SkillsManager.Instance.AllSkillsNotBeingDragged();
            }
        }
        else
        {
            if(objectType == ObjectType.Skill)
            {
                if (!gameObject.GetComponent<Button>().enabled)
                {
                    gameObject.GetComponent<Button>().enabled = true;
                }
                transform.SetSiblingIndex(PlaceHolder.transform.GetSiblingIndex());
                CheckForSameSkills(gameObject.GetComponent<Skills>());

                SkillsManager.Instance.ClearSkills();
                SkillsManager.Instance.AddSkillsToList();

            }
            SetRectTransform();
        }
        if(objectType == ObjectType.Skill)
        {
            SkillsManager.Instance.AllSkillsNotBeingDragged();

            Destroy(PlaceHolder);
        }
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

    public void SetRectTransform()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38, 36.97f);
        gameObject.transform.localScale = new Vector3(0.94f, 0.85f, 1);
    }

    public void ResetRectTransform()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(38, 38);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
