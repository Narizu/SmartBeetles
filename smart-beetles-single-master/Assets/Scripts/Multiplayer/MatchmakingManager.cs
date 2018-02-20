using UnityEngine;
using Photon;

public class MatchmakingManager : PunBehaviour {

    private LevelManager lvlManager;
    private int usersInRoom;

    private void Start()
    {
        lvlManager = GetComponent<LevelManager>();
    }

    public void joinMatchMakingRoom()
    {
        //PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
        Debug.Log("Couldn't join lobby");
        base.OnConnectionFail(cause);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        RoomOptions options = new RoomOptions();
        TypedLobby lobby = PhotonNetwork.lobby;
        options.maxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Matchmaking", options, lobby);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        usersInRoom = PhotonNetwork.playerList.Length;
        Debug.Log(usersInRoom);
        if (usersInRoom == 2)
            lvlManager.startMultiplayer();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        base.OnPhotonPlayerConnected(newPlayer);
        usersInRoom++;
        if (usersInRoom == 2)
            lvlManager.startMultiplayer();
    }

}
