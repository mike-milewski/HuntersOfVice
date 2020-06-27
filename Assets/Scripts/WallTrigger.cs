#pragma warning disable 0649
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioChanger audioChanger;

    [SerializeField]
    private GameObject WallParticle;

    [SerializeField]
    private bool ChangeToMiniBossTheme, ChangeToBossTheme;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            WallParticle.SetActive(true);

            CheckAudioSwitch();

            this.gameObject.SetActive(false);
        }
    }

    private void CheckAudioSwitch()
    {
        audioChanger.gameObject.SetActive(true);

        if (ChangeToMiniBossTheme)
        {
            audioChanger.GetChangeToMiniBossTheme = true;
        }
        else if(ChangeToBossTheme)
        {
            audioChanger.GetChangeToMainBossTheme = true;
        }
    }
}
