using System;
using UnityEngine;

namespace GameScene
{
    [RequireComponent(typeof(Health))]
    public class Death : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = gameObject.GetComponent<Health>();
        }

        private void Update()
        {
            int hp = _health.GetHp();
            if ( hp <= 0)
            {
                _health.SubtractLives(1);
            }

            if (_health.GetLives() <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}