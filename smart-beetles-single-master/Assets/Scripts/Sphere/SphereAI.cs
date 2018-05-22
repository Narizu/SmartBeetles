using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SphereAI : MonoBehaviour {

    private NavMeshAgent nav;
    private PacmanData pacData;
	private BikeData bikeData;
    public PacmanModeManager pmManager;
	public BikeModeManager bikeManager;
    private PhotonView pView;
    private Transform objective;

	private void Start ()
    {
        nav = GetComponent<NavMeshAgent>();
        pacData = GetComponent<PacmanData>();
        pView = GetComponent<PhotonView>();
        float speedVar = Random.Range(-0.5f, 0.5f);
        nav.speed += nav.speed * speedVar;
        nav.updateRotation = true;
        objective = pmManager.currentPac.transform;

    }
	
	private void Update ()
    {
        if (!pacData.isPacman()) {
            
            this.goToObjective();
        }
        else {
            Vector3 closestCheckpoint = Vector3.one * 999f;
            for (int i = 0; i < pmManager.checkPoints.Count; i++) {
                if (pmManager.checkPoints[i] != null) {
                    Vector3 currentDist = closestCheckpoint - transform.position;
                    Vector3 newDist = pmManager.checkPoints[i].transform.position - transform.position;
                    if (newDist.magnitude < currentDist.magnitude) {
                        closestCheckpoint = pmManager.checkPoints[i].transform.position;
                    }
                }
            }
            nav.destination = closestCheckpoint;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If we're on Pacman mode and we're Pacman
        if (GameData.getInstance().getGameMode() == GameMode.PACMAN && pacData != null && pacData.isPacman()) {
            PacmanData otherPacData = collision.collider.gameObject.GetComponent<PacmanData>();
            PhotonView otherPView = collision.collider.gameObject.GetComponent<PhotonView>();
            // If we collided against other player
            if (otherPacData != null) {
                // If the other player is Pacman as was not recently touched
                if (otherPacData.isPacman() && !pacData.justTouched &&
                    GetComponent<Rigidbody>().velocity.magnitude > otherPacData.gameObject.GetComponent<Rigidbody>().velocity.magnitude) {
                    pView.RPC("loseGarbage", PhotonTargets.All);
                    otherPacData.gameObject.GetComponent<PhotonView>().RPC("loseGarbage", PhotonTargets.All);
                }
            }
        }
    }

    private void goToObjective()
    {
        nav.destination = this.objective.position;
        transform.LookAt(this.objective.position);
    }

    public void setObjective(Transform newObjective)
    {
        this.objective = newObjective;
    }
}
