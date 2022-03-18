using System;
using UnityEngine;

namespace GameScene.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public float speed = 10f;
        public float stoppingDistance = 20f;
        public float retreatDistance = 10f;

        public Transform player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }


        private void Update()
        {
            Vector2 pos = transform.position;
            Vector2 playerPos = player.position;
            Vector3 direction = playerPos - pos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            
            gameObject.GetComponent<Rigidbody2D>().rotation = angle;
            
            float enemyPlayerDistance = Vector2.Distance(pos, playerPos);
            
            if (enemyPlayerDistance > stoppingDistance) 
            {
                transform.position = Vector2.MoveTowards
                (
                    pos,
                    playerPos,
                    speed * Time.deltaTime
                );
            }
            else if (enemyPlayerDistance < retreatDistance)
            {
                transform.position = Vector2.MoveTowards
                    (
                        pos,
                        playerPos,
                        -speed * Time.deltaTime
                    );
            }
            else if (enemyPlayerDistance < stoppingDistance && enemyPlayerDistance > stoppingDistance)
            {   
                transform.position = pos;
            }
        }

    }
}

