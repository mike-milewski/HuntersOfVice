using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Character character;

    private Vector3 direction = Vector3.zero;
    private Vector3 Movement;

    [SerializeField]
    private float Speed, LookRotation;
    
    public Vector3 GetMovement
    {
        get
        {
            return Movement;
        }
        set
        {
            Movement = value;
        }
    }

    public float GetSpeed
    {
        get
        {
            return Speed;
        }
        set
        {
            Speed = value;
        }
    }

    private void Reset()
    {
        character = GetComponent<Character>();
    }

    private void Awake()
    {
        character = GetComponent<Character>();

        animator = GetComponent<Animator>();

        cam.GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        
        Movement = new Vector3(direction.x, 0, direction.z);

        Movement = cam.transform.TransformDirection(Movement);
        Movement.y = 0.0f;

        if (Movement != Vector3.zero)
        {
            Quaternion Look = Quaternion.LookRotation(Movement);
            Quaternion LookDir = Look;

            character.GetRigidbody.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookRotation * Time.deltaTime);
        }

        character.GetRigidbody.transform.position += Movement * Speed * Time.deltaTime;

        direction.Normalize();

        if (direction.x > 0 || direction.z > 0 || direction.x < 0 || direction.z < 0)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            direction = Vector3.zero;
        }
    }
}
