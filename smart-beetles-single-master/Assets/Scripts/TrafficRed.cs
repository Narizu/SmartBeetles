using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficRed : MonoBehaviour
{

	private SphereControl sphere;
	private float maxSpeed;
	private float maxSpeedTraffic;

	void Start ()
	{

		maxSpeed = 2f;
		maxSpeedTraffic = TrafficSettings.redSpeed;

	}

	private void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{

			if (sphere == null)
				sphere = other.gameObject.GetComponent<SphereControl> ();

			Debug.Log("Entro al Rojo");
			sphere.SetMaxSpeed(maxSpeedTraffic);

		}

	}

	private void OnTriggerExit (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{

			if (sphere == null)
				sphere = other.gameObject.GetComponent<SphereControl> ();

			Debug.Log("Salgo del Rojo");
			sphere.SetMaxSpeed(maxSpeed);

		}

	}

}
