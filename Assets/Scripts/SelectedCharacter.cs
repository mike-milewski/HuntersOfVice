#pragma warning disable 0649
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedCharacter : MonoBehaviour
{
    public static SelectedCharacter instance = null;

    [SerializeField]
    private bool KnightSelected, ShadowPriestSelected;

    private Scene scene;

    public bool GetKnightSelected
    {
        get
        {
            return KnightSelected;
        }
        set
        {
            KnightSelected = value;
        }
    }

    public bool GetShadowPriestSelected
    {
        get
        {
            return ShadowPriestSelected;
        }
        set
        {
            ShadowPriestSelected = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if(scene.buildIndex == 0)
        {
            KnightSelected = false;
            ShadowPriestSelected = false;
        }
        else
        {
            if(KnightSelected)
            {
                GameManager.Instance.GetKnight.SetActive(true);
                GameManager.Instance.GetShadowPriest.SetActive(false);
            }
            else if(ShadowPriestSelected)
            {
                GameManager.Instance.GetShadowPriest.SetActive(true);
                GameManager.Instance.GetKnight.SetActive(false);
            }
        }
    }
}
