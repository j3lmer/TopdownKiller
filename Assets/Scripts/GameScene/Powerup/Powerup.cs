using UnityEngine;

namespace GameScene.Powerup
{
    public class Powerup : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if(!col.gameObject.CompareTag("Player")) return;
            Destroy(gameObject);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
}
