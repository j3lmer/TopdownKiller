using System;
using UnityEngine;

namespace GameScene.Controllers
{
    [RequireComponent(typeof(EnemyController))]
    public class GameControllerHelper : GameController
    {
        private EnemyController _enemyController;

        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
        }

        public void SmallWave(int index, int i)
        {
            EnemySpawner((index + 1) + (i + 1), () => _enemyController.SpawnEnemy());
        }

        public void StartBossWave(int bossNumber)
        {
            EnemySpawner(bossNumber, () => _enemyController.SpawnDefaultBoss(bossNumber));
        }

        private void EnemySpawner(int numberOfEnemies, Action function)
        {
            for (int i = 0; i < numberOfEnemies; i++) function();
        }
    }
}