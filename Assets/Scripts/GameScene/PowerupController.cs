using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScene
{
    public class PowerupController : MonoBehaviour
    {
        [SerializeField]
        private GameObject powerupPrefab;

        private void Awake()
        {
            StartCoroutine(SpawnPowerups());
        }
        
        private IEnumerator SpawnPowerups()
        {
            while (true)
            {
                //PowerupTypes
                int powerupType = Random.Range(0, Enum.GetNames(typeof(PowerupTypes)).Length - 1);
                Vector2 spawnLocation = new Vector2(Random.Range(-25, 25), Random.Range(10 , -10));
                
                GameObject powerup = Instantiate(powerupPrefab, spawnLocation, Quaternion.identity);
                powerup.GetComponent<Powerup.Powerup>().SetPowerupType(powerupType);
                
                yield return new WaitForSeconds(Random.Range(5, 20));
            }
        }
    }
}
