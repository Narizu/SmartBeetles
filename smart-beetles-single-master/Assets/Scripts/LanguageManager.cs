using UnityEngine;
using SimpleJSON;
using System.Collections;

public class LanguageManager : MonoBehaviour {

    private JSONNode languageFile;

    public void initLangFile()
    {
        TextAsset file = Resources.Load("languages") as TextAsset;
        string content = file.ToString();
        languageFile = JSON.Parse(content);
    }

	public string getText(string key)
    {
        if (languageFile == null) this.initLangFile();
        var lang = getLanguage(GameData.getInstance().getLanguage());
        //var v = languageFile[key][lang].Value;
        return languageFile[key][lang].Value;
    }

    private string getLanguage(Language language)
    {
        switch (language)
        {
            case Language.SPANISH:
                return "sp";
            case Language.ENGLISH:
                return "en";
            default:
                return "sp";
        }
    }

}
