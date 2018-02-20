using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class RankingController : MonoBehaviour, IRankingObserver {

    private RankingObservableObject data = new RankingObservableObject();
    public GameObject fatherCanvas;
    public GameObject prefabLine;
    public GameObject[] medals = new GameObject[3];
    public GameObject waitingPanel;
    public GameObject rankingObtainedPanel;

    public string level;

    void Start()
    {
        data.subscribe(this);
        if (level == null) level = "1";
        StartCoroutine(RankingRestManager.getScores(level, data));
    }

    public void dataFilled()
    {
        int count = 1;
        List<RankingUser> listOfUsers = data.getListOfUsers();
        listOfUsers.Sort();
        foreach (RankingUser user in listOfUsers)
        {
            //GameObject line = (GameObject)Resources.Load("RankingPosition");
            GameObject line = (GameObject)Instantiate(prefabLine, fatherCanvas.transform);
            line.transform.SetParent(fatherCanvas.transform);
            ScoreLineManager scoreLine = line.GetComponent<ScoreLineManager>();
            scoreLine.setValues(user.getUser(), count.ToString(), user.getScore());
            scoreLine.transform.localScale = new Vector3(1.0f, 0.3f, 0.3f);
            count++;            
        }
        medalsFilled();
    }

    private void medalsFilled()
    {
        List<RankingUser> listOfUsers = data.getListOfUsers();
        listOfUsers.Sort();
        for (int i = 0; i<medals.Length; i++)
        {
            ScoreLineManager scoreLine = medals[i].GetComponent<ScoreLineManager>();
            RankingUser user = listOfUsers[i];
            scoreLine.setValues(user.getUser(), (i+1).ToString(), user.getScore());
            //scoreLine.transform.localScale = new Vector3(1.0f, 0.3f, 0.3f);
        }
        waitingPanel.SetActive(false);
        rankingObtainedPanel.SetActive(true);
    }






}
