#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PoolController
{
    private Queue<GameObject> PooledObject = new Queue<GameObject>();

    [SerializeField]
    private GameObject ObjectToPool;

    [SerializeField]
    private Transform PoolParent;

    [SerializeField]
    private int PoolAmount;

    public GameObject GetObjectToPool
    {
        get
        {
            return ObjectToPool;
        }
        set
        {
            ObjectToPool = value;
        }
    }

    public int GetPoolAmount
    {
        get
        {
            return PoolAmount;
        }
        set
        {
            PoolAmount = value;
        }
    }

    public Queue<GameObject> GetPooledObject
    {
        get
        {
            return PooledObject;
        }
        set
        {
            PooledObject = value;
        }
    }

    public Transform GetPoolParent
    {
        get
        {
            return PoolParent;
        }
        set
        {
            PoolParent = value;
        }
    }
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance = null;

    [SerializeField]
    private PoolController[] poolcontroller;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion

        AddTextForPlayerDamage(poolcontroller[0].GetPoolAmount);
        AddTextForEnemyDamage(poolcontroller[1].GetPoolAmount);
        AddTextForPlayerHeal(poolcontroller[2].GetPoolAmount);
        AddTextForPlayerStatusIcon(poolcontroller[3].GetPoolAmount);
        AddTextForEnemyHeal(poolcontroller[4].GetPoolAmount);
        AddTextForEnemyStatusIcon(poolcontroller[5].GetPoolAmount);
        AddPlayerStatusIcon(poolcontroller[6].GetPoolAmount);
        AddEnemyStatusIcon(poolcontroller[7].GetPoolAmount);
        AddExperienceText(poolcontroller[8].GetPoolAmount);
        AddHitParticle(poolcontroller[9].GetPoolAmount);
        AddPlayerCastParticle(poolcontroller[10].GetPoolAmount);
        AddEnemyCastParticle(poolcontroller[11].GetPoolAmount);
        AddWhirlWindSlashParticle(poolcontroller[12].GetPoolAmount);
        AddPoisonSporeParticle(poolcontroller[13].GetPoolAmount);
        AddHealParticle(poolcontroller[14].GetPoolAmount);
        AddLevelParticle(poolcontroller[15].GetPoolAmount);
        AddHpItemParticle(poolcontroller[16].GetPoolAmount);
        AddMpItemParticle(poolcontroller[17].GetPoolAmount);
        AddStrengthUpParticle(poolcontroller[18].GetPoolAmount);
        AddRemoveStatusParticle(poolcontroller[19].GetPoolAmount);
        AddLevelUpText(poolcontroller[20].GetPoolAmount);
        AddStunningStingerParticle(poolcontroller[21].GetPoolAmount);
        AddCoinText(poolcontroller[22].GetPoolAmount);
        AddItemMessage(poolcontroller[23].GetPoolAmount);
        AddPoisonEffectParticle(poolcontroller[24].GetPoolAmount);
        AddStunEffectParticle(poolcontroller[25].GetPoolAmount);
        AddIlluminationEffectParticle(poolcontroller[26].GetPoolAmount);
        AddHopParticle(poolcontroller[27].GetPoolAmount);
        AddGaiasProwessParticle(poolcontroller[28].GetPoolAmount);
        AddSylvanBlessingParticle(poolcontroller[29].GetPoolAmount);
        AddSlamParticle(poolcontroller[30].GetPoolAmount);
        AddMonsterEntryText(poolcontroller[31].GetPoolAmount);
        AddSlagParticle(poolcontroller[32].GetPoolAmount);
        AddSylvanStormParticle(poolcontroller[33].GetPoolAmount);
        AddEnemyAppearParticle(poolcontroller[34].GetPoolAmount);
        AddVicePlanterParticle(poolcontroller[35].GetPoolAmount);
        AddShatterParticle(poolcontroller[36].GetPoolAmount);
        AddAlleviateParticle(poolcontroller[37].GetPoolAmount);
        AddAegisParticle(poolcontroller[38].GetPoolAmount);
        AddBraveWingParticle(poolcontroller[39].GetPoolAmount);
        AddSinisterPossessionParticle(poolcontroller[40].GetPoolAmount);
        AddContractParticle(poolcontroller[41].GetPoolAmount);
        AddDiabolicLightningParticle(poolcontroller[42].GetPoolAmount);
        AddSoulPierceParticle(poolcontroller[43].GetPoolAmount);
        AddNetherStarParticle(poolcontroller[44].GetPoolAmount);
        AddNetherStarExplosionParticle(poolcontroller[45].GetPoolAmount);
        AddUpliftParticle(poolcontroller[46].GetPoolAmount);
        AddManaPulseParticle(poolcontroller[47].GetPoolAmount);
        AddBurningEffectParticle(poolcontroller[48].GetPoolAmount);
        AddAquaBulletParticle(poolcontroller[49].GetPoolAmount);
        AddMiasmaPulseParticle(poolcontroller[50].GetPoolAmount);
        AddStatueLaserParticle(poolcontroller[51].GetPoolAmount);
        AddLaserExplosionParticle(poolcontroller[52].GetPoolAmount);
        AddAlphaSporeParticle(poolcontroller[53].GetPoolAmount);
        AddBetaSporeParticle(poolcontroller[54].GetPoolAmount);
        AddGammaSporeParticle(poolcontroller[55].GetPoolAmount);
        AddDisasterSporeParticle(poolcontroller[56].GetPoolAmount);
        AddIronCapParticle(poolcontroller[57].GetPoolAmount);
        AddMildewSplashParticle(poolcontroller[58].GetPoolAmount);
        AddQuicknessParticle(poolcontroller[59].GetPoolAmount);
        AddConfusionBreathParticle(poolcontroller[60].GetPoolAmount);
        AddHarrowParticle(poolcontroller[61].GetPoolAmount);
        AddShockParticle(poolcontroller[62].GetPoolAmount);
        AddWindParticle(poolcontroller[63].GetPoolAmount);
        AddBurnParticle(poolcontroller[64].GetPoolAmount);
        AddDarkParticle(poolcontroller[65].GetPoolAmount);
        AddLightParticle(poolcontroller[66].GetPoolAmount);
        AddEarthHammerParticle(poolcontroller[67].GetPoolAmount);
        AddSmashWaveParticle(poolcontroller[68].GetPoolAmount);
        AddLuxSecundusParticle(poolcontroller[69].GetPoolAmount);
        AddLuxTertiumParticle(poolcontroller[70].GetPoolAmount);
        AddSoothingOrbParticle(poolcontroller[71].GetPoolAmount);
        AddLuxAmplificationParticle(poolcontroller[72].GetPoolAmount);
        AddSkillPointText(poolcontroller[73].GetPoolAmount);
    }

    private void AddTextForPlayerDamage(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[0].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[0].GetPoolParent.transform, false);
            poolcontroller[0].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddTextForEnemyDamage(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[1].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[1].GetPoolParent.transform, false);
            poolcontroller[1].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddTextForPlayerHeal(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[2].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[2].GetPoolParent.transform, false);
            poolcontroller[2].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddTextForPlayerStatusIcon(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[3].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[3].GetPoolParent.transform, false);
            poolcontroller[3].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddTextForEnemyHeal(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[4].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[4].GetPoolParent.transform, false);
            poolcontroller[4].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddTextForEnemyStatusIcon(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[5].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[5].GetPoolParent.transform, false);
            poolcontroller[5].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddPlayerStatusIcon(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[6].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[6].GetPoolParent.transform, false);
            poolcontroller[6].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddEnemyStatusIcon(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[7].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[7].GetPoolParent.transform, false);
            poolcontroller[7].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddExperienceText(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[8].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[8].GetPoolParent.transform, false);
            poolcontroller[8].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddHitParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[9].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[9].GetPoolParent.transform, false);
            poolcontroller[9].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddPlayerCastParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[10].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[10].GetPoolParent.transform, false);
            poolcontroller[10].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddEnemyCastParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[11].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[11].GetPoolParent.transform, false);
            poolcontroller[11].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddWhirlWindSlashParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[12].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[12].GetPoolParent.transform, false);
            poolcontroller[12].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddPoisonSporeParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[13].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[13].GetPoolParent.transform, false);
            poolcontroller[13].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddHealParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[14].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[14].GetPoolParent.transform, false);
            poolcontroller[14].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddLevelParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[15].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[15].GetPoolParent.transform, false);
            poolcontroller[15].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddHpItemParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[16].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[16].GetPoolParent.transform, false);
            poolcontroller[16].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddMpItemParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[17].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[17].GetPoolParent.transform, false);
            poolcontroller[17].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddStrengthUpParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[18].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[18].GetPoolParent.transform, false);
            poolcontroller[18].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddRemoveStatusParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[19].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[19].GetPoolParent.transform, false);
            poolcontroller[19].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddLevelUpText(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[20].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[20].GetPoolParent.transform, false);
            poolcontroller[20].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddStunningStingerParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[21].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[21].GetPoolParent.transform, false);
            poolcontroller[21].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddCoinText(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[22].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[22].GetPoolParent.transform, false);
            poolcontroller[22].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddItemMessage(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[23].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[23].GetPoolParent.transform, false);
            poolcontroller[23].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddPoisonEffectParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[24].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[24].GetPoolParent.transform, false);
            poolcontroller[24].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddStunEffectParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[25].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[25].GetPoolParent.transform, false);
            poolcontroller[25].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddIlluminationEffectParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[26].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[26].GetPoolParent.transform, false);
            poolcontroller[26].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddHopParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[27].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[27].GetPoolParent.transform, false);
            poolcontroller[27].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddGaiasProwessParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[28].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[28].GetPoolParent.transform, false);
            poolcontroller[28].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddSylvanBlessingParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[29].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[29].GetPoolParent.transform, false);
            poolcontroller[29].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddSlamParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[30].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[30].GetPoolParent.transform, false);
            poolcontroller[30].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddMonsterEntryText(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[31].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[31].GetPoolParent.transform, false);
            poolcontroller[31].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddSlagParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[32].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[32].GetPoolParent.transform, false);
            poolcontroller[32].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddSylvanStormParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[33].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[33].GetPoolParent.transform, false);
            poolcontroller[33].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddEnemyAppearParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[34].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[34].GetPoolParent.transform, false);
            poolcontroller[34].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddVicePlanterParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[35].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[35].GetPoolParent.transform, false);
            poolcontroller[35].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddShatterParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[36].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[36].GetPoolParent.transform, false);
            poolcontroller[36].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddAlleviateParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[37].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[37].GetPoolParent.transform, false);
            poolcontroller[37].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddAegisParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[38].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[38].GetPoolParent.transform, false);
            poolcontroller[38].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddBraveWingParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[39].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[39].GetPoolParent.transform, false);
            poolcontroller[39].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddSinisterPossessionParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[40].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[40].GetPoolParent.transform, false);
            poolcontroller[40].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddContractParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[41].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[41].GetPoolParent.transform, false);
            poolcontroller[41].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddDiabolicLightningParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[42].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[42].GetPoolParent.transform, false);
            poolcontroller[42].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddSoulPierceParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[43].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[43].GetPoolParent.transform, false);
            poolcontroller[43].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddNetherStarParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[44].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[44].GetPoolParent.transform, false);
            poolcontroller[44].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddNetherStarExplosionParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[45].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[45].GetPoolParent.transform, false);
            poolcontroller[45].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddUpliftParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[46].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[46].GetPoolParent.transform, false);
            poolcontroller[46].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddManaPulseParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[47].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[47].GetPoolParent.transform, false);
            poolcontroller[47].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddBurningEffectParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[48].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[48].GetPoolParent.transform, false);
            poolcontroller[48].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddAquaBulletParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[49].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[49].GetPoolParent.transform, false);
            poolcontroller[49].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddMiasmaPulseParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[50].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[50].GetPoolParent.transform, false);
            poolcontroller[50].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddStatueLaserParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[51].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[51].GetPoolParent.transform, false);
            poolcontroller[51].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddLaserExplosionParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[52].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[52].GetPoolParent.transform, false);
            poolcontroller[52].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddAlphaSporeParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[53].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[53].GetPoolParent.transform, false);
            poolcontroller[53].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddBetaSporeParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[54].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[54].GetPoolParent.transform, false);
            poolcontroller[54].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddGammaSporeParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[55].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[55].GetPoolParent.transform, false);
            poolcontroller[55].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddDisasterSporeParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[56].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[56].GetPoolParent.transform, false);
            poolcontroller[56].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddIronCapParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[57].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[57].GetPoolParent.transform, false);
            poolcontroller[57].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddMildewSplashParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[58].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[58].GetPoolParent.transform, false);
            poolcontroller[58].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddQuicknessParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[59].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[59].GetPoolParent.transform, false);
            poolcontroller[59].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddConfusionBreathParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[60].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[60].GetPoolParent.transform, false);
            poolcontroller[60].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddHarrowParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[61].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[61].GetPoolParent.transform, false);
            poolcontroller[61].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddShockParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[62].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[62].GetPoolParent.transform, false);
            poolcontroller[62].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddWindParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[63].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[63].GetPoolParent.transform, false);
            poolcontroller[63].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddBurnParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[64].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[64].GetPoolParent.transform, false);
            poolcontroller[64].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddDarkParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[65].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[65].GetPoolParent.transform, false);
            poolcontroller[65].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddLightParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[66].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[66].GetPoolParent.transform, false);
            poolcontroller[66].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddEarthHammerParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[67].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[67].GetPoolParent.transform, false);
            poolcontroller[67].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddSmashWaveParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[68].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[68].GetPoolParent.transform, false);
            poolcontroller[68].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddLuxSecundusParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[69].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[69].GetPoolParent.transform, false);
            poolcontroller[69].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddLuxTertiumParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[70].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[70].GetPoolParent.transform, false);
            poolcontroller[70].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddSoothingOrbParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[71].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[71].GetPoolParent.transform, false);
            poolcontroller[71].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddLuxAmplificationParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[72].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[72].GetPoolParent.transform, false);
            poolcontroller[72].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    private void AddSkillPointText(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[73].GetObjectToPool);
            PO.transform.SetParent(poolcontroller[73].GetPoolParent.transform, false);
            poolcontroller[73].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    public GameObject GetPlayerDamageText()
    {
        return poolcontroller[0].GetPooledObject.Dequeue();
    }

    public GameObject GetEnemyDamageText()
    {
        return poolcontroller[1].GetPooledObject.Dequeue();
    }

    public GameObject GetPlayerHealText()
    {
        return poolcontroller[2].GetPooledObject.Dequeue();
    }

    public GameObject GetPlayerStatusText()
    {
        return poolcontroller[3].GetPooledObject.Dequeue();
    }

    public GameObject GetEnemyHealText()
    {
        return poolcontroller[4].GetPooledObject.Dequeue();
    }

    public GameObject GetEnemyStatusText()
    {
        return poolcontroller[5].GetPooledObject.Dequeue();
    }

    public GameObject GetPlayerStatusIcon()
    {
        return poolcontroller[6].GetPooledObject.Dequeue();
    }

    public GameObject GetEnemyStatusIcon()
    {
        return poolcontroller[7].GetPooledObject.Dequeue();
    }

    public GameObject GetExperienceText()
    {
        return poolcontroller[8].GetPooledObject.Dequeue();
    }

    public GameObject GetHitParticle()
    {
        return poolcontroller[9].GetPooledObject.Dequeue();
    }

    public GameObject GetPlayerCastParticle()
    {
        return poolcontroller[10].GetPooledObject.Dequeue();
    }

    public GameObject GetEnemyCastParticle()
    {
        return poolcontroller[11].GetPooledObject.Dequeue();
    }

    public GameObject GetWhirlwindSlashParticle()
    {
        return poolcontroller[12].GetPooledObject.Dequeue();
    }

    public GameObject GetPoisonSporeParticle()
    {
        return poolcontroller[13].GetPooledObject.Dequeue();
    }

    public GameObject GetHealParticle()
    {
        return poolcontroller[14].GetPooledObject.Dequeue();
    }

    public GameObject GetLevelParticle()
    {
        return poolcontroller[15].GetPooledObject.Dequeue();
    }

    public GameObject GetHpItemParticle()
    {
        return poolcontroller[16].GetPooledObject.Dequeue();
    }

    public GameObject GetMpItemParticle()
    {
        return poolcontroller[17].GetPooledObject.Dequeue();
    }

    public GameObject GetStrengthUpParticle()
    {
        return poolcontroller[18].GetPooledObject.Dequeue();
    }

    public GameObject GetRemoveStatusParticle()
    {
        return poolcontroller[19].GetPooledObject.Dequeue();
    }

    public GameObject GetLevelUpText()
    {
        return poolcontroller[20].GetPooledObject.Dequeue();
    }

    public GameObject GetStunningStingerParticle()
    {
        return poolcontroller[21].GetPooledObject.Dequeue();
    }

    public GameObject GetCoinText()
    {
        return poolcontroller[22].GetPooledObject.Dequeue();
    }

    public GameObject GetItemMessage()
    {
        return poolcontroller[23].GetPooledObject.Dequeue();
    }

    public GameObject GetPoisonEffectParticle()
    {
        return poolcontroller[24].GetPooledObject.Dequeue();
    }

    public GameObject GetStunEffectParticle()
    {
        return poolcontroller[25].GetPooledObject.Dequeue();
    }

    public GameObject GetIlluminationEffectParticle()
    {
        return poolcontroller[26].GetPooledObject.Dequeue();
    }

    public GameObject GetHopParticle()
    {
        return poolcontroller[27].GetPooledObject.Dequeue();
    }

    public GameObject GetGaiasProwessParticle()
    {
        return poolcontroller[28].GetPooledObject.Dequeue();
    }

    public GameObject GetSylvanBlessingParticle()
    {
        return poolcontroller[29].GetPooledObject.Dequeue();
    }

    public GameObject GetSlamParticle()
    {
        return poolcontroller[30].GetPooledObject.Dequeue();
    }

    public GameObject GetMonsterEntryText()
    {
        return poolcontroller[31].GetPooledObject.Dequeue();
    }

    public GameObject GetSlagParticle()
    {
        return poolcontroller[32].GetPooledObject.Dequeue();
    }

    public GameObject GetSylvanStormParticle()
    {
        return poolcontroller[33].GetPooledObject.Dequeue();
    }

    public GameObject GetEnemyAppearParticle()
    {
        return poolcontroller[34].GetPooledObject.Dequeue();
    }

    public GameObject GetVicePlanterParticle()
    {
        return poolcontroller[35].GetPooledObject.Dequeue();
    }

    public GameObject GetShatterParticle()
    {
        return poolcontroller[36].GetPooledObject.Dequeue();
    }

    public GameObject GetAlleviateParticle()
    {
        return poolcontroller[37].GetPooledObject.Dequeue();
    }

    public GameObject GetAegisParticle()
    {
        return poolcontroller[38].GetPooledObject.Dequeue();
    }

    public GameObject GetBraveWingParticle()
    {
        return poolcontroller[39].GetPooledObject.Dequeue();
    }

    public GameObject GetSinisterPossessionParticle()
    {
        return poolcontroller[40].GetPooledObject.Dequeue();
    }

    public GameObject GetContractParticle()
    {
        return poolcontroller[41].GetPooledObject.Dequeue();
    }

    public GameObject GetDiabolicLightningParticle()
    {
        return poolcontroller[42].GetPooledObject.Dequeue();
    }

    public GameObject GetSoulPierceParticle()
    {
        return poolcontroller[43].GetPooledObject.Dequeue();
    }

    public GameObject GetNetherStarParticle()
    {
        return poolcontroller[44].GetPooledObject.Dequeue();
    }

    public GameObject GetNetherStarExplosionParticle()
    {
        return poolcontroller[45].GetPooledObject.Dequeue();
    }

    public GameObject GetUpliftParticle()
    {
        return poolcontroller[46].GetPooledObject.Dequeue();
    }

    public GameObject GetManaPulseParticle()
    {
        return poolcontroller[47].GetPooledObject.Dequeue();
    }

    public GameObject GetBurningEffectParticle()
    {
        return poolcontroller[48].GetPooledObject.Dequeue();
    }

    public GameObject GetAquaBulletParticle()
    {
        return poolcontroller[49].GetPooledObject.Dequeue();
    }

    public GameObject GetMiasmaPulseParticle()
    {
        return poolcontroller[50].GetPooledObject.Dequeue();
    }

    public GameObject GetStatueLaserParticle()
    {
        return poolcontroller[51].GetPooledObject.Dequeue();
    }

    public GameObject GetLaserExplosionParticle()
    {
        return poolcontroller[52].GetPooledObject.Dequeue();
    }

    public GameObject GetAlphaSporeParticle()
    {
        return poolcontroller[53].GetPooledObject.Dequeue();
    }

    public GameObject GetBetaSporeParticle()
    {
        return poolcontroller[54].GetPooledObject.Dequeue();
    }

    public GameObject GetGammaSporeParticle()
    {
        return poolcontroller[55].GetPooledObject.Dequeue();
    }

    public GameObject GetDisasterSporeParticle()
    {
        return poolcontroller[56].GetPooledObject.Dequeue();
    }

    public GameObject GetIronCapParticle()
    {
        return poolcontroller[57].GetPooledObject.Dequeue();
    }

    public GameObject GetMildewSplashParticle()
    {
        return poolcontroller[58].GetPooledObject.Dequeue();
    }

    public GameObject GetQuicknessParticle()
    {
        return poolcontroller[59].GetPooledObject.Dequeue();
    }

    public GameObject GetConfusionBreathParticle()
    {
        return poolcontroller[60].GetPooledObject.Dequeue();
    }

    public GameObject GetHarrowParticle()
    {
        return poolcontroller[61].GetPooledObject.Dequeue();
    }

    public GameObject GetShockParticle()
    {
        return poolcontroller[62].GetPooledObject.Dequeue();
    }

    public GameObject GetWindParticle()
    {
        return poolcontroller[63].GetPooledObject.Dequeue();
    }

    public GameObject GetBurnParticle()
    {
        return poolcontroller[64].GetPooledObject.Dequeue();
    }

    public GameObject GetDarkParticle()
    {
        return poolcontroller[65].GetPooledObject.Dequeue();
    }

    public GameObject GetLightParticle()
    {
        return poolcontroller[66].GetPooledObject.Dequeue();
    }

    public GameObject GetEarthHammerParticle()
    {
        return poolcontroller[67].GetPooledObject.Dequeue();
    }

    public GameObject GetSmashWaveParticle()
    {
        return poolcontroller[68].GetPooledObject.Dequeue();
    }

    public GameObject GetLuxSecundusParticle()
    {
        return poolcontroller[69].GetPooledObject.Dequeue();
    }

    public GameObject GetLuxTertiumParticle()
    {
        return poolcontroller[70].GetPooledObject.Dequeue();
    }

    public GameObject GetSoothingOrbParticle()
    {
        return poolcontroller[71].GetPooledObject.Dequeue();
    }

    public GameObject GetLuxAmplificationParticle()
    {
        return poolcontroller[72].GetPooledObject.Dequeue();
    }

    public GameObject GetSkillPointText()
    {
        return poolcontroller[73].GetPooledObject.Dequeue();
    }

    public void ReturnPlayerDamageToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[0].GetPoolParent.transform, false);

        poolcontroller[0].GetPooledObject.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnEnemyDamageToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[1].GetPoolParent.transform, false);

        poolcontroller[1].GetPooledObject.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnPlayerHealToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[2].GetPoolParent.transform, false);

        poolcontroller[2].GetPooledObject.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnPlayerStatusTextToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[3].GetPoolParent.transform, false);

        poolcontroller[3].GetPooledObject.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnEnemyHealToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[4].GetPoolParent.transform, false);

        poolcontroller[4].GetPooledObject.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnEnemyStatusTextToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[5].GetPoolParent.transform, false);

        poolcontroller[5].GetPooledObject.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnPlayerStatusIconToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[6].GetPoolParent.transform, false);

        poolcontroller[6].GetPooledObject.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnEnemyStatusIconToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[7].GetPoolParent.transform, false);

        poolcontroller[7].GetPooledObject.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnExperienceTextToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[8].GetPoolParent.transform, false);

        poolcontroller[8].GetPooledObject.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnHitParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[9].GetPoolParent.transform, false);

        poolcontroller[9].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnPlayerCastParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[10].GetPoolParent.transform, false);

        poolcontroller[10].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnEnemyCastParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[11].GetPoolParent.transform, false);

        poolcontroller[11].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnWhirlwindSlashParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[12].GetPoolParent.transform, false);

        poolcontroller[12].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnPoisonSporeParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[13].GetPoolParent.transform, false);

        poolcontroller[13].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnHealParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[14].GetPoolParent.transform, false);

        poolcontroller[14].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnLevelParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[15].GetPoolParent.transform, false);

        poolcontroller[15].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnHpItemParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[16].GetPoolParent.transform, false);

        poolcontroller[16].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnMpItemParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[17].GetPoolParent.transform, false);

        poolcontroller[17].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnStrengthUpParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[18].GetPoolParent.transform, false);

        poolcontroller[18].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnRemoveStatusParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[19].GetPoolParent.transform, false);

        poolcontroller[19].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnLevelUpTextToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[20].GetPoolParent.transform, false);

        poolcontroller[20].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnStunningStingerParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[21].GetPoolParent.transform, false);

        poolcontroller[21].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnCoinTextToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[22].GetPoolParent.transform, false);

        poolcontroller[22].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnItemMessageToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[23].GetPoolParent.transform, false);

        poolcontroller[23].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnPoisonEffectParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[24].GetPoolParent.transform, false);

        poolcontroller[24].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnStunEffectParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[25].GetPoolParent.transform, false);

        poolcontroller[25].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnIlluminationEffectParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[26].GetPoolParent.transform, false);

        poolcontroller[26].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnHopParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[27].GetPoolParent.transform, false);

        poolcontroller[27].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnGaiasProwessParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[28].GetPoolParent.transform, false);

        poolcontroller[28].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnSylvanBlessingParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[29].GetPoolParent.transform, false);

        poolcontroller[29].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnSlamParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[30].GetPoolParent.transform, false);

        poolcontroller[30].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnMonsterEntryTextToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[31].GetPoolParent.transform, false);

        poolcontroller[31].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnSlagParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[32].GetPoolParent.transform, false);

        poolcontroller[32].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnSylvanStormParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[33].GetPoolParent.transform, false);

        poolcontroller[33].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnEnemyAppearParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[34].GetPoolParent.transform, false);

        poolcontroller[34].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnVicePlanterParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[35].GetPoolParent.transform, false);

        poolcontroller[35].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnShatterParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[36].GetPoolParent.transform, false);

        poolcontroller[36].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnAlleviateParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[37].GetPoolParent.transform, false);

        poolcontroller[37].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnAegisParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[38].GetPoolParent.transform, false);

        poolcontroller[38].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnBraveWingParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[39].GetPoolParent.transform, false);

        poolcontroller[39].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnSinisterPossessionParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[40].GetPoolParent.transform, false);

        poolcontroller[40].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnContractParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[41].GetPoolParent.transform, false);

        poolcontroller[41].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnDiabolicLightningParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[42].GetPoolParent.transform, false);

        poolcontroller[42].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnSoulPierceParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[43].GetPoolParent.transform, false);

        poolcontroller[43].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnNetherStarParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[44].GetPoolParent.transform, false);

        poolcontroller[44].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnNetherStarExplosionParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[45].GetPoolParent.transform, false);

        poolcontroller[45].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnUpliftParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[46].GetPoolParent.transform, false);

        poolcontroller[46].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnManaPulseParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[47].GetPoolParent.transform, false);

        poolcontroller[47].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnBurningEffectParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[48].GetPoolParent.transform, false);

        poolcontroller[48].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnAquaBulletParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[49].GetPoolParent.transform, false);

        poolcontroller[49].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnMiasmaPulseParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[50].GetPoolParent.transform, false);

        poolcontroller[50].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnStatueLaserParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[51].GetPoolParent.transform, false);

        poolcontroller[51].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnLaserExplosionParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[52].GetPoolParent.transform, false);

        poolcontroller[52].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnAlphaSporeParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[53].GetPoolParent.transform, false);

        poolcontroller[53].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnBetaSporeParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[54].GetPoolParent.transform, false);

        poolcontroller[54].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnGammaSporeParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[55].GetPoolParent.transform, false);

        poolcontroller[55].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnDisasterSporeParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[56].GetPoolParent.transform, false);

        poolcontroller[56].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnIronCapParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[57].GetPoolParent.transform, false);

        poolcontroller[57].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnMildewSplashParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[58].GetPoolParent.transform, false);

        poolcontroller[58].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnQuicknessParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[59].GetPoolParent.transform, false);

        poolcontroller[59].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnConfusionBreathParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[60].GetPoolParent.transform, false);

        poolcontroller[60].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnHarrowParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[61].GetPoolParent.transform, false);

        poolcontroller[61].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnShockParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[62].GetPoolParent.transform, false);

        poolcontroller[62].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnWindParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[63].GetPoolParent.transform, false);

        poolcontroller[63].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnBurnParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[64].GetPoolParent.transform, false);

        poolcontroller[64].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnDarkParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[65].GetPoolParent.transform, false);

        poolcontroller[65].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnLightParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[66].GetPoolParent.transform, false);

        poolcontroller[66].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnEarthHammerParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[67].GetPoolParent.transform, false);

        poolcontroller[67].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnSmashWaveParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[68].GetPoolParent.transform, false);

        poolcontroller[68].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnLuxSecundusParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[69].GetPoolParent.transform, false);

        poolcontroller[69].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnLuxTertiumParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[70].GetPoolParent.transform, false);

        poolcontroller[70].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnSoothingOrbParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[71].GetPoolParent.transform, false);

        poolcontroller[71].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnLuxAmplificationParticleToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[72].GetPoolParent.transform, false);

        poolcontroller[72].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }

    public void ReturnSkillPointTextToPool(GameObject Object)
    {
        Object.transform.SetParent(poolcontroller[73].GetPoolParent.transform, false);

        poolcontroller[73].GetPooledObject.Enqueue(Object);
        Object.SetActive(false);
    }
}