using UnityEngine;
using System.Collections;

public enum ParticleEffect { HitParticle, LevelupParticle, WhirlwindSlashParticle, Heal, CastParticle, EnemyCastParticle, PoisonSpore, HpItem, MpItem,
                             StrengthUp, RemoveStatus, StunningStinger, Illumination, Hop, GaiasProwess, SylvanBlessing, Slam, Slag, SylvanStorm, EnemyAppear,
                             VicePlanter }

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
            case (ParticleEffect.PoisonSpore):
                ObjectPooler.Instance.ReturnPoisonSporeParticleToPool(gameObject);
                break;
            case (ParticleEffect.Heal):
                ObjectPooler.Instance.ReturnHealParticleToPool(gameObject);
                break;
            case (ParticleEffect.CastParticle):
                ObjectPooler.Instance.ReturnPlayerCastParticleToPool(gameObject);
                break;
            case (ParticleEffect.EnemyCastParticle):
                ObjectPooler.Instance.ReturnEnemyCastParticleToPool(gameObject);
                break;
            case (ParticleEffect.HpItem):
                ObjectPooler.Instance.ReturnHpItemParticleToPool(gameObject);
                break;
            case (ParticleEffect.MpItem):
                ObjectPooler.Instance.ReturnMpItemParticleToPool(gameObject);
                break;
            case (ParticleEffect.StrengthUp):
                ObjectPooler.Instance.ReturnStrengthUpParticleToPool(gameObject);
                break;
            case (ParticleEffect.RemoveStatus):
                ObjectPooler.Instance.ReturnRemoveStatusParticleToPool(gameObject);
                break;
            case (ParticleEffect.StunningStinger):
                ObjectPooler.Instance.ReturnStunningStingerParticleToPool(gameObject);
                break;
            case (ParticleEffect.Illumination):
                ObjectPooler.Instance.ReturnIlluminationEffectParticleToPool(gameObject);
                break;
            case (ParticleEffect.Hop):
                ObjectPooler.Instance.ReturnHopParticleToPool(gameObject);
                break;
            case (ParticleEffect.GaiasProwess):
                ObjectPooler.Instance.ReturnGaiasProwessParticleToPool(gameObject);
                break;
            case (ParticleEffect.SylvanBlessing):
                ObjectPooler.Instance.ReturnSylvanBlessingParticleToPool(gameObject);
                break;
            case (ParticleEffect.Slam):
                ObjectPooler.Instance.ReturnSlamParticleToPool(gameObject);
                break;
            case (ParticleEffect.Slag):
                ObjectPooler.Instance.ReturnSlagParticleToPool(gameObject);
                break;
            case (ParticleEffect.SylvanStorm):
                ObjectPooler.Instance.ReturnSylvanStormParticleToPool(gameObject);
                break;
            case (ParticleEffect.EnemyAppear):
                ObjectPooler.Instance.ReturnEnemyAppearParticleToPool(gameObject);
                break;
            case (ParticleEffect.VicePlanter):
                ObjectPooler.Instance.ReturnVicePlanterParticleToPool(gameObject);
                break;
        }
    }
}
