using UnityEngine;
using System.Collections;

/// <summary>
/// EnemyDigitController class manages Digit enemy types in-game
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
    public class EnemyDigitController : Enemy
    {

        /// <summary>
        /// Moves enemy towards target destination
        /// </summary>
        protected void seekTarget()
        {
            Vector2 from = transform.position;
            Vector2 to = player.transform.position;

            transform.position = Vector2.Lerp(from, to, (Time.deltaTime * fallSpeed));

        }


        void Start()
        {

            player = GameObject.Find("Sprite_Player");

            fallSpeed = 1.2f;
            healthValue = 1;

            isLaunched = false;
        }


        void Update()
        {

            if (isLaunched)
            {
                if (canMove)
                {
                    seekTarget();
                }

                if (isKilled)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = SpawnPointController.enemyDefeatedSprite;
                    fadeOut();
                }
            }
        }

    }
    */
}
