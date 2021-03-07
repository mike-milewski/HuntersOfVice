using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "Settings")]
public class Settings : ScriptableObject
{
    public string CharacterSavior;

    public bool UseParticleEffects, MuteAudio, SecretCharacterUnlocked;
}
