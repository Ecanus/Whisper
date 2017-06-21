using UnityEngine;
using System.Collections;

/// <summary>
/// Barricade parent class manages all generalised methods and attributes
/// for barricade subclasses
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// </summary>
///

namespace Whisper
{

    public abstract class Barricade : MonoBehaviour, IQuadChild
    {
        /// <summary>
        /// Barricade state of being in motion
        /// </summary>
        protected bool isMoving;

        /// <summary>
        /// The spawn origin.
        /// </summary>
        protected GameObject spawnOrigin;

        /// <summary>
        /// State of being allowed to move from spawnPoint
        /// </summary>
        //protected bool isLaunched;

        /// <summary>
        /// Tells barricade to move in a downwards direction
        /// </summary>
        protected abstract void fall();

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Basin"))
            {
                //transform.position = spawnOrigin.transform.position;
                //isLaunched = false;
                this.GetComponentInParent<EntityManager>().deleteObjectFromList(this.gameObject);
                Destroy(this.gameObject);
            }
        }


        /// <summary>
        /// Handles enemy behaviour when hit by bullet
        /// </summary>
        public void OnTriggerStay(Collider other)
        {

            
        }

        /// <summary>
        /// IQuadChild Method for halting motion
        /// </summary>
        public void halt()
        {
            isMoving = false;
        }

        /// <summary>
        /// IQuadChild Methord fo actuating motion
        /// </summary>
        public void actuate()
        {
            isMoving = true;
        }

    }

}