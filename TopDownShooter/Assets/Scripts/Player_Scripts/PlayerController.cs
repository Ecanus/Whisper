using UnityEngine;
using UnityEngine.UI;
using CnControls;
using UnityEngine.EventSystems;
using System.Collections.Generic;


/// <summary>
/// PlayerController class manages all player related responsibilities
/// 
/// @author - Dedie K.
/// @version - 0.0.1
/// 
/// 
/// </summary>
/// 

namespace Whisper
{
    public class PlayerController : MonoBehaviour
    {

        /// <summary>
        /// Player movement speed modifier
        /// </summary>
        [SerializeField]
        private float playerSpeed;

        /// <summary>
        /// Prefab of player bullet sprite
        /// </summary>
        [SerializeField]
        private GameObject bulletPrefab;

        /// <summary>
        /// GameObject containing prefab of a bullet instance
        /// </summary>
        private GameObject bulletInstance;

        /// <summary>
        /// The menu manager.
        /// </summary>
        [SerializeField]
        private MenuManager menuManager;

        /// <summary>
        /// Vector in direction from player to mouse position
        /// </summary>
        private Vector3 shootVector;

        /// <summary>
        /// Name of the quad that player is currently located in.
        /// </summary>
        [SerializeField]
        private string quadName;

        /// <summary>
        /// the Player score
        /// </summary>
        [SerializeField]
        private int playerNumScore;

        /// <summary>
        /// UI gameobject of player number score
        /// </summary>
        [SerializeField]
        private GameObject UI_NumScore;

        [SerializeField]
        private Slider scoreSlider;

        /// <summary>
        /// Text of player number score
        /// </summary>
        private Text numScoreText;

        /// <summary>
        /// State of being able to place a whisper onscreen
        /// </summary>
        private bool canPlaceWhisper;

        /// <summary>
        /// State of the game being paused
        /// </summary>
        private bool isPaused;

        public static string currentQuad;


        /// <summary>
        /// Handles the input of player character.
        /// Maintains character momentum, as values are constantly updated, rather than
        /// only on press or release.
        /// </summary>
        private void handleMovement()
        {

            //Check if we are running either in the Unity editor or in a standalone build.

            float vMotion = CnInputManager.GetAxisRaw("Vertical") * playerSpeed;
            float hMotion = CnInputManager.GetAxisRaw("Horizontal") * playerSpeed;

            vMotion *= Time.deltaTime;
            hMotion *= Time.deltaTime;

            transform.Translate(hMotion, vMotion, 0f);

        }

        /// <summary>
        /// Handles the player firing - both Bullets and Whispers.
        /// </summary>
        private void handleFiring()
        {

            /* 
             * Left Click handler, for bullet firing 
            */
            if (CnInputManager.GetButtonDown("Jump"))
            {

                /* Initialise values to store raycast */
                RaycastHit rayHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool mouseRayCheck = Physics.Raycast(ray, out rayHit, 1000);

                /* If ray makes contact with world, determine vector between mouseposition and player */
                if (mouseRayCheck && !isPaused)
                {

                    /* Assign values to shootVector variable */
                    shootVector.x = 0;
                    shootVector.y = 1;

                    /* Normalize to bound vector values and make scaling uniform */
                    shootVector.Normalize();

                    /* Instantiate bullet using existing prefab, at current player location */
                    bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                    bulletInstance.GetComponent<Bullet>().fireBullet(shootVector, quadName);

                    /* Destroy the particular instance after 1.5 seconds */
                    Destroy(bulletInstance, 0.25f);
                }
            }


            /* 
             * For placing Whisper
             */
            else if (Input.touchCount > 0)
            {
                //check every touch happening on the screen
                foreach (Touch currentTouch in Input.touches)
                {
                    //check if touches in the begin phase are touching anything apart of the UI
                    if (currentTouch.phase == TouchPhase.Began)
                    {
                        PointerEventData pointer = new PointerEventData(EventSystem.current);
                        pointer.position = currentTouch.position;

                        List<RaycastResult> raycastResults = new List<RaycastResult>();
                        EventSystem.current.RaycastAll(pointer, raycastResults);

                        if (raycastResults.Count > 0)
                        {
                            continue;
                        }
                    }

                    //if we have drags or other phases, ignore them
                    else
                    {
                        continue;
                    }


                    //if we have not touched a UI element and it's a touch in began phase, place the whisper
                    RaycastHit rayHit;
                    Ray ray = Camera.main.ScreenPointToRay(currentTouch.position);
                    bool mouseRayCheck = Physics.Raycast(ray, out rayHit, 1000);
                    bool quadTag = rayHit.transform.CompareTag("Quad");

                    if (mouseRayCheck && quadTag && !isPaused)
                    {
                        string whisperQuadName = rayHit.collider.gameObject.name + "_Whisper";
                        GameObject whisperObject = GameObject.Find(whisperQuadName);

                        whisperObject.gameObject.GetComponent<WhisperController>().activateWhisper();
                        canPlaceWhisper = false;
                    }

                }

            }
        }

        /// <summary>
        /// Gets the state of the whisper.
        /// </summary>
        /// <returns><c>true</c>, if whisper state was gotten, <c>false</c> otherwise.</returns>
        public bool getWhisperPlaceable()
        {
            return canPlaceWhisper;
        }

        /// <summary>
        /// Sets the state of the whisper.
        /// </summary>
        public void setWhisperPlaceable(bool newState)
        {
            canPlaceWhisper = newState;
        }

        public bool getIsPaused()
        {
            return isPaused;
        }

        public void setIsPaused(bool newState)
        {
            isPaused = newState;
        }

        /// <summary>
        /// Upon taking damage, game pauses and restart menu appears
        /// </summary>
        private void handleDamage()
        {
            menuManager.lostGame();
        }


        /// <summary>
        /// Increases player score
        /// </summary>
        public void increaseScore(int value)
        {
            playerNumScore += value;
            //Barricade.fallSpeed += 0.010f;
            playerSpeed += 0.005f;

            numScoreText = UI_NumScore.gameObject.GetComponent<Text>();
            numScoreText.text = playerNumScore + "%";

            scoreSlider.value = playerNumScore;
        }


        /// <summary>
        /// Constrains the player position within the camera viewport
        /// Scales with resolutions
        /// </summary>
        private void cameraConstrain()
        {

            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp(pos.x, 0.05f, 0.95f);
            pos.y = Mathf.Clamp(pos.y, 0.05f, 0.95f);
            transform.position = Camera.main.ViewportToWorldPoint(pos);

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Barricade")
            {
                handleDamage();
            }

            if (other.gameObject.tag == "Quad")
            {
                currentQuad = other.gameObject.name;
                Debug.Log(other.gameObject.name);
            }
        }

        private void OnTriggerStay(Collider other)
        {

            if (other.gameObject.tag == "Quad")
            {
                currentQuad = other.gameObject.name;
                quadName = other.gameObject.name;
            }
        }

        // Use this for initialization
        void Start()
        {

            /* Player values */
            playerSpeed = 6f;
            playerNumScore = 0;
            canPlaceWhisper = true;
            isPaused = false;


        }

        // Update is called once per frame
        void Update()
        {

            handleMovement();
            handleFiring();
            cameraConstrain();

        }
    }

}
