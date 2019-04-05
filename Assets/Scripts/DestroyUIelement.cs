using UnityEngine;

public class DestroyUIelement : MonoBehaviour
{
    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public void DestroyUI()
    {
        Destroy(this.gameObject);
    }
}
