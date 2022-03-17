using UnityEngine;

namespace GameScene.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public Transform player; // changed this to Transform
        private Rigidbody2D rb;
        private Vector2 movement;
        private float moveSpeed = 5f;
        private float range = 8f;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }

        private void FixedUpdate()
        {
            MoveCharacter(movement);
        }

        void MoveCharacter(Vector2 direction)
        {
            if (IsPlayerInRange(range))
            {
                rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
            }
        }

        private bool IsPlayerInRange(float range)
        {
            return Vector3.Distance(transform.position, player.transform.position) >= range;
        }

    }
}

