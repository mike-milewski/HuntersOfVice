#pragma warning disable 0649
using UnityEngine;
using System.Collections;

public enum ParticleEffect { HitParticle, LevelupParticle, WhirlwindSlashParticle, Heal, CastParticle, EnemyCastParticle, PoisonSpore, HpItem, MpItem,
                             StrengthUp, RemoveStatus, StunningStinger, Illumination, Hop, GaiasProwess, SylvanBlessing, Slam, Slag, SylvanStorm, EnemyAppear,
                             VicePlanter, Shatter, Alleviate, Contract, Aegis, BraveLight, SinisterPossession, DiabolicLightning, SoulPierce, NetherStar,
                             NetherStarExplosion, Uplift, ManaPulse, AquaBullet, MiasmaPulse, StatueLaser, LaserExplosion, AlphaSpore, BetaSpore, GammaSpore, 
                             DisasterSpore, IronCap, Quickness, MildewSplash, ConfusionBreath, Harrow, Shock, Wind, Burn, Dark, Light }

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
            case (ParticleEffect.Shatter):
                ObjectPooler.Instance.ReturnShatterParticleToPool(gameObject);
                break;
            case (ParticleEffect.Alleviate):
                ObjectPooler.Instance.ReturnAlleviateParticleToPool(gameObject);
                break;
            case (ParticleEffect.Aegis):
                ObjectPooler.Instance.ReturnAegisParticleToPool(gameObject);
                break;
            case (ParticleEffect.BraveLight):
                ObjectPooler.Instance.ReturnBraveWingParticleToPool(gameObject);
                break;
            case (ParticleEffect.SinisterPossession):
                ObjectPooler.Instance.ReturnSinisterPossessionParticleToPool(gameObject);
                break;
            case (ParticleEffect.Contract):
                ObjectPooler.Instance.ReturnContractParticleToPool(gameObject);
                break;
            case (ParticleEffect.DiabolicLightning):
                ObjectPooler.Instance.ReturnDiabolicLightningParticleToPool(gameObject);
                break;
            case (ParticleEffect.SoulPierce):
                ObjectPooler.Instance.ReturnSoulPierceParticleToPool(gameObject);
                break;
            case (ParticleEffect.NetherStar):
                ObjectPooler.Instance.ReturnNetherStarParticleToPool(gameObject);
                break;
            case (ParticleEffect.NetherStarExplosion):
                ObjectPooler.Instance.ReturnNetherStarExplosionParticleToPool(gameObject);
                break;
            case (ParticleEffect.Uplift):
                ObjectPooler.Instance.ReturnUpliftParticleToPool(gameObject);
                break;
            case (ParticleEffect.ManaPulse):
                ObjectPooler.Instance.ReturnManaPulseParticleToPool(gameObject);
                break;
            case (ParticleEffect.AquaBullet):
                ObjectPooler.Instance.ReturnAquaBulletParticleToPool(gameObject);
                break;
            case (ParticleEffect.MiasmaPulse):
                ObjectPooler.Instance.ReturnMiasmaPulseParticleToPool(gameObject);
                break;
            case (ParticleEffect.StatueLaser):
                ObjectPooler.Instance.ReturnStatueLaserParticleToPool(gameObject);
                break;
            case (ParticleEffect.LaserExplosion):
                ObjectPooler.Instance.ReturnLaserExplosionParticleToPool(gameObject);
                break;
            case (ParticleEffect.AlphaSpore):
                ObjectPooler.Instance.ReturnAlphaSporeParticleToPool(gameObject);
                break;
            case (ParticleEffect.BetaSpore):
                ObjectPooler.Instance.ReturnBetaSporeParticleToPool(gameObject);
                break;
            case (ParticleEffect.GammaSpore):
                ObjectPooler.Instance.ReturnGammaSporeParticleToPool(gameObject);
                break;
            case (ParticleEffect.DisasterSpore):
                ObjectPooler.Instance.ReturnDisasterSporeParticleToPool(gameObject);
                break;
            case (ParticleEffect.IronCap):
                ObjectPooler.Instance.ReturnIronCapParticleToPool(gameObject);
                break;
            case (ParticleEffect.MildewSplash):
                ObjectPooler.Instance.ReturnMildewSplashParticleToPool(gameObject);
                break;
            case (ParticleEffect.Quickness):
                ObjectPooler.Instance.ReturnQuicknessParticleToPool(gameObject);
                break;
            case (ParticleEffect.ConfusionBreath):
                ObjectPooler.Instance.ReturnConfusionBreathParticleToPool(gameObject);
                break;
            case (ParticleEffect.Harrow):
                ObjectPooler.Instance.ReturnHarrowParticleToPool(gameObject);
                break;
            case (ParticleEffect.Shock):
                ObjectPooler.Instance.ReturnShockParticleToPool(gameObject);
                break;
            case (ParticleEffect.Wind):
                ObjectPooler.Instance.ReturnWindParticleToPool(gameObject);
                break;
            case (ParticleEffect.Burn):
                ObjectPooler.Instance.ReturnBurnParticleToPool(gameObject);
                break;
            case (ParticleEffect.Dark):
                ObjectPooler.Instance.ReturnDarkParticleToPool(gameObject);
                break;
            case (ParticleEffect.Light):
                ObjectPooler.Instance.ReturnLightParticleToPool(gameObject);
                break;
        }
    }
}
