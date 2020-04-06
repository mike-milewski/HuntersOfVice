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
        DontDestroyOnLoad(gameObject);
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
}