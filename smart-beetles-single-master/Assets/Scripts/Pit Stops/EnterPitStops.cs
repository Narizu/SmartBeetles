using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterPitStops : MonoBehaviour
{

	private SphereControl sphere;
	private BikeRun bikeRun;
	private PointsManager pointsManager;
	private ParticleManager particleManager;
	private GameManager gameManager;
	private float maxSpeedBike;
	private float maxSpeed;
	private int code;

	private void Awake ()
	{

		bikeRun = GameObject.Find ("Ground (Bike)").GetComponent<BikeRun> ();
		bikeRun.SetRunOff ();
		pointsManager = GameObject.Find ("CanvasPitStops").GetComponent<PointsManager> ();
		particleManager = GameObject.Find ("Particle System").GetComponent<ParticleManager> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		maxSpeedBike = BikeSettings.bikeSpeed;
		maxSpeed = 2f;
		code = 0;

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
				particleManager.SetParticles (true);

			} else if (code == 2 && bikeRun.GetRun ()) {
			
				bikeRun.SetRunOff ();
				sphere.SetMaxSpeed (maxSpeed);
				pointsManager.SetBikeBlue ();
				particleManager.SetParticles (false);
				gameManager.winGame ("1");
			
			}

		}

	}

	public void restartPitStop()
	{
		
		if (sphere == null) {
			
			sphere = gameObject.GetComponent<SphereControl> ();

		}

		bikeRun.SetRunOff ();
		pointsManager.SetBikeDefault ();
		particleManager.SetParticles (false);
		//sphere.SetMaxSpeed (maxSpeed);

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
