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
            _currentWave = cw;
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
        
        // Master controller which states how many waves shall be started, what kind of enemies they will spawn and how long each wave will last
        private IEnumerator WaveController()
        {
            // TODO: Maak lijst aan Ienumerator functies die verschillende soorten enemies spawnen (net als spawnDefaultEnemies). Loop een paar keer, als index deelbaar is door X, spawn dan andere soort enemies
            bool finished = false;
            IEnumerator spawnDefaultEnemies = _enemyController.SpawnEnemies();

            Debug.Log("wavecontroller started");
            Task task = new Task(StartWaves(3, spawnDefaultEnemies, 20));
            
            task.Finished += delegate(bool manual)
            {
                finished = true;
            };

            yield return new WaitUntil(() => finished);
            Debug.Log("wavecontroller finished");
            finished = false;
        }

        // Sets total amount of waves, starts them, gives some extra time between waves
        private IEnumerator StartWaves(int totalWaves, IEnumerator waveType, int waveLength)
        {
            SetTotalWaves(totalWaves);
            
            for (int i = 0; i < _totalWaves; i++)
            {
                yield return new WaitForSeconds(1);
                Debug.Log("wavetask started");


                StartTask(i, waveType, waveLength);
          
                var i1 = i;
                yield return new WaitUntil(() => GetCurrentWave() == i1 + 1);
                
                Debug.Log("wavetask finished");

                yield return new WaitForSeconds(5);
            }
        }


        // Starts a wave and listens for it to finish, updates the current wave index when its finished
        private void StartTask(int index, IEnumerator waveType, int waveLengthInSeconds)
        {
            Task spawnDefaultEnemies = new Task(WaveComponent(index + 1, waveType, waveLengthInSeconds));
            spawnDefaultEnemies.Finished += delegate(bool manual)
            {
                SetCurrentWave(index + 1);
            };
        }
        
        // Component for starting given coroutines
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
