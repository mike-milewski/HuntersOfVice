#pragma warning disable 0649
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EarthEffigy : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private RuneGolem runeGolem;

    [SerializeField]
    private Transform TextHolder, StatusEffectIconTransform;

    [SerializeField]
    private GameObject GetStatusIcon;

    [SerializeField]
    private GameObject[] ParticleObjectsToDisable;

    [SerializeField]
    private ChangeEnemyMaterial[] changeEnemyMaterial;

    [SerializeField]
    private Spin[] spin;

    [SerializeField]
    private Sprite StatusEffectSprite;

    [SerializeField]
    private string StatusEffectName;

    private Character PlayerCharacter;

    private void OnEnable()
    {
        ResetStats();

        if (GameManager.Instance.GetKnight.activeInHierarchy)
        {
            PlayerCharacter = GameManager.Instance.GetKnight.GetComponent<Character>();
        }
        if(GameManager.Instance.GetShadowPriest.activeInHierarchy)
        {
            PlayerCharacter = GameManager.Instance.GetShadowPriest.GetComponent<Character>();
        }
        if(GameManager.Instance.GetToadstool.activeInHierarchy)
        {
            PlayerCharacter = GameManager.Instance.GetToadstool.GetComponent<Character>();
        }
    }

    public void Dead()
    {
        runeGolem.GetRuneGolemPhases[runeGolem.GetPhaseIndex].GetEarthEffigyKillCount++;

        if (runeGolem.GetRuneGolemPhases[runeGolem.GetPhaseIndex].GetEarthEffigyKillCount >= runeGolem.GetRuneGolemPhases[runeGolem.GetPhaseIndex].GetEarthEffigysToKill)
        {
            GiveStatusEffect();
        }

        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        enemy.GetLocalHealth.gameObject.SetActive(false);

        if (SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().GetTarget != null)
        {
            SkillsManager.Instance.GetCharacter.GetComponent<BasicAttack>().RemoveTarget();
        }

        for (int i = 0; i < ParticleObjectsToDisable.Length; i++)
        {
            ParticleObjectsToDisable[i].SetActive(false);
        }
        for (int j = 0; j < spin.Length; j++)
        {
            spin[j].enabled = false;
        }
        for(int k = 0; k < changeEnemyMaterial.Length; k++)
        {
            changeEnemyMaterial[k].ChangeEquipmentToAlphaMaterial();

            StartCoroutine(changeEnemyMaterial[k].EquipmentFade());
        }
    }

    private void ResetStats()
    {
        for (int i = 0; i < ParticleObjectsToDisable.Length; i++)
        {
            ParticleObjectsToDisable[i].SetActive(true);
        }
        for(int j = 0; j < spin.Length; j++)
        {
            spin[j].enabled = true;
        }

        enemy.GetLocalHealth.gameObject.SetActive(true);
        enemy.GetHealth.ResetHealthAnimation();
        enemy.GetHealth.IncreaseHealth(character.MaxHealth);
        enemy.GetLocalHealthInfo();
        enemy.GetEnemyInfo();

        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void GiveStatusEffect()
    {
        PlayerStatus();
    }

    private TextMeshProUGUI PlayerStatus()
    {
        var StatusTxt = ObjectPooler.Instance.GetPlayerStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(TextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + StatusEffectName;

        StatusTxt.GetComponentInChildren<Image>().sprite = StatusEffectSprite;

        StatusEffectSkillText();

        return StatusTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void StatusEffectSkillText()
    {
        if (GetStatusIcon.GetComponent<StatusIcon>())
        {
            GetStatusIcon = ObjectPooler.Instance.GetPlayerStatusIcon();

            GetStatusIcon.SetActive(true);

            GetStatusIcon.transform.SetParent(StatusEffectIconTransform, false);

            GetStatusIcon.GetComponent<StatusIcon>().GetEnemyTarget = null;

            GetStatusIcon.GetComponent<StatusIcon>().GetEffectStatus = EffectStatus.EarthenProtection;

            GetStatusIcon.GetComponent<StatusIcon>().EarthenProtectionInput();

            GetStatusIcon.GetComponentInChildren<Image>().sprite = StatusEffectSprite;

            GetStatusIcon.GetComponent<StatusIcon>().CheckStatusEffect();
        }
    }
}
