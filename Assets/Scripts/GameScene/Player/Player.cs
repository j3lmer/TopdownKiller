using UnityEngine;

namespace GameScene.Player
{
    [RequireComponent(typeof(Death)), RequireComponent(typeof(Score))]
    public class Player : MonoBehaviour
    {
        private Health _health;
        private Shooting _shooter;
        
        private float _countingTime;
        private double _timeUntilShoot = 0.2f;
        public const double MaxTimeUntilShoot = 0.1f;

        public double GetMaxTimeUntilShoot()
        {
            return MaxTimeUntilShoot;
        }
        
        public double GetTimeUntilShoot()
        {
            return _timeUntilShoot;
        }

        public void SetTimeUntilShoot(double time)
        {
            _timeUntilShoot = time;
        }

        private void Awake()
        {
            _health = GetComponent<Health>();
            _shooter = GetComponent<Shooting>();

            _health.SetHp(100);
            _health.SetLives(3);
        }

        private void Update()
        {
            _countingTime += Time.deltaTime;
            if (Input.GetButton("Fire1") && _timeUntilShoot < _countingTime)
            {
                _countingTime = 0f;
                _shooter.Shoot(Color.white);
            }
        }
    }
}