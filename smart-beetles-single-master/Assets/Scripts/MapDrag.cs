using UnityEngine;
using System.Collections;

public class MapDrag : MonoBehaviour {

    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;

    private RectTransform rect;
    private Vector3 offsetPoint;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnTouchDown()
    {
        offsetPoint = rect.position - Input.mousePosition;
    }

    public void OnDrag()
    {
        Vector3 newPos = Input.mousePosition + offsetPoint;
        if (newPos.x < left.position.x + Screen.width/2 || newPos.x > right.position.x - Screen.width/2)
            newPos.x = rect.position.x;
        if (newPos.y > up.position.y - Screen.height / 2 || newPos.y < down.position.y + Screen.height / 2)
            newPos.y = rect.position.y;
        rect.position = newPos;
    }
}
