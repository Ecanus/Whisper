using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


/// <summary>
/// PlayerBehaviour class is responsible for keeping track of the control settings and handling input
/// of the relative player
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 
public class PlayerBehaviour : MonoBehaviour {

	/// <summary>
	/// The relative player being handled
	/// </summary>
	private GameObject player;


	/// <summary>
	/// Variable affecting the coefficient of player movement
	/// </summary>
	private int motionCoeff;

	/// <summary>
	/// The bullet speed.
	/// </summary>
	private float bulletSpeed;

	/// <summary>
	/// The rocket sprite contained by the player
	/// </summary>
	private GameObject bulletPrefab;

	/// <summary>
	/// GameObject containing prefab of a bullet instance
	/// </summary>
	private GameObject bulletInstance;

	/// <summary>
	/// Vector in direction from player sprite to mouse position
	/// </summary>
	private Vector3 shootVector;

	/// <summary>
	/// the Player score
	/// </summary>
	[SerializeField]
	private int playerScore;

	/// <summary>
	/// Text of player score
	/// </summary>
	private GameObject scoreText;
	private Text score;


	private float verticalExtent;
	private float horizontalExtent;


	/// <summary>
	/// Handles the input of player character.
	/// Maintains character momentum, as values are constantly updated, rather than
	/// only on press or release.
	/// 
	/// @param 
	/// </summary>
	private void handleMovement()
	{
		
		float vMotion = Input.GetAxis ("Vertical") * motionCoeff;
		float hMotion = Input.GetAxis ("Horizontal") * motionCoeff;
		vMotion *= Time.deltaTime;
		hMotion *= Time.deltaTime;

		transform.Translate (hMotion, vMotion, 0);
	}

	private void handleFiring()
	{

		//Left Click handling. For standard bullets
		if (Input.GetMouseButtonDown(0)) 
		{	
			int layerMask = 1 << 5;
			RaycastHit rayHit;
			RaycastHit playerRayHit;

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Ray playerRay = new Ray (transform.position, transform.forward);

			bool mouseRayCheck = Physics.Raycast (ray, out rayHit, 1000);//, layerMask);
			bool playerRayCheck = Physics.Raycast (playerRay, out playerRayHit, 1000);//, layerMask);

			if (mouseRayCheck && playerRayCheck) 
			{
				float xValue = rayHit.point.x - transform.position.x;
				float yValue = rayHit.point.y - transform.position.y;

				shootVector.x = xValue;
				shootVector.y = yValue;;


				bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
				bulletInstance.GetComponent<Bullet>().fireBullet(shootVector);
				Destroy (bulletInstance, 0.5f);
			}
		}

		//Right Click handling. For placing Whisper
		if (Input.GetMouseButtonDown (1)) 
		{
			
			RaycastHit rayHit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			bool mouseRayCheck = Physics.Raycast (ray, out rayHit, 1000);


			//if(rayHit.collider.isTrigger)

			GameObject whisperImage = GameObject.Find ("WhisperIndicator_Top");
			whisperImage.SetActive (false);
		}
	}


	private void handleDamage()
	{
		Time.timeScale = 0;
	}

	public void increaseScore ()
	{
		playerScore++;
		score = scoreText.gameObject.GetComponent<Text>();
		score.text = "Score: " + playerScore;
	}

	/// <summary>
	/// Constrains the player position within the camera viewport
	/// Scales with resolutions
	/// </summary>
	private void cameraConstrain()
	{


		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		pos.x = Mathf.Clamp(pos.x, 0.05f, 0.95f);
		pos.y = Mathf.Clamp(pos.y, 0.05f, 0.95f);
		transform.position = Camera.main.ViewportToWorldPoint(pos);

	}


	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "Enemy")
		{
			handleDamage();
		}

	}

	// Use this for initialization
	void Start () {

		//getCameraBounds();

		motionCoeff = 30;
		bulletSpeed = 50;
		bulletPrefab = GameObject.Find ("Sprite_Bullet");

		playerScore = 0;

		scoreText = GameObject.Find("Player_Score");

	}
	
	// Update is called once per frame
	void Update () {
	
		Vector2 pos = transform.position;
		transform.position = pos;


		handleMovement();
		handleFiring ();
		cameraConstrain ();

	}
}
