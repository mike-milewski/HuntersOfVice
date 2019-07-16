using UnityEngine;
using System.Collections;

public enum ParticleEffect { HitParticle, LevelupParticle, WhirlwindSlashParticle }

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
    [SerializeField]
    private ParticleEffect particleEffect;

    [SerializeField]
    private ParticleSystem ps;

    [SerializeField]
    private float Duration;

    private void Awake()
    {
        ps = this.GetComponent<ParticleSystem>();   
    }

    private void OnEnable()
    {
        if(Duration > -1)
        StartCoroutine(CheckIfAlive());
    }

    private IEnumerator CheckIfAlive ()
	{
        yield return new WaitForSeconds(Duration);
        ps.transform.localScale = new Vector3(1, 1, 1);
        CheckParticleType();
    }

    private void CheckParticleType()
    {
        switch(particleEffect)
        {
            case (ParticleEffect.HitParticle):
                ObjectPooler.Instance.ReturnHitParticleToPool(gameObject);
                break;
            case (ParticleEffect.WhirlwindSlashParticle):
                ObjectPooler.Instance.ReturnWhirlwindSlashParticleToPool(gameObject);
                break;
            case (ParticleEffect.LevelupParticle):
                Destroy(gameObject);
                break;
        }
    }
}
