using UnityEngine;
using System.Collections;

/// <summary>
/// BarricadeStandardController class manages all behaviour of standard barricades in-game
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
///

namespace Whisper
{
    public class BarricadeStandardController : Barricade
    {

        [SerializeField]
        private float fallSpeed;

        private QuadController quadController;

        /// <summary>
        /// Moves Barricade in downwoards direction
        /// </summary>
        protected override void fall()
        {
            if (!quadController.canMoveEntitiesInQuad())
            {
                return;
            }
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
        }


        // Use this for initialization
        void Start()
        {
            //isLaunched = false;
            quadController = GetComponentInParent<QuadController>();
            fallSpeed = quadController.getCurrentBarricadeSpeed();
        }

        // Update is called once per frame
        void Update()
        {
            fall();
        }
    }

}

