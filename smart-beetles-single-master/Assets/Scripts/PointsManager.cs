using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{

	public Text text;
	Vector3 temp;

	void Start ()
	{

		temp = transform.position;

	}

	void Update ()
	{

		text.text = ImportPitStops.instance.GetName (0);
		temp = ImportPitStops.instance.GetXY (0);
		transform.localPosition = temp;

	}

}
