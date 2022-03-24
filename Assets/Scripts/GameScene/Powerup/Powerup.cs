using GameScene.Enemy;
using GameScene.Player;
using UnityEngine;

namespace GameScene.Powerup
{
    public class Powerup : MonoBehaviour
    {
        private int _powerupType;
        private GameObject _player;

        public int GetPowerupType()
        {
            return _powerupType;
        }

        public void SetPowerupType(int powerupType)
        {
            _powerupType = powerupType;
        }

        public void SetColor(Color color)
        {
            GetComponent<SpriteRenderer>().color = color;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            _player = col.gameObject;
            Destroy(gameObject);
            ExecutePowerup();
        }

        private void ExecutePowerup()
        {
            switch (_powerupType)
            {
                case (int) PowerupTypes.RemoveEnemies:
                    DestroyEnemies();
                    break;
                case (int) PowerupTypes.SpeedboostPlayer:
                    SpeedboostPlayer();
                    break;
                case (int) PowerupTypes.HealthBoost:
                    HealthBoost();
                    break;
                case (int) PowerupTypes.Points:
                    AddPoints();
                    break;
                case (int) PowerupTypes.StopEnemies:
                    StopEnemies();
                    break;
                case (int) PowerupTypes.OneUp:
                    OneUp();
                    break;
                case (int) PowerupTypes.QuickShot:
                    QuickShot();
                    break;
                case (int) PowerupTypes.IncreaseBullets:
                    IncreaseBullets();
                    break;
            }
        }

        private void DestroyEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }

        private void HealthBoost()
        {
            Health playerHealth = _player.GetComponent<Health>();
            playerHealth.AddHp(50);
        }

        private void SpeedboostPlayer()
        {
            PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();
            playerMovement.AddMoveSpeed(2);
        }

        private void AddPoints()
        {
            _player.GetComponent<Score>().UpdateScore(50);
        }

        private void StopEnemies()
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<EnemyMovement>().SetCanMove(false);
            }
        }

        private void OneUp()
        {
            _player.GetComponent<Health>().AddLives(1);
        }

        private void QuickShot()
        {
            double maxSpeed = 1f;

            Player.Player player = _player.GetComponent<Player.Player>();

            double hypoSpeed = player.GetTimeUntilShoot() - 0.005f;

            if (hypoSpeed > maxSpeed) return;
            player.SetTimeUntilShoot(hypoSpeed);
        }

        private void IncreaseBullets()
        {
            Shooting shooter = _player.GetComponent<Shooting>();
            shooter.SetFireMode(shooter.GetFireMode() + 1);
        }
    }
}