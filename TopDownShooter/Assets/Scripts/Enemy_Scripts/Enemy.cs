using UnityEngine;
using System.Collections;


/// <summary>
/// EnemyController parent class manages all generalised methods and attributes
/// for enemy subclasses
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
/// 

namespace Whisper
{

    public class Enemy : MonoBehaviour, IQuadChild
    {

        /// <summary>
        /// Points awarded to player when this enemy is defeated
        /// </summary>
        public static int scoreValue = 1;

        /// <summary>
        /// Player gameobject
        /// </summary>
        [SerializeField]
        private GameObject player;

        /// <summary>
        /// The spawn origin.
        /// </summary>
        protected GameObject spawnOrigin;

        /// <summary>
        /// Health value of enemy
        /// </summary>
        [SerializeField]
        protected int healthValue;

        /// <summary>
        /// Speed at which Enemy falls towards bottom of screen
        /// </summary>
        protected float fallSpeed;

        /// <summary>
        /// Enemy state of being in motion
        /// </summary>
        [SerializeField]
        protected bool canMove;

        /// <summary>
        /// State of having lost all healthValue
        /// </summary>
        [SerializeField]
        protected bool isKilled;

        /// <summary>
        /// State of being allowed to leave spawnPoint
        /// </summary2>
        [SerializeField]
        protected bool isLaunched;


        public void Start()
        {
            Debug.Log("enemy call start");
            player = GameObject.Find("Sprite_Player");
            fallSpeed = GetComponentInParent<QuadController>().getCurrentEnemySpeed();
        }

        public void Update()
        {
            if (this.transform.parent.name != PlayerController.currentQuad)
            {
                return;
            }

            seekTarget();

        }

        /// <summary>
        /// Moves enemy towards target destination
        /// </summary>
        private void seekTarget()
        {

            Vector2 from = transform.position;
            Vector2 to = player.transform.position;

            transform.position = Vector2.Lerp(from, to, (Time.deltaTime * fallSpeed));
        }

        /// <summary>
        /// Launch the enemy from the specified spawnPointName.
        /// </summary>
        public virtual void launch()
        {
            isLaunched = true;
        }

        /// <summary>
        /// Sets the enemy spawnOrigin using spawnPoint parameter.
        /// </summary>
        /// <param name="spawnPoint">Spawn point.</param>
        public virtual void setSpawnPoint(GameObject spawnPoint)
        {
            spawnOrigin = spawnPoint;
        }

        /// <summary>
        /// Handles enemy behaviour when hit by bullet
        /// </summary>
        public virtual void isShot()
        {
            /* If hit, decrease health */
            healthValue--;

            if ((healthValue <= 0))
            {
                player.gameObject.GetComponent<PlayerController>().increaseScore(scoreValue);
                isKilled = true;
            }
        }


        /// <summary>
        /// Fades the out enemy upon defeat. Relocates Enemy once fade is complete
        /// </summary>
        protected void fadeOut()
        {
            Color enemyColor = gameObject.GetComponent<SpriteRenderer>().color;
            enemyColor = new Color(1f, 1f, 1f, Mathf.SmoothStep(1f, 0f, 0.5f));
            gameObject.GetComponent<SpriteRenderer>().color = enemyColor;

            StartCoroutine("respawn");

        }

        /*
        /// <summary>
        /// Places enemy back into SpawnPointController enemyArray attribute
        /// </summary>
        private IEnumerator respawn()
        {
            yield return new WaitForSeconds(0.075f);

            transform.position = spawnOrigin.transform.position;

            gameObject.GetComponent<SpriteRenderer>().sprite = SpawnPointController.enemyNormalSprite;

            Color enemyColor = gameObject.GetComponent<SpriteRenderer>().color;
            enemyColor = new Color(1f, 1f, 1f, 1f);
            gameObject.GetComponent<SpriteRenderer>().color = enemyColor;

            isLaunched = false;
            isKilled = false;
        }
        */

        public void OnTriggerEnter(Collider other)
        {
            //Debug.Log("Collision with: " + other.tag);
            if (other.CompareTag("Basin") || other.CompareTag("Bullet"))
            {
                this.GetComponentInParent<EntityManager>().deleteObjectFromList(this.gameObject);
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// IQuadChild Method for halting motion
        /// </summary>
        public void halt()
        {
            canMove = false;
        }

        /// <summary>
        /// IQuadChild Methord for actuating motion
        /// </summary>
        public void actuate()
        {
            canMove = true;
        }
    }

}