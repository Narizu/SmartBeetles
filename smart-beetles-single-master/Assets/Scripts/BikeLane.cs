using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeLane : MonoBehaviour
{

	public RawImage runImage;

	private SphereControl sphere;
	private float maxSpeed;
	private float maxSpeedBike;

	private void Start ()
	{

		runImage.enabled = false;
		maxSpeed = 2f;
		maxSpeedBike = BikeSettings.bikeSpeed;

	}

	private void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{
			
			if (sphere == null)
				sphere = other.gameObject.GetComponent<SphereControl> ();
			
			runImage.enabled = true;
			sphere.SetMaxSpeed(maxSpeedBike);

		}

	}

	private void OnTriggerExit (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{

			if (sphere == null)
				sphere = other.gameObject.GetComponent<SphereControl> ();
			
			runImage.enabled = false;
			sphere.SetMaxSpeed(maxSpeed);

		}

	}

}
