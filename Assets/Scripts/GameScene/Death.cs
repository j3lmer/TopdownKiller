using System;
using UnityEngine;

namespace GameScene
{
    [RequireComponent(typeof(Health))]
    public class Death : MonoBehaviour
    {
        private Health _health ;

        private void Awake()
        {
            _health = gameObject.GetComponent<Health>();
        }

        private void Update()
        {
            if (_health.hp <= 0)
            {
                _health.lives -= 1;
            }

            if (_health.lives <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}