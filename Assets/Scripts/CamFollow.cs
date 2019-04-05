using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    private Transform Player;

    [SerializeField]
    private Vector3 Offset;

    [SerializeField]
    private float SmoothFactor, offsetY, offsetZ;

    private void LateUpdate()
    {
        if(Input.GetMouseButton(1))
        {
            Quaternion CamTurn = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 5, Vector3.up);

            Offset = CamTurn * Offset;
        }

        Vector3 newpos = Player.position + Offset;
        /*
        Offset.y = Mathf.Clamp(Offset.y, 0, 10);
        Offset.z = Mathf.Clamp(Offset.z, -8, 10);

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Offset.y += 1 * 4 * Time.deltaTime;
            Offset.z -= 1 * 4 * Time.deltaTime;
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Offset.y -= 1 * 4 * Time.deltaTime;
            Offset.z += 1 * 4 * Time.deltaTime;
        }
        */
        transform.position = Vector3.Slerp(transform.position, newpos, SmoothFactor);

        transform.LookAt(Player);
    }
}
