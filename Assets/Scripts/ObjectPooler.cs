using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance = null;

    private Queue<GameObject> UiTexts = new Queue<GameObject>();

    [SerializeField]
    private GameObject UiText;

    [SerializeField]
    private Transform CanvasTransform;

    [SerializeField]
    private int PoolAmount;

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        AddText(PoolAmount);
    }

    private void AddText(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var UI = Instantiate(UiText);
            UiTexts.Enqueue(UI);

            UI.transform.SetParent(CanvasTransform, false);
            UI.gameObject.SetActive(false);
        }
    }

    public GameObject GetText()
    {
        return UiTexts.Dequeue();
    }

    public void ReturnToPool(GameObject textObject)
    {
        textObject.SetActive(false);
        UiTexts.Enqueue(textObject);
    }
}
