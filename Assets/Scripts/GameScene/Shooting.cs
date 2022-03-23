using UnityEngine;

namespace GameScene
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletForce = 20f;

        // Update is called once per frame
        public void Shoot(Color color)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<SpriteRenderer>().color = color;
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().SetShooterTag(gameObject.tag);
        }
    }
}