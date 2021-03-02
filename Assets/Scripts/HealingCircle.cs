#pragma warning disable 0649
using UnityEngine;

public class HealingCircle : MonoBehaviour
{
    [SerializeField]
    private Settings settings;

    private Character Player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            if(other.GetComponent<Character>().CurrentHealth >= other.GetComponent<Character>().MaxHealth && 
               other.GetComponent<Character>().CurrentMana >= other.GetComponent<Character>().MaxMana)
            {
                return;
            }
            else
            {
                Player = other.GetComponent<Character>();
                Heal();
            }
        }
    }

    private void Heal()
    {
        SoundManager.Instance.Heal();
        if(settings.UseParticleEffects)
        {
            var HealParticle = ObjectPooler.Instance.GetHealParticle();

            HealParticle.SetActive(true);

            HealParticle.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.0f, Player.transform.position.z);

            HealParticle.transform.SetParent(Player.transform, true);
        }

        Player.GetComponent<Health>().IncreaseHealth(Player.MaxHealth);
        Player.GetComponent<Mana>().IncreaseMana(Player.MaxMana);
    }
}
