using UnityEngine;
using UnityEngine.UI;

public class MinimapBehaviour : MonoBehaviour {

    public Image minimap;
    private Vector2 mapOffset;
    private float ratio = 0.1f;

    private void Start()
    {
        mapOffset = new Vector2(minimap.transform.localPosition.x, minimap.transform.localPosition.y);
    }


	private void Update()
    {

    }
}
