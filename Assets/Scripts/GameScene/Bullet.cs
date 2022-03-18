using UnityEngine;

namespace GameScene
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private GameObject hitEffect;
        
        private float _despawnTime = 2f;
        private float aliveTime = 0f;

        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Bullet") || col.CompareTag("Powerup")) return;
            Health health = col.GetComponent<Health>();
            
            if (health)
            {
                health.hp -= 10;
            }
            
            Destroy(gameObject);
        }

        private void Update()
        {
            aliveTime += Time.deltaTime;

            if (aliveTime >= _despawnTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
