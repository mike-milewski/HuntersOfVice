using UnityEngine;
using System.Collections;

public class ResetItemMessage : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(ReverseMessage());
    }

    private IEnumerator ReverseMessage()
    {
        float timer = 2.0f;
        yield return new WaitForSeconds(timer);
        gameObject.GetComponent<Animator>().SetBool("Appear", false);
        yield return new WaitForSeconds(timer - 1);
        ObjectPooler.Instance.ReturnItemMessageToPool(gameObject);
    }
}
