using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace GameScene
{
    [RequireComponent(typeof(EnemyController)), RequireComponent(typeof(PowerupController))]
    public class GameController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject playerPrefab;

        private GameObject _player;
        private EnemyController _enemyController;
        
        private void Awake()
        {
            _enemyController = gameObject.GetComponent<EnemyController>();
            _player = MakePlayer();
            StartCoroutine(WaveController());
        }
        
        private GameObject MakePlayer() 
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(), Quaternion.identity);
            player.AddComponent<Player.Player>();
            return player;
        }
        
        private IEnumerator WaveController()
        {
            //TODO: laat op scherm zien welke wave het is

            bool[] finishedWaves = {false, false, false};
            yield return new WaitForSeconds(1);
            
            Task spawnDefaultEnemies = new Task(WaveComponent(1, _enemyController.SpawnEnemies(), 20));
            
            spawnDefaultEnemies.Finished += delegate(bool manual)
            {
                finishedWaves[0] = true;
            };

            yield return new WaitUntil(() => finishedWaves[0]);
        }
        
        private IEnumerator WaveComponent(int numberOfCouroutines, IEnumerator function, int waveLengthInSeconds)
        {
            List<Coroutine> coroutines = new List<Coroutine>();
            
            for (int i = 0; i < numberOfCouroutines; i++)
            {
                Coroutine thisCoroutine = StartCoroutine(function);
                coroutines.Add(thisCoroutine);
            }
            
            yield return new WaitForSeconds(waveLengthInSeconds);
            
            coroutines.ForEach(delegate(Coroutine coroutine)
            {
                StopCoroutine(coroutine);
            });

            yield return null;
        }
    }
}
