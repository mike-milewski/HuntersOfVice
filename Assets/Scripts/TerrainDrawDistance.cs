#pragma warning disable 0649
using UnityEngine;

public class TerrainDrawDistance : MonoBehaviour
{
    [SerializeField]
    private Terrain m_terrain;  //reference to your terrain

    [SerializeField]
    private float DrawDistance; // how far you want to be able to see the grass

    void Start()
    {
        m_terrain.detailObjectDistance = DrawDistance;
    }
}
