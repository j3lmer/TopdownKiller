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


        private void Awake()
        {
            _shooter = gameObject.GetComponent<Shooting>();
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
    }
}
