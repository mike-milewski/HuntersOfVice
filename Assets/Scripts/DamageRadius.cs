#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Shapes { NONE, Circle, Cylinder, Rectangle }

public class DamageRadius : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private EnemyAI enemyAI;

    [SerializeField]
    private EnemySkills enemySkills;

    [SerializeField]
    private Image DamageShape;

    [SerializeField]
    private Sprite DamageShapeCircle, DamageShapeCylinder, DamageShapeRectangle;

    private bool IsInRadius;

    public bool GetIsInRadius
    {
        get
        {
            return IsInRadius;
        }
        set
        {
            IsInRadius = value;
        }
    }

    public Image GetDamageShape
    {
        get
        {
            return DamageShape;
        }
        set
        {
            DamageShape = value;
        }
    }

    public Shapes shapes;

    public Shapes GetShapes
    {
        get
        {
            return shapes;
        }
        set
        {
            shapes = value;
        }
    }

    private void OnEnable()
    {
        if (shapes == Shapes.Rectangle)
        {
            DamageShape.transform.localScale = new Vector3(1, 1, 1);

            Vector3 Trans = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);

            transform.position = Trans + character.transform.forward * 1.6f;
        }
        if(shapes == Shapes.Circle)
        {
            DamageShape.transform.localScale = new Vector3(1, 1, 1);

            transform.position = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
        }
    }

    public float SetCircleColliderSize()
    {
        float CircleRadius = (float)enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX / (float)40;

        return CircleRadius;
    }

    public Vector3 SetRectangleColliderSize()
    {
        Vector3 WidthHeight = new Vector3(DamageShape.transform.localScale.x, 1, DamageShape.transform.localScale.y);

        return WidthHeight;
    }

    private void Update()
    {
        if(enemyAI.GetStates != States.Immobile)
        {
            switch (shapes)
            {
                case (Shapes.Circle):
                    var Circle = DamageShapeCircle;
                    DamageShape.GetComponent<Image>().sprite = Circle;
                    IncreaseCircle();
                    break;
                case (Shapes.Rectangle):
                    var Rectangle = DamageShapeRectangle;
                    DamageShape.GetComponent<Image>().sprite = Rectangle;
                    IncreaseRectangle();
                    break;
            }
        }
        else
        {
            enemySkills.DisableRadiusImage();
            enemySkills.DisableRadius();
        }
    }

    private void IncreaseCircle()
    {
        DamageShape.rectTransform.sizeDelta = new Vector2(
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.x, 0, enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX),
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.y, 0, enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY));

        if (DamageShape.rectTransform.sizeDelta.x < enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX && DamageShape.rectTransform.sizeDelta.y < 
                                                    enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY)

           DamageShape.rectTransform.sizeDelta += new Vector2(300f, 300f) * Time.deltaTime;
    }

    private void IncreaseRectangle()
    {
        DamageShape.rectTransform.sizeDelta = new Vector2(
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.x, 0, enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX),
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.y, 0, enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY));

        if (DamageShape.rectTransform.sizeDelta.x < enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaX)
        {
            DamageShape.rectTransform.sizeDelta += new Vector2(300, 0) * Time.deltaTime;
        }
        if(DamageShape.rectTransform.sizeDelta.y < enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSizeDeltaY)
        {
            DamageShape.rectTransform.sizeDelta += new Vector2(0, 300) * Time.deltaTime;
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
            }
        }
    }

    private bool CheckStatusEffectLimit()
    {
        bool ReachedLimit = false;

        if (enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatusEffectHolder.transform.childCount >= 6)
        {
            ReachedLimit = true;
        }
        return ReachedLimit;
    }

    public void TakeRadiusDamage()
    {
        if (enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetStatus != Status.NONE)
        {
            if(!CheckStatusEffectLimit())
            {
                enemySkills.PlayerStatus();
            }
        }

        if(enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency > 0)
        {
            enemySkills.SkillDamageText(enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetPotency,
                                    enemySkills.GetManager[enemyAI.GetAiStates[enemyAI.GetStateArrayIndex].GetSkillIndex].GetSkillName);

            enemyAI.GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
        }
    }
}
