using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoWindowManager : MonoBehaviour {

    public GameObject checkPointsInfo1;
    public GameObject checkPointsInfo2;
    public GameObject checkPointsInfo3;

    private LanguageManager languageManager;

    void Start()
    {
        languageManager = GetComponent<LanguageManager>();
    }

    public void showSubLevelInfo(string subLevelName)
    {
        if (subLevelName == "CheckPointsInfoWindow")
        {
            checkPointsInfo1.SetActive(true);
        }
        translateUI();
    }

    public void closeSubLevelInfo(string subLevelName)
    {
        if (subLevelName == "CheckPointsInfoWindow")
        {
            checkPointsInfo1.SetActive(false);
            checkPointsInfo2.SetActive(false);
            checkPointsInfo3.SetActive(false);
        }
    }

    public void goToInfoWindow2()
    {
        checkPointsInfo1.SetActive(false);
        checkPointsInfo2.SetActive(true);
    }

    public void goToInfoWindow3()
    {
        checkPointsInfo2.SetActive(false);
        checkPointsInfo3.SetActive(true);
    }

    public void translateUI()
    {
        GameObject[] translatableObjectsArray = GameObject.FindGameObjectsWithTag("TranslatableObject");
        foreach (GameObject translatableObject in translatableObjectsArray)
        {
            translatableObject.GetComponent<Text>().text = languageManager.getText(translatableObject.name);

        }
    }
}
