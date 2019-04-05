using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void StartGame(GameObject Loadingbar)
    {
        Loadingbar.SetActive(true);
    }

    public void LoadGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
