#pragma warning disable 0414, 0649
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private Character character, Knight, ShadowPriest;

    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private Animator ShopAnimator, UpgradeAnimator, BuyAnimator;

    [SerializeField]
    private Transform CurrentPosition;

    [SerializeField]
    private float ShoppingDistance;

    private bool IsInShop = false;

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

    public Transform GetCurrentPosition
    {
        get
        {
            return CurrentPosition;
        }
        set
        {
            CurrentPosition = value;
        }
    }

    private void OnEnable()
    {
        if(Knight.gameObject.activeInHierarchy)
        {
            character = Knight;
        }
        else if(ShadowPriest.gameObject.activeInHierarchy)
        {
            character = ShadowPriest;
        }
    }

    private void Update()
    {
        if(IsInShop)
        {
            if(Vector3.Distance(this.transform.position, character.transform.position) > ShoppingDistance)
            {
                SoundManager.Instance.ReverseMenu();
                ShopAnimator.SetBool("FadeIn", false);
                UpgradeAnimator.SetBool("FadeIn", false);
                BuyAnimator.SetBool("FadeIn", false);
                GameManager.Instance.CloseInventoryMenu();
                IsInShop = false;
            }
        }
    }

    public void SetIsInShopToFalse()
    {
        IsInShop = false;
    }
}
