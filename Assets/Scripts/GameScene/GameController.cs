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
        
        private bool[] finishedWaves = {false, false, false};

        public bool[] GetFinishedWaves()
        {
            return finishedWaves;
        }

        public bool GetFinishedWaveAtIndex(int index)
        {
            return finishedWaves[index];
        }

        public void SetFinishedWaves(int index, bool value)
        {
            finishedWaves[index] = value;
        }
        
        private void Awake()
        {
            _enemyController = gameObject.GetComponent<EnemyController>();
            _player          = MakePlayer();
            
            StartCoroutine(WaveController());
        }
        
        private GameObject MakePlayer() 
        {
            return Instantiate
                (
                    playerPrefab,
                    new Vector3(),
                    Quaternion.identity
                )
                .AddComponent<Player.Player>()
                .gameObject;
        }
        
        private IEnumerator WaveController()
        {
            //TODO: laat op scherm zien welke wave het is

            yield return new WaitForSeconds(1);
            
            Task spawnDefaultEnemies = new Task(WaveComponent(1, _enemyController.SpawnEnemies(), 20));
            
            spawnDefaultEnemies.Finished += delegate(bool manual)
            {
                SetFinishedWaves(0, true);
            };

            yield return new WaitUntil(() => GetFinishedWaveAtIndex(0));
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
