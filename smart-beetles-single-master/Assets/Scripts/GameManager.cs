using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject waitingUI;
    public GameObject quitUI;
    private LanguageManager languageManager;
    private string username;
    public AudioManager audioManager;
    private List<Transform> playerTransforms;
    public Text pointsTextField;

    private void Start()
    {
        StartCoroutine(DatabaseManager.getUserName(SystemInfo.deviceUniqueIdentifier));
        //username = GameData.getInstance().getUserName();
        languageManager = GetComponent<LanguageManager>();
        translateUI();
        //playerTransforms = new List<Transform>();
        setSound();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            goToPreviousScene();

        
    }

    private void setSound()
    {
        if (GameData.getInstance().getSoundType() == SoundType.ENABLED)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
    //public void loadScene(string name)
    //{
    //    GameData.getInstance().setLastLevel(SceneManager.GetActiveScene().name);
    //    GameMode gameMode = GameData.getInstance().getGameMode();
    //    string city = GameData.getInstance().getCity();
    //    LevelType levelType = GameData.getInstance().getLevelType();
    //    if (city != null) {
    //        if (levelType != LevelType.NONE) {
    //            SceneManager.LoadScene(SceneList.getInstance().getSceneID(name));
    //        }
    //        else {
    //            GameData.getInstance().setCity(name);
    //            SceneManager.LoadScene(SceneList.getInstance().getSceneID(city + "Levels"));
    //        }
    //    }
    //    else SceneManager.LoadScene(SceneList.getInstance().getSceneID(name));
    //}

    //public void singlePlayer()
    //{
    //    GameData.getInstance().setGameMode(GameMode.SINGLE);
    //    loadScene("LevelSelection");
    //}

    //public void multiPlayer(bool pacman)
    //{
    //    if (pacman) {
    //        GameData.getInstance().setGameMode(GameMode.PACMAN);
    //        loadScene("LevelSelection");
    //    }
    //    else {
    //        GameData.getInstance().setGameMode(GameMode.MATCHMAKING);
    //        loadScene("PlayersLobby");
    //    }
    //}

    //public void loadLevel(string name)
    //{
    //    loadScene(name);
    //}

    public void goToPreviousScene()
    {
        PhotonNetwork.Disconnect();
        if (!GameData.getInstance().isMainScreen())
            SceneManager.LoadScene(SceneList.getInstance().getSceneID(GameData.getInstance().getLastLevel()));
        else Application.Quit();
    }

    //public void goToCity(string city)
    //{
    //    GameData.getInstance().setCity(city);
    //    startLevel();
    //    //loadScene(city);
    //}

    //public void startLevel()
    //{
    //    GameData.getInstance().setLevelType(LevelType.CHECKPOINTS);
    //    loadScene("CheckpointsMode");
    //}

    public void goToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void showQuitScreen()
    {
        quitUI.SetActive(true);
        translateUI();
    }

    public void hideQuitScreen()
    {
        quitUI.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }


    public void translateUI()
    {
        GameObject[] translatableObjectsArray = GameObject.FindGameObjectsWithTag("TranslatableObject");
        foreach (GameObject translatableObject in translatableObjectsArray)
        {
            translatableObject.GetComponent<Text>().text = languageManager.getText(translatableObject.name);
        }
    }

    public void goToInitialScreen()
    {
        PhotonNetwork.Disconnect();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScreen");
    }

    public void gameStarts()
    {
        waitingUI.SetActive(false);
        GetComponent<TimeUIManager>().startTime();
        Debug.Log(GameData.getInstance().getUserName());
    }

    public void winGame(string levelId)
    {
        var sendingTime = TimeData.getInstance().getTimeToString();
        print(sendingTime);
        var sendingScore = TimeData.getInstance().getScore().ToString();

        StartCoroutine(RankingRestManager.setNewScore(levelId, sendingTime, sendingScore));

        //completedUI.SetActive(true);
        winPanel.SetActive(true);
        pointsTextField.text = sendingScore.ToString();
        translateUI();
        //GameObject.Find("game-over-text-win").GetComponent<Text>().text = languageManager.getText("game-over-text-win");
        //GameObject.Find("you-win-title").GetComponent<Text>().text = languageManager.getText("you-win-title");

        Time.timeScale = 0;
        GetComponent<TimeUIManager>().endTime();
    }

    public void loseGame()
    {
        
        losePanel.SetActive(true);
        GameObject.Find("game-over-text-lose").GetComponent<Text>().text = languageManager.getText("game-over-text-lose");
        Time.timeScale = 0;
        GetComponent<TimeUIManager>().endTime();
    }

    public void updatePlayersNumber(int playersNum)
    {
        //GameObject.Find("ConnectedPlayersNumber").GetComponent<Text>().text = playersNum.ToString();
    }

    //public void addPlayer(Transform newPlayer)
    //{
    //    playerTransforms.Add(newPlayer);
    //}

    public void removePlayer(Transform playerToRemove)
    {
        playerTransforms.Remove(playerToRemove);
    }

    public void goToRanking()
    {
        SceneManager.LoadScene("Ranking");
    }


    public void goToSubLevelSelection(string city)
    {
        GameData.getInstance().setCity(city);
        SceneManager.LoadScene("SubLevelSelection");
    }

    public void goToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void loadLevel(string mode)
    {
        GameData.getInstance().setGameMode(GameMode.PACMAN);
        string city = GameData.getInstance().getCity();
        string sceneName = city + "_" + mode;
        SceneManager.LoadScene(sceneName);
    }


}
