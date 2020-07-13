#pragma warning disable 0649
using UnityEngine;

public class NoRotationHealthBar : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private void Awake()
    {
        cam.GetComponent<Camera>();
    }

    void LateUpdate ()
    {
        transform.LookAt(cam.transform);
	}
}
