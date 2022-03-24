using System;
using GameScene.Player;
using UnityEngine;

namespace GameScene.Enemy
{
    [RequireComponent(typeof(Death))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float coolDownTime = 0.5f;
        
        private Player.Player _player;
        private Shooting _shooter;
        private Health _health;
        private int _enemyType = 0;
        private float _shootTimer;

        public int GetEnemyType()
        {
            return _enemyType;
        }

        public void SetEnemyType(int type)
        {
            _enemyType = type;
        }

        private void Awake()
        {
            _shooter = GetComponent<Shooting>();
            _health = GetComponent<Health>();
            _health.SetHp(50);
            _health.SetLives(1);

            if (GameObject.FindGameObjectWithTag("Player"))
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
            }
        }

        private void Update()
        {
            _shootTimer += Time.deltaTime;
            if (_shootTimer > coolDownTime && _player)
            {
                _shootTimer = 0f;
                _shooter.Shoot(Color.gray);
            }
        }

        private void OnDestroy()
        {
            if (_player)
            {
                _player.GetComponent<Score>().UpdateScore(10);
            }
        }
    }
}