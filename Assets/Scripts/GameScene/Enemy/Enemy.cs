using System;
using UnityEngine;

namespace GameScene.Enemy
{
    [RequireComponent(typeof(Death))]
    public class Enemy : MonoBehaviour
    {
        private Player.Player _player;
        private Shooting _shooter;
        private Health _health;

        [SerializeField] 
        private float coolDownTime = 0.5f;

        private float _shootTimer;
        private void Awake()
        {
            _shooter        = GetComponent<Shooting>();
            _health         = GetComponent<Health>();
            _health.hp      = 50;
            _health.lives   = 1;

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
                _shooter.Shoot(Color.black);
            }
        }

        private void OnDestroy()
        {
            if(_player) _player.points += 10;
        }
    }
}
