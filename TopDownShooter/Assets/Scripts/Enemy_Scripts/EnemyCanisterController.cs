using UnityEngine;
using System.Collections;

/// <summary>
/// EnemyDigitController class manages Canister enemy types in-game.
/// NOT IMPLEMENTED
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 

namespace Whisper
{
    public class EnemyCanisterController : Enemy
    {

        /// <summary>
        /// Uses a sine wave pattern to approach player
        /// </summary>
        protected void seekTarget()
        {
            float frequency = 5f;
            float magnitude = 0.1f;

            Vector3 pos = transform.position;
            Vector3 axis = Vector3.down;

            pos += (Vector3.down * Time.deltaTime * fallSpeed);
            transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        }


        // Use this for initialization
        void Start()
        {
            fallSpeed = 1f;
            healthValue = 3;
        }

        // Update is called once per frame
        void Update()
        {

            seekTarget();

            if (isKilled)
            {
                fadeOut();
            }
        }
    }

}
