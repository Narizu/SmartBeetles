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

	private Vector3 temp;
	private EnterPitStops script1;
	private EnterPitStops script2;
	private EnterPitStops script3;
	private EnterPitStops script4;
	private int code1;
	private int code2;

	private void Start ()
	{

		script1 = GameObject.Find ("GameObject1").GetComponent<EnterPitStops> ();
		script2 = GameObject.Find ("GameObject2").GetComponent<EnterPitStops> ();
		script3 = GameObject.Find ("GameObject3").GetComponent<EnterPitStops> ();
		script4 = GameObject.Find ("GameObject4").GetComponent<EnterPitStops> ();

		UpdateCode ();

	}

	private void Update ()
	{

		if (text1.text == "error" || text2.text == "error" || text3.text == "error" || text4.text == "error") {
			
			UpdateInfo ();
		
		}

	}

	private void UpdateInfo ()
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

	private void UpdateCode () {
	
		code1 = Random.Range (1, 4);

		switch (code1) {
		case 1:
			script1.setCode (1);
			break;
		case 2:
			script2.setCode (1);
			break;
		case 3:
			script3.setCode (1);
			break;
		case 4:
			script4.setCode (1);
			break;
		}

		code2 = Random.Range (1, 4);

		while (code1 == code2) {

			code2 = Random.Range (1, 4);

		}

		switch (code2) {
		case 1:
			script1.setCode (2);
			break;
		case 2:
			script2.setCode (2);
			break;
		case 3:
			script3.setCode (2);
			break;
		case 4:
			script4.setCode (2);
			break;
		}

		print ("GameObject1 = " + script1.getCode ());
		print ("GameObject2 = " + script2.getCode ());
		print ("GameObject3 = " + script3.getCode ());
		print ("GameObject4 = " + script4.getCode ());
	
	}

}
