using System;
using UnityEngine;

namespace GameScene
{
    public class Health : MonoBehaviour
    {
        private int _hp;
        private int _lives;
        private bool _isPlayer;
        private LifeTracker _tracker;

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

        public void CheckForPlayerHealth()
        {
            if (_isPlayer)
            {
                _tracker.UpdateHealth(GetHp());
            }
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
        

        private void CheckForPlayerLives()
        {
            if (_isPlayer)
            {
                _tracker.UpdateLives(GetLives());
            }

            if (_lives > 0)
            {
                SetHp(100);
            }

        }
    }
}