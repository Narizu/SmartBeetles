using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficRed : MonoBehaviour
{

	private SphereControl sphere;
	private float maxSpeed;
	private float maxSpeedTraffic;
	private TrafficColor trafficColor;

	private void Start ()
	{

		maxSpeed = 2f;
		maxSpeedTraffic = TrafficSettings.redSpeed;
		trafficColor = GameObject.Find ("Ground (Traffic)").GetComponent<TrafficColor> ();
		trafficColor.SetColorGreen ();

	}

	private void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{

			if (sphere == null)
				sphere = other.gameObject.GetComponent<SphereControl> ();

			trafficColor.SetColorRed ();
			sphere.SetMaxSpeed(maxSpeedTraffic);

		}

	}

	private void OnTriggerExit (Collider other)
	{

		if (other.gameObject.tag == "Sphere")
		{

			if (sphere == null)
				sphere = other.gameObject.GetComponent<SphereControl> ();

			trafficColor.SetColorGreen ();
			sphere.SetMaxSpeed(maxSpeed);

		}

	}

}
