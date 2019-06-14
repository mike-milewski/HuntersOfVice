using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance = null;

    [SerializeField]
    private Texture2D DefaultCursor;

    [SerializeField]
    private Texture2D AttackCursor;

    [SerializeField]
    private float MouseRange;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, MouseRange))
        {
            if(hit.collider.GetComponent<Enemy>())
            {
                SetAttackCursor();
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
}
