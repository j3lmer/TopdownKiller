using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace GameScene
{
    [
        RequireComponent(typeof(EnemyController)), 
        RequireComponent(typeof(PowerupController)), 
        RequireComponent(typeof(WaveTracker))
    ]
    public class GameController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject playerPrefab;

        private GameObject _player;
        private EnemyController _enemyController;
        private WaveTracker _tracker;
        
        private int _currentWave = 0;
        private int _totalWaves = 3;

        public int GetCurrentWave()
        {
            return _currentWave;
        }
        
        public void SetCurrentWave(int cw)
        {
            _currentWave =  cw;
            _tracker.UpdateWaveText(cw);
        }
        
        public int GetTotalWaves()
        {
            return _totalWaves;
        }
        
        public void SetTotalWaves(int tw)
        {
            _totalWaves = tw;
        }
      
        
        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
            _tracker         = GetComponent<WaveTracker>();
            _player          = MakePlayer();

            _tracker.UpdateWaveText(0);
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

            for (int i = 0; i < _totalWaves; i++)
            {
                yield return new WaitForSeconds(1);
          
                var i1 = i;
                yield return new WaitUntil(() => GetCurrentWave() == i1 + 1);
                
                yield return new WaitForSeconds(5);
            }
        }


        private Task StartTask(int index)
        {
            Task spawnDefaultEnemies = new Task(WaveComponent(index + 1, _enemyController.SpawnEnemies(), 20));
            spawnDefaultEnemies.Finished += delegate(bool manual)
            {
                SetCurrentWave(index + 1);
            };

            return spawnDefaultEnemies;
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
            
            StopCoroutines(coroutines);

            yield return null;
        } 
        
        private void StopCoroutines(List<Coroutine> coroutines)
        {
            coroutines.ForEach(delegate(Coroutine coroutine)
            {
                StopCoroutine(coroutine);
            });
        }
    }
}
