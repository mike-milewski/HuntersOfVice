#pragma warning disable 0649
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ObjectType { Skill, Weapon, Armor }

public class DragUiObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private UiDropZone zone;

    [SerializeField]
    private Transform ParentObj, MenuParent;

    [SerializeField]
    private float RectSizeX, RectSizeY;

    [SerializeField]
    private ObjectType objectType;

    private GameObject PlaceHolder = null;

    private bool Dragging;

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

    public Transform GetMenuParent
    {
        get
        {
            return MenuParent;
        }
        set
        {
            MenuParent = value;
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
        if(Time.timeScale == 1 && !Dragging)
        {
            Dragging = true;

            if (objectType == ObjectType.Skill)
            {
                PlaceHolder = new GameObject();
                PlaceHolder.transform.SetParent(zone.transform, true);

                RectTransform RectTrans = PlaceHolder.AddComponent<RectTransform>();

                RectTrans.sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<RectTransform>().sizeDelta.y);

                PlaceHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());

                SkillsManager.Instance.AllSkillsBeingDragged();
            }
            if (objectType == ObjectType.Weapon || objectType == ObjectType.Armor)
            {
                if (gameObject.transform.GetComponentInParent<EquippedCheck>())
                {
                    gameObject.GetComponent<Equipment>().UnEquip();
                }
                if (zone.GetComponentInChildren<DragUiObject>())
                {
                    if (zone.GetComponent<GridLayoutGroup>())
                    {
                        zone.GetComponentInChildren<DragUiObject>().GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                    else
                    {
                        zone.GetComponentInChildren<DragUiObject>().GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }
                }
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Dragging)
        {
            gameObject.transform.SetParent(ParentObj, true);
            transform.position = eventData.position;

            if (objectType == ObjectType.Skill)
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Dragging)
        {
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (gameObject.transform.parent != zone.transform)
            {
                if (objectType == ObjectType.Skill)
                {
                    if (gameObject.GetComponent<Skills>().GetStatusIcon != null)
                    {
                        if (gameObject.GetComponent<Skills>().GetStatusIcon.activeInHierarchy)
                        {
                            if (gameObject.GetComponent<Skills>().GetStatusIcon.GetComponent<StatusIcon>())
                            {
                                gameObject.GetComponent<Skills>().GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
                                if (GameManager.Instance.GetShadowPriest.activeInHierarchy)
                                {
                                    SkillsManager.Instance.GetContractSkill = null;
                                    SkillsManager.Instance.GetContractStack--;
                                }

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
                if (objectType == ObjectType.Weapon)
                {
                    if (MenuParent.childCount >= GameManager.Instance.GetEquipmentMenu.GetMaxWeapons)
                    {
                        gameObject.transform.SetParent(zone.transform, true);
                        gameObject.GetComponent<Equipment>().Equip();
                    }
                    else
                    {
                        gameObject.transform.SetParent(MenuParent, true);
                        gameObject.transform.position = new Vector2(MenuParent.transform.position.x, MenuParent.transform.position.y);
                    }
                }
                if (objectType == ObjectType.Armor)
                {
                    if (MenuParent.childCount >= GameManager.Instance.GetEquipmentMenu.GetMaxArmor)
                    {
                        gameObject.transform.SetParent(zone.transform, true);
                        gameObject.GetComponent<Equipment>().Equip();
                    }
                    else
                    {
                        gameObject.transform.SetParent(MenuParent, true);
                        gameObject.transform.position = new Vector2(MenuParent.transform.position.x, MenuParent.transform.position.y);
                    }
                }
                if (objectType == ObjectType.Skill)
                {
                    gameObject.transform.SetParent(MenuParent, true);
                    gameObject.transform.position = new Vector2(MenuParent.transform.position.x, MenuParent.transform.position.y);
                }

                ResetRectTransform();

                if (!GameManager.Instance.GetSkillPanel.GetComponent<Image>().enabled)
                {
                    gameObject.GetComponent<Mask>().showMaskGraphic = false;
                    gameObject.GetComponent<Skills>().GetCoolDownImage.GetComponent<Mask>().showMaskGraphic = false;
                    gameObject.GetComponent<Image>().raycastTarget = false;
                }

                if (objectType == ObjectType.Skill)
                {
                    SkillsManager.Instance.ClearSkills();
                    SkillsManager.Instance.AddSkillsToList();
                    SkillsManager.Instance.AllSkillsNotBeingDragged();
                }
                if (objectType == ObjectType.Weapon || objectType == ObjectType.Armor)
                {
                    SoundManager.Instance.UnEquipItem();
                    if (zone.GetComponentInChildren<DragUiObject>())
                    {
                        zone.GetComponentInChildren<DragUiObject>().GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                    if (!GameManager.Instance.GetEquipmentToggle)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if (objectType == ObjectType.Skill)
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
            if (objectType == ObjectType.Skill)
            {
                SkillsManager.Instance.AllSkillsNotBeingDragged();

                Destroy(PlaceHolder);
            }
            Dragging = false;
        }
    }

    public void CheckObjectDrag()
    {
        if(Dragging)
        {
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (gameObject.transform.parent != zone.transform)
            {
                if (objectType == ObjectType.Skill)
                {
                    if (gameObject.GetComponent<Skills>().GetStatusIcon != null)
                    {
                        if (gameObject.GetComponent<Skills>().GetStatusIcon.activeInHierarchy)
                        {
                            if (gameObject.GetComponent<Skills>().GetStatusIcon.GetComponent<StatusIcon>())
                            {
                                gameObject.GetComponent<Skills>().GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
                                if (GameManager.Instance.GetShadowPriest.activeInHierarchy)
                                {
                                    SkillsManager.Instance.GetContractSkill = null;
                                    SkillsManager.Instance.GetContractStack--;
                                }

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
                if (objectType == ObjectType.Weapon)
                {
                    if (MenuParent.childCount >= GameManager.Instance.GetEquipmentMenu.GetMaxWeapons)
                    {
                        gameObject.transform.SetParent(zone.transform, true);
                        gameObject.GetComponent<Equipment>().Equip();
                    }
                    else
                    {
                        gameObject.transform.SetParent(MenuParent, true);
                        gameObject.transform.position = new Vector2(MenuParent.transform.position.x, MenuParent.transform.position.y);
                    }
                }
                if (objectType == ObjectType.Armor)
                {
                    if (MenuParent.childCount >= GameManager.Instance.GetEquipmentMenu.GetMaxArmor)
                    {
                        gameObject.transform.SetParent(zone.transform, true);
                        gameObject.GetComponent<Equipment>().Equip();
                    }
                    else
                    {
                        gameObject.transform.SetParent(MenuParent, true);
                        gameObject.transform.position = new Vector2(MenuParent.transform.position.x, MenuParent.transform.position.y);
                    }
                }
                if (objectType == ObjectType.Skill)
                {
                    gameObject.transform.SetParent(MenuParent, true);
                    gameObject.transform.position = new Vector2(MenuParent.transform.position.x, MenuParent.transform.position.y);
                }

                ResetRectTransform();

                if (!GameManager.Instance.GetSkillPanel.GetComponent<Image>().enabled)
                {
                    gameObject.GetComponent<Mask>().showMaskGraphic = false;
                    gameObject.GetComponent<Skills>().GetCoolDownImage.GetComponent<Mask>().showMaskGraphic = false;
                    gameObject.GetComponent<Image>().raycastTarget = false;
                }

                if (objectType == ObjectType.Skill)
                {
                    SkillsManager.Instance.ClearSkills();
                    SkillsManager.Instance.AddSkillsToList();
                    SkillsManager.Instance.AllSkillsNotBeingDragged();
                }
                if (objectType == ObjectType.Weapon || objectType == ObjectType.Armor)
                {
                    SoundManager.Instance.UnEquipItem();
                    if (zone.GetComponentInChildren<DragUiObject>())
                    {
                        zone.GetComponentInChildren<DragUiObject>().GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                    if (!GameManager.Instance.GetEquipmentToggle)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if (objectType == ObjectType.Skill)
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
            if (objectType == ObjectType.Skill)
            {
                SkillsManager.Instance.AllSkillsNotBeingDragged();

                Destroy(PlaceHolder);
            }
            Dragging = false;
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
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(RectSizeX, RectSizeY);
        gameObject.transform.localScale = new Vector3(0.94f, 0.85f, 1);
    }

    public void ResetRectTransform()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(RectSizeX, RectSizeY);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}