#pragma warning disable 0649
using TMPro;
using UnityEngine;

public class HealingSphere : MonoBehaviour
{
    private Character character;

    [SerializeField]
    private Transform HealTextTransform;

    [SerializeField]
    private Settings settings;

    [SerializeField]
    private string SkillName;

    [SerializeField]
    private bool HealsMpOnly;

    [SerializeField]
    private float HealAmount;

    private float Percentage;

    private void OnDisable()
    {
        if(transform.parent)
        {
            if(transform.parent.GetComponent<EnableGameObject>())
            {
                transform.parent.GetComponent<EnableGameObject>().enabled = true;
            }
        }
    }

    private void OnEnable()
    {
        AppearParticle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            character = other.GetComponent<Character>();

            Heal();
            HealParticle();

            gameObject.SetActive(false);
        }
    }

    private TextMeshProUGUI Heal()
    {
        SoundManager.Instance.Heal();

        var HealTxt = ObjectPooler.Instance.GetPlayerHealText();

        HealTxt.SetActive(true);

        HealTxt.transform.SetParent(HealTextTransform.transform, false);

        if(!HealsMpOnly)
        {
            Percentage = (HealAmount / 100) * character.MaxHealth;

            Mathf.Round(Percentage);

            character.GetComponent<Health>().IncreaseHealth((int)Percentage);
        }
        else
        {
            Percentage = (HealAmount / 100) * character.MaxMana;

            Mathf.Round(Percentage);

            character.GetComponent<Mana>().IncreaseMana((int)Percentage);
        }

        if (!HealsMpOnly)
        {
            if (character.CurrentHealth > 0)
            {
                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + SkillName + " " + Mathf.Round(Percentage);
            }
        }
        else
        {
            if (character.CurrentHealth > 0)
            {
                HealTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + SkillName + " " + Mathf.Round(Percentage) + "</size>" + "<size=20>" + " MP";
            }
        }
        return HealTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void AppearParticle()
    {
        if (settings.UseParticleEffects)
        {
            var Appear = ObjectPooler.Instance.GetSoothingOrbParticle();

            Appear.SetActive(true);

            Appear.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        }
    }

    private void HealParticle()
    {
        if (settings.UseParticleEffects)
        {
            var Healparticle = ObjectPooler.Instance.GetHealParticle();

            Healparticle.SetActive(true);

            Healparticle.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 1.0f, character.transform.position.z);

            Healparticle.transform.SetParent(character.transform, true);
        }
    }
}
