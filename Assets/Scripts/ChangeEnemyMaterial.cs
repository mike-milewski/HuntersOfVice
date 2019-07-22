using System.Collections;
using UnityEngine;

public class ChangeEnemyMaterial : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField]
    private Material AlphaMaterial, OpaqueMaterial;

    [SerializeField]
    private GameObject ParentObject;

    private void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void OnEnable()
    {
        skinnedMeshRenderer.material = OpaqueMaterial;
    }

    public void ChangeToAlphaMaterial()
    {
        skinnedMeshRenderer.material = AlphaMaterial;

        Color alpha = skinnedMeshRenderer.material.color;
        skinnedMeshRenderer.material.color = alpha;
        alpha.a = 1.0f;
        skinnedMeshRenderer.material.color = alpha;
    }

    private void ChangeToOpaqueMaterial()
    {
        skinnedMeshRenderer.material = OpaqueMaterial;
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
        ParentObject.SetActive(false);
        yield return null;
    }
}
