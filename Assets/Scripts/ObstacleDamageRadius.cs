#pragma warning disable 0649, 0414
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ObstacleShapes { Circle, Rectangle }

public enum ObstacleStatus { NONE, DamageOverTime, Stun }

public class ObstacleDamageRadius : MonoBehaviour
{
    [SerializeField]
    private Settings settings;

    private Character PlayerTarget = null;

    [SerializeField]
    private string DamageName, StatusEffectName, StatusEffectDescrption;

    [SerializeField]
    private int DamagePotency;

    [SerializeField]
    private ObstacleStatus obstacleStatus;

    [SerializeField]
    private float DamageTime, TimeToIncrease, SizeDeltaX, SizeDeltaY, InvokeEffectTime, InvokeParticleEffectTime, StatusDuration, DamageTick;

    [SerializeField]
    private Vector3 ShapeSize;

    [SerializeField]
    private Transform StatusEffectTextTransform, DamageTextTransform, StatusIconTransform;

    [SerializeField]
    private Image DamageShape;

    [SerializeField]
    private Sprite DamageShapeCircle, DamageShapeRectangle;

    [SerializeField]
    [Tooltip("Image of the status effect inflicted. Only apply if the skill will have a status effect.")]
    private Sprite StatusSprite = null;

    [SerializeField]
    private GameObject StatusIcon = null;

    [SerializeField]
    private GameObject Particle;

    private bool IsInRadius, DisabledRadius;

    [SerializeField]
    private ObstacleShapes shapes;

    private void OnEnable()
    {
        switch (shapes)
        {
            case (ObstacleShapes.Circle):
                var Circle = DamageShapeCircle;
                DamageShape.GetComponent<Image>().sprite = Circle;
                break;
            case (ObstacleShapes.Rectangle):
                var Rectangle = DamageShapeRectangle;
                DamageShape.GetComponent<Image>().sprite = Rectangle;
                SetBoxColliderSize();
                break;
        }

        if (shapes == ObstacleShapes.Rectangle)
        {
            Vector3 Trans = new Vector3(gameObject.transform.position.x, transform.position.y, gameObject.transform.position.z);

            transform.position = Trans + gameObject.transform.up * 8f;
        }
        if (shapes == ObstacleShapes.Circle)
        {
            DamageShape.transform.localScale = new Vector3(1, 1, 1);

            transform.position = new Vector3(gameObject.transform.position.x, transform.position.y, gameObject.transform.position.z);
        }
    }

    private void OnDisable()
    {
        ResetSizeDelta();

        TimeToIncrease = 0;
    }

    private float SetCircleColliderSize()
    {
        float CircleRadius = SizeDeltaX / (float)40;

        return CircleRadius;
    }

    private Vector3 SetRectangleColliderSize()
    {
        Vector3 WidthHeight = new Vector3(DamageShape.transform.localScale.x, 1, DamageShape.transform.localScale.y);

        return WidthHeight;
    }

    private void Update()
    {
        if(!DisabledRadius)
        IncreaseRadiusAndTime();
    }

    private void IncreaseRadiusAndTime()
    {
        switch (shapes)
        {
            case (ObstacleShapes.Circle):
                IncreaseCircle();
                break;
            case (ObstacleShapes.Rectangle):
                IncreaseRectangle();
                break;
        }

        TimeToIncrease += Time.deltaTime;
        if(TimeToIncrease >= DamageTime)
        {
            switch (shapes)
            {
                case (ObstacleShapes.Circle):
                    CheckIfPlayerIsInCircleRadius(DamageShape.transform.position, SetCircleColliderSize());
                    DisableRadius();
                    break;
                case (ObstacleShapes.Rectangle):
                    InvokeEffect();
                    DisableRadius();
                    break;
            }
        }
    }

    private void SetBoxColliderSize()
    {
        gameObject.GetComponent<BoxCollider>().size = new Vector2(SizeDeltaX, SizeDeltaY);
    }

