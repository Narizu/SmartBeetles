using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{

	public GameObject go1;
	public Text text1;
	public GameObject go2;
	public Text text2;
	public GameObject go3;
	public Text text3;
	public GameObject go4;
	public Text text4;
	Vector3 temp;


	void Update ()
	{

		if (text1.text == "error" || text2.text == "error" || text3.text == "error" || text4.text == "error") {
			
			UpdateInfo ();

		}

	}

	void UpdateInfo ()
	{

		// Los números 14, 36, 44 y 81 se han obtenido llamando a la función auxiliar que he creado GetID de ImportPitStops.
		// Al conocer ya los números he eliminado dicha llamada para hacer más rápido el proceso, aunque la función sigue estando allí.

		print ("Name: " + ImportPitStops.instance.GetName (14) + ", x: " + ImportPitStops.instance.GetXY (14).x + ", y: " + ImportPitStops.instance.GetXY (14).y);
		text1.text = ImportPitStops.instance.GetName (14);
		temp = ImportPitStops.instance.GetXY (14);
		go1.transform.localPosition = temp;

		print ("Name: " + ImportPitStops.instance.GetName (36) + ", x: " + ImportPitStops.instance.GetXY (36).x + ", y: " + ImportPitStops.instance.GetXY (36).y);
		text2.text = ImportPitStops.instance.GetName (36);
		temp = ImportPitStops.instance.GetXY (36);
		go2.transform.localPosition = temp;

		print ("Name: " + ImportPitStops.instance.GetName (44) + ", x: " + ImportPitStops.instance.GetXY (44).x + ", y: " + ImportPitStops.instance.GetXY (44).y);
		text3.text = ImportPitStops.instance.GetName (44);
		temp = ImportPitStops.instance.GetXY (44);
		go3.transform.localPosition = temp;

		print ("Name: " + ImportPitStops.instance.GetName (81) + ", x: " + ImportPitStops.instance.GetXY (81).x + ", y: " + ImportPitStops.instance.GetXY (81).y);
		text4.text = ImportPitStops.instance.GetName (81);
		temp = ImportPitStops.instance.GetXY (81);
		go4.transform.localPosition = temp;

	}

}
