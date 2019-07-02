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
}
