using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Character Equipment")]
public class EquipmentData : ScriptableObject
{
    public string EquipmentName;

    public PlayerElement Element;

    public int StatIncrease;
}
