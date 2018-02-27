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
		maxSpeedBike = 3f;

	}

	private void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{
			
			runImage.enabled = true;
			sphere = other.gameObject.GetComponent<SphereControl>();
			sphere.SetMaxSpeed(maxSpeedBike);

		}

	}

	private void OnTriggerExit (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{
			
			runImage.enabled = false;
			sphere = other.gameObject.GetComponent<SphereControl>();
			sphere.SetMaxSpeed(maxSpeed);

		}

	}

}
