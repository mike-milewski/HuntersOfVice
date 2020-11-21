#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    private Ray ray;

    private RaycastHit hit;

    [SerializeField]
    private Texture2D DefaultCursor;

    [SerializeField]
    private Texture2D AttackCursor, SpeechCursor, ActionCursor;

    [SerializeField]
    private float EnemyMouseRange, ActionMouseRange, SpeechMouseRange;

    [SerializeField]
    private int SceneIndex;

    private Scene scene;

    private void OnEnable()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Awake()
    {
        SetDefaultCursor();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, EnemyMouseRange))
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                SetAttackCursor();
            }
            else
            {
                SetDefaultCursor();
            }
        }
        else if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (!hit.collider.GetComponent<Enemy>())
            {
                SetDefaultCursor();
            }  
        }

        if (Input.GetMouseButtonDown(0))
        {
            CheckScene();
        }
    }

    private void CheckScene()
    {
        if (SceneIndex == 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.GetComponent<Animator>())
                {
                    if (hit.collider.GetComponent<CharacterSelector>().GetCharacterClass == "Knight")
                    {
                        if (!hit.collider.GetComponent<CharacterSelector>().GetSelectedCharacter.GetKnightSelected)
                        {
                            hit.collider.GetComponent<Animator>().SetBool("CharacterSelection", true);
                        }

                        hit.collider.GetComponent<CharacterSelector>().GetSelectedCharacter.GetKnightSelected = true;
                        hit.collider.GetComponent<CharacterSelector>().GetSelectedCharacter.GetShadowPriestSelected = false;

                        hit.collider.GetComponent<CharacterSelector>().PlayPanelAndButtonAnimations();
                        hit.collider.GetComponent<CharacterSelector>().ShowCharacterInformation();

                        hit.collider.GetComponent<CharacterSelector>().ShowCharacterSkills();

                        hit.collider.GetComponent<BoxCollider>().enabled = false;
                    }
                    else if (hit.collider.GetComponent<CharacterSelector>().GetCharacterClass == "Shadow Priest")
                    {
                        if (!hit.collider.GetComponent<CharacterSelector>().GetSelectedCharacter.GetShadowPriestSelected)
                        {
                            hit.collider.GetComponent<Animator>().SetBool("CharacterSelection", true);
                        }

                        hit.collider.GetComponent<CharacterSelector>().GetSelectedCharacter.GetShadowPriestSelected = true;
                        hit.collider.GetComponent<CharacterSelector>().GetSelectedCharacter.GetKnightSelected = false;

                        hit.collider.GetComponent<CharacterSelector>().PlayPanelAndButtonAnimations();
                        hit.collider.GetComponent<CharacterSelector>().ShowCharacterInformation();

                        hit.collider.GetComponent<CharacterSelector>().ShowCharacterSkills();

                        hit.collider.GetComponent<BoxCollider>().enabled = false;
                    }
                }
            }
        }
        else
        {
            OpenTreasureChest();
            OpenShopMenu();
        }
    }

    private void OpenTreasureChest()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, ActionMouseRange))
        {
            if (hit.collider.GetComponent<TreasureChest>())
            {
                if(GameManager.Instance.GetKnight.activeInHierarchy)
                {
                    if(hit.collider.GetComponent<TreasureChest>().GetEquipment[0].GetEquipmentType == EquipmentType.Weapon)
                    {
                        if(hit.collider.GetComponent<TreasureChest>().GetEquipment[0].GetComponent<DragUiObject>().GetMenuParent.childCount >= 
                           GameManager.Instance.GetEquipmentMenu.GetMaxWeapons)
                        {
                            GameManager.Instance.MaxWeaponsReachedText();
                        }
                        else
                        {
                            hit.collider.GetComponent<TreasureChest>().GetAnimator.SetBool("OpenChest", true);
                        }
                    }

                    if(hit.collider.GetComponent<TreasureChest>().GetEquipment[0].GetEquipmentType == EquipmentType.Armor)
                    {
                        if (hit.collider.GetComponent<TreasureChest>().GetEquipment[0].GetComponent<DragUiObject>().GetMenuParent.childCount >=
                           GameManager.Instance.GetEquipmentMenu.GetMaxArmor)
                        {
                            GameManager.Instance.MaxArmorReachedText();
                        }
                        else
                        {
                            hit.collider.GetComponent<TreasureChest>().GetAnimator.SetBool("OpenChest", true);
                        }
                    }
                }

                if(GameManager.Instance.GetShadowPriest.activeInHierarchy)
                {
                    if (hit.collider.GetComponent<TreasureChest>().GetEquipment[1].GetEquipmentType == EquipmentType.Weapon)
                    {
                        if (hit.collider.GetComponent<TreasureChest>().GetEquipment[1].GetComponent<DragUiObject>().GetMenuParent.childCount >=
                           GameManager.Instance.GetEquipmentMenu.GetMaxWeapons)
                        {
                            GameManager.Instance.MaxWeaponsReachedText();
                        }
                        else
                        {
                            hit.collider.GetComponent<TreasureChest>().GetAnimator.SetBool("OpenChest", true);
                        }
                    }

                    if (hit.collider.GetComponent<TreasureChest>().GetEquipment[1].GetEquipmentType == EquipmentType.Armor)
                    {
                        if (hit.collider.GetComponent<TreasureChest>().GetEquipment[1].GetComponent<DragUiObject>().GetMenuParent.childCount >=
                           GameManager.Instance.GetEquipmentMenu.GetMaxArmor)
                        {
                            GameManager.Instance.MaxArmorReachedText();
                        }
                        else
                        {
                            hit.collider.GetComponent<TreasureChest>().GetAnimator.SetBool("OpenChest", true);
                        }
                    }
                }
            }
        }
    }

    private void OpenShopMenu()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, SpeechMouseRange))
            {
                if (hit.collider.GetComponent<ShopKeeper>())
                {
                    SoundManager.Instance.Menu();
                    hit.collider.GetComponent<ShopKeeper>().GetIsInShop = true;
                    hit.collider.GetComponent<ShopKeeper>().GetAnimator.SetBool("FadeIn", true);
                }
            }
        }
    }

    private void ChangeCursor()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, EnemyMouseRange))
            {
                if (hit.collider.GetComponent<Enemy>())
                {
                    SetAttackCursor();
                }
                else
                {
                    SetDefaultCursor();
                }
            }
            if (Physics.Raycast(ray, out hit, ActionMouseRange))
            {
                if (hit.collider.GetComponent<TreasureChest>())
                {
                    SetActionCursor();
                }
                else
                {
                    SetDefaultCursor();
                }
            }
            if (Physics.Raycast(ray, out hit, SpeechMouseRange))
            {
                if (hit.collider.GetComponent<ShopKeeper>())
                {
                    SetSpeechCursor();
                }
                else
                {
                    SetDefaultCursor();
                }
            }
        }
        else
        {
            SetDefaultCursor();
        }
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetAttackCursor()
    {
        Cursor.SetCursor(AttackCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetSpeechCursor()
    {
        Cursor.SetCursor(SpeechCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetActionCursor()
    {
        Cursor.SetCursor(ActionCursor, Vector2.zero, CursorMode.Auto);
    }
}
