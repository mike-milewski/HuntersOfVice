using UnityEngine;

[CreateAssetMenu(fileName = "Material")]
public class MaterialData : ScriptableObject
{
    public Sprite MaterialSprite;

    public string MaterialName;

    public int ShopPoints;

    [TextArea]
    public string MaterialDescription;
}
