using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPitStops : MonoBehaviour
{

	private SphereControl sphere;
	private float maxSpeed;
	private float maxSpeedBike;
	private BikeRun bikeRun;
	private int code;

	private void Start ()
	{

		maxSpeed = 2f;
		maxSpeedBike = BikeSettings.bikeSpeed;
		bikeRun = GameObject.Find ("Ground (Bike)").GetComponent<BikeRun> ();
		bikeRun.SetRunOff ();
		code = 0;

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

	public void setCode (int c)
	{

		code = c;

	}

	public int getCode ()
	{

		return code;

	}

	/*
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
	*/
}
