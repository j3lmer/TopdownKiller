using UnityEngine;

namespace GameScene
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject hitEffect;

        private float _despawnTime = 2f;
        private float _aliveTime = 0f;
        private string _shooter;

        public void SetShooterTag(string shooter)
        {
            _shooter = shooter;
        }

        public string GetShooterTag()
        {
            return _shooter;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Bullet") || col.CompareTag("Powerup")) return;
            if (col.CompareTag("Enemy") && GetShooterTag() == "Enemy") return;
            if (col.CompareTag("Player") && GetShooterTag() == "Player") return;

            Health health = col.GetComponent<Health>();
            if (health) health.SubtractHp(10);

            Destroy(gameObject);
        }

        private void Update()
        {
            _aliveTime += Time.deltaTime;
            if (_aliveTime >= _despawnTime) Destroy(gameObject);
        }
    }
}