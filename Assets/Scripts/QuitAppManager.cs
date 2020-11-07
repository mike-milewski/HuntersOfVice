using UnityEngine;

public class QuitAppManager : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        GameManager.Instance.GetCharacter.DefaultStats();
    }
}
