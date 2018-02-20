using UnityEngine;
using UnityEngine.UI;
using Photon;

public class OnlineManager : PunBehaviour {

    public GameObject userList;
    public Transform startingPoint;

    private void Start()
    {
        Debug.Log("ENTRO EN ONLINE MANAGER");
        if (PlayerPrefs.GetInt("Multiplayer") == 0)
            enabled = false;
        //else PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();

        PhotonNetwork.player.name = "Player" + Mathf.Round(Random.value * 100);
    }

    private void OnPhotonRandomJoinFailed()
    {
        Debug.Log("There are no rooms, creating one.");
        PhotonNetwork.CreateRoom("Joac");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined successfully!");
        GameObject sphere = PhotonNetwork.Instantiate("SpherePrefab", Vector3.zero, Quaternion.identity, 0);
        sphere.transform.position = startingPoint.position;
        sphere.GetComponent<SphereControl>().enabled = true;
        Camera.main.GetComponent<CameraBehaviour>().target = sphere.transform;

        foreach (PhotonPlayer player in PhotonNetwork.playerList) {
            GameObject userButton = (GameObject)Instantiate(Resources.Load("UserTab"));
            userButton.name = player.name;
            userButton.GetComponentInChildren<Text>().text = player.name;
            userButton.transform.parent = userList.transform;
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        base.OnPhotonPlayerConnected(newPlayer);

        GameObject userButton = (GameObject)Instantiate(Resources.Load("UserTab"));
        userButton.name = newPlayer.name;
        userButton.GetComponentInChildren<Text>().text = newPlayer.name;
        userButton.transform.parent = userList.transform;

    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);

        Destroy(userList.transform.Find(otherPlayer.name).gameObject);
    }

}
