using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    public GameObject[] options;

    public Sprite[] avatars;
    public GameObject soundCat;
    public GameObject controlsCat;
    public GameObject langCat;
    public GameObject userCat;
    public GameObject credCat;

    public GameObject roller;

    public InputField nameField;

    private LanguageManager languageManager;

    public GameObject avatarsPanel;

    public Sprite imageSoundOn;
    public Sprite imageSoundOff;

    private void Awake()
    {

        languageManager = GetComponent<LanguageManager>();
        languageManager.initLangFile();
        changeSettingsTexts();
        //nameField.text = GameData.getInstance().getUserName();
        //selectUser();
        this.setAvatarInSettings();
    }

    public void selectSound()
    {
        foreach (GameObject go in options) {
            //if (go.name == "SoundOn" || go.name == "SoundOff")
            if (go.name == "SoundOn")
                go.SetActive(true);
            else go.SetActive(false);
        }
        //changeSelectedLabelSounds();
        setSoundImage();
        updateSelectedLabel(soundCat);

    }

    private void setSoundImage()
    {
        if (GameData.getInstance().getSoundType() == SoundType.ENABLED)
        {
            GameObject.Find("SoundOn").GetComponent<Image>().sprite = imageSoundOn;
        }
        else
        {
            GameObject.Find("SoundOn").GetComponent<Image>().sprite = imageSoundOff;
        }
    }

    public void selectControls()
    {
        foreach (GameObject go in options) {
            if (go.name == "TouchControl" || go.name == "SensorControl")
                go.SetActive(true);
            else go.SetActive(false);
        }
        changeSelectedLabelControl();
        updateSelectedLabel(controlsCat);

    }

    public void selectUser()
    {
        foreach (GameObject go in options) {
            if (go.name == "Avatar" || go.name == "Nickname")
            {
                go.SetActive(true);
                if(go.name == "Nickname")
                {
                    InputField inputField = GameObject.Find("NicknameInput").GetComponent<InputField>();
                    inputField.ActivateInputField();
                }
            }
            else go.SetActive(false);
        }

        updateSelectedLabel(userCat);
    }

    public void selectLanguage()
    {
        foreach (GameObject go in options)
        {
            if (go.name == "settings-language-english" || go.name == "settings-language-spanish")
                go.SetActive(true);
            else go.SetActive(false);          
        }
        changeSettingsTexts();
        updateSelectedLabel(langCat);
        changeSelectedLabelLanguage();
    }

    public void selectCredits()
    {
        foreach (GameObject go in options)
        {
            if (go.name == "settings-credits-content")
                go.SetActive(true);
            else go.SetActive(false);
        }
        changeSettingsTexts();
        updateSelectedLabel(credCat);
        
    }

    private void updateSelectedLabel(GameObject cat)
    {
        Vector3 position = roller.transform.position;
        position.y = cat.transform.position.y;
        roller.transform.position = position;
    }

    public void setSensorControl()
    {
        GameData.getInstance().setControlType(ControlType.SENSOR);
        changeSelectedLabelControl();
    }

    public void setTouchControl()
    {

        GameData.getInstance().setControlType(ControlType.TOUCH);
        changeSelectedLabelControl();
    }

    private void changeSelectedLabelControl()
    {
        ControlType control = GameData.getInstance().getControlType();
        Color active;
        Color inactive;
        if (control == ControlType.SENSOR)
        {
            active = GameObject.Find("SensorControl").GetComponent<Image>().color;
            active.a = 1;
            GameObject.Find("SensorControl").GetComponent<Image>().color = active;
            inactive = GameObject.Find("TouchControl").GetComponent<Image>().color;
            inactive.a = 0.4f;
            GameObject.Find("TouchControl").GetComponent<Image>().color = inactive;
        }
        else if (control == ControlType.TOUCH)
        {
            active = GameObject.Find("TouchControl").GetComponent<Image>().color;
            active.a = 1;
            GameObject.Find("TouchControl").GetComponent<Image>().color = active;
            inactive = GameObject.Find("SensorControl").GetComponent<Image>().color;
            inactive.a = 0.4f;
            GameObject.Find("SensorControl").GetComponent<Image>().color = inactive;
        }
    }

    public void changeSound()
    {
        if(GameData.getInstance().getSoundType() == SoundType.ENABLED)
        {
            AudioListener.volume = 0;
            GameData.getInstance().setSoundType(SoundType.DISABLED);
            GameObject.Find("SoundOn").GetComponent<Image>().sprite = imageSoundOff;
        }
        else
        {
            AudioListener.volume = 1;
            GameData.getInstance().setSoundType(SoundType.ENABLED);
            GameObject.Find("SoundOn").GetComponent<Image>().sprite = imageSoundOn;
        }
    }

    public void enableSound()
    {
        AudioListener.volume = 1;
        GameData.getInstance().setSoundType(SoundType.ENABLED);
        //changeSelectedLabelSounds();
    }

    public void disableSound()
    {
        AudioListener.volume = 0;
        GameData.getInstance().setSoundType(SoundType.DISABLED);
        //changeSelectedLabelSounds();
    }

    //private void changeSelectedLabelSounds()
    //{
    //    SoundType sound = GameData.getInstance().getSoundType();
    //    Color active;
    //    Color inactive;
    //    if (sound == SoundType.ENABLED)
    //    {
    //        active = GameObject.Find("SoundOn").GetComponent<Image>().color;
    //        active.a = 1;
    //        GameObject.Find("SoundOn").GetComponent<Image>().color = active;
    //        inactive = GameObject.Find("SoundOff").GetComponent<Image>().color;
    //        inactive.a = 0.4f;
    //        GameObject.Find("SoundOff").GetComponent<Image>().color = inactive;
    //    }
    //    else if (sound == SoundType.DISABLED)
    //    {
    //        active = GameObject.Find("SoundOff").GetComponent<Image>().color;
    //        active.a = 1;
    //        GameObject.Find("SoundOff").GetComponent<Image>().color = active;
    //        inactive = GameObject.Find("SoundOn").GetComponent<Image>().color;
    //        inactive.a = 0.4f;
    //        GameObject.Find("SoundOn").GetComponent<Image>().color = inactive;
    //    }
    //}


    public void changeLanguageString(string lang)
    {
       
        if (lang == "Spanish")
        {
            GameData.getInstance().setLanguage(Language.SPANISH);
        }
        if (lang == "English")
        {
            GameData.getInstance().setLanguage(Language.ENGLISH);
        }
        changeSelectedLabelLanguage();
        changeSettingsTexts();
    }

    private void changeSelectedLabelLanguage()
    {
        Language lang = GameData.getInstance().getLanguage();
        Color active;
        Color inactive;
        if (lang == Language.SPANISH)
        {
            active = GameObject.Find("settings-language-spanish").GetComponent<Text>().color;
            active.a = 1;
            GameObject.Find("settings-language-spanish").GetComponent<Text>().color = active;
            inactive = GameObject.Find("settings-language-english").GetComponent<Text>().color;
            inactive.a = 0.4f;
            GameObject.Find("settings-language-english").GetComponent<Text>().color = inactive;
        }
        else if (lang == Language.ENGLISH)
        {
            active = GameObject.Find("settings-language-english").GetComponent<Text>().color;
            active.a = 1;
            GameObject.Find("settings-language-english").GetComponent<Text>().color = active;
            inactive = GameObject.Find("settings-language-spanish").GetComponent<Text>().color;
            inactive.a = 0.4f;
            GameObject.Find("settings-language-spanish").GetComponent<Text>().color = inactive;
        }
    }

    private void changeSettingsTexts()
    {
        GameObject[] translatableObjectsArray = GameObject.FindGameObjectsWithTag("TranslatableObject");
        foreach (GameObject translatableObject in translatableObjectsArray)
        {
            translatableObject.GetComponent<Text>().text = languageManager.getText(translatableObject.name);

        }
    }

    public void saveUserName()
    {        
        
        Text feedbackMessage = GameObject.Find("feedback-username-text").GetComponent<Text>();
        string userName = GameObject.Find("NicknameInput").GetComponent<InputField>().text;
        Debug.Log(userName);
        feedbackMessage.text = languageManager.getText("settings-saving-username");
        StartCoroutine(DatabaseManager.updateUserName(userName));
        //GameData.getInstance().setUserName(userName);
        feedbackMessage.text = languageManager.getText("settings-username-saved");
    }

    public void changeAvatar(string newAvatar)
    {
        switch (newAvatar)
        {
            case "yellow":
                GameData.getInstance().setAvatar(AvatarColor.YELLOW);
                break;
            case "blue":
                GameData.getInstance().setAvatar(AvatarColor.BLUE);
                break;
            case "orange":
                GameData.getInstance().setAvatar(AvatarColor.ORANGE);
                break;
            case "pink":
                GameData.getInstance().setAvatar(AvatarColor.PINK);
                break;
        }
        this.setAvatarInSettings();
        avatarsPanel.SetActive(false);

    }

    private void setAvatarInSettings()
    {
        AvatarColor color = GameData.getInstance().getAvatar();
        Image avatarButtonImage = GameObject.Find("Avatar").GetComponent<Image>();
        avatarButtonImage.sprite = getAvatarImage(color);


    }

    private Sprite getAvatarImage(AvatarColor color)
    {
        return avatars[(int)color];
    }

    public void showAvatarsPanel()
    {
        avatarsPanel.SetActive(true);
    }

}
