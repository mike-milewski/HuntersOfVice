#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ItemType { HpHeal, MpHeal }

public class Items : MonoBehaviour
{
    [SerializeField]
    private StatusEffect statusEffect;

    [SerializeField]
    private Character Player, Knight, ShadowPriest, Toadstool;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private Button button;

    [SerializeField]
    private Transform HealTextTransform;

    [SerializeField]
    private Transform TextHolder = null, StatusEffectIconTrans = null;

    [SerializeField]
    private GameObject StatusIcon = null;

    [SerializeField]
    private TextMeshProUGUI ItemText, CoolDownText;

    [SerializeField]
    private ItemType itemType;

    [SerializeField]
    private Image CooldownImage;

    [SerializeField]
    private Sprite StatusEffectImage = null;

    [SerializeField]
    private string ItemName, DamageName, StatusEffectName;

    [SerializeField]
    private int HealAmount, Power;

    [SerializeField]
    private float ApplyItemUse, Cooldown;

    [SerializeField]
    private bool UnlockedPassive, HasStatusEffect;

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

    public Image GetCoolDownImage
    {
        get
        {
            return CooldownImage;
        }
        set
        {
            CooldownImage = value;
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

    public bool GetUnlockedPassive
    {
        get
        {
            return UnlockedPassive;
        }
        set
        {
            UnlockedPassive = value;
        }
    }

    public bool GetStatusEffect
    {
        get
        {
            return HasStatusEffect;
        }
        set
        {
            HasStatusEffect = value;
        }
    }

    private void Awake()
    {
        cooldown = Cooldown;

        if(Knight.gameObject.activeInHierarchy)
        {
            Player = Knight;
        }
        if(ShadowPriest.gameObject.activeInHierarchy)
        {
            Player = ShadowPriest;
        }
        if(Toadstool.gameObject.activeInHierarchy)
        {
            Player = Toadstool;
        }
    }

    private void Update()
    {
        if(Knight != null || ShadowPriest != null || Toadstool != null)
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
        if(!GameManager.Instance.GetBeatGame)
        {
            switch (itemType)
            {
                case (ItemType.HpHeal):
                    ReadyHpHealing();
                    MiasmaPulsePassive();
                    break;
                case (ItemType.MpHeal):
                    ReadyMpHealing();
                    ManaPulsePassive();
                    break;
            }
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
        if(Knight.CurrentHealth > 0 || ShadowPriest.CurrentHealth > 0 || Toadstool.CurrentHealth > 0)
        {
            SoundManager.Instance.ItemHeal();

            if(Knight.gameObject.activeInHierarchy)
            {
                Knight.GetComponent<Health>().IncreaseHealth(HpHeal(HealAmount));
            }
            if(ShadowPriest.gameObject.activeInHierarchy)
            {
                ShadowPriest.GetComponent<Health>().IncreaseHealth(HpHeal(HealAmount));
            }
            if (Toadstool.gameObject.activeInHierarchy)
            {
                Toadstool.GetComponent<Health>().IncreaseHealth(HpHeal(HealAmount));
            }

            HealText();
        }
    }

    private void MpHealing()
    {
        if(Knight.CurrentHealth > 0 || ShadowPriest.CurrentHealth > 0 || Toadstool.CurrentHealth > 0)
        {
            SoundManager.Instance.ItemHeal();

            if(Knight.gameObject.activeInHierarchy)
            {
                Knight.GetComponent<Mana>().IncreaseMana(MpHeal(HealAmount));
            }
            if(ShadowPriest.gameObject.activeInHierarchy)
            {
                ShadowPriest.GetComponent<Mana>().IncreaseMana(MpHeal(HealAmount));
            }
            if (Toadstool.gameObject.activeInHierarchy)
            {
                Toadstool.GetComponent<Mana>().IncreaseMana(MpHeal(HealAmount));
            }

            HealText();
        }
    }

    private int HpHeal(float value)
    {
        float Percentage = value / 100;

        float HealReduction = GameManager.Instance.GetHealingReduction / 100;

        if (GameManager.Instance.GetHealingReduction > 100)
        {
            HealReduction = 1;
        }

        float HealthAmt = Mathf.Abs((Player.MaxHealth * Percentage) - ((Player.MaxHealth * Percentage) * HealReduction));

        Mathf.Round(HealthAmt);

        return (int)HealthAmt;
    }

    private int MpHeal(float value)
    {
        float Percentage = value / 100;

        float HealReduction = GameManager.Instance.GetHealingReduction / 100;

        if (GameManager.Instance.GetHealingReduction > 100)
        {
            HealReduction = 1;
        }

        float ManaAmt = Mathf.Abs((Player.MaxMana * Percentage) - ((Player.MaxMana * Percentage) * HealReduction));

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

        HpParticles.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2f, Player.transform.position.z);

        HpParticles.transform.SetParent(Player.transform);
    }

    private void MpParticle()
    {
        var MpParticles = ObjectPooler.Instance.GetMpItemParticle();

        MpParticles.SetActive(true);

        MpParticles.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2f, Player.transform.position.z);

        MpParticles.transform.SetParent(Player.transform);
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

    private void ManaPulsePassive()
    {
        if (UnlockedPassive)
        {
            if(Knight.gameObject.activeInHierarchy)
            {
                SetUpDamagePerimiter(Knight.gameObject.transform.position, 15f);
            }
            else if(ShadowPriest.gameObject.activeInHierarchy)
            {
                SetUpDamagePerimiter(ShadowPriest.gameObject.transform.position, 5f);
            }
            ManaPulseParticle();
        }
        else return;
    }

    private void MiasmaPulsePassive()
    {
        if (UnlockedPassive)
        {
            SetUpDamagePerimiter(ShadowPriest.gameObject.transform.position, 5f);

            MiasmaPulseParticle();
        }
        else return;
    }

    private void ManaPulseParticle()
    {
        var ManaPulse = ObjectPooler.Instance.GetManaPulseParticle();

        ManaPulse.SetActive(true);

        ManaPulse.transform.position = new Vector3(ShadowPriest.transform.position.x, ShadowPriest.transform.position.y, ShadowPriest.transform.position.z);
    }

    private void MiasmaPulseParticle()
    {
        var ManaPulse = ObjectPooler.Instance.GetMiasmaPulseParticle();

        ManaPulse.SetActive(true);

        ManaPulse.transform.position = new Vector3(ShadowPriest.transform.position.x, ShadowPriest.transform.position.y, ShadowPriest.transform.position.z);
    }

    private void SetUpDamagePerimiter(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Enemy>())
            {
                if (settings.UseParticleEffects)
                {
                    var HitParticle = ObjectPooler.Instance.GetHitParticle();

                    HitParticle.SetActive(true);

                    HitParticle.transform.position = new Vector3(hitColliders[i].transform.position.x, hitColliders[i].transform.position.y + 0.5f,
                                                                 hitColliders[i].transform.position.z);

                    HitParticle.transform.SetParent(hitColliders[i].transform);
                }
                DamageSkillText(hitColliders[i].GetComponent<Enemy>());

                if (HasStatusEffect)
                {
                    if(!CheckStatus(hitColliders[i].GetComponent<Enemy>()))
                    {
                        StatusEffectIconTrans = hitColliders[i].GetComponent<Enemy>().GetDebuffTransform;

                        PoisonStatus(hitColliders[i].GetComponent<Enemy>());
                    }
                }
            }
        }
    }

