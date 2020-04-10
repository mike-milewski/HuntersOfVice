using UnityEngine;

public class SetAudioVolume : MonoBehaviour
{
    public void OnEnable()
    {
        if(this.GetComponent<AudioSource>() != null && MenuButtons.Instance != null)
        this.GetComponent<AudioSource>().volume = MenuButtons.Instance.GetSlider.value;
    }
}
