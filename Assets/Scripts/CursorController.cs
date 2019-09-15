using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance = null;

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
        ChangeCursor();

        if(Input.GetMouseButtonDown(0))
        OpenTreasureChest();
    }

    private void OpenTreasureChest()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, ActionMouseRange))
        {
            if(hit.collider.GetComponent<TreasureChest>())
            {
                hit.collider.GetComponent<TreasureChest>().GetAnimator.SetBool("OpenChest", true);
                hit.collider.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void ChangeCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

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
