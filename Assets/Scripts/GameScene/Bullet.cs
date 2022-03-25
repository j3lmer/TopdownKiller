using UnityEngine;

namespace GameScene
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject hitEffect;

        private float _despawnTime = 2f;
        private float _aliveTime;
        private GameObject _shooter;

        public void SetShooter(GameObject shooter)
        {
            _shooter = shooter;
        }

        public GameObject GetShooter()
        {
            return _shooter;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            int damage = 10;
            if (col.CompareTag("Bullet") || col.CompareTag("Powerup")) return;
            if (GetShooter())
            {
                if (col.CompareTag("Enemy") && GetShooter().CompareTag("Enemy")) return;
                if (col.CompareTag("Player") && GetShooter().CompareTag("Player")) return;
            }
            
            if (GetShooter())
            {
                GameObject shooter = GetShooter();
                if 
                (
                    shooter.GetComponent<Enemy.Enemy>() &&
                    shooter.GetComponent<Enemy.Enemy>().GetEnemyType() == 1
                ) damage = 50;
            }
            
            if (col.TryGetComponent(out Health health)) health.SubtractHp(damage);
            Destroy(gameObject);
        }

        private void Update()
        {
            _aliveTime += Time.deltaTime;
            if (_aliveTime >= _despawnTime) Destroy(gameObject);
        }
    }
}