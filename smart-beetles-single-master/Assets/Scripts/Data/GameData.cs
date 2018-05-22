using System.Collections.Generic;
using UnityEngine;

public enum GameMode {SINGLE, PACMAN, MATCHMAKING, BIKE}
public enum LevelType {NONE, CHECKPOINTS}
public enum ControlType {SENSOR, TOUCH}
public enum Language {SPANISH, ENGLISH}
public enum SoundType { ENABLED, DISABLED}
public enum AvatarColor { YELLOW, BLUE, ORANGE, PINK}

public class GameData{

    private static GameData instance;
    private Stack<string> prevLevels;
    private string city;
    private GameMode gameMode;
    private LevelType levelType;
    private ControlType controlType;
    private Language language;
    private bool isPac;
    private string userName;
    private string devideID;
    private string userID;
    private float maximumTime = 100;

    public GameData()
    {
        prevLevels = new Stack<string>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //language = Language.SPANISH;
        language = (Language) PlayerPrefs.GetInt("language", 1);
    }

    public static GameData getInstance()
    {
        if (instance == null)
            instance = new GameData();
        return instance;
    }

    public void setLastLevel(string level)
    {
        prevLevels.Push(level);
    }

    public string getLastLevel()
    {
        return prevLevels.Pop();
    }

    public bool isMainScreen()
    {
        return prevLevels.Count == 0;
    }

    public GameMode getGameMode()
    {
        return gameMode;
    }

    public void setGameMode(GameMode mode)
    {
        gameMode = mode;
    }

    public string getCity()
    {
        return city;
    }

    public void setCity(string name)
    {
        city = name;
    }

    public LevelType getLevelType()
    {
        return levelType;
    }

    public void setLevelType(LevelType type)
    {
        levelType = type;
    }

    public bool isPacman()
    {
        return isPac;
    }

    public void setPacman(bool pac)
    {
        isPac = pac;
    }

    public ControlType getControlType()
    {
        return (ControlType) PlayerPrefs.GetInt("control", 1);
    }

    public void setControlType(ControlType control)
    {
        PlayerPrefs.SetInt("control", (int)control);
        //controlType = control;
    }

    public SoundType getSoundType()
    {
        return (SoundType)PlayerPrefs.GetInt("sound", 1);
    }

    public void setSoundType(SoundType sound)
    {
        PlayerPrefs.SetInt("sound", (int)sound);
    }

    public void setLanguage(Language lang)
    {
        PlayerPrefs.SetInt("language", (int)lang);
        //language = lang;
    }

    public Language getLanguage()
    {
        return (Language) PlayerPrefs.GetInt("language", 1);
    }

    public void setUserName(string name)
    {
        PlayerPrefs.SetString("username", name);
    }

    public string getUserName()
    {
        return PlayerPrefs.GetString("username", "Beetle");
    }

    public void setDeviceID(string deviceID)
    {
        PlayerPrefs.SetString("deviceid", deviceID);
    }

    public string getDeviceID()
    {
        return PlayerPrefs.GetString("deviceid");
    }

    public void setUserID(string userID)
    {
        PlayerPrefs.SetString("userid", userID);
    }

    public string getUserID()
    {
        return PlayerPrefs.GetString("userid");
    }

    public void setAvatar(AvatarColor color)
    {
        PlayerPrefs.SetInt("avatar", (int)color);
        //language = lang;
    }

    public AvatarColor getAvatar()
    {
        return (AvatarColor)PlayerPrefs.GetInt("avatar", 1);
    }

    public float getMaximumTime()
    {
        return maximumTime;
    }
}