    private bool CheckStatus(Enemy enemy)
    {
        bool PoisonStatus = false;

        foreach (EnemyStatusIcon enemystatus in enemy.GetDebuffTransform.GetComponentsInChildren<EnemyStatusIcon>())
        {
            if (enemystatus.GetStatusEffect == StatusEffect.Poison)
            {
                PoisonStatus = true;
            }
        }

        return PoisonStatus;
    }

    private TextMeshProUGUI PoisonStatus(Enemy enemy)
    {
        TextHolder = enemy.GetUI;

        var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ Poison";

        StatusTxt.GetComponentInChildren<Image>().sprite = StatusEffectImage;

        ApplyPoisonStatus(enemy);

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void ApplyPoisonStatus(Enemy enemy)
    {
        StatusEffectIconTrans = enemy.GetDebuffTransform;

        StatusIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

        StatusIcon.SetActive(true);

        StatusIcon.transform.SetParent(StatusEffectIconTrans, false);

        StatusIcon.GetComponentInChildren<Image>().sprite = StatusEffectImage;

        StatusIcon.GetComponent<EnemyStatusIcon>().GetHasPoisonStatus = true;

        StatusIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = StatusEffect.Poison;
        StatusIcon.GetComponent<EnemyStatusIcon>().GetPlayer = SkillsManager.Instance.GetCharacter.GetComponent<PlayerController>();
        StatusIcon.GetComponentInChildren<Image>().sprite = StatusEffectImage;
        StatusIcon.GetComponent<EnemyStatusIcon>().PoisonStatus();

        StatusIcon.GetComponent<EnemyStatusIcon>().CreatePoisonEffectParticle();
    }

    private TextMeshProUGUI DamageSkillText(Enemy Target)
    {
        var DamageTxt = ObjectPooler.Instance.GetEnemyDamageText();

        int HitPower = Power + ShadowPriest.CharacterIntelligence;

        if(Target != null)
        {
            DamageTxt.SetActive(true);

            TextHolder = Target.GetUI;

            DamageTxt.transform.SetParent(TextHolder.transform, false);

            if(HitPower - Target.GetCharacter.CharacterIntelligence < 0)
            {
                Target.GetHealth.ModifyHealth(-1);

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + DamageName + " " + 1;
            }
            else
            {
                Target.GetHealth.ModifyHealth(-(HitPower - Target.GetCharacter.CharacterIntelligence));

                DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=15>" + DamageName + " " + (HitPower - Target.GetCharacter.CharacterIntelligence);
            }
        }

        if (Target.GetAI != null)
        {
            if (Target.GetAI.GetPlayerTarget == null)
            {
                Target.GetAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
            }

            if (Target.GetAI.GetStates != States.Skill && Target.GetAI.GetStates != States.ApplyingAttack && Target.GetAI.GetStates != States.SkillAnimation)
            {
                Target.GetAI.GetStates = States.Damaged;
            }
        }
        if (Target.GetPuckAI != null)
        {
            if (Target.GetPuckAI.GetPlayerTarget == null)
            {
                Target.GetPuckAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
                Target.GetPuckAI.GetSphereTrigger.gameObject.SetActive(false);
                Target.GetPuckAI.GetStates = BossStates.Chase;
                Target.GetPuckAI.EnableSpeech();
            }

            Target.GetPuckAI.CheckHP();
        }
        if (Target.GetRuneGolemAI != null)
        {
            if (Target.GetRuneGolemAI.GetPlayerTarget == null)
            {
                Target.GetRuneGolemAI.GetPlayerTarget = SkillsManager.Instance.GetCharacter;
                Target.GetRuneGolemAI.GetSphereTrigger.gameObject.SetActive(false);
                Target.GetRuneGolemAI.GetStates = RuneGolemStates.Chase;
            }

            Target.GetRuneGolemAI.CheckHP();
        }
        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private TextMeshProUGUI StatusEffects()
    {
        var StatusTxt = ObjectPooler.Instance.GetEnemyStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + StatusEffectName;

        StatusTxt.GetComponentInChildren<Image>().sprite = StatusEffectImage;

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }
}
