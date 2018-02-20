using UnityEngine;
using System.Collections;

public class SphereNetwork : MonoBehaviour {

    private Vector3 truePos;
    private Quaternion trueRot;
    private PhotonView pView;
    //private GameObject completedUI;
    private PacmanData pacData;
    private SphereRPC sphRPC;

    private void Start()
    {
        pView = GetComponent<PhotonView>();
        //transform.position = GameObject.Find("GameManager").GetComponent<PacmanModeManager>().startPoint.position;
        if (GameData.getInstance().getGameMode() != GameMode.PACMAN)
        {
            GameObject go = GameObject.Find("GameManager");
            CheckpointLevelManager lm = go.GetComponent<CheckpointLevelManager>();
            //completedUI = go.GetComponent<GameManager>().completedUI;
            if (!pView.isMine)
                GetComponentInChildren<Renderer>().material.color = Color.blue;
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            truePos = (Vector3)stream.ReceiveNext();
            trueRot = (Quaternion)stream.ReceiveNext();
        }
    }

    private void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        pacData = GetComponent<PacmanData>();
        sphRPC = GetComponent<SphereRPC>();
        pacData.pView = GetComponent<PhotonView>();
        //sphRPC.completedUI = GameObject.Find("GameManager").GetComponent<GameManager>().completedUI;
        //sphRPC.waitingUI = GameObject.Find("GameManager").GetComponent<GameManager>().waitingUI;
        SphereAI sphAI = GetComponent<SphereAI>();
        if (sphAI != null)
            sphAI.pmManager = GameObject.Find("GameManager").GetComponent<PacmanModeManager>();
    }

    private void FixedUpdate()
    {
        if (!pView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, truePos, 0.25f);
            transform.rotation = Quaternion.Lerp(transform.rotation, trueRot, 0.25f);
        }
    }

}
