#pragma warning disable 0649
using UnityEngine;

public class NetherStar : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;
    
    private void Update()
    {
        transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
    }
}
