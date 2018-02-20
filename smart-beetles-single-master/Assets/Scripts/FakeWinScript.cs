using UnityEngine;
using System.Collections;

public class FakeWinScript : MonoBehaviour {

	
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().winGame("1");
        }
	}
}
