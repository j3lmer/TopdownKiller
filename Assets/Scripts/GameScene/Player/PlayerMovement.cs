using System;
using UnityEngine;

namespace GameScene.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Camera camera;

        [SerializeField]
        private float moveSpeed = 5f;

        private Rigidbody2D rb;

        private Vector2 _movement;
        private Vector2 _mousePos;

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }

        public void AddMoveSpeed(float speed)
        {
            moveSpeed += speed;
        }

        private void SubtractMoveSpeed(float speed)
        {
            moveSpeed -= speed;
        }


        private void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            _mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        }

        void FixedUpdate() 
        {
            rb.MovePosition(rb.position +_movement * moveSpeed * Time.fixedDeltaTime);

            Vector2 lookDir = _mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }
}
