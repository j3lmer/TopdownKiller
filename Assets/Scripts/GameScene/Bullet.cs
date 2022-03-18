using System;
using UnityEngine;

namespace GameScene
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private GameObject hitEffect;


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<Collider2D>().CompareTag("Bullet")) return;
            Health health = col.GetComponent<Health>();
            
            if (health)
            {
                health.hp -= 10;
            }
            
            Destroy(gameObject);
        }
    }
}
