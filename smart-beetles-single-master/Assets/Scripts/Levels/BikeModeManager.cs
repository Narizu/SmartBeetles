using UnityEngine;
using UnityEngine.UI;
using Photon;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine.AI;

public class BikeModeManager : PunBehaviour {

	public Text debug;
	public Text score;
	public GameObject points;
	private List<Transform> spawnPoints;
	public List<GameObject> checkPoints;
	public GameObject currentPac;
	public Image[] garbageUI;
	public Transform startPoint;
	public GameObject waitingUI;

	//Manager
	private GameManager gameManager;

	// Components
	private PacmanData pacData;
	private PhotonView pView;
	private IPacmanLevelManager pacLevel;

	// Connection params
	private int playersAllowed = 5;
	private bool waiting;
	private float waitingTime;

	// Score
	public float maxTime;
	public int maxBusted;

	// Garbage
	public GameObject[] garbage = new GameObject[4];
	public Checkpoint[] garbageScripts = new Checkpoint[4];

	private void Start()
	{
		PhotonNetwork.offlineMode = true;


		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		pacLevel = GetComponent<IPacmanLevelManager>();
		spawnPoints = new List<Transform>();
		checkPoints = new List<GameObject>();
		foreach (Transform point in points.GetComponentsInChildren<Transform>())
			if (point.tag == "SpawnPoint")
				spawnPoints.Add(point);
		//PhotonNetwork.ConnectUsingSettings("0.1");

		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnReceivedRoomListUpdate()
	{
		PhotonNetwork.JoinRandomRoom(new Hashtable { { "op", 1 }, { "pac", 1 } }, 5);
	}

	public override void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		waitingUI.GetComponentInChildren<Text>().text = "Server connection error: " + cause.ToString();
	}

	public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
	{
		waitingUI.GetComponentInChildren<Text>().text = "Server connection error: " + codeAndMsg.ToString();
	}

	public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
	{
		//waitingUI.GetComponentInChildren<Text>().text = "Creating room...";
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.customRoomPropertiesForLobby = new string[] { "op", "pac" };
		roomOptions.customRoomProperties = new Hashtable() { { "op", 1 }, { "pac", 1 } };
		roomOptions.maxPlayers = 5;
		int roomID = (int)Random.Range(1, 9999);
		PhotonNetwork.CreateRoom("Pacman" + roomID.ToString("0000"), roomOptions, null);
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("FUNCTION: OnJoinedRoom");
		//gameManager.updatePlayersNumber(PhotonNetwork.playerList.Length);
		//gameManager.addPlayer(gameObject.transform);
		//waitingUI.SetActive(false);
		gameManager.gameStarts();
		// Initialize scene
		GameObject sphere = PhotonNetwork.Instantiate("SpherePrefab", startPoint.position, Quaternion.identity, 0);
		SphereControl sphereControl = sphere.GetComponent<SphereControl>();
		pView = sphere.GetComponent<PhotonView>();
		pacData = sphere.GetComponent<PacmanData>();
		if (PhotonNetwork.playerList.Length == 1) pacData.host = true;
		pacData.timeText = debug;
		pacData.setPacman(true);
		sphereControl.enabled = true;
		//sphere.GetComponent<SphereNetwork>().enabled = false;
		Camera.main.GetComponent<CameraBehaviour>().target = sphere.transform;

		currentPac = sphere;
		if (pacData.host) {
			fillWithAI();
			spawnCheckpoints();
		}
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		// Player joins the game
		Debug.Log("FUNCTION: OnPhotonPlayerConnected");
		//gameManager.updatePlayersNumber(PhotonNetwork.playerList.Length);
		//gameManager.addPlayer(PhotonView.Find(1000 + newPlayer.ID).gameObject.transform);

	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
	{
		// If player leaves mid-game
		if (Time.timeScale != 0) {
			// Añadir IA
		}
		gameManager.updatePlayersNumber(PhotonNetwork.playerList.Length);
		gameManager.removePlayer(PhotonView.Find(otherPlayer.ID).gameObject.transform);
	}

	public Vector3 getRandomSpawnPoint()
	{
		int index = (int) Random.Range(0, spawnPoints.Count - 1);
		return spawnPoints[index].position;
	}

	public void updateScore(float time, int busted)
	{
		maxTime = time;
		maxBusted = busted;
		score.text = string.Format("Time: {0}\nBusted: {1}", time, busted);
	}

	private void fillWithAI()
	{
		for (int i = PhotonNetwork.playerList.Length; i < playersAllowed; i++) {
			GameObject sphere = PhotonNetwork.Instantiate("SphereAI", getRandomSpawnPoint(), Quaternion.identity, 0);
			sphere.GetComponent<SphereAI>().enabled = true;
			sphere.GetComponent<NavMeshAgent>().enabled = true;
			sphere.GetComponent<SphereNetwork>().enabled = false;
			sphere.GetComponent<PacmanData>().setPacman(false);
		}
	}

	public void spawnCheckpoints()
	{
		for (int i = 0; i < pacLevel.objectiveProgress(); i++) {
			GameObject checkpoint = PhotonNetwork.Instantiate(garbage[i].name, spawnPoints[i].position, Quaternion.identity, 0);
			garbageScripts[i] = checkpoint.GetComponent<Checkpoint>();
			checkpoint.GetComponent<Checkpoint>().id = i;
		}
	}

	public void resetCheckpoints()
	{
		currentPac.GetComponent<PhotonView>().RPC("removeAllCheckpoints", PhotonTargets.All);
		spawnCheckpoints();
	}

}
