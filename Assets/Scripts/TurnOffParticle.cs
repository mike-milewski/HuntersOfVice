using UnityEngine;

public class TurnOffParticle : MonoBehaviour
{
    public static TurnOffParticle Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DisableParticle()
    {
        this.gameObject.SetActive(false);
    }

    public void EnableParticle()
    {
        this.gameObject.SetActive(true);
    }
}
