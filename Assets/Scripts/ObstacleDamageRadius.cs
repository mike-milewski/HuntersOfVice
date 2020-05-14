﻿#pragma warning disable 0649, 0414
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ObstacleShapes { Circle, Rectangle }

public enum ObstacleStatus { NONE, DamageOverTime, Stun }

public class ObstacleDamageRadius : MonoBehaviour
{
    private Character PlayerTarget = null;

    [SerializeField]
    private string DamageName, StatusEffectName;

    [SerializeField]
    private int DamagePotency;

    [SerializeField]
    private ObstacleStatus obstacleStatus;

    [SerializeField]
    private float DamageTime, TimeToIncrease, SizeDeltaX, SizeDeltaY;

    [SerializeField]
    private Transform StatusEffectTextTransform, DamageTextTransform;

    [SerializeField]
    private Image DamageShape;

    [SerializeField]
    private Sprite DamageShapeCircle, DamageShapeCylinder, DamageShapeRectangle;

    [SerializeField]
    [Tooltip("Image of the status effect inflicted. Only apply if the skill will have a status effect.")]
    private Sprite StatusSprite = null;

    [SerializeField]
    private GameObject StatusIcon = null;

    [SerializeField]
    private GameObject Particle;

    private bool IsInRadius;

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
                break;
        }

        if (shapes == ObstacleShapes.Rectangle)
        {
            Vector3 Trans = new Vector3(gameObject.transform.position.x, transform.position.y, gameObject.transform.position.z);

            transform.position = Trans + gameObject.transform.forward * 1.6f;
        }
        if (shapes == ObstacleShapes.Circle)
        {
            DamageShape.transform.localScale = new Vector3(1, 1, 1);

            transform.position = new Vector3(gameObject.transform.position.x, transform.position.y, gameObject.transform.position.z);
        }
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
            TakeRadiusEffects();
            TimeToIncrease = 0;
        }
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
            DamageShape.rectTransform.sizeDelta += new Vector2(100, 0) * Time.deltaTime;
        }
        if (DamageShape.rectTransform.sizeDelta.y < SizeDeltaY)
        {
            DamageShape.rectTransform.sizeDelta += new Vector2(0, 100) * Time.deltaTime;
        }
    }

    public void ResetLocalScale()
    {
        DamageShape.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ResetSizeDelta()
    {
        DamageShape.rectTransform.sizeDelta = new Vector2(0, 0);
    }

    public void CheckIfPlayerIsInRectangleRadius(Vector3 center, Vector3 radius, Quaternion rotation)
    {
        Collider[] hitColliders = Physics.OverlapBox(center, radius, rotation);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<PlayerController>())
            {
                IsInRadius = true;
                PlayerTarget = hitColliders[i].GetComponent<Character>();
            }
        }
    }

    public void CheckIfPlayerIsInCircleRadius(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<PlayerController>())
            {
                IsInRadius = true;
                PlayerTarget = hitColliders[i].GetComponent<Character>();
            }
        }
    }

    public void TakeRadiusEffects()
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
            StatusIcon = ObjectPooler.Instance.GetEnemyStatusIcon();

            StatusIcon.SetActive(true);

            StatusIcon.transform.SetParent(StatusEffectTextTransform, false);

            StatusIcon.GetComponent<EnemyStatusIcon>().GetStatusEffect = (StatusEffect)obstacleStatus;

            StatusIcon.GetComponent<Image>().sprite = StatusSprite;

            StatusIcon.GetComponent<EnemyStatusIcon>().EnemyInput();
        }
        else
        {
            StatusIcon.GetComponent<EnemyStatusIcon>().EnemyInput();
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
}