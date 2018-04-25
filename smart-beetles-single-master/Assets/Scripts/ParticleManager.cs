using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

	private SphereControl sphere;
	private ParticleSystem ps;

	private void Start ()
	{

		ps = GetComponent<ParticleSystem> ();
		var em = ps.emission;
		em.enabled = false;

	}

	private void Update ()
	{
		
		if (sphere == null) {

			sphere = GameObject.FindGameObjectWithTag ("Sphere").GetComponent<SphereControl> ();

		}

		transform.position = sphere.transform.position;

	}

	public void SetParticles (bool partiOnOff)
	{
	
		var em = ps.emission;
		em.enabled = partiOnOff;

	}

}
