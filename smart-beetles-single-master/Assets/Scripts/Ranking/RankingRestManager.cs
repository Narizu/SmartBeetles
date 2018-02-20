using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RankingRestManager : MonoBehaviour {

    public static IEnumerator getScores(string levelId, RankingObservableObject allUsers)
    {
        WWW www = new WWW("http://pixelder.com/api/smart/api.php/score?filter=levelid,eq," + levelId);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            print(www.error);
        }
        else {
            var d = JSON.Parse(www.text)["score"]["records"];
            fillList(d, allUsers);
        }
    }

    private static void fillList(JSONNode d, RankingObservableObject allUsers)
    {
        for (int i = 0; i < d.Count; i++)
        {
            RankingUser newUser = new RankingUser();
            newUser.setScore(d[i][5]);
            newUser.setUser(d[i][2]);
            allUsers.addUser(newUser);
        }

        allUsers.filled();

    }


    public static IEnumerator setNewScore(string levelId, string time, string score)
    {
        string userID = GameData.getInstance().getUserID();
        string userName = GameData.getInstance().getUserName();
        WWWForm form = new WWWForm();
        form.AddField("userid", userID);
        form.AddField("username", userName);
        form.AddField("levelid", levelId);
        form.AddField("time", time);
        form.AddField("score", score);
        WWW www2 = new WWW("http://pixelder.com/api/smart/api.php/score/", form);
        yield return www2;
        if (!string.IsNullOrEmpty(www2.error))
        {
            print(www2.error);
        }
        else {         
            print("Score Added");            
        }
    }
}
