using UnityEngine;
using TMPro;

public class MonsterBook : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI MonsterInfoTxt;

    [SerializeField]
    private Transform MonsterButtonInfoTrans;

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
}
