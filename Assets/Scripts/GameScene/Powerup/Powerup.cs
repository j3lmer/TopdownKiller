using System;
using System.Collections;
using GameScene.Player;
using UnityEngine;

namespace GameScene.Powerup
{
    public class Powerup : MonoBehaviour
    {
        private int _powerupType;
        
        public int GetPowerupType()
        {
            return _powerupType;
        }

        public void SetPowerupType(int powerupType)
        {
            _powerupType = powerupType;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(!col.gameObject.CompareTag("Player")) return;
            Destroy(gameObject);
            ExecutePowerup();
        }

        private void ExecutePowerup()
        {
            switch (_powerupType)
            {
                case (int) PowerupTypes.RemoveEnemies:
                    DestroyEnemies();
                    // setColor(gray);
                    break;
                
                case (int) PowerupTypes.SpeedboostPlayer:
                    StartCoroutine(SpeedboostPlayer());
                    // setColor(blue);
                    break;
                
                case (int) PowerupTypes.HealthBoost:
                    //INVINCIBILITY
                    // setColor(green);
                    break;
                
                case (int) PowerupTypes.Points:
                    //POINTS
                    // setColor(yellow);
                    break;
                
                case (int) PowerupTypes.StopEnemies:
                    //STOP_ENEMIES
                    // setColor(red);
                    break;
                case (int) PowerupTypes.OneUp:
                    //OneUp
                    // setColor(green);
                    break;
            }
        }

        private void setColor()
        {
            
        }
        

        private void DestroyEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }

        private IEnumerator SpeedboostPlayer()
        {
            PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            player.SetMoveSpeed(10f);

            yield return new WaitForSeconds(4);
            player.SetMoveSpeed(5f);
        }
    }
}
