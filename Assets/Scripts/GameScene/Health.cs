using System;
using UnityEngine;

namespace GameScene
{
    public class Health : MonoBehaviour
    {
        private int _hp;
        private int _lives;
        private bool _isPlayer = false;
        private LifeTracker _tracker = null;

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
            CheckForPlayer();
        }

        public void SubtractHp(int hp)
        {
            _hp -= hp;
            CheckForPlayer();
        }

        public void CheckForPlayer()
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
            if (_isPlayer)
            {
                _tracker.UpdateLives(newLives);
            }

            if (_lives > 0)
            {
                SetHp(100);
            }
        }
    }
}