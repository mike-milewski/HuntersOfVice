using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    private Transform Knight, ShadowPriest;

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

        if(CheckKnightActive())
        {
            KnightPos();

            transform.LookAt(Knight);
        }
        else if(CheckShadowPriestActive())
        {
            ShadowPriestPos();

            transform.LookAt(ShadowPriest);
        }
    }

    private bool CheckKnightActive()
    {
        return Knight.gameObject.activeInHierarchy;
    }

    private bool CheckShadowPriestActive()
    {
        return ShadowPriest.gameObject.activeInHierarchy;
    }

    private void KnightPos()
    {
        Vector3 newpos = Knight.position + Offset;

        transform.position = Vector3.Slerp(transform.position, newpos, SmoothFactor);
    }

    private void ShadowPriestPos()
    {
        Vector3 newpos = ShadowPriest.position + Offset;

        transform.position = Vector3.Slerp(transform.position, newpos, SmoothFactor);
    }
}
