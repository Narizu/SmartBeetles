using UnityEngine;
using UnityEngine.UI;

public class CheckpointLevelManager : MonoBehaviour, LevelManager  {

    public GameObject completedUI;
    public Transform startingPoint;
    public Text timeText;
    public GameObject waitUI;

    private int checkpoints;
    private float time;
    private MatchmakingManager matchManager;
    private PhotonView pv;

    private void Start()
    {
        GameMode gameMode = GameData.getInstance().getGameMode();
        if (gameMode == GameMode.MATCHMAKING)
        {
            Time.timeScale = 0f;
            matchManager = GetComponent<MatchmakingManager>();
            matchManager.joinMatchMakingRoom();
        }
        else startSinglePlayer();
    }

    private void Update()
    {
        time += Time.deltaTime;
        print("Time: " + time.ToString());
        print("TimeData: " + TimeData.getInstance().getTime().ToString());
        string minSec = string.Format("{0}:{1:00}", (int)time / 60, (int)time % 60);
        timeText.text = minSec;
    }

    public void startMultiplayer()
    {
        GameObject sphere = PhotonNetwork.Instantiate("SpherePrefab", Vector3.zero, Quaternion.identity, 0);
        sphere.transform.parent = gameObject.transform;
        sphere.transform.position = startingPoint.position;
        sphere.GetComponent<SphereControl>().enabled = true;
        sphere.GetComponent<SphereNetwork>().enabled = false;
        pv = sphere.GetComponent<PhotonView>();
        //sphere.GetComponent<SphereRPC>().completedUI = completedUI;
        Camera.main.GetComponent<CameraBehaviour>().target = sphere.transform;
        waitUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void startSinglePlayer()
    {
        GameObject sphere = (GameObject)Instantiate(Resources.Load("SpherePrefab"));
        sphere.transform.parent = gameObject.transform;
        sphere.transform.position = startingPoint.position;
        sphere.GetComponent<SphereControl>().enabled = true;
        sphere.GetComponent<SphereNetwork>().enabled = false;
        sphere.GetComponent<PhotonView>().enabled = false;
        Camera.main.GetComponent<CameraBehaviour>().target = sphere.transform;
        waitUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public GameObject getCompletedCanvas()
    {
        return completedUI;
    }

    private void levelCompleted()
    {
        if (GameData.getInstance().getGameMode() == GameMode.MATCHMAKING) {
            Debug.Log("fin");
            Time.timeScale = 0f;
            completedUI.SetActive(true);
            completedUI.transform.Find("YouLost").gameObject.SetActive(false);
            pv.RPC("levelFinishedLost", PhotonTargets.Others);
        }
        else
        {
            Time.timeScale = 0f;
            completedUI.SetActive(true);
            completedUI.transform.Find("YouLost").gameObject.SetActive(false);
            //Guardar puntuación en base de datos
        }
    }

    public void checkpointPassed()
    {
        checkpoints++;
        if (checkpoints == 3)
            levelCompleted();
        Debug.Log("checkpoint");
    }

}
