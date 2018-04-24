using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeRun : MonoBehaviour
{

	public RawImage runImage;

	public void SetRunOn ()
	{

		runImage.enabled = true;

	}

	public void SetRunOff ()
	{

		runImage.enabled = false;

	}

	public bool GetRun ()
	{

		if (runImage.enabled) {

			return true;

		} else {
		
			return false;
		
		}

	}

}
