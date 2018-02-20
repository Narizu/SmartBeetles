using UnityEngine;
using System.Collections;

public class SpriteMovement : MonoBehaviour {

    public Transform target;
    public bool gotItem;
	
	private void Update ()
    {
	    Vector3 newPos = transform.position;
        Vector3 dir = target.position - newPos;
        if (!gotItem)
            dir = Camera.main.WorldToScreenPoint(target.position) - newPos;
        if (dir.magnitude > 5) {
            newPos += dir.normalized * 5f;
            transform.position = newPos;
        }
        else Destroy(gameObject);
	}
}
