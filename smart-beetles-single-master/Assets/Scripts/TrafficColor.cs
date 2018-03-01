using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficColor : MonoBehaviour
{

	public RawImage greenImage;
	public RawImage orangeImage;
	public RawImage redImage;

	public void SetColorGreen ()
	{

		greenImage.enabled = true;
		orangeImage.enabled = false;
		redImage.enabled = false;

	}

	public void SetColorOrange ()
	{

		greenImage.enabled = false;
		orangeImage.enabled = true;
		redImage.enabled = false;

	}

	public void SetColorRed ()
	{

		greenImage.enabled = false;
		orangeImage.enabled = false;
		redImage.enabled = true;

	}

}
