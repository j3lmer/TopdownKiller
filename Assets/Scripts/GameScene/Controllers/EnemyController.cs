using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScene.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;

        public void SpawnEnemy()
        {
            Vector2 spawnLocation = new Vector2(Random.Range(-25, 25), Random.Range(10, -10));
            Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        }

        public void SpawnDefaultBoss(int multiplyValue)
        {
            Vector2 spawnLocation = new Vector2(Random.Range(-25, 25), Random.Range(10, -10));
            GameObject defaultBoss = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);

            defaultBoss.transform.localScale *= 2;

            Health smallBossHealth = defaultBoss.GetComponent<Health>();
            Enemy.Enemy smallBossEnemy = defaultBoss.GetComponent<Enemy.Enemy>();

            smallBossHealth.SetHp(smallBossHealth.GetHp() * multiplyValue);
            smallBossEnemy.SetEnemyType(1);
        }
    }
}