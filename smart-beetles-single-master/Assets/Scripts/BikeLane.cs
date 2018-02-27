using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeLane : MonoBehaviour
{

	private SphereControl sphere;
	private float maxSpeed = 2f;
	private float maxSpeedBike = 3f;

	private void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{
			
			sphere = other.gameObject.GetComponent<SphereControl>();
			sphere.SetMaxSpeed (maxSpeedBike);

		}

	}

	private void OnTriggerExit (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{
			
			sphere = other.gameObject.GetComponent<SphereControl>();
			sphere.SetMaxSpeed (maxSpeed);

		}

	}

}
