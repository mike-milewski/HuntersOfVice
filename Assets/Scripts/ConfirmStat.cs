using UnityEngine;

public class ConfirmStat : MonoBehaviour
{
    [SerializeField]
    private GameObject StatusButton, Stats, StatPointsText, StatParent;

    public void ConfirmStatus(Experience experience)
    {
        experience.GetMaxStatPoints = experience.GetStatPoints;

        AddStats();

        if(experience.GetStatPoints <= 0)
        {
            Stats.SetActive(false);
            StatPointsText.SetActive(false);

            StatusButton.GetComponent<Animator>().SetBool("StatPoints", false);
            gameObject.SetActive(false);
        }
    }

    private void AddStats()
    {
        var stat = StatParent.GetComponentsInChildren<StatUpgrade>();

        for(int i = 0; i < StatParent.GetComponentsInChildren<StatUpgrade>().Length; i++)
        {
            stat[i].AddStats();
        }

        SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
    }
}
