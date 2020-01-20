#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ItemType { HpHeal, MpHeal }

public class Items : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Button button;

    [SerializeField]
    private Transform HealTextTransform;

    [SerializeField]
    private TextMeshProUGUI ItemText, CoolDownText;

    [SerializeField]
    private ItemType itemType;

    [SerializeField]
    private Image CooldownImage;

    [SerializeField]
    private string ItemName;
    
    [SerializeField] [TextArea]
    private string ItemDescription;

    [SerializeField]
    private int HealAmount;

    [SerializeField]
    private float ApplyItemUse, Cooldown;

    private float cooldown;

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

    public Button GetButton
    {
        get
        {
            return button;
        }
        set
        {
            button = value;
        }
    }

    public float GetCoolDown
    {
        get
        {
            return Cooldown;
        }
        set
        {
            Cooldown = value;
        }
    }

    public int GetHealAmount
    {
        get
        {
            return HealAmount;
        }
        set
        {
            HealAmount = value;
        }
    }

    private void Awake()
    {
        cooldown = Cooldown;
    }

    private void Update()
    {
        if(character != null)
        CheckCoolDownStatus();
    }

    private void CheckCoolDownStatus()
    {
        if(CooldownImage.fillAmount <= 0 && !SkillsManager.Instance.GetDisruptedSkill && !GameManager.Instance.GetIsDead && !SkillsManager.Instance.GetActivatedSkill)
        {
            button.interactable = true;

            CoolDownText.enabled = false;

            cooldown = Cooldown;

            return;
        }
        else
        {
            CooldownImage.fillAmount -= Time.deltaTime / Cooldown;
            button.interactable = false;
        }

        if(CooldownImage.fillAmount > 0)
        {
            cooldown -= Time.deltaTime;
            CoolDownText.text = Mathf.Clamp(cooldown, 0, Cooldown).ToString("F1");
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
        if(settings.UseParticleEffects)
        {
            HpParticle();
        }

        SoundManager.Instance.ItemBottle();

        Invoke("HpHealing", ApplyItemUse);
        CooldownImage.fillAmount = 1;
    }

    private void ReadyMpHealing()
    {
        if(settings.UseParticleEffects)
        {
            MpParticle();
        }

        SoundManager.Instance.ItemBottle();

        Invoke("MpHealing", ApplyItemUse);
        CooldownImage.fillAmount = 1;
    }

    private void HpHealing()
    {
        if(character.CurrentHealth > 0)
        {
            SoundManager.Instance.ItemHeal();

            character.GetComponent<Health>().IncreaseHealth(HpHeal(HealAmount));

            HealText();
        }
    }

    private void MpHealing()
    {
        if(character.CurrentHealth > 0)
        {
            SoundManager.Instance.ItemHeal();

            character.GetComponent<Mana>().IncreaseMana(MpHeal(HealAmount));

            HealText();
        }
    }

    private int HpHeal(float value)
    {
        float Percentage = value / 100;

        float HealthAmt = character.MaxHealth * Percentage;

        Mathf.Round(HealthAmt);

        return (int)HealthAmt;
    }

    private int MpHeal(float value)
    {
        float Percentage = value / 100;

        float ManaAmt = character.MaxMana * Percentage;

        Mathf.Round(ManaAmt);

        return (int)ManaAmt;
    }

    private TextMeshProUGUI HealText()
    {
        var HealingText = ObjectPooler.Instance.GetPlayerHealText();

        HealingText.SetActive(true);

        HealingText.transform.SetParent(HealTextTransform, false);

        if(itemType == ItemType.HpHeal)
        {
            HealingText.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + HpHeal(HealAmount).ToString();
        }
        else
        {
            HealingText.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + MpHeal(HealAmount) + "</size>" + "<size=20>" + " MP";
        }

        return HealingText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowItemDescriptionPanel(GameObject Panel)
    {
        Panel.SetActive(true);

        if(itemType == ItemType.HpHeal)
        {
            ItemText.text = "<size=12>" + "<u>" + ItemName + "</u>" + "</size>" + "\n\n" + "Recovers HP by " + HealAmount + "%" + "\n\n" + "Cooldown: " + Cooldown + "s";
        }
        else
        {
            ItemText.text = "<size=12>" + "<u>" + ItemName + "</u>" + "</size>" + "\n\n" + "Recovers MP by " + HealAmount + "%" + "\n\n" + "Cooldown: " + Cooldown + "s";
        }
    }

    private void HpParticle()
    {
        var HpParticles = ObjectPooler.Instance.GetHpItemParticle();

        HpParticles.SetActive(true);

        HpParticles.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 2f, character.transform.position.z);

        HpParticles.transform.SetParent(character.transform);
    }

    private void MpParticle()
    {
        var MpParticles = ObjectPooler.Instance.GetMpItemParticle();

        MpParticles.SetActive(true);

        MpParticles.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 2f, character.transform.position.z);

        MpParticles.transform.SetParent(character.transform);
    }

    public void ShowCoolDownText()
    {
        if(CooldownImage.fillAmount > 0)
        {
            CoolDownText.enabled = true;
        }
        else
        {
            CoolDownText.enabled = false;
        }
    }
}
