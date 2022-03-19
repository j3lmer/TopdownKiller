using System;
using UnityEngine;

namespace GameScene
{
    public class Health : MonoBehaviour
    {
        private int _hp;
        private int _lives;

        public int GetHp()
        {
            return _hp;
        }

        public void SetHp(int newHp)
        {
            _hp = newHp;
            if (gameObject.CompareTag("Player"))
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<LifeTracker>().UpdateHealth(newHp);
            }
        }
        
        
        public int GetLives()
        {
            return _lives;
        }

        public void SetLives(int newLives)
        {
            _lives = newLives;
            if (gameObject.CompareTag("Player"))
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<LifeTracker>().UpdateLives(newLives);
            }

            if (_lives > 0)
            {
                SetHp(100);
            }
        }
    }
}