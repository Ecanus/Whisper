using UnityEngine;
using System.Collections;

/// <summary>
/// SpawnPointController class manages enemy and barricade spawn point behaviour in-game
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
	[SerializeField]
	private GameObject enemyPrefab;
	[SerializeField]
	private GameObject barricadePrefab;
	[SerializeField]
	private GameObject blockPrefab;

	/// <summary>
	/// GameObjects to hold instantiated assets during runtime
	/// </summary>
	private GameObject enemyInstance;
	private GameObject barricadeInstance;
	private GameObject blockInstance;

	/// <summary>
	/// The sprites for barricades, defeated enemies and blocks
	/// </summary>
	private static Sprite[] barricadeSprites;
	private static Sprite[] blockSprites;
	private static Sprite enemyDefeatedSprite;

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
			int waveNumber = Random.Range (1, 4);
			StartCoroutine (createEnemy(waveNumber));
		}
	}

	/// <summary>
	/// Creates the enemy in waves, of up to 4 enemies
	/// </summary>
	/// <returns>The enemy.</returns>
	/// <param name="enemiesInWave">Enemies in wave.</param>
	private IEnumerator createEnemy(int enemiesInWave)
	{
		for (int f = 0; f <= enemiesInWave; f += 1) {

			enemyInstance = Instantiate (enemyPrefab, transform.position, transform.rotation) as GameObject;
			enemyInstance.transform.parent = gameObject.transform.parent;
			yield return new WaitForSeconds(0.2f);
		}
	}

	/// <summary>
	/// Spawns a random barricade type.
	/// </summary>
	private void spawnBarricade()
	{
		if (canCreate) 
		{
			/* Percentage based spawn rate */
			int spawnChance = Random.Range (0, 10);

			switch (spawnChance) {

			/* 30% chance to spawn Barricade Standard */
			case 0:
			case 1:
			case 2:
				createBarricade();
				break;
			
			/* 70% chance to spawn Block */
			default:
				createBlock ();
				break;
			}
		}
	}

	/// <summary>
	/// Creates the block.
	/// </summary>
	private void createBlock()
	{
		blockInstance = Instantiate (blockPrefab, transform.position, transform.rotation) as GameObject;
		blockInstance.transform.parent = gameObject.transform.parent;
		blockInstance.GetComponent<SpriteRenderer> ().sprite = blockSprites [Random.Range (0, blockSprites.Length)];
	}

	/// <summary>
	/// Creates the barricade.
	/// </summary>
	private void createBarricade()
	{
		barricadeInstance = Instantiate (barricadePrefab, transform.position, transform.rotation) as GameObject;
		barricadeInstance.transform.parent = gameObject.transform.parent;
		barricadeInstance.GetComponent<SpriteRenderer> ().sprite = barricadeSprites [Random.Range (0, barricadeSprites.Length)];
	}

	public static Sprite getDigitDefeatedSprite()
	{
		return enemyDefeatedSprite;
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

		barricadeSprites = new Sprite[4];
		barricadeSprites = Resources.LoadAll<Sprite> ("Enemy_Images/Barricades");

		blockSprites = new Sprite[4];
		blockSprites = Resources.LoadAll<Sprite> ("Enemy_Images/Blocks");

		enemyDefeatedSprite = Resources.Load<Sprite> ("Enemy_Images/Enemy_Digit_Defeated");

		InvokeRepeating("spawnEnemy", 1f, 3.0f);
		InvokeRepeating ("spawnBarricade", 1f, 2.1f);

	}

}
