using UnityEngine;

public class SphereRPC : MonoBehaviour {

    
    // Manager
    private GameObject gameManagerObject;
    private GameManager gameManager;

    // Components
    private PhotonView pView;
    private PacmanData pacData;
    private PacmanModeManager pmManager;

    private void Start()
    {
        pView = GetComponent<PhotonView>();
        gameManagerObject = GameObject.Find("GameManager");
        pmManager = gameManagerObject.GetComponent<PacmanModeManager>();
        if (!pView.isMine && GameData.getInstance().getGameMode() != GameMode.PACMAN)
        {
            gameObject.name = "Bola";
        }
        pacData = GetComponent<PacmanData>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    //[PunRPC]
    //private void levelFinishedLost()
    //{
    //    if (!pView.isMine)
    //    {
    //        gameManager.loseGame();
    //    }
    //}

    [PunRPC]
    private void enablePacman()
    {
        if (!pacData.isPacman())
            pacData.justTouched = true;
        pacData.setPacman(true);
        pmManager.currentPac = gameObject;
    }

    [PunRPC]
    private void disablePacman()
    {
        if (pacData.isPacman())
            pacData.justTouched = true;
        pacData.setPacman(false);
        transform.position = pmManager.getRandomSpawnPoint();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        pmManager.gameObject.GetComponent<IPacmanLevelManager>().resetProgress();
    }

    /*[PunRPC]
    private void hyperMode()
    {
        pacData.hyperMode = true;
    }*/

    [PunRPC]
    private void ghostBusted()
    {
        pacData.busted = true;
        Vector3 newPos = gameManagerObject.GetComponent<PacmanModeManager>().getRandomSpawnPoint();
        gameObject.transform.position = newPos;
    }

    [PunRPC]
    private void spawnGarbage(Vector3 position, int id)
    {
        if (pmManager == null)
            pmManager = gameManagerObject.GetComponent<PacmanModeManager>();
        GameObject checkpoint = (GameObject) Instantiate(pmManager.garbage[id]);
        checkpoint.transform.position = position;
        pmManager.garbageScripts[id] = checkpoint.GetComponent<Checkpoint>();
        checkpoint.GetComponent<Checkpoint>().id = id;
        //GameObject.Find("GameManager").GetComponent<PacmanModeManager>().checkPoints.Add(checkpoint);
        //checkpoint.GetComponent<Checkpoint>().id = GameObject.Find("GameManager").GetComponent<PacmanModeManager>().checkPoints.Count - 1;
    }

    [PunRPC]
    private void removeCheckpoint(int id)
    {
        pmManager = gameManagerObject.GetComponent<PacmanModeManager>();
        Destroy(pmManager.checkPoints[id]);
        pmManager.checkPoints.RemoveAt(id);
        foreach (GameObject go in pmManager.checkPoints) {
            if (go.GetComponent<Checkpoint>().id > id)
                go.GetComponent<Checkpoint>().id--;
        }
    }

    [PunRPC]
    private void removeAllCheckpoints()
    {
        pmManager = gameManagerObject.GetComponent<PacmanModeManager>();
        foreach (GameObject go in pmManager.checkPoints) {
            Destroy(go);
        }
        pmManager.checkPoints.Clear();
    }

    [PunRPC]
    private void startGame()
    {
        gameManager.gameStarts();
    }

    [PunRPC]
    private void endGame()
    {
        PhotonNetwork.LeaveRoom();
        if (pView.isMine)
        {
            if (GetComponent<SphereAI>() == null)
            {
                string level = "1";
                gameManager.winGame(level);

            }
            else gameManager.loseGame();
        }
        else gameManager.loseGame();

    }

    [PunRPC]
    private void endGameByTime()
    {
        gameManager.loseGame();
    }

    [PunRPC]
    private void collectGarbage(int id)
    {
        Debug.Log(id);
        pmManager.garbageUI[id].enabled = true;
        pacData.garbageOwned[id] = true;
        bool hasAllGarbage = true;
        for (int i = 0; i < pacData.garbageOwned.Length; i++) {
            if (!pacData.garbageOwned[i]) {
                hasAllGarbage = false;
                break;
            }
        }

        if (hasAllGarbage) {
            if (pView.isMine)
            {
                if (GetComponent<SphereAI>() == null)
                {
                    //gameManager.winGame();
                }
                else gameManager.loseGame();
            }
            else gameManager.loseGame();

        }
    }

    [PunRPC]
    private void loseGarbage()
    {
        if (pView.isMine) {
            for (int i = 0; i < pacData.garbageOwned.Length; i++) {
                if (pacData.garbageOwned[i]) {
                    pacData.garbageOwned[i] = false;
                    pacData.gargabePV[i].RPC("enableGarbage", PhotonTargets.All);
                    pmManager.garbageUI[i].enabled = false;
                    break;
                }
            }
            pacData.justTouched = true;
        }
    }

    [PunRPC]
    private void loseAllGarbage()
    {
        if (pView.isMine) {
            for (int i = 0; i < pacData.garbageOwned.Length; i++) {
                if (pacData.garbageOwned[i]) {
                    pacData.garbageOwned[i] = false;
                    pacData.gargabePV[i].RPC("enableGarbage", PhotonTargets.All);
                    pmManager.garbageUI[i].enabled = false;
                }
            }
            pacData.justTouched = true;
        }
    }

}
