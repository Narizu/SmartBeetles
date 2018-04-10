using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeLane : MonoBehaviour
{

	private SphereControl sphere;
	private float maxSpeed;
	private float maxSpeedBike;
	private BikeRun bikeRun;

	private void Start ()
	{

		maxSpeed = 2f;
		maxSpeedBike = BikeSettings.bikeSpeed;
		bikeRun = GameObject.Find ("Ground (Bike)").GetComponent<BikeRun> ();
		bikeRun.SetRunOff ();

	}

	private void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{
			
			if (sphere == null)
				sphere = other.gameObject.GetComponent<SphereControl> ();
			
			bikeRun.SetRunOn ();
			sphere.SetMaxSpeed(maxSpeedBike);

		}

	}

	private void OnTriggerExit (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{

			if (sphere == null)
				sphere = other.gameObject.GetComponent<SphereControl> ();
			
			bikeRun.SetRunOff ();
			sphere.SetMaxSpeed(maxSpeed);

		}

	}

}
