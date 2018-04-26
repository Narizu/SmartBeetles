using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereControl : MonoBehaviour {

    private Rigidbody sphereBody;

    private float acc = 1f;
    private float maxSpeed = 2f;
    private float angleRecoverSpeed = 0.1f;
    private float tiltThreshold = 0.3f;
    private Vector3 slopeVector = new Vector3(1, -1, 1) * 5f;
    private bool isPac;
    private PhotonView pView;
    private PacmanData pacData;

    private AudioManager audioManager;

	private EnterPitStops enterPitStops1;
	private EnterPitStops enterPitStops2;
	private EnterPitStops enterPitStops3;
	private EnterPitStops enterPitStops4;

    private void Start ()
    {
        sphereBody = GetComponent<Rigidbody>();
        pView = GetComponent<PhotonView>();
        pacData = GetComponent<PacmanData>();
        audioManager = GameObject.Find("AudioObject").GetComponent<AudioManager>();

		enterPitStops1 = GameObject.Find ("GameObject1").GetComponent<EnterPitStops> ();
		enterPitStops2 = GameObject.Find ("GameObject2").GetComponent<EnterPitStops> ();
		enterPitStops3 = GameObject.Find ("GameObject3").GetComponent<EnterPitStops> ();
		enterPitStops4 = GameObject.Find ("GameObject4").GetComponent<EnterPitStops> ();
    }

    private void FixedUpdate ()
    {
        if (!pacData.busted)
        {
            if (GameData.getInstance().getControlType() == ControlType.SENSOR) {
                #if UNITY_ANDROID || UNITY_IOS
                moveSphere(Input.acceleration.x, Input.acceleration.y);
                #endif

                #if UNITY_STANDALONE || UNITY_EDITOR
                moveSphere(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                #endif
            }
            else {
                if (Input.GetMouseButton(0)) {
                    Vector2 touchPos = Input.mousePosition;
                    Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.transform.position.y - transform.position.y));
                    Vector3 velocity = targetPos - transform.position;
                    Vector3 velReal = new Vector3(velocity.x, velocity.z, 0);
                    moveSphere(velReal.normalized.x, velReal.normalized.y);
                }
            }
        }
    }

    private void moveSphere(float h, float v)
    {
        //Calculate velocity
        Vector3 velocity = sphereBody.velocity;
        velocity.x += h * acc;
        velocity.z += v * acc;

        if (velocity.x > maxSpeed)
            velocity.x = maxSpeed;
        else if (velocity.x < -maxSpeed)
            velocity.x = -maxSpeed;

        if (velocity.z > maxSpeed)
            velocity.z = maxSpeed;
        else if (velocity.z < -maxSpeed)
            velocity.z = -maxSpeed;

        //Calculate camera angle
        /*Vector3 angle = Camera.main.transform.parent.transform.eulerAngles;
        
        //If speed is high, tilt camera
        if (h > tiltThreshold || h < -tiltThreshold)
            angle.z += h;
        if (v > tiltThreshold || v < -tiltThreshold)
            angle.x += -v;

        //Set maximum camera tilt
        if (angle.z > 10 && angle.z < 20)
            angle.z = 10;
        else if (angle.z < 350 && angle.z > 340)
            angle.z = 350;

        if (angle.x > 10 && angle.x < 20)
            angle.x = 10;
        else if (angle.x < 350 && angle.x > 340)
            angle.x = 350;

        //Slowly return to neutral angle
        if (angle.z > angleRecoverSpeed * 2f)
            angle.z -= angleRecoverSpeed;
        else if (angle.z < -angleRecoverSpeed * 2f)
            angle.z += angleRecoverSpeed;

        if (angle.x > angleRecoverSpeed * 2f)
            angle.x -= angleRecoverSpeed;
        else if (angle.x < -angleRecoverSpeed * 2f)
            angle.x += angleRecoverSpeed;*/

        velocity += slopeVector * Time.deltaTime;
        sphereBody.velocity = velocity;
        //Camera.main.transform.parent.transform.eulerAngles = angle;
		print(velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If we're on Pacman mode and we're Pacman
        if (GameData.getInstance().getGameMode() == GameMode.PACMAN && pacData != null && pacData.isPacman())
        {
            PacmanData otherPacData = collision.collider.gameObject.GetComponent<PacmanData>();
            PhotonView otherPView = collision.collider.gameObject.GetComponent<PhotonView>();
            // If we collided against other player
            if (otherPacData != null)
            {
                // If the other player is Pacman and was not recently touched
                if (otherPacData.isPacman() && !otherPacData.justTouched)
                {
                    //pView.RPC("disablePacman", PhotonTargets.All);
                    //GameObject.Find("GameManager").GetComponent<PacmanModeManager>().resetCheckpoints();
                    //otherPView.RPC("enablePacman", PhotonTargets.All);

                    Debug.Log("Choco con otro PJ!!!!!!");
                    audioManager.playCrash();

                    for (int i = 0; i < pacData.garbageOwned.Length; i++) {
                        if (pacData.garbageOwned[i]) {

                            PacmanModeManager pmManager = GameObject.Find("GameManager").GetComponent<PacmanModeManager>();
                            GameObject sprite = (GameObject)Instantiate(Resources.Load(pmManager.garbage[i].name + "Sprite"));
                            Vector3 position = pmManager.garbageUI[i].transform.position;
                            sprite.transform.position = position;
                            sprite.transform.parent = GameObject.Find("Canvas").transform;
                            sprite.transform.localScale *= 0.15f;
                            sprite.GetComponent<SpriteMovement>().gotItem = false;
                            sprite.GetComponent<SpriteMovement>().target = pacData.gargabePV[i].gameObject.transform;

                            pacData.garbageOwned[i] = false;
                            pacData.gargabePV[i].RPC("enableGarbage", PhotonTargets.All);
                            pmManager.garbageUI[i].enabled = false;

                            break;
                        }
                    }
                    pacData.justTouched = true;

                    otherPView.RPC("loseGarbage", PhotonTargets.Others);

                }
                else if (!otherPacData.isPacman() && !pacData.justTouched) {

                    Debug.Log("Choco con IA !!!!!!");
                    audioManager.playCrash();


                    for (int i = 0; i < pacData.garbageOwned.Length; i++) {
                        if (pacData.garbageOwned[i]) {

                            PacmanModeManager pmManager = GameObject.Find("GameManager").GetComponent<PacmanModeManager>();
                            GameObject sprite = (GameObject)Instantiate(Resources.Load(pmManager.garbage[i].name + "Sprite"));
                            Vector3 position = pmManager.garbageUI[i].transform.position;
                            sprite.transform.position = position;
                            //sprite.transform.parent = GameObject.Find("Canvas").transform;
                            sprite.transform.SetParent(GameObject.Find("Canvas").transform);
                            sprite.transform.localScale *= 0.15f;
                            sprite.GetComponent<SpriteMovement>().gotItem = false;
                            sprite.GetComponent<SpriteMovement>().target = pacData.gargabePV[i].gameObject.transform;

                            pacData.garbageOwned[i] = false;
                            pacData.gargabePV[i].RPC("enableGarbage", PhotonTargets.All);
                            pmManager.garbageUI[i].enabled = false;
                        }
                    }
                    pacData.justTouched = true;

                    otherPView.RPC("loseAllGarbage", PhotonTargets.Others);

					enterPitStops1.restartPitStop ();
					enterPitStops2.restartPitStop ();
					enterPitStops3.restartPitStop ();
					enterPitStops4.restartPitStop ();
                }
            }
        }
    }

	public void SetMaxSpeed (float speed)
	{

		maxSpeed = speed;

	}

}
