using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUp : MonoBehaviour
{
    private GameObject SkillMenuParent = null;

    [SerializeField]
    private TextMeshProUGUI SkillText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Image SkillImage;

    public TextMeshProUGUI GetSkillText
    {
        get
        {
            return SkillText;
        }
        set
        {
            SkillText = value;
        }
    }

    public Image GetSkillImage
    {
        get
        {
            return SkillImage;
        }
        set
        {
            SkillImage = value;
        }
    }

    public void PlayLevelUp()
    {
        animator.SetBool("LevelUp", true);
    }

    private void Start()
    {
        SkillMenuParent = gameObject.transform.parent.parent.gameObject;
    }

    public void ReverseLevelUp()
    {
        foreach(SkillMenu s in SkillMenuParent.GetComponentsInChildren<SkillMenu>())
        {
            s.ShowNewSkill();
        }

        animator.SetBool("LevelUp", false);
    }

    public void PlaySkillLearned()
    {
        animator.SetBool("NewSkill", true);
    }

    public void ReverseSkillLearned()
    {
        animator.SetBool("NewSkill", false);
    }

    public void ReturnLevelTextToPool()
    {
        if(!animator.GetBool("NewSkill"))
        {
            ObjectPooler.Instance.ReturnLevelUpTextToPool(this.gameObject.transform.parent.gameObject);
        }
    }
}
