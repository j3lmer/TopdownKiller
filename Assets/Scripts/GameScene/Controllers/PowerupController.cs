using System;
using System.Collections;
using System.Collections.Generic;
using GameScene.Player;
using GameScene.Powerup;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScene.Controllers
{
    public class PowerupController : MonoBehaviour
    {
        [SerializeField] private GameObject powerupPrefab;

        private void Awake()
        {
            StartCoroutine(SpawnPowerups());
        }

        private IEnumerator SpawnPowerups()
        {
            Dictionary<int, Color> colors = PowerupColors.Colors;

            while (true)
            {
                int powerupType = Random.Range(0, Enum.GetNames(typeof(PowerupTypes)).Length);
                powerupType = CheckValidPowerup(powerupType);
                Vector2 spawnLocation = new Vector2(Random.Range(-25, 25), Random.Range(10, -10));

                GameObject powerupGameObject = Instantiate(powerupPrefab, spawnLocation, Quaternion.identity);
                GameScene.Powerup.Powerup powerup = powerupGameObject.GetComponent<GameScene.Powerup.Powerup>();
                
                powerup.SetPowerupType(powerupType);
                powerup.SetColor(colors[powerupType]);

                yield return new WaitForSeconds(Random.Range(5, 20));
            }
        }

        private int CheckValidPowerup(int powerupTypeLocal)
        {
            switch (powerupTypeLocal)
            {
                case (int) PowerupTypes.SpeedboostPlayer:
                    PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                    if (playerMovement.GetMoveSpeed() >= playerMovement.GetMaxMoveSpeed())  powerupTypeLocal = (int) PowerupTypes.Points;
                    break;
                
                case (int) PowerupTypes.IncreaseBullets:
                    Shooting shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
                    if (shooter.GetFireMode() >= shooter.GetMaxFireMode()) powerupTypeLocal = (int) PowerupTypes.Points;
                    break;
                
                case (int) PowerupTypes.QuickShot:
                    Player.Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
                    if(player.GetMaxTimeUntilShoot() >= player.GetTimeUntilShoot()) powerupTypeLocal = (int) PowerupTypes.Points;
                    break;
            }

            return powerupTypeLocal;
        }
    }
}