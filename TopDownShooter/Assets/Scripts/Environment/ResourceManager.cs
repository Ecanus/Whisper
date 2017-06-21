using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Whisper
{
    public class ResourceManager : MonoBehaviour
    {

        public enum GameObjects
        {
            Barricade,
            Block,
            Enemy
        }

        private static Sprite[] barricadeSprites;
        private static Sprite[] blockSprites;

        public static Sprite enemyDefeatedSprite;
        public static Sprite enemySprite;


        public static GameObject enemyPrefab;
        public static GameObject barricadePrefab;
        public static GameObject blockPrefab;

        public static Sprite getRandomBarricade()
        {
            return barricadeSprites[Random.Range(0, barricadeSprites.Length)];
        }

        public static Sprite getRandomBlock()
        {
            return blockSprites[Random.Range(0, blockSprites.Length)];
        }

        // Use this for initialization
        void Start()
        {
            Debug.Log("Loading Resources");
            barricadeSprites = new Sprite[4];
            barricadeSprites = Resources.LoadAll<Sprite>("Enemy_Images/Barricades");

            blockSprites = new Sprite[4];
            blockSprites = Resources.LoadAll<Sprite>("Enemy_Images/Blocks");

            enemyDefeatedSprite = Resources.Load<Sprite>("Enemy_Images/Enemy_Digit_Defeated");
            enemySprite = Resources.Load<Sprite>("Enemy_Images/Enemy_Digit");

            enemyPrefab = Resources.Load("Prefabs/Sprite_Enemy") as GameObject;
            if (enemyPrefab == null)
            {
                Debug.Log("Enemy prefab null");
            }
            barricadePrefab = Resources.Load("Prefabs/Sprite_Barricade") as GameObject;
            if (barricadePrefab == null)
            {
                Debug.Log("barricade prefab null");
            }
            blockPrefab = Resources.Load("Prefabs/Sprite_BarricadeBlock") as GameObject;
            if (blockPrefab == null)
            {
                Debug.Log("block prefab null");
            }
        }
    }

}

