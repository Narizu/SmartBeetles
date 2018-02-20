using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public int id;

    private Canvas canvas;
    private PacmanModeManager pmManager;
    private AudioManager audioManager;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        pmManager = GameObject.Find("GameManager").GetComponent<PacmanModeManager>();
        audioManager = GameObject.Find("AudioObject").GetComponent<AudioManager>();
        GetComponent<PhotonView>().RPC("setSmallGarbageScript", PhotonTargets.All);
    }

    private void Update()
    {
        Vector3 direction = transform.position - pmManager.currentPac.transform.position;
        if (direction.magnitude > 4)
            canvas.transform.position = pmManager.currentPac.transform.position + direction.normalized * Screen.width/100f;
        else canvas.transform.position = transform.position;
        Vector3 angle = transform.eulerAngles;
        angle.y += 1;
        angle.x += 1;
        transform.eulerAngles = angle;
    }

	private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Sphere")
        {

            Debug.Log("Soy BASURA y me capturan");
            audioManager.playGarbageCollected();


            //switch (GameData.getInstance().getGameMode()) {
            //    case GameMode.SINGLE:
            //        GameObject.Find("GameManager").GetComponent<CheckpointLevelManager>().checkpointPassed();
            //        gameObject.SetActive(false);
            //        break;
            //    case GameMode.PACMAN:
                    PacmanData pacData = collider.gameObject.GetComponent<PacmanData>();
                    if (pacData != null && pacData.isPacman()) {
                        //pacData.gameObject.GetComponent<PhotonView>().RPC("collectGarbage", PhotonTargets.All, id);
                        GetComponent<PhotonView>().RPC("disableGarbage", PhotonTargets.All);
                        GameObject.Find("GameManager").GetComponent<PacmanModeManager>().garbageUI[id].enabled = true;
                        pacData.garbageOwned[id] = true;

                        GameObject sprite = (GameObject)Instantiate(Resources.Load(pmManager.garbage[id].name+"Sprite"));
                        Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
                        sprite.transform.position = position;
                        //sprite.transform.parent = GameObject.Find("Canvas").transform;
                        sprite.transform.SetParent(GameObject.Find("Canvas").transform);
                        sprite.transform.localScale *= Screen.width / 3000f;
                        sprite.GetComponent<SpriteMovement>().gotItem = true;
                        sprite.GetComponent<SpriteMovement>().target = pmManager.garbageUI[id].rectTransform;

                        pacData.gargabePV[id] = GetComponent<PhotonView>();
                        bool hasAllGarbage = true;
                        for (int i = 0; i < pacData.garbageOwned.Length; i++) {
                            if (!pacData.garbageOwned[i]) {
                                hasAllGarbage = false;
                                break;
                            }
                        }
                        if (hasAllGarbage)
                            pacData.GetComponent<PhotonView>().RPC("endGame", PhotonTargets.All);
                    }
            //        break;
            //}
        }
    }

    public void resetCheckpoint()
    {
        gameObject.SetActive(true);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) {
            // We own this player: send the others our data
            stream.SendNext(id);
        }
        else {
            // Network player, receive data
            id = (int)stream.ReceiveNext();
        }
    }

}
