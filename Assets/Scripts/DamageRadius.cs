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

    private void Start()
    {
        if(shapes == Shapes.Rectangle)
        {
            Vector3 Trans = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);

            transform.position = Trans + character.transform.forward;
        }
        if(shapes == Shapes.Circle)
        {
            transform.position = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
        }
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
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.x, 0, 100),
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.y, 0, 100));

        if (DamageShape.rectTransform.sizeDelta.x < 100 && DamageShape.rectTransform.sizeDelta.y < 100)
           DamageShape.rectTransform.sizeDelta += new Vector2(100f, 100f) * Time.deltaTime;
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
        DamageShape.rectTransform.sizeDelta = new Vector2(
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.x, 0, 30),
            Mathf.Clamp(DamageShape.rectTransform.sizeDelta.y, 0, 90));

        if (DamageShape.rectTransform.sizeDelta.x < 30 && DamageShape.rectTransform.sizeDelta.y < 90)
        {
            DamageShape.rectTransform.sizeDelta += new Vector2(100f, 100f) * Time.deltaTime;
        }
            
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
                    enemySkills.StatusEffectSkillTextTransform();
                }

                enemySkills.SkillDamageText(enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency, enemySkills.GetManager[enemySkills.GetRandomValue].GetSkillName);

                hitColliders[i].GetComponent<Health>().GetTakingDamage = true;

                hitColliders[i].GetComponent<Health>().ModifyHealth(-enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency - 
                                                                    -character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Character>().CharacterDefense);

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
                enemySkills.SkillDamageText(enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency, enemySkills.GetManager[enemySkills.GetRandomValue].GetSkillName);

                hitColliders[i].GetComponent<Health>().GetTakingDamage = true;

                hitColliders[i].GetComponent<Health>().ModifyHealth(-enemySkills.GetManager[enemySkills.GetRandomValue].GetPotency -
                                                                    character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<Character>().CharacterDefense);

                character.GetComponent<EnemyAI>().GetPlayerTarget.GetComponent<PlayerAnimations>().DamagedAnimation();
            }
        }
    }
}
