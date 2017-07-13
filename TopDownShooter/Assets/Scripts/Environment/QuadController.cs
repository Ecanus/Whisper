using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// QuadController class handles quad scrolling and all children
/// of the Quad gameObject during runtime
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 

namespace Whisper
{

    public class QuadController : MonoBehaviour
    {
  
        /// <summary>
        /// Speed of quad texture scrolling
        /// </summary>
        [SerializeField]
        private float scrollSpeed;

        private float enemySpeed;

        private float barricadeSpeed;

        /// <summary>
        /// Offset time for smooth scrolling resume on player reentry
        /// </summary>
        [SerializeField]
        private float currentOffsetTime;

        private EntityManager entityManager;

        /// <summary>
        /// The amount of time the player is in the quad. This determines speed
        /// </summary>
        private float currentElapsedTimeInQuad;


        private List<int> speedUpTiers;
        private bool isLaneExpanding;
        private bool isLaneRetracting;

        /// <summary>
        /// Scrolls the quad texture verticallly by scrollSpeed.
        /// </summary>
        private void scrollImage()
        {
            Vector2 offset = new Vector2(0f, (currentOffsetTime * scrollSpeed) % 1);
            currentOffsetTime += Time.deltaTime;
            GetComponent<Renderer>().material.mainTextureOffset = offset;
        }


        /// <summary>
        /// Speeds up the lane every X seconds
        /// </summary>
        private void speedUpLane()
        {
            currentElapsedTimeInQuad += Time.deltaTime;
            float tierTime = Mathf.Abs(PlayerController.totalTimeInQuads - currentElapsedTimeInQuad);
            int multiplier = speedUpTiers.IndexOf(Mathf.CeilToInt(tierTime / 10) * 10);
            enemySpeed = 0.5f + 0.2f*multiplier;
            barricadeSpeed = 3f + multiplier;

            
        }

        private IEnumerator expandLane()
        {
            isLaneExpanding = true;

            //make sure we are not expanding and retracting at the same time
            //if we are, just wait
            while (isLaneRetracting)
            {
                yield return new WaitForEndOfFrame();
            }

            while (this.transform.localScale.x < 5.75)
            {
                this.transform.localScale += new Vector3(0.05f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);

            }
            isLaneExpanding = false;
        }

        private IEnumerator retractLane()
        {

            //make sure we are not retracting while expanding
            //if we are, just wait
            while (isLaneExpanding)
            {
                yield return new WaitForEndOfFrame();
            }

            isLaneRetracting = true;
            while (this.transform.localScale.x > 4.75)
            {
                this.transform.localScale -= new Vector3(0.05f, 0f, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            isLaneRetracting = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !isLaneExpanding)
            {
                Debug.Log("Currently expanding " + this.gameObject.name);
                StartCoroutine(expandLane());
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                scrollImage();
                if (canMoveEntitiesInQuad()) speedUpLane();
            }

            //if (other.gameObject.CompareTag("Whisper"))
            //{
            //    entityManager.moveNonEnemies();
            //    //actuateAllBarricades();
            //}
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !isLaneRetracting)
            {
                Debug.Log("Currently retracting " + this.gameObject.name);
                StartCoroutine(retractLane());
                //haltAllChildren();
            }
        }

        public bool canMoveEntitiesInQuad()
        {
            if (PlayerController.currentQuad != this.name) return false;
            return true;
        }

        private void spawnEnemy()
        {
            if (!canMoveEntitiesInQuad()) return;
            StartCoroutine(SpawnEnemiesWithDelay());
        }

        private IEnumerator SpawnEnemiesWithDelay()
        {
            for (int x = 0; x < Random.Range(1, 5); x++)
            {
                yield return new WaitForSeconds(0.4f);
                entityManager.spawn(ResourceManager.GameObjects.Enemy);
            }
               
        }

        private void spawnObstacle()
        {
            if (!canMoveEntitiesInQuad()) return;

            if (Random.Range(1, 6) == 1)
            entityManager.spawn(ResourceManager.GameObjects.Barricade);

            else entityManager.spawn(ResourceManager.GameObjects.Block);

        }

        public float getCurrentBarricadeSpeed()
        {
            return barricadeSpeed;
        }

        public float getCurrentEnemySpeed()
        {
            return enemySpeed;
        }

        // Use this for initialization
        void Start()
        {
            scrollSpeed = 0.2f;
            enemySpeed = 0.5f;
            barricadeSpeed = 3f;

            currentOffsetTime = Time.deltaTime;
            currentElapsedTimeInQuad = 0f;
            this.gameObject.AddComponent(typeof(EntityManager));

            entityManager = GetComponent<EntityManager>();
            entityManager.setSpawnPoint(this.name);

            InvokeRepeating("spawnEnemy", 1f, 3f);
            InvokeRepeating("spawnObstacle", 1f, 1.75f);

            isLaneExpanding = false;
            isLaneRetracting = false;

            speedUpTiers = new List<int> {10,20,30,40,50,70,90,110};

        }

        // Update is called once per frame
        void Update()
        {
        }
    }

}