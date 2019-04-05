using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Shapes { Circle, Cylinder, Rectangle}

public class DamageRadius : MonoBehaviour
{
    [SerializeField]
    private Image DamageShape;

    [SerializeField]
    private Sprite DamageShapeCircle, DamageShapeCylinder, DamageShapeRectangle;

    [SerializeField]
    private float Radius;

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

    private void Update()
    {
        switch (shapes)
        {
            case (Shapes.Circle):
                var Circle = DamageShapeCircle;
                DamageShape.GetComponent<Image>().sprite = Circle;
                break;
            case (Shapes.Cylinder):
                var Cylinder = DamageShapeCylinder;
                DamageShape.GetComponent<Image>().sprite = Cylinder;
                break;
            case (Shapes.Rectangle):
                var Rectangle = DamageShapeRectangle;
                DamageShape.GetComponent<Image>().sprite = Rectangle;
                break;
        }
        DamageShape.transform.localScale = new Vector3(Radius, Radius, Radius);
    }

    private void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for(int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log(i);
        }
    }
}
