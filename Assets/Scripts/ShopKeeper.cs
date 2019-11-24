using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private Animator ShopAnimator, UpgradeAnimator, BuyAnimator;

    [SerializeField]
    private float ShoppingDistance;

    private bool IsInShop;

    public bool GetIsInShop
    {
        get
        {
            return IsInShop;
        }
        set
        {
            IsInShop = value;
        }
    }

    public Animator GetAnimator
    {
        get
        {
            return animator;
        }
        set
        {
            animator = value;
        }
    }

    private void Update()
    {
        if(IsInShop)
        {
            if(Vector3.Distance(this.transform.position, character.transform.position) > ShoppingDistance)
            {
                ShopAnimator.SetBool("FadeIn", false);
                UpgradeAnimator.SetBool("FadeIn", false);
                BuyAnimator.SetBool("FadeIn", false);
                IsInShop = false;
            }
        }
    }
}
