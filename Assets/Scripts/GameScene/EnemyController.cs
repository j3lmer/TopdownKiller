using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScene
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyPrefab;

        public IEnumerator SpawnEnemies()
        {
            while (true)
            {
                Vector2 spawnLocation = new Vector2(Random.Range(-25, 25), Random.Range(10 , -10));
                GameObject enemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(2, 10));
            }
        }
    }
}
