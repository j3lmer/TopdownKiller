using System;
using UnityEngine;

namespace GameScene.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public int hp = 50;
        
        [SerializeField] 
        private float coolDownTime = 0.5f;
        
        private float _shootTimer;

        private Shooting _shooter;
        private Player.Player _player;


        private void Awake()
        {
            _shooter = gameObject.GetComponent<Shooting>();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.gameObject.CompareTag("Bullet"))
            {
                hp -= 10;
            }
        }

        private void Update()
        {
            _shootTimer += Time.deltaTime;
            if (_shootTimer > coolDownTime)
            {
                _shootTimer = 0f;
                _shooter.Shoot();
            }
            
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            _player.points += 10;
        }
    }
}
