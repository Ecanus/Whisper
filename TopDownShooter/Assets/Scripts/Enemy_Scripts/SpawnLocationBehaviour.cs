using UnityEngine;
using System.Collections;

public class SpawnLocationBehaviour : MonoBehaviour {

	/// <summary>
	/// The enemy prefab.
	/// </summary>
	private GameObject enemyPrefab;

	/// <summary>
	/// The enemy instance.
	/// </summary>
	private GameObject enemyInstance;



	/// <summary>
	/// Spawns the enemy.
	/// </summary>
	private void spawnEnemy()
	{
		enemyInstance = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
		enemyInstance.SetActive (true);
	}


	private void createEnemies(int enemyType)
	{
		
	}

	// Use this for initialization
	void Start () {

		enemyPrefab = GameObject.Find ("Sprite_Enemy");
		InvokeRepeating("spawnEnemy", 1f, 1f);
		//enemyPrefab.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
