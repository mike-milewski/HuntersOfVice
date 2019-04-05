using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float Speed, LookRotation;
    
    [SerializeField]
    private float JumpSpeed;

    [SerializeField]
    private Character character;

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
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Grounded())
            {
                Jump();
            }
        }
        */
    }

    private void Move()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        
        Vector3 Movement = new Vector3(direction.x, 0, direction.z);

        Movement = cam.transform.TransformDirection(Movement);
        Movement.y = 0.0f;

        if (Movement != Vector3.zero)
        {
            Quaternion LookDir = Quaternion.LookRotation(Movement);

            character.GetRigidbody.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookRotation * Time.deltaTime);
        }
        else
        {
            character.GetRigidbody.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.rotation, LookRotation * Time.deltaTime);
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
        }
    }

    private void Jump()
    {
        character.GetRigidbody.velocity = new Vector2(0, JumpSpeed * Time.deltaTime);
    }

    private bool Grounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        RaycastHit Hit;

        if(Physics.Raycast(ray, out Hit, 1))
        {
            if(Hit.collider.tag == "Ground")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
