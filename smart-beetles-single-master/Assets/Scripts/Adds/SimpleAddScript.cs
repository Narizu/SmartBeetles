using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class SimpleAddScript : MonoBehaviour
{
    void Start()
    {
        //For Android
        //Advertisement.Initialize("1193195", false);

        //For IOS
        //Advertisement.Initialize("1193194", false);

        //StartCoroutine(ShowAdWhenReady());
    }
	/*
    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady())
        {
            yield return null;
        }
        Advertisement.Show();
    }*/
    //public void ShowAd()
    //{
    //    if (Advertisement.IsReady())
    //    {
    //        Advertisement.Show();
    //    }
    //}
}