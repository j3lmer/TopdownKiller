using Audio;
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
            //TODO: improve this
            int hp = _health.GetHp();
            if (hp <= 0)
            {
                _health.SubtractLives(1);
            }
        }

        public void Die()
        {
            FindObjectOfType<AudioManager>().Play("die");
            _health.SetAlive(false);

            Destroy(gameObject);
        }
    }
}