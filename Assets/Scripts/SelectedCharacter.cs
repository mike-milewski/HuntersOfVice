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
}
