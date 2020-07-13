#pragma warning disable 0649
using UnityEngine;

public class ToggleEquipmentInShop : MonoBehaviour
{
    [SerializeField]
    private Transform WeaponParent, ArmorParent;

    public void ToggleEquipment()
    {
        if(WeaponParent.gameObject.activeInHierarchy)
        {
            if(WeaponParent.childCount > 0)
            {
                foreach (Equipment equipment in WeaponParent.GetComponentsInChildren<Equipment>(true))
                {
                    if (!equipment.gameObject.activeInHierarchy)
                    {
                        equipment.gameObject.SetActive(true);
                    }
                    else
                    {
                        equipment.gameObject.SetActive(false);
                    }
                }
            }
        }
        if(ArmorParent.gameObject.activeInHierarchy)
        {
            if(ArmorParent.childCount > 0)
            {
                foreach (Equipment equipment in ArmorParent.GetComponentsInChildren<Equipment>(true))
                {
                    if (!equipment.gameObject.activeInHierarchy)
                    {
                        equipment.gameObject.SetActive(true);
                    }
                    else
                    {
                        equipment.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void ToggleWeaponBtn()
    {
        if (WeaponParent.childCount > 0)
        {
            foreach (Equipment equipment in WeaponParent.GetComponentsInChildren<Equipment>(true))
            {
                if (equipment.gameObject.activeInHierarchy)
                {
                    return;
                }
                else
                {
                    equipment.gameObject.SetActive(true);
                }
            }
        }
    }

    public void ToggleArmorBtn()
    {
        if (ArmorParent.childCount > 0)
        {
            foreach (Equipment equipment in ArmorParent.GetComponentsInChildren<Equipment>(true))
            {
                if (equipment.gameObject.activeInHierarchy)
                {
                    return;
                }
                else
                {
                    equipment.gameObject.SetActive(true);
                }
            }
        }
    }
}
