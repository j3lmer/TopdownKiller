using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace GameScene
{
    public class EnemyController : MonoBehaviour
    {
        public GameObject enemyPrefab;
        private Scene gameScene;

        private void Start()
        {
            gameScene = SceneManager.GetActiveScene();
            StartCoroutine(SpawnEnemies());
        }


        private IEnumerator SpawnEnemies()
        {
            while (SceneManager.GetActiveScene() == gameScene)
            {
                Vector2 spawnLocation = new Vector2(Random.Range(-10, 20), Random.Range(20 , -10));
                GameObject enemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(2, 10));
            }
        }
    }
}
