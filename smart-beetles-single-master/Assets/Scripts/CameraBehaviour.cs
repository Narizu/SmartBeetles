using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public Transform target;
    public float height = 5.0f;

    private void FixedUpdate ()
    {
        if (target != null)
        {
            Vector3 position = target.position;
            //position.y += height;
            position.y = height;
            transform.parent.position = position;
        }
	}
}
