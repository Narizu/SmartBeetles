using UnityEngine;
using System.Collections;

public class AngleManager : MonoBehaviour {

    private GameObject bottle;
    private GameObject banana;
    private GameObject paperball;
    private GameObject fishbone;
    private SmallGarbage miniMapGarbageScript;
    private float bottleAngle = 0;
    private float bananaAngle = 0;
    private float paperballAngle = 0;
    private float fishboneAngle = 0;
    // Use this for initialization
    void Start () {
        bottle = GameObject.Find("Botella");
        banana = GameObject.Find("Platanito");
        paperball = GameObject.Find("BolaPapel");
        fishbone = GameObject.Find("RaspaPescado");
        miniMapGarbageScript = GameObject.Find("Minimap").GetComponent<SmallGarbage>();
	}
	
	// Update is called once per frame
	void Update () {
        getAngles();
        setObjectAngles();
	}

    private void getAngles()
    {
		if (GameData.getInstance ().getGameMode () != GameMode.BIKE) {
			Vector3 dir = bottle.transform.position - transform.position;
			bottleAngle = Mathf.Atan2 (dir.z, dir.x) * Mathf.Rad2Deg;
			dir = banana.transform.position - transform.position;
			bananaAngle = Mathf.Atan2 (dir.z, dir.x) * Mathf.Rad2Deg;
			dir = paperball.transform.position - transform.position;
			paperballAngle = Mathf.Atan2 (dir.z, dir.x) * Mathf.Rad2Deg;
			dir = fishbone.transform.position - transform.position;
			fishboneAngle = Mathf.Atan2 (dir.z, dir.x) * Mathf.Rad2Deg;
		}
    }

    private void setObjectAngles()
    {
        miniMapGarbageScript.setAllAngles(bottleAngle, bananaAngle, paperballAngle, fishboneAngle);
    }
}
