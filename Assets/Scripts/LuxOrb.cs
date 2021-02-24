#pragma warning disable 0649
using UnityEngine;

public class LuxOrb : MonoBehaviour
{
    [SerializeField]
    private float DefaultSpeed, MinDetonateValue, MaxDetonateValue;

    [SerializeField]
    private Transform CenterPoint, OriginPoint;

    [SerializeField]
    private EnableGameObject enableGameObject;

    [SerializeField]
    private ObstacleDamageRadius obstacleDamageRadius;

    [SerializeField]
    private Settings settings;

    private float SetDetonateValue, Speed;

    private Vector3 Direction;

    private void OnEnable()
    {
        SetDetonateValue = Random.Range(MinDetonateValue, MaxDetonateValue);

        obstacleDamageRadius.GetIsInRadius = false;

        Speed = DefaultSpeed;

        transform.position = OriginPoint.position;

        SpawnAppearParticle();
    }

    private void Update()
    {
        Move();

        SetDetonatorTime();
    }

    private void SpawnAppearParticle()
    {
        if(settings.UseParticleEffects)
        {
            var particle = ObjectPooler.Instance.GetEnemyAppearParticle();

            particle.SetActive(true);

            particle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void Move()
    {
        Direction = new Vector3(CenterPoint.position.x - transform.position.x, 0, CenterPoint.position.z - transform.position.z).normalized;

        transform.position += Direction * Speed * Time.deltaTime;
    }

    private void SetDetonatorTime()
    {
        SetDetonateValue -= Time.deltaTime;

        if (SetDetonateValue <= 0)
        {
            Speed = 0;

            SetDetonation();
        }
    }

    private void SetDetonation()
    {
        obstacleDamageRadius.enabled = true;
    }

    public void Detonate()
    {
        obstacleDamageRadius.enabled = false;
        enableGameObject.enabled = true;
        gameObject.SetActive(false);
    }
}
