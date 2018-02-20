using UnityEngine;
using System.Collections;

public class OfflineManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PhotonNetwork.offlineMode = true;
        PhotonNetwork.JoinRandomRoom();
    }

}
