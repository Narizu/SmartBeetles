using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{

	public GameObject go1;
	public Image image1;
	public Text text1;
	public GameObject go2;
	public Image image2;
	public Text text2;
	public GameObject go3;
	public Image image3;
	public Text text3;
	public GameObject go4;
	public Image image4;
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

		image1.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image2.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image3.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image4.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");

		script1 = go1.GetComponent<EnterPitStops> ();
		script2 = go2.GetComponent<EnterPitStops> ();
		script3 = go3.GetComponent<EnterPitStops> ();
		script4 = go4.GetComponent<EnterPitStops> ();

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

		//print (ImportPitStops.instance.GetName (14) + " (" + ImportPitStops.instance.GetXY (14).x + ", " + ImportPitStops.instance.GetXY (14).y + ")");
		text1.text = ImportPitStops.instance.GetName (14);
		temp = ImportPitStops.instance.GetXY (14);
		go1.transform.localPosition = temp;

		//print (ImportPitStops.instance.GetName (36) + " (" + ImportPitStops.instance.GetXY (36).x + ", " + ImportPitStops.instance.GetXY (36).y + ")");
		text2.text = ImportPitStops.instance.GetName (36);
		temp = ImportPitStops.instance.GetXY (36);
		go2.transform.localPosition = temp;

		//print (ImportPitStops.instance.GetName (44) + " (" + ImportPitStops.instance.GetXY (44).x + ", " + ImportPitStops.instance.GetXY (44).y + ")");
		text3.text = ImportPitStops.instance.GetName (44);
		temp = ImportPitStops.instance.GetXY (44);
		go3.transform.localPosition = temp;

		//print (ImportPitStops.instance.GetName (81) + " (" + ImportPitStops.instance.GetXY (81).x + ", " + ImportPitStops.instance.GetXY (81).y + ")");
		text4.text = ImportPitStops.instance.GetName (81);
		temp = ImportPitStops.instance.GetXY (81);
		go4.transform.localPosition = temp;

	}

	private void UpdateCode ()
	{
	
		code1 = Random.Range (1, 5);

		switch (code1) {
		case 1:
			script1.setCode (1);
			image1.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
			break;
		case 2:
			script2.setCode (1);
			image2.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
			break;
		case 3:
			script3.setCode (1);
			image3.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
			break;
		case 4:
			script4.setCode (1);
			image4.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
			break;
		}

		code2 = Random.Range (1, 5);

		while (code1 == code2) {

			code2 = Random.Range (1, 5);

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

		print (script1.name + " = " + script1.getCode ());
		print (script2.name + " = " + script2.getCode ());
		print (script3.name + " = " + script3.getCode ());
		print (script4.name + " = " + script4.getCode ());
	
	}

	public void SetBikeBlue ()
	{

		image1.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image2.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image3.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image4.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");

	}

	public void SetBikeGreen ()
	{
	
		if (script1.getCode () == 2) {
		
			image1.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		} else if (script2.getCode () == 2) {
		
			image2.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		} else if (script3.getCode () == 2) {
		
			image3.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		} else {
		
			image4.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		}
	
	}

	public void SetBikeDefault ()
	{

		SetBikeBlue ();

		if (script1.getCode () == 1) {

			image1.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

		} else if (script2.getCode () == 1) {

			image2.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

		} else if (script3.getCode () == 1) {

			image3.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

		} else {

			image4.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

		}

	}

}
