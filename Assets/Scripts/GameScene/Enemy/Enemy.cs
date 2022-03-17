using System;
using UnityEngine;

namespace GameScene.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] 
        private int hp = 50;
        
        [SerializeField] 
        private float coolDownTime = 0.5f;
        
        private float shootTimer;

        private Shooting shooter;


        private void Awake()
        {
            shooter = gameObject.GetComponent<Shooting>();
        }

        private void Update()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > coolDownTime)
            {
                shootTimer = 0f;
                shooter.Shoot();
            }
            
            
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
