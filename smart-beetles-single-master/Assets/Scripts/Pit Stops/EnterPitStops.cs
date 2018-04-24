using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterPitStops : MonoBehaviour
{

	private SphereControl sphere;
	private float maxSpeed;
	private float maxSpeedBike;
	private BikeRun bikeRun;
	private int code;
	private PointsManager pointsManager;

	private void Awake ()
	{

		maxSpeed = 2f;
		maxSpeedBike = BikeSettings.bikeSpeed;
		bikeRun = GameObject.Find ("Ground (Bike)").GetComponent<BikeRun> ();
		bikeRun.SetRunOff ();
		code = 0;
		pointsManager = GameObject.Find ("CanvasPitStops").GetComponent<PointsManager> ();

	}
		
	private void OnTriggerEnter (Collider other)
	{
		
		if (other.gameObject.tag == "Sphere") {

			if (sphere == null) {
				
				sphere = other.gameObject.GetComponent<SphereControl> ();

			}

			if (code == 1) {

				bikeRun.SetRunOn ();
				sphere.SetMaxSpeed (maxSpeedBike);
				pointsManager.SetBikeBlue ();
				pointsManager.SetBikeGreen ();

			} else if (code == 2 && bikeRun.GetRun ()) {
			
				bikeRun.SetRunOff ();
				sphere.SetMaxSpeed (maxSpeed);
				pointsManager.SetBikeBlue ();
			
			}

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
		
}
