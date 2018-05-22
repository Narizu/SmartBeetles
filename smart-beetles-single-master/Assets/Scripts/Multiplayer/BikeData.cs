using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BikeData : MonoBehaviour {

	// General
	private bool isPac;
	private bool gameFinished = false;

	// UI
	public Text timeText;

	// Components
	public PhotonView pView;

	// Scores
	public bool hostTime;
	public float remainingTime = 2f * 60;
	//public float timeBeingPacman;
	//public int bustedGhosts;

	// Timers
	public bool justTouched;
	private float touchTimer;
	public float touchDuration = 2f;

	/*public bool hyperMode;
    private float hyperModeTimer;
    public float hyperModeDuration = 10f;*/

	public bool busted;
	private float bustedTimer;
	public float bustedDuration = 5f;

	// Garbage
	public bool[] garbageOwned = new bool[4];
	public PhotonView[] gargabePV = new PhotonView[4];

	public bool host;

	void Start()
	{
		remainingTime = GameData.getInstance().getMaximumTime();
	}

	private void Update()
	{
		remainingTime -= Time.deltaTime;
		TimeData.getInstance().setTime(remainingTime);
		if (timeText != null)
			timeText.text = string.Format("{0}:{1:00}", (int)remainingTime / 60, (int)remainingTime % 60);
		if (isPacman() && remainingTime <= 0 && !gameFinished)
		{

			pView.RPC("endGameByTime", PhotonTargets.All);
			gameFinished = true;
		}

		if (justTouched) {
			touchTimer += Time.deltaTime;
			if (touchTimer > touchDuration)
			{
				justTouched = false;
				touchTimer = 0;
			}
		}

		/*if (hyperMode)
        {
            float r = renderer.material.color.r + Time.deltaTime;
            float g = renderer.material.color.g + Time.deltaTime;
            float b = renderer.material.color.b + Time.deltaTime;
            if (r >= 1) r = 0;
            if (g >= 1) g = 0;
            if (b >= 1) b = 0;
            renderer.material.color = new Color(r, g, b);
            hyperModeTimer += Time.deltaTime;
            if (hyperModeTimer > hyperModeDuration)
            {
                hyperMode = false;
                hyperModeTimer = 0;
                pView.RPC("spawnHyperPill", PhotonTargets.All, GameObject.Find("GameManager").GetComponent<PacmanModeManager>().getRandomSpawnPoint());
            }
        }*/

		if (busted)
		{
			bustedTimer += Time.deltaTime;
			if (bustedTimer > bustedDuration)
			{
				busted = false;
				bustedTimer = 0;
				NavMeshAgent nav = GetComponent<NavMeshAgent>();
				if (nav != null)
					nav.Resume();
			}
		}

		/*if (isPac && pView.isMine) {
            timeBeingPacman += Time.deltaTime;
            timeText.text = string.Format("{0}:{1:00}", (int)timeBeingPacman / 60, (int)timeBeingPacman % 60);
        }*/
	}

	public void setPacman(bool pac)
	{
		isPac = pac;
		//if (pac) GetComponentInChildren<Renderer>().material.color = Color.yellow;
	}

	public bool isPacman()
	{
		return isPac;
	}

}
