#pragma warning disable 0649
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private CursorController cursorController;

    [SerializeField]
    private GameObject PausedText, SkillPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(Time.timeScale == 1)
            {
                PausedText.SetActive(true);
                cursorController.enabled = false;
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                CheckDrag();
                cursorController.enabled = true;
                PausedText.SetActive(false);
            }
        }
    }

    private void CheckDrag()
    {
        foreach(DragUiObject duio in SkillPanel.GetComponentsInChildren<DragUiObject>())
        {
            duio.CheckObjectDrag();
        }
    }

    public void DisablePause()
    {
        if(Time.timeScale == 1)
        {
            gameObject.SetActive(false);
        }
    }
}