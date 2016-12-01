using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	/// <summary>
	/// The player gameObject. Used to create a path towards the player sprite.
	/// </summary>
	private GameObject player;

	/// <summary>
	/// The player position vector. Keeps track of a vector from enemy to player
	/// </summary>
	private Vector3 playerPosition;



	[Header("Enemy States")] 
	[SerializeField]
	[Tooltip("Enemy Health")]
	public int healthValue;

	private void seekPlayer()
	{
		playerPosition.x = player.transform.position.x - transform.position.x;
		playerPosition.y = player.transform.position.y - transform.position.y;

		transform.Translate (playerPosition * Time.deltaTime);
	}



	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Sprite_Player");
		playerPosition.z = 0.0f;

		healthValue = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
		seekPlayer();
	}

	/// <summary>
	/// Handles enemy healt depletion when shot.
	/// </summary>
	public void isShot()
	{
		healthValue--;
		if ((healthValue <= 0) && gameObject.name == "Sprite_Enemy(Clone)") {
			Destroy (this.gameObject);
		}
	}

}
