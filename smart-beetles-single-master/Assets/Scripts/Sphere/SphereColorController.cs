using UnityEngine;
using System.Collections;

public class SphereColorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.setBeetleColor();
	}
	

    private void setBeetleColor()
    {
        AvatarColor color = GameData.getInstance().getAvatar();
        switch (color)
        {
            case AvatarColor.YELLOW:
                //GameObject.Find("ALAS").GetComponent<Renderer>().material.color = new Color(132, 193, 129, 1);
                //GameObject.Find("CUERPO").GetComponent<Renderer>().material.color = new Color(223, 205, 105, 1);
                GameObject.Find("ALAS").GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case AvatarColor.BLUE:
                //GameObject.Find("ALAS").GetComponent<Renderer>().material.color = new Color(85, 160, 174, 1);
                //GameObject.Find("CUERPO").GetComponent<Renderer>().material.color = new Color(209, 181, 160, 1);
                GameObject.Find("ALAS").GetComponent<Renderer>().material.color = Color.blue;
                break;
            case AvatarColor.ORANGE:
                //GameObject.Find("ALAS").GetComponent<Renderer>().material.color = new Color(149, 144, 121, 1);
                //GameObject.Find("CUERPO").GetComponent<Renderer>().material.color = new Color(231, 165, 100, 1);
                GameObject.Find("ALAS").GetComponent<Renderer>().material.color = Color.red;
                break;
            case AvatarColor.PINK:
                //GameObject.Find("ALAS").GetComponent<Renderer>().material.color = new Color(131, 90, 162, 1);
                //GameObject.Find("CUERPO").GetComponent<Renderer>().material.color = new Color(227, 133, 158, 1);
                GameObject.Find("ALAS").GetComponent<Renderer>().material.color = Color.magenta;
                break;
        }
    }
}
