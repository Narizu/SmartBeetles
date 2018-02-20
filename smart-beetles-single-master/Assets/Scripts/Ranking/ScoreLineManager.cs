using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLineManager : MonoBehaviour {

    private Text userName;
    private Text linePosition;
    private Text score;

    public void setValues(string name, string position, string s)
    {
        userName = transform.Find("Name").GetComponent<Text>();
        userName.text = name;
        linePosition = transform.Find("Position").GetComponent<Text>();
        linePosition.text = position;
        score = transform.Find("Score").GetComponent<Text>();
        score.text = s;
    }

}
