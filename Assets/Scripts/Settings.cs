using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "Settings")]
public class Settings : ScriptableObject
{
    public bool UseParticleEffects, MuteAudio;
}
