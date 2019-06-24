using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PoolController
{
    private Queue<GameObject> UiTexts = new Queue<GameObject>();

    [SerializeField]
    private GameObject UiObject;

    [SerializeField]
    private Transform UiParent;

    [SerializeField]
    private int PoolAmount;

    [SerializeField]
    private int ObjectNumber;

    public GameObject GetUiObject
    {
        get
        {
            return UiObject;
        }
        set
        {
            UiObject = value;
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

    public int GetObjectNum
    {
        get
        {
            return ObjectNumber;
        }
        set
        {
            ObjectNumber = value;
        }
    }

    public Queue<GameObject> GetUiTexts
    {
        get
        {
            return UiTexts;
        }
        set
        {
            UiTexts = value;
        }
    }

    public Transform GetUiParent
    {
        get
        {
            return UiParent;
        }
        set
        {
            UiParent = value;
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
    }

    private void AddTextForPlayerDamage(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(poolcontroller[0].GetUiObject);
            UI.transform.SetParent(poolcontroller[0].GetUiParent.transform, false);
            poolcontroller[0].GetUiTexts.Enqueue(UI);

            UI.gameObject.SetActive(false);
        }
    }

    private void AddTextForEnemyDamage(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(poolcontroller[1].GetUiObject);
            UI.transform.SetParent(poolcontroller[1].GetUiParent.transform, false);
            poolcontroller[1].GetUiTexts.Enqueue(UI);

            UI.gameObject.SetActive(false);
        }
    }

    private void AddTextForPlayerHeal(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(poolcontroller[2].GetUiObject);
            UI.transform.SetParent(poolcontroller[2].GetUiParent.transform, false);
            poolcontroller[2].GetUiTexts.Enqueue(UI);

            UI.gameObject.SetActive(false);
        }
    }

    private void AddTextForPlayerStatusIcon(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(poolcontroller[3].GetUiObject);
            UI.transform.SetParent(poolcontroller[3].GetUiParent.transform, false);
            poolcontroller[3].GetUiTexts.Enqueue(UI);

            UI.gameObject.SetActive(false);
        }
    }

    private void AddTextForEnemyHeal(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(poolcontroller[4].GetUiObject);
            UI.transform.SetParent(poolcontroller[4].GetUiParent.transform, false);
            poolcontroller[4].GetUiTexts.Enqueue(UI);

            UI.gameObject.SetActive(false);
        }
    }

    private void AddTextForEnemyStatusIcon(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(poolcontroller[5].GetUiObject);
            UI.transform.SetParent(poolcontroller[5].GetUiParent.transform, false);
            poolcontroller[5].GetUiTexts.Enqueue(UI);

            UI.gameObject.SetActive(false);
        }
    }

    private void AddPlayerStatusIcon(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(poolcontroller[6].GetUiObject);
            UI.transform.SetParent(poolcontroller[6].GetUiParent.transform, false);
            poolcontroller[6].GetUiTexts.Enqueue(UI);

            UI.gameObject.SetActive(false);
        }
    }

    private void AddEnemyStatusIcon(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(poolcontroller[7].GetUiObject);
            UI.transform.SetParent(poolcontroller[7].GetUiParent.transform, false);
            poolcontroller[7].GetUiTexts.Enqueue(UI);

            UI.gameObject.SetActive(false);
        }
    }

    private void AddExperienceText(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(poolcontroller[8].GetUiObject);
            UI.transform.SetParent(poolcontroller[8].GetUiParent.transform, false);
            poolcontroller[8].GetUiTexts.Enqueue(UI);

            UI.gameObject.SetActive(false);
        }
    }

    public GameObject GetPlayerDamageText()
    {
        return poolcontroller[0].GetUiTexts.Dequeue();
    }

    public GameObject GetEnemyDamageText()
    {
        return poolcontroller[1].GetUiTexts.Dequeue();
    }

    public GameObject GetPlayerHealText()
    {
        return poolcontroller[2].GetUiTexts.Dequeue();
    }

    public GameObject GetPlayerStatusText()
    {
        return poolcontroller[3].GetUiTexts.Dequeue();
    }

    public GameObject GetEnemyHealText()
    {
        return poolcontroller[4].GetUiTexts.Dequeue();
    }

    public GameObject GetEnemyStatusText()
    {
        return poolcontroller[5].GetUiTexts.Dequeue();
    }

    public GameObject GetPlayerStatusIcon()
    {
        return poolcontroller[6].GetUiTexts.Dequeue();
    }

    public GameObject GetEnemyStatusIcon()
    {
        return poolcontroller[7].GetUiTexts.Dequeue();
    }

    public GameObject GetExperienceText()
    {
        return poolcontroller[8].GetUiTexts.Dequeue();
    }

    public void ReturnPlayerDamageToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[0].GetUiParent.transform, false);

        poolcontroller[0].GetUiTexts.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnEnemyDamageToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[1].GetUiParent.transform, false);

        poolcontroller[1].GetUiTexts.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnPlayerHealToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[2].GetUiParent.transform, false);

        poolcontroller[2].GetUiTexts.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnPlayerStatusTextToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[3].GetUiParent.transform, false);

        poolcontroller[3].GetUiTexts.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnEnemyHealToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[4].GetUiParent.transform, false);

        poolcontroller[4].GetUiTexts.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnEnemyStatusTextToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[5].GetUiParent.transform, false);

        poolcontroller[5].GetUiTexts.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnPlayerStatusIconToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[6].GetUiParent.transform, false);

        poolcontroller[6].GetUiTexts.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnEnemyStatusIconToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[7].GetUiParent.transform, false);

        poolcontroller[7].GetUiTexts.Enqueue(textObject);
        textObject.SetActive(false);
    }

    public void ReturnExperienceTextToPool(GameObject textObject)
    {
        textObject.transform.SetParent(poolcontroller[8].GetUiParent.transform, false);

        poolcontroller[8].GetUiTexts.Enqueue(textObject);
        textObject.SetActive(false);
    }
}
