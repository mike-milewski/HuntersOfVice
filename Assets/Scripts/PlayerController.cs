#pragma warning disable 0649
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

    private bool ControlsReversed;
    
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

    public bool GetIsReversed
    {
        get
        {
            return ControlsReversed;
        }
        set
        {
            ControlsReversed = value;
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
        if(!ControlsReversed)
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.z = Input.GetAxis("Vertical");
        }
        else
        {
            direction.x = Input.GetAxis("ReversedHorizontal");
            direction.z = Input.GetAxis("ReversedVertical");
        }
        
        Movement = new Vector3(direction.x, 0, direction.z);

        Movement = cam.transform.TransformDirection(Movement);
        Movement.y = 0.0f;

        if (Movement != Vector3.zero)
        {
            if(!SkillsManager.Instance.GetWhirlwind)
            {
                Quaternion Look = Quaternion.LookRotation(Movement);
                Quaternion LookDir = Look;

                Quaternion characterRotation = Quaternion.Slerp(this.transform.rotation, LookDir, LookRotation * Time.deltaTime);

                character.transform.rotation = characterRotation;
            }
        }

        character.GetRigidbody.transform.position += Movement * character.GetMoveSpeed * Time.deltaTime;

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

    public void SetMoveToFalse()
    {
        animator.SetFloat("Speed", 0);
        direction = Vector3.zero;
    }
}
