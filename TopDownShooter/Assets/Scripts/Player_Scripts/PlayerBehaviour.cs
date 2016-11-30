using UnityEngine;
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

	// Use this for initialization
	void Start () {

		motionCoeff = 30;
		bulletSpeed = 50;
		bulletPrefab = GameObject.Find ("Sprite_Bullet");

	}
	
	// Update is called once per frame
	void Update () {
	
		handleMovement();
		handleFiring ();
		cameraConstrain ();

	}


	/// <summary>
	/// Handles the input of player character.
	/// Maintains character momentum, as values are constantly updated, rather than
	/// only on press or release.
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

		if (Input.GetMouseButtonDown(0)) 
		{	

			RaycastHit rayHit;
			RaycastHit playerRayHit;

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Ray playerRay = new Ray (transform.position, transform.forward);

			bool mouseRayCheck = Physics.Raycast (ray,  out rayHit, 1000);
			bool playerRayCheck = Physics.Raycast (playerRay, out playerRayHit, 1000);

			if (mouseRayCheck && playerRayCheck) 
			{
				float xValue = rayHit.point.x - transform.position.x;
				float yValue = rayHit.point.y - transform.position.y;

				shootVector.x = xValue;
				shootVector.y = yValue;;
				shootVector.z = 0.0f;

				bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
				bulletInstance.GetComponent<Bullet>().setIsFiredTrue(shootVector);
			}
		}

	}


	/// <summary>
	/// Constrains the player position within the camera viewport
	/// Scales with aspect ratios
	/// </summary>
	private void cameraConstrain()
	{
		
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		pos.x = Mathf.Clamp(pos.x, 0, 1);
		pos.y = Mathf.Clamp(pos.y, 0, 1);
		transform.position = Camera.main.ViewportToWorldPoint(pos);

	}
}
