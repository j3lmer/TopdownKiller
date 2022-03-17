using UnityEngine;

namespace GameScene
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] 
        private Transform firePoint;
        
        [SerializeField] 
        private GameObject bulletPrefab;
        
        [SerializeField] 
        private float bulletForce = 20f;

        // Update is called once per frame
        public void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}
