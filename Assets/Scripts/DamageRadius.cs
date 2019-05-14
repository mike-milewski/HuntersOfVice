using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Shapes { Circle, Cylinder, Rectangle }

public class DamageRadius : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private EnemySkills enemySkills;

    [SerializeField]
    private Image DamageShape;

    [SerializeField]
    private Sprite DamageShapeCircle, DamageShapeCylinder, DamageShapeRectangle;

    [SerializeField]
    private float Radius;

    public float GetRadius
    {
        get
        {
            return Radius;
        }
        set
        {
            Radius = value;
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
        if(shapes == Shapes.Rectangle)
        {
            DamageShape.transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
        }
    }

    private void Update()
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

    private void IncreaseCircle()
    {
        DamageShape.transform.localScale = new Vector3(
            Mathf.Clamp(DamageShape.transform.localScale.x, 0, Radius),
            Mathf.Clamp(DamageShape.transform.localScale.y, 0, Radius),
            Mathf.Clamp(DamageShape.transform.localScale.z, 0, Radius));

        if (DamageShape.transform.localScale.x < Radius && DamageShape.transform.localScale.y < Radius && DamageShape.transform.localScale.z < Radius)
           DamageShape.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f) * Time.deltaTime;
    }

    private void IncreaseCylinder()
    {
        DamageShape.transform.localScale = new Vector3(
            Mathf.Clamp(DamageShape.transform.localScale.x, 0, Radius),
            Mathf.Clamp(DamageShape.transform.localScale.y, 0, Radius),
            Mathf.Clamp(DamageShape.transform.localScale.z, 0, Radius));

        if (DamageShape.transform.localScale.x < Radius && DamageShape.transform.localScale.y < Radius && DamageShape.transform.localScale.z < Radius)
            DamageShape.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f) * Time.deltaTime;
    }

    private void IncreaseRectangle()
    {
        DamageShape.transform.localScale = new Vector3(
            Mathf.Clamp(DamageShape.transform.localScale.y, 0, .4f),
            Mathf.Clamp(DamageShape.transform.localScale.y, 0, Radius),
            Mathf.Clamp(DamageShape.transform.localScale.z, 0, Radius));

        if (DamageShape.transform.localScale.y < Radius && DamageShape.transform.localScale.z < Radius)
            DamageShape.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f) * Time.deltaTime;
    }

    public void ResetLocalScale()
    {
        DamageShape.transform.localScale = new Vector3(0, 0, 0);
    }

    //Used for AOE damage skills with a circle shaped radius.
    public void TakeDamageSphereRadius(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for(int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].GetComponent<PlayerController>())
            {
                enemySkills.GetTextHolder = character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Health>().GetDamageTextParent.transform;

                enemySkills.SkillDamageText(enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency, enemySkills.GetManager[enemySkills.GetRandomValue].GetSkillName);

                hitColliders[i].GetComponent<Health>().GetTakingDamage = true;

                hitColliders[i].GetComponent<Health>().ModifyHealth(-enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency - 
                                                                    character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Character>().CharacterDefense);

                character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
            }
        }
    }

    //Used for AOE damage skills with a rectangle shaped radius.
    public void TakeDamageRectangleRadius(Vector3 center, Vector3 radius)
    {
        Collider[] hitColliders = Physics.OverlapBox(center, DamageShape.transform.localScale, character.transform.rotation);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Health>())
            {
                enemySkills.GetTextHolder = character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Health>().GetDamageTextParent.transform;

                enemySkills.SkillDamageText(enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency, enemySkills.GetManager[enemySkills.GetRandomValue].GetSkillName);

                hitColliders[i].GetComponent<Health>().GetTakingDamage = true;

                hitColliders[i].GetComponent<Health>().ModifyHealth(-enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency -
                                                                    character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Character>().CharacterDefense);

                character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
            }
        }
    }
}
