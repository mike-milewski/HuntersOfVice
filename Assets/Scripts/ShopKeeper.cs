#pragma warning disable 0414
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

    private Quaternion ShopRotation;

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

    private void Awake()
    {
        ShopRotation = this.transform.rotation;
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
                GameManager.Instance.CloseInventoryMenu();
                IsInShop = false;
            }
            TurnToPlayer();
        }
        else
        {
            ReturnToPosition();
        }
    }

    private void TurnToPlayer()
    {
        Vector3 TargetPosition = new Vector3(character.transform.position.x - this.transform.position.x, 0,
                                             character.transform.position.z - this.transform.position.z).normalized;

        Quaternion LookDir = Quaternion.LookRotation(TargetPosition);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, LookDir, 5 * Time.deltaTime);
    }

    private void ReturnToPosition()
    {
        if(this.transform.rotation == ShopRotation)
        {
            return;
        }
        else
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, ShopRotation, 5 * Time.deltaTime);
        }
    }

    public void SetIsInShopToFalse()
    {
        IsInShop = false;
    }
}
