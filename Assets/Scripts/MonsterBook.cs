using UnityEngine;
using TMPro;

public class MonsterBook : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI MonsterInfoTxt;

    [SerializeField]
    private Transform MonsterButtonInfoTrans, BossButtonInfoTrans;

    public Transform GetMonsterTransform
    {
        get
        {
            return MonsterButtonInfoTrans;
        }
        set
        {
            MonsterButtonInfoTrans = value;
        }
    }

    public Transform GetBossTransform
    {
        get
        {
            return BossButtonInfoTrans;
        }
        set
        {
            BossButtonInfoTrans = value;
        }
    }

    public TextMeshProUGUI GetMonsterInfoTxt
    {
        get
        {
            return MonsterInfoTxt;
        }
        set
        {
            MonsterInfoTxt = value;
        }
    }

    public void ShowInfoPanel(GameObject Panel)
    {
        Panel.SetActive(true);
    }

    public void ShowMonsterButton()
    {
        if(MonsterButtonInfoTrans.gameObject.activeInHierarchy)
        {
            foreach (MonsterInformation mi in MonsterButtonInfoTrans.GetComponentsInChildren<MonsterInformation>(true))
            {
                if (mi.gameObject.activeInHierarchy)
                {
                    mi.gameObject.SetActive(false);
                }
                else
                {
                    mi.gameObject.SetActive(true);
                }
            }
        }
    }

    public void ShowBossButton()
    {
        if(BossButtonInfoTrans.gameObject.activeInHierarchy)
        {
            foreach (MonsterInformation mi in BossButtonInfoTrans.GetComponentsInChildren<MonsterInformation>(true))
            {
                if (mi.gameObject.activeInHierarchy)
                {
                    mi.gameObject.SetActive(false);
                }
                else
                {
                    mi.gameObject.SetActive(true);
                }
            }
        }
        
    }
}
