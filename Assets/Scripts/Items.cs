using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ItemType { HpHeal, MpHeal }

public class Items : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Button button;

    [SerializeField]
    private Transform HealTextTransform;

    [SerializeField]
    private ItemType itemType;

    [SerializeField]
    private Image CooldownImage;

    [SerializeField]
    private int HealAmount;

    [SerializeField]
    private float ApplyItemUse, Cooldown;

    public ItemType GetItemType
    {
        get
        {
            return itemType;
        }
        set
        {
            itemType = value;
        }
    }

    private void Update()
    {
        if(character != null)
        CheckCoolDownStatus();
    }

    private void CheckCoolDownStatus()
    {
        if(CooldownImage.fillAmount <= 0)
        {
            button.interactable = true;

            return;
        }
        else
        {
            CooldownImage.fillAmount -= Time.deltaTime / Cooldown;
            button.interactable = false;
        }
    }

    public void Use()
    {
        switch (itemType)
        {
            case (ItemType.HpHeal):
                ReadyHpHealing();
                break;
            case (ItemType.MpHeal):
                ReadyMpHealing();
                break;
        }
    }

    private void ReadyHpHealing()
    {
        if(CooldownImage.fillAmount <= 0)
        Invoke("HpHealing", ApplyItemUse);
        CooldownImage.fillAmount = 1;
    }

    private void ReadyMpHealing()
    {
        if(CooldownImage.fillAmount <= 0)
        Invoke("MpHealing", ApplyItemUse);
        CooldownImage.fillAmount = 1;
    }

    private void HpHealing()
    {
        if(character.CurrentHealth > 0)
        {
            character.GetComponent<Health>().IncreaseHealth(HealAmount);

            HealText();
        }
    }

    private void MpHealing()
    {
        if(character.CurrentHealth > 0)
        {
            character.GetComponent<Mana>().IncreaseMana(HealAmount);

            HealText();
        }
    }

    private TextMeshProUGUI HealText()
    {
        var HealingText = ObjectPooler.Instance.GetPlayerHealText();

        HealingText.SetActive(true);

        HealingText.transform.SetParent(HealTextTransform, false);

        if(itemType == ItemType.HpHeal)
        {
            HealingText.GetComponentInChildren<TextMeshProUGUI>().text = HealAmount.ToString();
        }
        else
        {
            HealingText.GetComponentInChildren<TextMeshProUGUI>().text = HealAmount + " MP";
        }

        return HealingText.GetComponentInChildren<TextMeshProUGUI>();
    }
}
