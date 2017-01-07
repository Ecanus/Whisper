using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


/// <summary>
/// PlayerController class manages all player related responsibilities
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 
public class PlayerController : MonoBehaviour {

	/// <summary>
	/// The relative gameobject of player being handled
	/// </summary>
	private GameObject player;

	/// <summary>
	/// Player movement speed modifier
	/// </summary>
	private float playerSpeed;

	/// <summary>
	/// Prefab of player bullet sprite
	/// </summary>
	private GameObject bulletPrefab;

	/// <summary>
	/// GameObject containing prefab of a bullet instance
	/// </summary>
	private GameObject bulletInstance;

	/// <summary>
	/// Vector in direction from player to mouse position
	/// </summary>
	private Vector3 shootVector;

	/// <summary>
	/// Name of the quad that player is currently located in.
	/// </summary>
	private string quadName;

	/// <summary>
	/// the Player score
	/// </summary>
	private int player_NumScore;

	/// <summary>
	/// UI gameobject of player number score
	/// </summary>
	private GameObject UI_NumScore;

	/// <summary>
	/// Text of player number score
	/// </summary>
	private Text numScoreText;


	/// <summary>
	/// Handles the input of player character.
	/// Maintains character momentum, as values are constantly updated, rather than
	/// only on press or release.
	/// </summary>
	private void handleMovement()
	{
		
		float vMotion = Input.GetAxis ("Vertical") * playerSpeed;
		float hMotion = Input.GetAxis ("Horizontal") * playerSpeed;
		vMotion *= Time.deltaTime;
		hMotion *= Time.deltaTime;

		transform.Translate (hMotion, vMotion, 0);
	}

	private void handleFiring()
	{

		/* 
		 * Left Click handler, for bullet firing 
		*/
		if (Input.GetMouseButtonDown(0)) 
		{	
			
			/* Initialise values to store raycast */
			RaycastHit rayHit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			bool mouseRayCheck = Physics.Raycast (ray, out rayHit, 1000);

			/* If ray makes contact with world, determine vector between mouseposition and player */
			if (mouseRayCheck)
			{
				/* Determine x, y coordinates using ray from mousePosition to player position */
				float xValue = rayHit.point.x - transform.position.x;
				float yValue = rayHit.point.y - transform.position.y;

				/* Assign values to shootVector variable */
				shootVector.x = xValue;
				shootVector.y = yValue;;

				/* Normalize to bound vector values and make scaling uniform */
				shootVector.Normalize();

				/* Instantiate bullet using existing prefab, at current player location */
				bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
				bulletInstance.GetComponent<Bullet>().fireBullet(shootVector, quadName);

				/* Destroy the particular instance after 1.5 seconds */
				Destroy (bulletInstance, 0.5f);
			}
		}


		/* 
		 * Right Click handler, for placing Whisper
		 */
		if (Input.GetMouseButtonDown (1)) 
		{
			
			RaycastHit rayHit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			bool mouseRayCheck = Physics.Raycast (ray, out rayHit, 1000);
			bool quadTag = (rayHit.transform.tag == "Quad");

			if (mouseRayCheck && quadTag) 
			{
				string whisperQuadName = rayHit.collider.gameObject.name + "_Whisper";
				Debug.Log (whisperQuadName);

				GameObject whisperObject = GameObject.Find (whisperQuadName);

				Image whisperImage = whisperObject.GetComponent<Image>();
				Color whisperColor = whisperImage.color;
				whisperColor.a = 0f;
				whisperImage.color = whisperColor;

				//whisperImage.SetActive (false);
			}


		}
	}


	/// <summary>
	/// Upon taking damage, game pauses and restart menu appears
	/// </summary>
	private void handleDamage()
	{
		Time.timeScale = 0;
	}


	/// <summary>
	/// Increases player score
	/// </summary>
	public void increaseScore ()
	{
		player_NumScore++;
		numScoreText = UI_NumScore.gameObject.GetComponent<Text>();
		numScoreText.text = "Score: " + player_NumScore;
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


	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Barricade")
		{
			//handleDamage();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Quad") 
		{
			quadName = other.gameObject.name;
		}
	}

	// Use this for initialization
	void Start () {

		/* Player values */
		playerSpeed = 12f;
		player_NumScore = 0;

		/* Bullet values */
		bulletPrefab = GameObject.Find("Sprite_Bullet");

		/* UI values */
		UI_NumScore = GameObject.Find("Player_NumScore");

	}
	
	// Update is called once per frame
	void Update () {

		handleMovement();
		handleFiring ();
		cameraConstrain ();

	}
}
