using UnityEngine;
using System.Collections;

/// <summary>
/// Rocket.
/// </summary>
//[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

	private GameObject player;

	private Vector3 fireDirection;

	public bool isFired;



	/// <summary>
	/// Sets fireDirection and sets isFIred to true
	/// </summary>
	/// <param name="shootDirection">Vector calculated from player to mouse position. Determines bullet trajectory.</param>
	public void fireBullet(Vector3 shootDirection)
	{
		fireDirection = shootDirection;
		fireDirection.Normalize ();
		isFired = true;

	}	

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyBehaviour>().isShot();
			player.gameObject.GetComponent<PlayerBehaviour> ().increaseScore ();
			//other.gameObject.GetComponent<EnemyBehaviour> ().healthValue--;
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
		player = GameObject.Find ("Sprite_Player");

	}
	
	// Update is called once per frame
	void Update () {


		//destroyBullet();
		if (isFired) 
		{
			Physics2D.IgnoreCollision (player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			transform.Translate(fireDirection * Time.deltaTime * 65);
		}
	
	}
}
