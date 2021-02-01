#pragma warning disable 0649
using System.Collections;
using UnityEngine;

public class MuteBossAudio : MonoBehaviour
{
    [SerializeField]
    private AudioChanger audioChanger;

    private void OnEnable()
    {
        StartCoroutine("WaitForVictoryPose");
    }

    private void Update()
    {
        audioChanger.MuteAudio();
    }

    private IEnumerator WaitForVictoryPose()
    {
        yield return new WaitForSeconds(5);
        GameManager.Instance.GetCharacter.GetComponent<Animator>().SetBool("VictoryPose", true);
    }
}
