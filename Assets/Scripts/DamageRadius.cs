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
            DamageShape.rectTransform.sizeDelta = new Vector2(enemySkills.GetManager[enemySkills.GetRandomValue].GetSizeDeltaX, 
                                                              enemySkills.GetManager[enemySkills.GetRandomValue].GetSizeDeltaY);

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
        float T = (float)enemySkills.GetManager[enemySkills.GetRandomValue].GetSizeDeltaX / (float)40;

        return T;
    }

    public Vector3 SetRectangleColliderSize()
    {
        Vector3 WH = new Vector3(DamageShape.transform.localScale.x, 1, DamageShape.transform.localScale.y);

        return WH;
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
                case (Shapes.Cylinder):
                    var Cylinder = DamageShapeCylinder;
                    DamageShape.GetComponent<Image>().sprite = Cylinder;
                    IncreaseCylinder();
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
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.x, 0, enemySkills.GetManager[enemySkills.GetRandomValue].GetSizeDeltaX),
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.y, 0, enemySkills.GetManager[enemySkills.GetRandomValue].GetSizeDeltaY));

        if (DamageShape.rectTransform.sizeDelta.x < enemySkills.GetManager[enemySkills.GetRandomValue].GetSizeDeltaX && DamageShape.rectTransform.sizeDelta.y < 
                                                    enemySkills.GetManager[enemySkills.GetRandomValue].GetSizeDeltaY)

           DamageShape.rectTransform.sizeDelta += new Vector2(100f, 100f) * Time.deltaTime;
    }

    private void IncreaseCylinder()
    {
        /*
        DamageShape.transform.localScale = new Vector3(
            Mathf.Clamp(DamageShape.transform.localScale.x, 0, Radius),
            Mathf.Clamp(DamageShape.transform.localScale.y, 0, Radius),
            Mathf.Clamp(DamageShape.transform.localScale.z, 0, Radius));

        if (DamageShape.transform.localScale.x < Radius && DamageShape.transform.localScale.y < Radius && DamageShape.transform.localScale.z < Radius)
            DamageShape.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f) * Time.deltaTime;
            */
    }

    private void IncreaseRectangle()
    {
        DamageShape.transform.localScale = new Vector3(enemySkills.GetManager[enemySkills.GetRandomValue].GetShapeSize.x, 
                                                       Mathf.Clamp(DamageShape.transform.localScale.z, 0, enemySkills.GetManager[enemySkills.GetRandomValue].GetShapeSize.z),
                                                       Mathf.Clamp(DamageShape.transform.localScale.z, 0, enemySkills.GetManager[enemySkills.GetRandomValue].GetShapeSize.z));

        if (DamageShape.transform.localScale.y < enemySkills.GetManager[enemySkills.GetRandomValue].GetShapeSize.y)
        {
            DamageShape.transform.localScale += new Vector3(0f, 0f, 7f) * Time.deltaTime;
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

    //Used for AOE damage skills with a circle shaped radius.
    public void TakeDamageSphereRadius(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for(int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].GetComponent<PlayerController>())
            {
                if (enemySkills.GetManager[enemySkills.GetRandomValue].GetStatus != Status.NONE)
                {
                    enemySkills.PlayerStatus();
                }

                enemySkills.SkillDamageText(enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency, enemySkills.GetManager[enemySkills.GetRandomValue].GetSkillName);

                character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
            }
        }
    }

    //Used for AOE damage skills with a rectangle shaped radius.
    public void TakeDamageRectangleRadius(Vector3 center, Vector3 radius)
    {
        Collider[] hitColliders = Physics.OverlapBox(center, radius, character.transform.rotation);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Health>())
            {
                if (enemySkills.GetManager[enemySkills.GetRandomValue].GetStatus != Status.NONE)
                {
                    enemySkills.PlayerStatus();
                }

                enemySkills.SkillDamageText(enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency, enemySkills.GetManager[enemySkills.GetRandomValue].GetSkillName);

                character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
            }
        }
    }
}
