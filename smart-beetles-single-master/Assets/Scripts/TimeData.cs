using UnityEngine;
using System.Collections;

public class TimeData {
    private static TimeData instance;
    private float time;
    private int score = 0;


    public static TimeData getInstance()
    {
        if (instance == null)
            instance = new TimeData();
        return instance;
    }

    public void setTime(float t)
    {
        time = t;
    }

    public float getTime()
    {
        return time;
    }

    public string getTimeToString()
    {
        return time.ToString("0.0");

    }

    private string formattedTime(float rTime)
    {
        int secs = (int)rTime % 60;
        int mins = (int)rTime / 60;
        return System.String.Format("{0:00}:{1:00}", mins, secs);
    }

    public int getScore()
    {

        return (int) (time*10);
    }
}
