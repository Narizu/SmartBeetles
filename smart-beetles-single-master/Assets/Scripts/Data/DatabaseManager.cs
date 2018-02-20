using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Text;
using UnityEngine.Networking;
//using UnityEngine.Experimental.Networking;

public class DatabaseManager : MonoBehaviour {





    public static IEnumerator getUserName(string deviceID)
    {
        WWW www = new WWW("http://pixelder.com/api/smart/api.php/user?filter=deviceid,eq,"+deviceID);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            print(www.error);
        }
        else {
            string name = JSON.Parse(www.text)["user"]["records"][0][1];
            if (name != null)
            {
                string userID = JSON.Parse(www.text)["user"]["records"][0][0];
                GameData.getInstance().setUserName(name);
                GameData.getInstance().setDeviceID(deviceID);
                GameData.getInstance().setUserID(userID);
            }
            else {
                string userID;
                WWWForm form = new WWWForm();
                form.AddField("nickname", "Beetle");
                form.AddField("deviceid", deviceID);
                WWW www2 = new WWW("http://pixelder.com/api/smart/api.php/user", form);
                yield return www2;
                if (!string.IsNullOrEmpty(www2.error))
                {
                    print(www2.error);
                }
                else {
                    userID = www2.text;
                    print("User Created");
                    GameData.getInstance().setUserName(name);
                    GameData.getInstance().setDeviceID(deviceID);
                    GameData.getInstance().setUserID(userID);
                }

            }
        }
        
    }

    public static IEnumerator updateUserName(string name)
    {
        string userID;
        WWWForm form = new WWWForm();
        form.AddField("nickname", name);
        form.AddField("deviceid", SystemInfo.deviceUniqueIdentifier);
        WWW www2 = new WWW("http://pixelder.com/api/smart/api.php/user/" + GameData.getInstance().getUserID(), form);
        yield return www2;
        if (!string.IsNullOrEmpty(www2.error))
        {
            print(www2.error);
        }
        else {
            userID = www2.text;
            print("User Updated");
            GameData.getInstance().setUserName(name);
        }
    }
}
