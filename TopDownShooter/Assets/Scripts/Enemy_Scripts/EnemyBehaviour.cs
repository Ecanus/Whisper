using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	private GameObject player;
	private Vector3 playerPosition;


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
	}
	
	// Update is called once per frame
	void Update () {
	
		seekPlayer();
	}
}
