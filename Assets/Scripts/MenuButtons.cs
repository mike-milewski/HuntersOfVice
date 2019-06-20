using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public static MenuButtons Instance = null;

    private void Awake()
    {
        #region Singleton
        Instance = this;
        #endregion
    }

    public void NewGame(GameObject Loadingbar)
    {
        Loadingbar.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        Debug.Log("Return");
        FadeScreen.Instance.GetReturnToMenu = true;
        FadeScreen.Instance.GetFadeState = FadeState.FADEOUT;
    }

    public void ToggleMenuPanel(GameObject Panel)
    {
        if(!Panel.activeInHierarchy)
        {
            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }
}
