#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private Items[] item;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(item[0].GetButton.interactable)
            {
                item[0].GetButton.onClick.Invoke();
            }
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            if(item[1].GetButton.interactable)
            {
                item[1].GetButton.onClick.Invoke();
            }
            else
            {
                GameManager.Instance.CannotExecuteText();
            }
        }
    }
}
