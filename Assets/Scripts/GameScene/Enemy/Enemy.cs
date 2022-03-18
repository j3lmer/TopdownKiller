using System;
using UnityEngine;

namespace GameScene.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] 
        private float coolDownTime = 0.5f;
        
        private float _shootTimer;

        private Shooting _shooter;
        private Player.Player _player;
        private Health _health;
        private Death _death;



        private void Awake()
        {
            _death = gameObject.AddComponent<Death>();
            _health = GetComponent<Health>();
            
            _health.hp = 50;
            _health.lives = 1;
            
            _shooter = gameObject.GetComponent<Shooting>();
            if(GameObject.FindGameObjectWithTag("Player"))
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
            }
        }

        private void Update()
        {
            _shootTimer += Time.deltaTime;
            if (_shootTimer > coolDownTime)
            {
                _shootTimer = 0f;
                _shooter.Shoot(Color.black);
            }
        }

        private void OnDestroy()
        {
            _player.points += 10;
        }
    }
}
