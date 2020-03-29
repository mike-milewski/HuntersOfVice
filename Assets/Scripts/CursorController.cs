#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance = null;

    private Ray ray;

    private RaycastHit hit;

    [SerializeField]
    private Texture2D DefaultCursor;

    [SerializeField]
    private Texture2D AttackCursor, SpeechCursor, ActionCursor;

    [SerializeField]
    private float EnemyMouseRange, ActionMouseRange, SpeechMouseRange;

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        SetDefaultCursor();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, EnemyMouseRange))
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                CursorController.Instance.SetAttackCursor();
            }
            else
            {
                SetDefaultCursor();
            }
        }

        //ChangeCursor();

        if (Input.GetMouseButtonDown(0))
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
                hit.collider.GetComponent<TreasureChest>().GetAnimator.SetBool("OpenChest", true);
            }
        }

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            
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
                    CursorController.Instance.SetAttackCursor();
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
                    CursorController.Instance.SetActionCursor();
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
                    CursorController.Instance.SetSpeechCursor();
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
