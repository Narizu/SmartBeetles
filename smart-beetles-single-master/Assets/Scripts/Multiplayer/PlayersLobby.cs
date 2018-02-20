using UnityEngine;
using Photon;

public class PlayersLobby : PunBehaviour {

	private void Start ()
    {
        //PhotonNetwork.ConnectUsingSettings(Application.bundleIdentifier);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.countOfPlayersOnMaster);
    }

}
