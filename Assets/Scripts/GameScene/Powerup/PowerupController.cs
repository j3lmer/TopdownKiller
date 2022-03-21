using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScene.Powerup
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
            Color[] colors = {Color.magenta, Color.blue, Color.green, Color.yellow, Color.red, Color.white};

            while (true)
            {
                //PowerupTypes
                int powerupType = Random.Range(0, Enum.GetNames(typeof(PowerupTypes)).Length);
                Vector2 spawnLocation = new Vector2(Random.Range(-25, 25), Random.Range(10 , -10));
                
                GameObject powerupGameObject = Instantiate(powerupPrefab, spawnLocation, Quaternion.identity);
                GameScene.Powerup.Powerup powerup = powerupGameObject.GetComponent<GameScene.Powerup.Powerup>();
                powerup.SetPowerupType(powerupType);
                powerup.SetColor(colors[powerupType]);
                
                yield return new WaitForSeconds(Random.Range(5, 20));
            }
        }
    }
}
