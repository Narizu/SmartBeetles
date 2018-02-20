using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeUIManager : MonoBehaviour {

    private float remainingTime;
    public Text clockText;
    private bool started = false;
    
    // Use this for initialization
	void Start () {
        remainingTime = GameData.getInstance().getMaximumTime();
        clockText.text = formattedTime(remainingTime);
    }
	
	
	void Update () {
        if (started)
        {
            remainingTime -= Time.deltaTime;

            clockText.text = formattedTime(remainingTime);
        }
    }

    private string formattedTime(float rTime) {
        int secs = (int) rTime % 60;
        int mins = (int) rTime / 60;
        return System.String.Format("{0:00}:{1:00}", mins, secs);
    }

    public void startTime()
    {
        started = true;
    }

    public void endTime()
    {
        started = false;
    }
}
