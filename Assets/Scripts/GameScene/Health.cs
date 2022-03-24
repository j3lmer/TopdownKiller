using GameScene.Trackers;
using UnityEngine;

namespace GameScene
{
    public class Health : MonoBehaviour
    {
        private int _hp;
        private int _lives;
        private bool _isPlayer;
        private bool _alive = true;
        private LifeTracker _tracker;

        public bool GetAlive()
        {
            return _alive;
        }

        public void SetAlive(bool alive)
        {
            _alive = alive;
        }

        private void Awake()
        {
            if (gameObject.CompareTag("Player"))
            {
                _isPlayer = true;
                _tracker = GameObject.FindGameObjectWithTag("GameController").GetComponent<LifeTracker>();
            }
        }

        public int GetHp()
        {
            return _hp;
        }

        public void SetHp(int newHp)
        {
            _hp = newHp;
            CheckForPlayerHealth();
        }

        public void SubtractHp(int hp)
        {
            _hp -= hp;
            CheckForPlayerHealth();
        }

        public void AddHp(int hp)
        {
            _hp += hp;
            CheckForPlayerHealth();
        }

        public int GetLives()
        {
            return _lives;
        }

        public void SetLives(int newLives)
        {
            _lives = newLives;
            CheckForPlayerLives();
        }

        public void AddLives(int lives)
        {
            _lives += lives;
            CheckForPlayerLives();
        }

        public void SubtractLives(int lives)
        {
            _lives -= lives;
            if (_lives > 0) SetHp(100);
            if(_lives <= 0) GetComponent<Death>().Die();

            CheckForPlayerLives();
        }

        public void CheckForPlayerHealth()
        {
            if (_isPlayer) _tracker.UpdateHealth(GetHp());
        }

        private void CheckForPlayerLives()
        {
            if (_isPlayer) _tracker.UpdateLives(GetLives());
        }
    }
}