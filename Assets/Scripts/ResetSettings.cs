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
    }
}
