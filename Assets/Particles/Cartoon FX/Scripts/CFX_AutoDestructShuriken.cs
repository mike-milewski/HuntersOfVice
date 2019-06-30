using UnityEngine;
using System.Collections;

// Cartoon FX  - (c) 2015 Jean Moreno

// Automatically destructs an object when it has stopped emitting particles and when they have all disappeared from the screen.
// Check is performed every 0.5 seconds to not query the particle system's state every frame.
// (only deactivates the object if the OnlyDeactivate flag is set, automatically used with CFX Spawn System)

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = this.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
	}
	
	private IEnumerator CheckIfAlive ()
	{
        yield return new WaitForSeconds(ps.main.duration);
        ObjectPooler.Instance.ReturnHitParticleToPool(gameObject);
    }
}
