using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Shapes { Circle, Cylinder, Rectangle }

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

    private void Awake()
    {
        this.gameObject.SetActive(false);

        switch(shapes)
        {
            case (Shapes.Rectangle):
                DamageShape.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                break;
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

    private void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for(int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log(i);
        }
    }
}
