using UnityEngine;
using System.Collections;

/// <summary>
/// Rocket.
/// </summary>
//[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

	private GameObject player;

	private Vector3 fireDirection;

	private float bulletSpeed;

	[SerializeField]
	private string isFiredDomain;

	public bool isFired;



	/// <summary>
	/// Sets fireDirection and sets isFIred to true
	/// </summary>
	/// <param name="shootDirection">Vector calculated from player to mouse position. Determines bullet trajectory.</param>
	public void fireBullet(Vector3 shootDirection, string firedDomain)
	{
		fireDirection = shootDirection;
		//fireDirection.Normalize ();
		isFired = true;

		isFiredDomain = firedDomain;

	}	


	private void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyBehaviour>().isShot();
			player.gameObject.GetComponent<PlayerBehaviour> ().increaseScore ();
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if ((other.gameObject.name == isFiredDomain) && (gameObject.name != "Sprite_Bullet")) 
		{
			Debug.Log("FireDirection Before: " + fireDirection.x + ", " + fireDirection.y);
			fireDirection.x = (-1) * fireDirection.x;
		}
	}

	private void destroyBullet()
	{
		if (gameObject.name == "Sprite_Bullet(Clone)") 
		{
			Destroy (gameObject, 2);
		}
	}

	// Use this for initialization
	void Start () {
		bulletSpeed = 30f;
		player = GameObject.Find ("Sprite_Player");

	}
	
	// Update is called once per frame
	void Update () {


		if (isFired) 
		{
			//Physics.IgnoreCollision (player.GetComponent<Collider>(), GetComponent<Collider>());
			transform.Translate(fireDirection * Time.deltaTime * bulletSpeed);
		}
	
	}
}
