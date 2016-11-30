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

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Sprite_Player");

	}
	
	// Update is called once per frame
	void Update () {

		if (isFired) 
		{
			Physics2D.IgnoreCollision (player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			transform.Translate(fireDirection * Time.deltaTime * 65);
		}
	
	}

	public void setIsFiredTrue(Vector3 shootDirection)
	{
		
		Debug.Log ("ShootDirection: " + "(" +shootDirection.x + ", " + shootDirection.y + ")");
		fireDirection = shootDirection;
		fireDirection.Normalize ();
		isFired = true;

	}
}
