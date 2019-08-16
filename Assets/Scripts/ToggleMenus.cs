using UnityEngine;
using UnityEngine.UI;

public class ToggleMenus : MonoBehaviour
{
    public void RaycastTargetTrue()
    {
        if(!gameObject.GetComponent<Image>().raycastTarget)
        {
            gameObject.GetComponent<Image>().raycastTarget = true;
        }
        else
        {
            gameObject.GetComponent<Image>().raycastTarget = false;
        }
    }
}
