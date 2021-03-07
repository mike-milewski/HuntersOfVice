using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerPrefs.DeleteKey("SecretCharacterUnlocked");
    }
}
