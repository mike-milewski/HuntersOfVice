using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private Items[] item;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Z) && item[0].GetButton.interactable)
        {
            item[0].GetButton.onClick.Invoke();
        }
        if(Input.GetKey(KeyCode.X) && item[1].GetButton.interactable)
        {
            item[1].GetButton.onClick.Invoke();
        }
    }
}
