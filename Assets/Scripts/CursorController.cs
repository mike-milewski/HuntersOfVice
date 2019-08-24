using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance = null;

    [SerializeField]
    private Texture2D DefaultCursor;

    [SerializeField]
    private Texture2D AttackCursor;

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
        if(Input.GetMouseButtonDown(0))
        SearchForTreasureChest();
    }

    private void SearchForTreasureChest()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 25))
        {
            if(hit.collider.GetComponent<TreasureChest>())
            {
                hit.collider.GetComponent<TreasureChest>().GetAnimator.SetBool("OpenChest", true);
                hit.collider.GetComponent<BoxCollider>().enabled = false;
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
}
