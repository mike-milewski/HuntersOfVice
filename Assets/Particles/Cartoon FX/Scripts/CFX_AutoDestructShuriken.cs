using UnityEngine;
using System.Collections;

// Cartoon FX  - (c) 2015 Jean Moreno

// Automatically destructs an object when it has stopped emitting particles and when they have all disappeared from the screen.
// Check is performed every 0.5 seconds to not query the particle system's state every frame.
// (only deactivates the object if the OnlyDeactivate flag is set, automatically used with CFX Spawn System)

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ps;

    [SerializeField]
    private float Duration;

    private void Awake()
    {
        ps = this.GetComponent<ParticleSystem>();   
    }

    private void OnEnable()
    {
        if(Duration > -1)
        StartCoroutine(CheckIfAlive());
    }

    private IEnumerator CheckIfAlive ()
	{
        yield return new WaitForSeconds(Duration);
        ps.transform.localScale = new Vector3(1, 1, 1);
        ObjectPooler.Instance.ReturnHitParticleToPool(gameObject);
    }
}
