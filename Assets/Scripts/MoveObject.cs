#pragma warning disable 0649
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private float Speed, DisappearValue;

    [SerializeField]
    private Transform CenterPoint, OriginPoint;

    [SerializeField]
    private EnableGameObject enableGameObject;

    [SerializeField]
    private Settings settings;

    private Vector3 Direction;

    private float SetDisappearValue;

    private void OnEnable()
    {
        transform.position = OriginPoint.position;

        SetDisappearValue = DisappearValue;
    }

    private void Update()
    {
        Move();

        Disappear();
    }

    private void AppearParticle()
    {
        if (settings.UseParticleEffects)
        {
            var Appear = ObjectPooler.Instance.GetSoothingOrbParticle();

            Appear.SetActive(true);

            Appear.transform.position = new Vector3(transform.position.x, transform.transform.position.y + 0.3f, transform.position.z);
        }
    }

    private void Move()
    {
        Direction = new Vector3(CenterPoint.position.x - transform.position.x, 0, CenterPoint.position.z - transform.position.z).normalized;

        transform.position += Direction * Speed * Time.deltaTime;
    }

    private void Disappear()
    {
        SetDisappearValue -= Time.deltaTime;
        if (SetDisappearValue <= 0)
        {
            AppearParticle();
            enableGameObject.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
