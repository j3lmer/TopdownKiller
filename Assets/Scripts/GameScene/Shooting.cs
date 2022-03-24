using System;
using GameScene.Powerup;
using UnityEngine;

namespace GameScene
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Transform[] firePoints;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletForce = 20f;
        private int _fireMode = 0;

        public int GetFireMode()
        {
            return _fireMode;
        }

        public void SetFireMode(int fireMode)
        {
            if (fireMode > Enum.GetNames(typeof(FireModes)).Length -1) return;
            _fireMode = fireMode;
        }
        // Update is called once per frame
        public void Shoot(Color color)
        {
            Debug.Log(GetFireMode() + gameObject.name);
            switch (_fireMode)
            {
                case (int) FireModes.Default:
                    FireBullet(color, firePoints[0], firePoints[0].up);
                    break;
                
                case (int) FireModes.Twin:
                    FireBullet(color, firePoints[1], firePoints[1].up);
                    FireBullet(color, firePoints[2], firePoints[2].up);
                    break;
                
                case (int) FireModes.Triple:
                    FireBullet(color, firePoints[0], firePoints[0].up);
                    FireBullet(color, firePoints[1], firePoints[1].up - (firePoints[1].right * 0.25f));
                    FireBullet(color, firePoints[2], firePoints[2].up + (firePoints[2].right * 0.25f));
                    break;
            }
        }
        
        private void FireBullet(Color color, Transform firePoint, Vector3 direction)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<SpriteRenderer>().color = color;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletForce, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().SetShooterTag(gameObject.tag);
        }
    }
}