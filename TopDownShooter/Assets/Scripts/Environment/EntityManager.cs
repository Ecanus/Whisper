using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Whisper
{
    public class EntityManager : MonoBehaviour
    {

        private List<GameObject> spawnedObjects;

        [SerializeField]
        private GameObject spawnPoint;

        private float speed;

        public void Start()
        {
            spawnedObjects = new List<GameObject>();
            speed = 0.8f;
        }

        
        public void deleteObjectFromList(GameObject objectToFind)
        {
            //Debug.Log("DELETING: " + objectToFind.tag);
            spawnedObjects.Remove(objectToFind);
        }


        public void setSpawnPoint(string lane)
        {
            lane = lane.Split('_')[1];
            spawnPoint = GameObject.Find("Spawn_" + lane);
        }
        

        public void spawn(ResourceManager.GameObjects gameObject)
        {

            GameObject objectToSpawn = new GameObject();

            switch (gameObject) {
                
                case ResourceManager.GameObjects.Barricade:
                    objectToSpawn = Instantiate<GameObject>(ResourceManager.barricadePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            objectToSpawn.GetComponent<SpriteRenderer>().sprite = ResourceManager.getRandomBarricade();
            objectToSpawn.transform.parent = spawnPoint.transform.parent;
                    break;

                case ResourceManager.GameObjects.Block:
                    objectToSpawn = Instantiate<GameObject>(ResourceManager.blockPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            objectToSpawn.GetComponent<SpriteRenderer>().sprite = ResourceManager.getRandomBlock();
            objectToSpawn.transform.parent = spawnPoint.transform.parent;
                    break;

                case ResourceManager.GameObjects.Enemy:
                    objectToSpawn = Instantiate<GameObject>(ResourceManager.enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            objectToSpawn.transform.parent = spawnPoint.transform.parent;
                    break;

            }

            spawnedObjects.Add(objectToSpawn);
        }

        //public void moveAllObstacles()
        //{
        //    foreach (GameObject go in spawnedObjects)
        //    {
        //        go.transform.Translate(Vector3.down * Time.deltaTime * speed);
        //    }
        //}

        //public void moveNonEnemies()
        //{
        //    foreach (GameObject go in spawnedObjects)
        //    {
        //        if (go.name.ToLower().Contains("enemy"))
        //        {
        //            continue;
        //        }

        //        go.transform.Translate(Vector3.down * Time.deltaTime * speed);


        //    }
        //}


    }

}