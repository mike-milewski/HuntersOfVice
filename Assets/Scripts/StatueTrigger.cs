#pragma warning disable 0649
using UnityEngine;

public class StatueTrigger : MonoBehaviour
{
    [SerializeField]
    private StatueObstacle[] statueObstacle;

    [SerializeField]
    private SpikeTrap[] spikeTraps;

    [SerializeField]
    private GameObject[] StatueTriggersColliders;

    [SerializeField]
    private bool TurnOffAllTrapObjects;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            if(TurnOffAllTrapObjects)
            {
                TurnOffAllTraps();
            }
            else
            {
                StatueTriggersColliders[0].SetActive(true);
                StatueTriggersColliders[1].SetActive(false);

                CheckStatues(other.GetComponent<PlayerController>());
            }
        }
    }

    private void TurnOffAllTraps()
    {
        for(int i = 0; i < statueObstacle.Length; i++)
        {
            if(statueObstacle[i].enabled)
            {
                statueObstacle[i].GetObstacleDamageRadius.enabled = false;
                statueObstacle[i].enabled = false;
            }
        }
        for(int j = 0; j < spikeTraps.Length; j++)
        {
            spikeTraps[j].GetComponent<BoxCollider>().enabled = false;
        }
        for(int k = 0; k < StatueTriggersColliders.Length; k++)
        {
            if(StatueTriggersColliders[k].activeInHierarchy)
            {
                StatueTriggersColliders[k].SetActive(false);
            }
        }
    }

    private void CheckStatues(PlayerController Player)
    {
        for(int i = 0; i < statueObstacle.Length; i++)
        {
            if(!statueObstacle[i].enabled)
            {
                statueObstacle[i].enabled = true;
                statueObstacle[i].GetPlayerTarget = Player.GetComponent<Character>();
                statueObstacle[i].GetIsAttacking = false;
                statueObstacle[i].GetIsFollowing = true;
                statueObstacle[i].GetComponent<ReturnObjectToOriginalRotation>().enabled = false;
            }
            else
            {
                statueObstacle[i].GetIsAttacking = false;
                statueObstacle[i].enabled = false;
                statueObstacle[i].GetObstacleDamageRadius.enabled = false;
                statueObstacle[i].GetComponent<ReturnObjectToOriginalRotation>().enabled = true;
            }
        }
    }
}
