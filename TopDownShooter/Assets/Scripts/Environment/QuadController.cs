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
        /// Calls the halt() method of all children of this Quad
        /// </summary>
        private void haltAllChildren()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<IQuadChild>().halt();
            }
        }

        /// <summary>
        /// Calls the actuate() method of all children of this Quad
        /// </summary>
        private void actuateAllChildren()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<IQuadChild>().actuate();
            }
        }

        /// <summary>
        /// Calls the actuate() method of only non-enemy children of this Quad
        /// </summary>
        private void actuateAllBarricades()
        {
            foreach (Transform child in transform)
            {
                if (!child.CompareTag("Enemy"))
                {
                    child.GetComponent<IQuadChild>().actuate();
                }
            }
        }

        /// <summary>
        /// Speeds up the lane every X seconds
        /// </summary>
        private void speedUpLane()
        {
            currentElapsedTimeInQuad += Time.deltaTime;
            if (currentElapsedTimeInQuad > scrollSpeed * 50)
            {
                Debug.Log("SPEED UP LANE");
                scrollSpeed += (scrollSpeed / 2);
                enemySpeed += 0.1f;
                barricadeSpeed += 0.5f;
            }
            else
            {
                //Debug.Log(currentElapsedTimeInQuad);
            }
        }




        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                scrollImage();
                //actuateAllChildren();
                entityManager.moveAllObstacles();
                speedUpLane();
            }

            if (other.gameObject.CompareTag("Whisper"))
            {
                entityManager.moveNonEnemies();
                //actuateAllBarricades();
            }
        }



        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Whisper"))
            {
                //haltAllChildren();
            }
        }

        private void spawnEnemy()
        {
            if (PlayerController.currentQuad != this.name) return;

            entityManager.spawn(ResourceManager.GameObjects.Enemy);
        }

        private void spawnObstacle()
        {
            if (PlayerController.currentQuad != this.name) return;


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
            
        }

        // Update is called once per frame
        void Update()
        {
        }
    }

}