using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScene
{
    public class PowerupController : MonoBehaviour
    {
        [SerializeField]
        private GameObject powerupPrefab;

        private void Start()
        {
            StartCoroutine(SpawnPowerups());
        }

        private IEnumerator SpawnPowerups()
        {
            while (true)
            {
                Vector2 spawnLocation = new Vector2(Random.Range(-25, 25), Random.Range(10 , -10));
                GameObject powerup = Instantiate(powerupPrefab, spawnLocation, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(10, 20));
            }
        }
    }
}
