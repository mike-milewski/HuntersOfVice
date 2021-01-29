#pragma warning disable 0649
using UnityEngine;
using TMPro;

public class TipText : MonoBehaviour
{
    [SerializeField]
    private Animator TipTextAnimator;

    [SerializeField]
    private TextMeshProUGUI tipText;

    [SerializeField]
    private string tipString;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            ShowTip();
            CheckCollider();
        }
    }

    private void CheckCollider()
    {
        if(gameObject.GetComponent<BoxCollider>())
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public TextMeshProUGUI ShowTip()
    {
        tipText.gameObject.SetActive(true);

        TipTextAnimator.Play("TipText", -1, 0f);

        SoundManager.Instance.RecievedItem();

        tipText.text = tipString;

        return tipText;
    }
}
