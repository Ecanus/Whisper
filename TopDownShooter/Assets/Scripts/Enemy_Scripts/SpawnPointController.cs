using UnityEngine;
using System.Collections;

/// <summary>
/// SpawnPointController class manages enemy spawn point behaviour in-game
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 
public class SpawnPointController : MonoBehaviour, IQuadChild {

	/// <summary>
	/// The enemy prefab.
	/// </summary>
	private GameObject enemyPrefab;

	/// <summary>
	/// The enemy instance.
	/// </summary>
	private GameObject enemyInstance;

	/// <summary>
	/// SpawnPoint state of being able to spawn new enemies
	/// </summary>
	private bool canCreate;

	/// <summary>
	/// Spawns the enemy.
	/// </summary>
	private void spawnEnemy()
	{
		if (canCreate) 
		{
			enemyInstance = Instantiate (enemyPrefab, transform.position, transform.rotation) as GameObject;
			enemyInstance.transform.parent = gameObject.transform.parent;
			enemyInstance.SetActive (true);
		}
	}

	public void halt()
	{
		canCreate = false;
	}

	public void actuate()
	{
		canCreate = true;
	}

	// Use this for initialization
	void Start () {

		enemyPrefab = GameObject.Find ("Sprite_EnemyDigit");
		InvokeRepeating("spawnEnemy", 1f, 0.5f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
