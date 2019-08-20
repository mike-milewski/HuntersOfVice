using UnityEngine;

public class EquippedCheck : MonoBehaviour
{
    [SerializeField]
    private bool IsEquipped;

    public bool GetIsEquipped
    {
        get
        {
            return IsEquipped;
        }
        set
        {
            IsEquipped = value;
        }
    }
}
