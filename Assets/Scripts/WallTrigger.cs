using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject WallParticle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            WallParticle.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
