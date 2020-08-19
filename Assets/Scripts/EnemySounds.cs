#pragma warning disable 0649
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource audiosource;

    [SerializeField]
    private AudioClip audioclip;

    public void PlayMushroomManWalkSE()
    {
        audiosource.PlayOneShot(audioclip);
    }
}
