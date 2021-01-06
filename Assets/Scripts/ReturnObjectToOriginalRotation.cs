#pragma warning disable 0649
using UnityEngine;

public class ReturnObjectToOriginalRotation : MonoBehaviour
{
    [SerializeField]
    private StatueObstacle statueObstacle;

    [SerializeField]
    private float RotationSpeed;

    private Quaternion LookDirection, DefaultDirection;

    private Vector3 Distance;

    public Quaternion GetDefaultDirection
    {
        get
        {
            return DefaultDirection;
        }
        set
        {
            DefaultDirection = value;
        }
    }

    private void Update()
    {
        Distance = new Vector3(transform.position.x, transform.position.y - DefaultDirection.y, transform.position.z);

        LookDirection = Quaternion.LookRotation(Distance);

        transform.rotation = Quaternion.Slerp(transform.rotation, LookDirection, RotationSpeed * Time.deltaTime);

        if(transform.rotation == LookDirection)
        {
            statueObstacle.DisableAudio();
            this.enabled = false;
        }
    }
}
