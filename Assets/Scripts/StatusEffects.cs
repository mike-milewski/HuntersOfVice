using UnityEngine;
using UnityEngine.UI;

public class StatusEffects : MonoBehaviour
{
    [SerializeField]
    private EffectStatus PlayerStatusEffect;

    [SerializeField]
    private StatusEffect EnemystatusEffect;

    [SerializeField]
    private Character character;

    [SerializeField]
    private GameObject StatusIcon;

    [SerializeField]
    private Transform StatusEffectIconTrans = null;

    [SerializeField]
    private string StatusEffectName, StatusDescription;

    [SerializeField] [Tooltip("The duration of the status effect. Set this to -1 to make the effect permanent.")]
    private float StatusDuration;

    public Character GetCharacter
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
        }
    }

    public GameObject GetStatusIcon
    {
        get
        {
            return StatusIcon;
        }
        set
        {
            StatusIcon = value;
        }
    }

    public Transform GetStatusEffectIconTrans
    {
        get
        {
            return StatusEffectIconTrans;
        }
        set
        {
            StatusEffectIconTrans = value;
        }
    }

    public string GetStatusEffectName
    {
        get
        {
            return StatusEffectName;
        }
        set
        {
            StatusEffectName = value;
        }
    }

    public string GetStatusDescription
    {
        get
        {
            return StatusDescription;
        }
        set
        {
            StatusDescription = value;
        }
    }

    public float GetStatusDuration
    {
        get
        {
            return StatusDuration;
        }
        set
        {
            StatusDuration = value;
        }
    }

    public EffectStatus GetPlayerStatusEffect
    {
        get
        {
            return PlayerStatusEffect;
        }
        set
        {
            PlayerStatusEffect = value;
        }
    }

    public StatusEffect GetEnemyStatusEffect
    {
        get
        {
            return EnemystatusEffect;
        }
        set
        {
            EnemystatusEffect = value;
        }
    }
}
