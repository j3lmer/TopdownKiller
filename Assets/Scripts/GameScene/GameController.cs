using System.Collections;
using UnityEngine;

namespace GameScene
{
    [RequireComponent(typeof(EnemyController)), RequireComponent(typeof(PowerupController))]
    public class GameController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject playerPrefab;

        private GameObject _player;
        private EnemyController _enemyController; 
        
        private void Awake()
        {
            _enemyController = gameObject.GetComponent<EnemyController>();
            _player = MakePlayer();
            StartCoroutine(WaveController());
        }
        
        private GameObject MakePlayer() 
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(), Quaternion.identity);
            player.AddComponent<Player.Player>();
            return player;
        }
        
        private IEnumerator WaveController()
        {
            yield return new WaitForSeconds(1);
            _enemyController.spawnDefaultEnemy = true;
            StartCoroutine(_enemyController.SpawnEnemies());
            yield return null;
        }
    }
}
