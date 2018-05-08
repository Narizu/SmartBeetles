using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{

	public GameObject go;
	public Image image;
	public Text text;

	/*
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

	private EnterPitStops script1;
	private EnterPitStops script2;
	private EnterPitStops script3;
	private EnterPitStops script4;
	*/

	private int code1;
	private int code2;

	private List<EnterPitStops> scripts;
	private EnterPitStops script;
	private Vector3 temp;
	private List<int> inside;
	private bool once;
	//private bool twice;

	private void Start ()
	{
		/*
		image1.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image2.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image3.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image4.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");

		script1 = go1.GetComponent<EnterPitStops> ();
		script2 = go2.GetComponent<EnterPitStops> ();
		script3 = go3.GetComponent<EnterPitStops> ();
		script4 = go4.GetComponent<EnterPitStops> ();
*/
		scripts = new List<EnterPitStops> ();
		inside = new List<int> ();
		image.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		text.text = "";
		once = true;
		//twice = true;

		//UpdateCode ();

	}

	private void Update ()
	{

		if (ImportPitStops.instance.GetName (0) != "" && once/* || text2.text == "error" || text3.text == "error" || text4.text == "error"*/) {

			once = false;
			UpdateInfo ();

		} /*else if (once) {
		
			UpdateInfo ();
		
		}*/

	}

	private void UpdateInfo ()
	{

		// Los números 14, 36, 44 y 81 se han obtenido llamando a la función auxiliar que he creado GetID de ImportPitStops.
		// Al conocer ya los números he eliminado dicha llamada para hacer más rápido el proceso, aunque la función sigue estando allí.

		/*
		if (!twice) {
		
			once = false;
		
		}

		twice = false;
		*/

		for (int i = 0; i < 102; i++) {
		
			if (ImportPitStops.instance.GetXY (i).x > -200 && ImportPitStops.instance.GetXY (i).x < 200 && ImportPitStops.instance.GetXY (i).y > -150 && ImportPitStops.instance.GetXY (i).y < 150) {

				inside.Add (i);
				//print ("Entra " + i);

			} /*else {

				print ("No entra " + i);

			}*/

		}
		/*
		for (int i = 0; i < inside.Count; i++) {

			print (i + ": " + inside [i]);

		}
		*/

		for (int i = 0; i < inside.Count; i++) {

			//GameObject go = new GameObject ();
			//Image image;
			//Text text;

			//Instantiate (go);
			//Instantiate (image);
			//Instantiate (text);

			//mygo.transform.parent = transform;

			//bikeRun = GameObject.Find ("Ground (Bike)").GetComponent<BikeRun> ();

			print (ImportPitStops.instance.GetName (inside[i]) + " (" + ImportPitStops.instance.GetXY (inside[i]).x + ", " + ImportPitStops.instance.GetXY (inside[i]).y + ")");
			text.text = ImportPitStops.instance.GetName (inside[i]);
			temp = ImportPitStops.instance.GetXY (inside[i]);
			GameObject mygo = Instantiate (go, new Vector3 (0, 0, 0), Quaternion.identity, transform);
			mygo.transform.localPosition = temp;
			mygo.transform.localRotation = Quaternion.identity;
			script = mygo.GetComponent<EnterPitStops> ();
			scripts.Add (script);
			Image myimage = Instantiate (image, new Vector3 (0, 0, 0), Quaternion.identity, mygo.transform);
			myimage.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

		}

		if (!once) {
			
			UpdateCode ();

		}

		/*
		print (ImportPitStops.instance.GetID ("CH2M"));
		print (ImportPitStops.instance.GetID ("DecoBike"));
		print (ImportPitStops.instance.GetID ("HNTB Corporation, Reflexion Health Jimbo's...Naturally & iCommute"));
		print (ImportPitStops.instance.GetID ("Sempra Energy & Underground Elephant"));
		*/

		/*
		print (ImportPitStops.instance.GetName (14) + " (" + ImportPitStops.instance.GetXY (14).x + ", " + ImportPitStops.instance.GetXY (14).y + ")");
		text1.text = ImportPitStops.instance.GetName (14);
		temp = ImportPitStops.instance.GetXY (14);
		go1.transform.localPosition = temp;

		print (ImportPitStops.instance.GetName (36) + " (" + ImportPitStops.instance.GetXY (36).x + ", " + ImportPitStops.instance.GetXY (36).y + ")");
		text2.text = ImportPitStops.instance.GetName (36);
		temp = ImportPitStops.instance.GetXY (36);
		go2.transform.localPosition = temp;

		print (ImportPitStops.instance.GetName (44) + " (" + ImportPitStops.instance.GetXY (44).x + ", " + ImportPitStops.instance.GetXY (44).y + ")");
		text3.text = ImportPitStops.instance.GetName (44);
		temp = ImportPitStops.instance.GetXY (44);
		go3.transform.localPosition = temp;

		print (ImportPitStops.instance.GetName (81) + " (" + ImportPitStops.instance.GetXY (81).x + ", " + ImportPitStops.instance.GetXY (81).y + ")");
		text4.text = ImportPitStops.instance.GetName (81);
		temp = ImportPitStops.instance.GetXY (81);
		go4.transform.localPosition = temp;
		*/
		/*
		print (ImportPitStops.instance.GetName (22) + " (" + ImportPitStops.instance.GetXY (22).x + ", " + ImportPitStops.instance.GetXY (22).y + ")");
		text1.text = ImportPitStops.instance.GetName (22);
		temp = ImportPitStops.instance.GetXY (22);
		go1.transform.localPosition = temp;

		print (ImportPitStops.instance.GetName (50) + " (" + ImportPitStops.instance.GetXY (50).x + ", " + ImportPitStops.instance.GetXY (50).y + ")");
		text2.text = ImportPitStops.instance.GetName (50);
		temp = ImportPitStops.instance.GetXY (50);
		go2.transform.localPosition = temp;

		print (ImportPitStops.instance.GetName (53) + " (" + ImportPitStops.instance.GetXY (53).x + ", " + ImportPitStops.instance.GetXY (53).y + ")");
		text3.text = ImportPitStops.instance.GetName (53);
		temp = ImportPitStops.instance.GetXY (53);
		go3.transform.localPosition = temp;

		print (ImportPitStops.instance.GetName (80) + " (" + ImportPitStops.instance.GetXY (80).x + ", " + ImportPitStops.instance.GetXY (80).y + ")");
		text4.text = ImportPitStops.instance.GetName (80);
		temp = ImportPitStops.instance.GetXY (80);
		go4.transform.localPosition = temp;
		*/
	}

	private void UpdateCode ()
	{
	
		code1 = Random.Range (0, scripts.Count);
		scripts [code1].setCode (1);

		/*
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
		*/

		code2 = Random.Range (0, scripts.Count);

		while (code1 == code2) {

			code2 = Random.Range (0, scripts.Count);

		}

		scripts [code2].setCode (2);

		/*
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
		*/

		for (int i = 0; i < scripts.Count; i++) {
			
			print (i + " = " + scripts[i].getCode ());

		}
	
	}

	public void SetBikeBlue ()
	{
		print ("Set Bike Blue");
		image.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		/*
		image1.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image2.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image3.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		image4.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
		*/
	}

	public void SetBikeGreen ()
	{
		print ("Set Bike Green");
		if (script.getCode () == 2) {
		
			print ("Get Code 2");
			image.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		}
		/*
		if (script1.getCode () == 2) {
		
			image1.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		} else if (script2.getCode () == 2) {
		
			image2.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		} else if (script3.getCode () == 2) {
		
			image3.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		} else {
		
			image4.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		}
		*/
	}

	public void SetBikeDefault ()
	{
		print ("Set Bike Default");
		SetBikeBlue ();
		if (script.getCode () == 1) {
		
			print ("Get Code 2");
			image.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
		
		}
		/*
		if (script1.getCode () == 1) {

			image1.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

		} else if (script2.getCode () == 1) {

			image2.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

		} else if (script3.getCode () == 1) {

			image3.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

		} else {

			image4.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

		}
		*/
	}

}
