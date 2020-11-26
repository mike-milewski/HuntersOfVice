#pragma warning disable 0649
using UnityEngine;

public class ReturnObjectToOriginalRotation : MonoBehaviour
{
    [SerializeField]
    private StatueObstacle statueObstacle;

    [SerializeField]
    private float RotationSpeed;

    private Quaternion LookDirection;

    private Vector3 Distance;

    private void Update()
    {
        Distance = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        LookDirection = Quaternion.LookRotation(Distance).normalized;

        statueObstacle.transform.rotation = Quaternion.Slerp(statueObstacle.transform.rotation, LookDirection, RotationSpeed * Time.deltaTime);

        if(transform.rotation == Quaternion.LookRotation(Distance).normalized)
        {
            statueObstacle.DisableAudio();
            this.enabled = false;
        }
    }
}
