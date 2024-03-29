using System;
using UnityEngine;

namespace GameScene.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float stoppingDistance = 20f;
        [SerializeField] private float retreatDistance = 10f;

        private Rigidbody2D rb;
        private bool _canMove = true;

        public void SetCanMove(bool canMove)
        {
            _canMove = canMove;
        }

        public bool GetCanMove()
        {
            return _canMove;
        }

        private void Awake()
        {
            if (GameObject.FindGameObjectWithTag("Player"))
                player = GameObject.FindGameObjectWithTag("Player").transform;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!player) return;
            if (!transform) return;

            Vector2 pos = transform.position;
            Vector2 playerPos = player.position;
            Vector3 direction = playerPos - pos;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float enemyPlayerDistance = Vector2.Distance(pos, playerPos);

            rb.rotation = angle;

            Move(pos, playerPos, enemyPlayerDistance);
        }

        private void Move(Vector2 pos, Vector2 playerPos, float enemyPlayerDistance)
        {
            if (!_canMove) return;
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