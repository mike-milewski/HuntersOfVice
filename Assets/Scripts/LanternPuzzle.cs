#pragma warning disable 0649
using UnityEngine;

public class LanternPuzzle : MonoBehaviour
{
    [SerializeField]
    private GameObject LanternObject;

    public void TurnOnLantern()
    {
        LanternObject.SetActive(true);
    }
}
