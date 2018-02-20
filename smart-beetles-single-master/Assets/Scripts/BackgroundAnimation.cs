using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimation : MonoBehaviour {

    public RectTransform bg1;
    public RectTransform bg2;
    public CanvasScaler canvas;

    private float speed = 50f;

    private void Update ()
    {
        Vector3 pos1 = bg1.anchoredPosition;
        pos1.x += speed * Time.deltaTime;
        if (pos1.x > canvas.referenceResolution.x)
            pos1.x = bg2.anchoredPosition.x - bg1.sizeDelta.x+10;
        bg1.anchoredPosition = pos1;

        Vector3 pos2 = bg2.anchoredPosition;
        pos2.x += speed * Time.deltaTime;
        if (pos2.x > canvas.referenceResolution.x)
            pos2.x = bg1.anchoredPosition.x - bg2.sizeDelta.x;
        bg2.anchoredPosition = pos2;
    }
}
