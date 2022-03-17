using UnityEngine;

namespace GameScene
{
    public class Bullet : MonoBehaviour
    {

        public GameObject hitEffect;
    
        private void OnCollisionEnter2D(Collision2D col)
        {
            // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy(effect, 5f);
            Destroy(gameObject);
        }
    }
}
