using UnityEngine;

public class DestroyManagers : MonoBehaviour
{
    private void Awake()
    {
        if(GameObject.Find("GameManager"))
        {
            GameObject GM = GameManager.Instance.gameObject;

            Destroy(GM);
        }
        if(GameObject.Find("ObjectPooler"))
        {
            GameObject OP = ObjectPooler.Instance.gameObject;

            Destroy(OP);
        }
    }
}