    private void IncreaseCircle()
    {
        DamageShape.rectTransform.sizeDelta = new Vector2(
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.x, 0, SizeDeltaX),
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.y, 0, SizeDeltaY));

        if (DamageShape.rectTransform.sizeDelta.x < SizeDeltaX && DamageShape.rectTransform.sizeDelta.y <
                                                    SizeDeltaY)

            DamageShape.rectTransform.sizeDelta += new Vector2(100f, 100f) * Time.deltaTime;
    }

    private void IncreaseRectangle()
    {
        DamageShape.rectTransform.sizeDelta = new Vector2(
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.x, 0, SizeDeltaX),
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.y, 0, SizeDeltaY));

        if (DamageShape.rectTransform.sizeDelta.x < SizeDeltaX)
        {
            DamageShape.rectTransform.sizeDelta += new Vector2(100, 0) * 13 * Time.deltaTime;
        }
        if (DamageShape.rectTransform.sizeDelta.y < SizeDeltaY)
        {
            DamageShape.rectTransform.sizeDelta += new Vector2(0, 100) * 13 * Time.deltaTime;
        }
    }

    private void ResetLocalScale()
    {
        DamageShape.transform.localScale = new Vector3(0, 0, 0);
    }

    private void ResetSizeDelta()
    {
        DamageShape.rectTransform.sizeDelta = new Vector2(0, 0);
    }

    private void CheckIfPlayerIsInCircleRadius(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        InvokeParticle();

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<PlayerController>())
            {
                IsInRadius = true;
                PlayerTarget = hitColliders[i].GetComponent<Character>();
                InvokeEffect();
            }
        }
    }

    private void InvokeParticle()
    {
        Invoke("CastParticleEffect", InvokeParticleEffectTime);
    }

    private void InvokeEffect()
    {
        Invoke("TakeRadiusEffects", InvokeEffectTime);
    }

    private void EnableRadius()
    {
        DisabledRadius = false;
    }

    private void DisableRadius()
    {
        ResetSizeDelta();

        DisabledRadius = true;

        TimeToIncrease = 0;

        Invoke("EnableRadius", 1);
    }

    private void TakeRadiusEffects()
    {
        if(IsInRadius)
        {
            if (obstacleStatus != ObstacleStatus.NONE)
            {
                PlayerStatus();
            }

            if (DamagePotency > 0)
            {
                DamageText(DamagePotency, DamageName);

                PlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
            }
        }
    }

    private void CastParticleEffect()
    {
        if (settings.UseParticleEffects)
        {
            Particle = ObjectPooler.Instance.GetPoisonSporeParticle();

            Particle.SetActive(true);

            Particle.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);

            Particle.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private TextMeshProUGUI PlayerStatus()
    {
        var StatusEffectText = ObjectPooler.Instance.GetPlayerStatusText();

        StatusEffectText.SetActive(true);

        StatusEffectText.transform.SetParent(StatusEffectTextTransform, false);

        StatusEffectText.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ " + StatusEffectName;

        StatusEffectText.GetComponentInChildren<Image>().sprite = StatusSprite;

        StatusEffectSkillTextTransform();

        return StatusEffectText.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void StatusEffectSkillTextTransform()
    {
        if (!StatusIcon.activeInHierarchy)
        {
            StatusIcon = ObjectPooler.Instance.GetPlayerStatusIcon();

            StatusIcon.SetActive(true);

            StatusIcon.transform.SetParent(StatusIconTransform, false);

            StatusIcon.GetComponent<StatusIcon>().GetObstacleEffect = true;

            StatusIcon.GetComponent<StatusIcon>().GetDamageOrHealTick = DamageTick;

            StatusIcon.GetComponent<StatusIcon>().GetTempTick = StatusIcon.GetComponent<StatusIcon>().GetDamageOrHealTick;

            StatusIcon.GetComponent<StatusIcon>().GetEffectStatus = (EffectStatus)obstacleStatus;

            StatusIcon.GetComponent<Image>().sprite = StatusSprite;

            StatusIcon.GetComponent<StatusIcon>().GetObstacleEffect = true;

            StatusIcon.GetComponent<StatusIcon>().GetStatusDescription.text = StatusEffectDescrption;

            StatusIcon.GetComponent<StatusIcon>().GetDuration = StatusDuration;
        }
        else
        {
            StatusIcon.GetComponent<StatusIcon>().GetObstacleEffect = true;

            StatusIcon.GetComponent<StatusIcon>().GetDamageOrHealTick = DamageTick;

            StatusIcon.GetComponent<StatusIcon>().GetTempTick = StatusIcon.GetComponent<StatusIcon>().GetDamageOrHealTick;

            StatusIcon.GetComponent<StatusIcon>().GetStatusDescription.text = StatusEffectDescrption;

            StatusIcon.GetComponent<StatusIcon>().GetDuration = StatusDuration;
        }
    }

    private TextMeshProUGUI DamageText(int Damage, string Name)
    {
        var DamageTxt = ObjectPooler.Instance.GetPlayerDamageText();

        DamageTxt.SetActive(true);

        DamageTxt.transform.SetParent(DamageTextTransform, false);

        if (Damage - PlayerTarget.GetComponent<Character>().CharacterDefense < 0)
        {
            PlayerTarget.GetComponentInChildren<Health>().ModifyHealth(-1);

            DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + Name + " " + "1";
        }
        else
        {
            PlayerTarget.GetComponentInChildren<Health>().ModifyHealth(-(Damage - PlayerTarget.GetComponent<Character>().CharacterDefense));

            DamageTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<size=25>" + Name + " " +
                                                                       (Damage - PlayerTarget.GetComponent<Character>().CharacterDefense).ToString();
        }

        if (!SkillsManager.Instance.GetActivatedSkill)
        {
            PlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
        }

        return DamageTxt.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            PlayerTarget = other.GetComponent<Character>();

            IsInRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PlayerTarget = null;

            IsInRadius = false;
        }
    }
}