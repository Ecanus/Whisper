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
	/// Original prefabs of assets to be instantiated.
	/// </summary>
	private GameObject enemyPrefab;
	private GameObject barricadePrefab;
	private GameObject blockPrefab;

	/// <summary>
	/// GameObjects to hold instantiated assets during runtime
	/// </summary>
	private GameObject enemyInstance;
	private GameObject barricadeInstance;
	private GameObject blockInstance;

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
		}
	}

	private void spawnBarricade()
	{
		if (canCreate) 
		{
			barricadeInstance = Instantiate (barricadePrefab, transform.position, transform.rotation) as GameObject;
			barricadeInstance.transform.parent = gameObject.transform.parent;
		}
	}

	private void spawnBlock()
	{
		if (canCreate) 
		{
			blockInstance = Instantiate (blockPrefab, transform.position, transform.rotation) as GameObject;
			blockInstance.transform.parent = gameObject.transform.parent;
		}
	}

	/// <summary>
	/// IQuadChild Method for halting motion
	/// </summary>
	public void halt()
	{
		canCreate = false;
	}

	/// <summary>
	/// IQuadChild Methord fo actuating motion
	/// </summary>
	public void actuate()
	{
		canCreate = true;
	}

	// Use this for initialization
	void Start () {

		enemyPrefab = GameObject.Find ("Sprite_EnemyDigit");
		barricadePrefab = GameObject.Find ("Sprite_BarricadeStandard");
		blockPrefab = GameObject.Find ("Sprite_BarricadeBlock");

		InvokeRepeating("spawnEnemy", 1f, 0.5f);
		InvokeRepeating ("spawnBarricade", 1f, 1f);
		//InvokeRepeating ("spawnBlock", 1f, 1f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
