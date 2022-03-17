using UnityEngine;

namespace GameScene.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public Rigidbody2D rb;
        public Camera camera;

        public Vector2 movement;
        private Vector2 _mousePos; 
    

        // Update is called once per frame
        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            _mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        }

        void FixedUpdate() 
        {
            rb.MovePosition(rb.position +movement * moveSpeed * Time.fixedDeltaTime);

            Vector2 lookDir = _mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }
}
