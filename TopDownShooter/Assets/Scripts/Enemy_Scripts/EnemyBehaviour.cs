using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	/// <summary>
	/// The destination gameObject. The location towards which the enemy goes towards.
	/// </summary>
	private GameObject destination;

	/// <summary>
	/// The player position vector. Keeps track of a vector from enemy to player
	/// </summary>
	private Vector2 destinationPos;



	[Header("Enemy States")] 
	[SerializeField]
	[Tooltip("Enemy Health")]
	private int healthValue;

	private void seekDestination()
	{
		//destinationPos.x = destination.transform.position.x - transform.position.x;
		//destinationPos.y = destination.transform.position.y - transform.position.y;
		Vector2 from = transform.position;
		Vector2 to = destination.transform.position;

		transform.position = Vector2.Lerp (from, to, 0.01f);;
	}



	// Use this for initialization
	void Start () {

		destination = GameObject.Find ("Sprite_Player");

		healthValue = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
		seekDestination();

	}

	/// <summary>
	/// Handles enemy healt depletion when shot.
	/// </summary>
	public void isShot()
	{
		healthValue--;
		if ((healthValue <= 0)){// && gameObject.name == "Sprite_Enemy(Clone)") {
			Destroy (this.gameObject);
		}
	}

}
