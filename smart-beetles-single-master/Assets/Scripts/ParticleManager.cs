using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

	private SphereControl sphere;

	void Start ()
	{
		
		//if (sphere == null) {

			sphere = GameObject.FindGameObjectWithTag ("Sphere").GetComponent<SphereControl> ();

		//}

	}

	void Update ()
	{
		
		if (sphere == null) {

			sphere = GameObject.FindGameObjectWithTag ("Sphere").GetComponent<SphereControl> ();

		}

		transform.position = sphere.transform.position;

	}

}
