#pragma warning disable 0649
using UnityEngine;

public class EnableToadstool : MonoBehaviour
{
    [SerializeField]
    private ToadstoolWalk toadStoolWalk;

    public void EnableToadstoolScript()
    {
        toadStoolWalk.enabled = true;
    }
}
