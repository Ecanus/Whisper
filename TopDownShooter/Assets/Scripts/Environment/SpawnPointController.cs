using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// SpawnPointController class manages enemy and barricade spawn point behaviour in-game
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 


namespace Whisper
{
    /*
    public class SpawnPointController : MonoBehaviour, IQuadChild
    {


        [SerializeField]
        private int arrayPointer;
        [SerializeField]
        private int blockArrayPointer;
        [SerializeField]
        private float spawnInvokeTimer;


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
        /// Array of enemies
        /// </summary>
        [SerializeField]
        private GameObject[] enemyArray;

        [SerializeField]
        private GameObject[] barricadeArray;

        [SerializeField]
        private GameObject[] blockArray;

        /// <summary>
        /// The sprites for barricades, defeated enemies and blocks
        /// </summary>
        private Sprite[] barricadeSprites;
        private Sprite[] blockSprites;
        public static Sprite enemyDefeatedSprite;
        public static Sprite enemyNormalSprite;

        /// <summary>
        /// SpawnPoint state of being able to spawn new enemies
        /// </summary>
        private bool canCreate;


        /// <summary>
        /// Populates the enemy array.
        /// </summary>
        private void populateArrays()
        {
            for (int f = 0; f <= 9; f += 1)
            {

                enemyArray[f] = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
                enemyArray[f].transform.parent = gameObject.transform.parent;
                enemyArray[f].GetComponent<Enemy>().setSpawnPoint(this.gameObject);

                blockArray[f] = Instantiate(blockPrefab, transform.position, transform.rotation) as GameObject;
                blockArray[f].transform.parent = gameObject.transform.parent;
                blockArray[f].GetComponent<Barricade>().setSpawnPoint(this.gameObject);
            }


            for (int f = 0; f <= 7; f += 1)
            {
                barricadeArray[f] = Instantiate(barricadePrefab, transform.position, transform.rotation) as GameObject;
                barricadeArray[f].transform.parent = gameObject.transform.parent;
                barricadeArray[f].GetComponent<Barricade>().setSpawnPoint(this.gameObject);
            }

        }

        /// <summary>
        /// Spawns the Enemy.
        /// </summary>
        private void spawnEnemy()
        {

            if (canCreate)
            {
                int waveNumber = Random.Range(1, 4);
                StartCoroutine(createEnemy(waveNumber));
            }
        }

        /// <summary>
        /// Creates the enemy in waves, of up to 4 enemies
        /// </summary>
        /// <returns>The enemy.</returns>
        /// <param name="enemiesInWave">Enemies in wave.</param>
        private IEnumerator createEnemy(int enemiesInWave)
        {
            for (int f = 0; f <= enemiesInWave; f += 1)
            {

                enemyArray[f].GetComponent<Enemy>().launch();
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
                // Percentage based spawn rate 
                int spawnChance = Random.Range(0, 11);

                switch (spawnChance)
                {

                    // 36% chance to spawn Barricade Standard 
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        launchBarricade();
                        break;

                    // 64% chance to spawn Block
                    default:
                        launchBlock();
                        break;
                }
            }
        }

        /// <summary>
        /// Creates the block.
        /// </summary>
        private void launchBlock()
        {
            blockArray[blockArrayPointer].GetComponent<BlockController>().setOffset();
            blockArray[blockArrayPointer].GetComponent<Barricade>().launch();
            blockArray[blockArrayPointer].GetComponent<SpriteRenderer>().sprite = blockSprites[Random.Range(0, blockSprites.Length)];

            blockArrayPointer++;
            blockArrayPointer = blockArrayPointer % 10;
        }

        /// <summary>
        /// Creates the barricade.
        /// </summary>
        private void launchBarricade()
        {
            barricadeArray[arrayPointer].GetComponent<Barricade>().launch();
            barricadeArray[arrayPointer].GetComponent<SpriteRenderer>().sprite = barricadeSprites[Random.Range(0, barricadeSprites.Length)];

            arrayPointer++;
            arrayPointer = arrayPointer % 8;
        }

        /// <summary>
        /// Updates the time between enemy spawns
        /// </summary>
        public void updateEnemySpawnTimer()
        {
            if (spawnInvokeTimer <= 1f)
            {
                spawnInvokeTimer = 1f;
                return;
            }

            spawnInvokeTimer -= 0.45f;
            CancelInvoke("spawnEnemy");
            InvokeRepeating("spawnEnemy", 0f, spawnInvokeTimer);
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
        void Start()
        {

            
            spawnInvokeTimer = 3.0f;

            arrayPointer = 0;
            blockArrayPointer = 0;

            barricadeSprites = new Sprite[4];
            barricadeSprites = Resources.LoadAll<Sprite> ("Enemy_Images/Barricades");

            blockSprites = new Sprite[4];
            blockSprites = Resources.LoadAll<Sprite> ("Enemy_Images/Blocks");

            enemyArray = new GameObject[10];
            barricadeArray = new GameObject[8];
            blockArray = new GameObject[10];
            populateArrays();

            enemyDefeatedSprite = Resources.Load<Sprite> ("Enemy_Images/Enemy_Digit_Defeated");
            enemyNormalSprite = Resources.Load<Sprite> ("Enemy_Images/Enemy_Digit");

            InvokeRepeating("spawnEnemy", 1f, spawnInvokeTimer);
            InvokeRepeating ("spawnBarricade", 1f, 1.75f);
            InvokeRepeating ("updateEnemySpawnTimer", 60f, 60f);
            
        }

    }
*/
}
