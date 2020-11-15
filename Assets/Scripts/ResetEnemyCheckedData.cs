#pragma warning disable 0649
using UnityEngine;

public class ResetEnemyCheckedData : MonoBehaviour
{
    [SerializeField]
    private CharacterData[] EnemyData;

    private void OnEnable()
    {
        for(int i = 0; i < EnemyData.Length; i++)
        {
            EnemyData[i].CheckedData = false;
        }
    }
}
