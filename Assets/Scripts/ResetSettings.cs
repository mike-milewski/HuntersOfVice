#pragma warning disable 0649
using UnityEngine;

public class ResetSettings : MonoBehaviour
{
    [SerializeField]
    private Settings settings;

    private void OnEnable()
    {
        settings.MuteAudio = false;
        settings.UseParticleEffects = true;

        SelectedCharacter selectedCharacter = GameObject.FindObjectOfType<SelectedCharacter>();

        if(selectedCharacter != null)
        {
            selectedCharacter.GetKnightSelected = false;
            selectedCharacter.GetShadowPriestSelected = false;
        }
    }
}
