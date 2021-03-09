using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerPrefs : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted");
    }
}
