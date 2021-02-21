#pragma warning disable 0649
using System.Collections;
using UnityEngine;

public class ChangeEnemyMaterial : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer = null;

    [SerializeField]
    private MeshRenderer meshRenderer = null;

    [SerializeField]
    private EnemyRespawn enemyRespawn = null;

    [SerializeField]
    private Material AlphaMaterial, OpaqueMaterial;

    [SerializeField]
    private GameObject ParentObject;

    private void Awake()
    {
        if(skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
        if(meshRenderer != null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
    }

    private void OnEnable()
    {
        if(skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.material = OpaqueMaterial;
        }
        if(meshRenderer != null)
        {
            meshRenderer.material = OpaqueMaterial;
        }
    }

    public void ChangeEquipmentToAlphaMaterial()
    {
        meshRenderer.material = AlphaMaterial;

        Color alpha = meshRenderer.material.color;
        meshRenderer.material.color = alpha;
        alpha.a = 1.0f;
        meshRenderer.material.color = alpha;
    }

    public void ChangeToAlphaMaterial()
    {
        skinnedMeshRenderer.material = AlphaMaterial;

        Color alpha = skinnedMeshRenderer.material.color;
        skinnedMeshRenderer.material.color = alpha;
        alpha.a = 1.0f;
        skinnedMeshRenderer.material.color = alpha;
    }

    public void ChangeMeshToAlphaMaterial()
    {
        meshRenderer.material = AlphaMaterial;

        Color alpha = meshRenderer.material.color;
        meshRenderer.material.color = alpha;
        alpha.a = 1.0f;
        meshRenderer.material.color = alpha;
    }

    private void ChangeToOpaqueMaterial()
    {
        skinnedMeshRenderer.material = OpaqueMaterial;
    }

    public IEnumerator EquipmentFade()
    {
        Color alpha = meshRenderer.material.color;
        meshRenderer.material.color = alpha;
        yield return new WaitForSeconds(3f);
        while (alpha.a > 0.1f)
        {
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer.material.color = alpha;
            yield return new WaitForSeconds(0.1f);
            alpha.a -= 11 * Time.deltaTime;
            meshRenderer.material.color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
        ParentObject.SetActive(false);
        yield return null;
    }

    public IEnumerator Fade()
    {
        Color alpha = skinnedMeshRenderer.material.color;
        skinnedMeshRenderer.material.color = alpha;
        yield return new WaitForSeconds(3f);
        while (alpha.a > 0.1f)
        {
            alpha.a -= 11 * Time.deltaTime;
            skinnedMeshRenderer.material.color = alpha;
            yield return new WaitForSeconds(0.1f);
            alpha.a -= 11 * Time.deltaTime;
            skinnedMeshRenderer.material.color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
        if(enemyRespawn != null)
        {
            enemyRespawn.enabled = true;
        }
        ParentObject.SetActive(false);
        yield return null;
    }
}
