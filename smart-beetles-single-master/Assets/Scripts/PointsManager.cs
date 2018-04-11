using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{

	Text text;
	Vector3 temp;

	void Start ()
	{

		text = GetComponent<Text> ();
		temp = transform.position;

	}

	void Update ()
	{

		text.text = ImportPitStops.instance.GetName (0);
		//temp = ImportPitStops.instance.GetXY (0);
		//transform.position = temp;

	}

}
