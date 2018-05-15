using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{

	public GameObject go;
	public Text text;
	public Image image;

	private List<EnterPitStops> scripts;
	private List<Image> images;
	private List<int> inside;
	private EnterPitStops script;
	private Vector3 temp;
	private bool once;
	private int code1;
	private int code2;

	private void Start ()
	{

		text.text = "";

		scripts = new List<EnterPitStops> ();
		images = new List<Image> ();
		inside = new List<int> ();
		once = true;

	}

	private void Update ()
	{

		if (ImportPitStops.instance.GetName (0) != "" && once) {

			once = false;
			UpdateInfo ();

		} 

	}

	private void UpdateInfo ()
	{

		for (int i = 0; i < 102; i++) {
		
			if (ImportPitStops.instance.GetXY (i).x > -200 && ImportPitStops.instance.GetXY (i).x < 200 && ImportPitStops.instance.GetXY (i).y > -150 && ImportPitStops.instance.GetXY (i).y < 150) {

				inside.Add (i);

			} 

		}

		for (int i = 0; i < inside.Count; i++) {

			//print (ImportPitStops.instance.GetName (inside[i]) + " (" + ImportPitStops.instance.GetXY (inside[i]).x + ", " + ImportPitStops.instance.GetXY (inside[i]).y + ")");
			text.text = ImportPitStops.instance.GetName (inside[i]);
			temp = ImportPitStops.instance.GetXY (inside[i]);
			GameObject mygo = Instantiate (go, new Vector3 (0, 0, 0), Quaternion.identity, transform);
			mygo.transform.localPosition = temp;
			mygo.transform.localRotation = Quaternion.identity;
			script = mygo.GetComponent<EnterPitStops> ();
			scripts.Add (script);

		}

		if (!once) {
			
			UpdateCode ();

		}

		for (int i = 0; i < scripts.Count; i++) {

			Image myimage = Instantiate (image, new Vector3 (0, 0, 3), Quaternion.identity, scripts [i].gameObject.transform);

			if (scripts [i].getCode () == 1) {

				myimage.sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
				
			} else {
				
				myimage.sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");
				
			}

			myimage.transform.localPosition = new Vector3 (0, 0, -3);
			myimage.transform.localRotation = Quaternion.identity;
			myimage.transform.SetSiblingIndex (0);
			images.Add (myimage);

		}
			
	}

	private void UpdateCode ()
	{
	
		code1 = Random.Range (0, scripts.Count);
		scripts [code1].setCode (1);

		code2 = Random.Range (0, scripts.Count);

		while (code1 == code2) {

			code2 = Random.Range (0, scripts.Count);

		}

		scripts [code2].setCode (2);
		/*
		for (int i = 0; i < scripts.Count; i++) {
			
			print (i + " = " + scripts[i].getCode ());

		}
		*/
	}

	public void SetBikeBlue ()
	{

		for (int i = 0; i < images.Count; i++) {
			
			images[i].sprite = Resources.Load<Sprite> ("Sprites/bikeBlue");

		}
			
	}

	public void SetBikeGreen ()
	{

		for (int i = 0; i < images.Count; i++) {

			if (scripts [i].getCode () == 2) {
				
				images[i].sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");
				
			}

		}
			
	}

	public void SetBikeDefault ()
	{
		
		SetBikeBlue ();

		for (int i = 0; i < images.Count; i++) {

			if (scripts [i].getCode () == 1) {

				images[i].sprite = Resources.Load<Sprite> ("Sprites/bikeGreen");

			}

		}
			
	}

	public List<EnterPitStops> GetScripts ()
	{

		return scripts;

	}

}
