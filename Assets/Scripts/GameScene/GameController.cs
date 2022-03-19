using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameScene
{
    [RequireComponent(typeof(EnemyController)), RequireComponent(typeof(PowerupController))]
    public class GameController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject playerPrefab;

        private GameObject _player;

        private int playerLives;
        
        private void Awake()
        {
            _player = MakePlayer();
            StartCoroutine(gameObject.GetComponent<EnemyController>().SpawnEnemies());
        }
        
        private GameObject MakePlayer() 
        {
            return Instantiate(playerPrefab, new Vector3(), Quaternion.identity);
        }
    }
}