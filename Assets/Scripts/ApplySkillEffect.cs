using UnityEngine;

public class ApplySkillEffect : MonoBehaviour
{
    private ParticleSystem particlesystem;

    [SerializeField]
    private float ApplyTime;

    private void Awake()
    {
        particlesystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        Invoke("ApplyEffect", ApplyTime);
    }

    private void ApplyEffect()
    {

    }
}
