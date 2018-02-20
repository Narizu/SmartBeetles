using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SmallGarbage : MonoBehaviour {

    public Image bottleTexture;
    public Image bananaTexture;
    public Image paperballTexture;
    public Image fishboneTexture;
    public Image player;
    private float bottleAngle = 0;
    private float bananaAngle = 0;
    private float paperballAngle = 0;
    private float fishboneAngle = 0;
    private float radius;

    void Start () {
        radius = Screen.height * 0.16f;
    }
	
	void Update () {
        move();
    }

    void move ()
    {
        var angle = Quaternion.AngleAxis(bottleAngle, Vector3.forward) * new Vector3(radius, 0, 0); ;
        bottleTexture.transform.position = player.transform.position + angle;
        angle = Quaternion.AngleAxis(bananaAngle, Vector3.forward) * new Vector3(radius, 0, 0); ;
        bananaTexture.transform.position = player.transform.position + angle;
        angle = Quaternion.AngleAxis(paperballAngle, Vector3.forward) * new Vector3(radius, 0, 0); ;
        paperballTexture.transform.position = player.transform.position + angle;
        angle = Quaternion.AngleAxis(fishboneAngle, Vector3.forward) * new Vector3(radius, 0, 0); ;
        fishboneTexture.transform.position = player.transform.position + angle;
    }

    public void setAllAngles(float bottleAn, float bananaAn, float paperAn, float fishboneAn)
    {
        bottleAngle = bottleAn;
        bananaAngle = bananaAn;
        paperballAngle = paperAn;
        fishboneAngle = fishboneAn;
    }

    public void disableGarbageImage(string name)
    {
        switch (name) { 
            case "Bottle(Clone)":
                bottleTexture.enabled = false;
                break;
            case "Banana(Clone)":
                bananaTexture.enabled = false;
                break;
            case "PaperBall(Clone)":
                paperballTexture.enabled = false;
                break;
            case "Fishbone(Clone)":
                fishboneTexture.enabled = false;
                break;
        }
       
    }

    public void enableGarbageImage(string name)
    {
        switch (name)
        {
            case "Bottle(Clone)":
                bottleTexture.enabled = true;
                break;
            case "Banana(Clone)":
                bananaTexture.enabled = true;
                break;
            case "PaperBall(Clone)":
                paperballTexture.enabled = true;
                break;
            case "Fishbone(Clone)":
                fishboneTexture.enabled = true;
                break;
        }

    }
}
