using System;
using UnityEngine;

namespace GameScene.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        private float _moveSpeed = 5f;

        private Rigidbody2D _rb;

        private Vector2 _movement;
        private Vector2 _mousePos;
        public const float Maxmovespeed = 10f;

        public float GetMaxMoveSpeed()
        {
            return Maxmovespeed;
        }

        public float GetMoveSpeed()
        {
            return _moveSpeed;
        }

        public void SetMoveSpeed(float speed)
        {
            _moveSpeed = speed;
        }

        public void AddMoveSpeed(float speed)
        {
            if (_moveSpeed + speed > Maxmovespeed) return;
            _moveSpeed += speed;
        }

        private void SubtractMoveSpeed(float speed)
        {
            _moveSpeed -= speed;
        }

        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
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
            _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);

            Vector2 lookDir = _mousePos - _rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = angle;
        }
    }
}