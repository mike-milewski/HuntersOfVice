using UnityEngine;
using System.Collections;

public class ResetItemMessage : MonoBehaviour
{
    private IEnumerator ReverseMessage()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<Animator>().SetBool("Appear", false);
        yield return new WaitForSeconds(1);
        ObjectPooler.Instance.ReturnItemMessageToPool(gameObject);
    }
}
