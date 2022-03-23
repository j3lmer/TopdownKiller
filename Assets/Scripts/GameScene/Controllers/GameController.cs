using System.Collections;
using System.Collections.Generic;
using GameScene.Trackers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene.Controllers
{
    [
        RequireComponent(typeof(EnemyController)),
        RequireComponent(typeof(PowerupController)),
        RequireComponent(typeof(WaveTracker))
    ]
    public class GameController : MonoBehaviour
    {
        //TODO: differentieer tussen totale waves en waves binnen waves.

        [SerializeField] private GameObject playerPrefab;

        private GameObject _player;
        private Health _playerHealth;
        private EnemyController _enemyController;
        private WaveTracker _tracker;
        private Dictionary<string, IEnumerator> _enemySpawnerFunctions = new Dictionary<string, IEnumerator>();

        private int _currentWave;
        private int _currentWaveLocal;
        private int _totalWaves = 3;
        private int _localWaves = 3;
        private bool _hasCompleted = false;

        public int[] GetCurrentWave()
        {
            return new int[] {_currentWave, _currentWaveLocal};
        }

        public void SetCurrentWave(int cw, int cwl)
        {
            _currentWave = cw;
            _currentWaveLocal = cwl;
            _tracker.UpdateWaveText(cw, cwl);
        }

        public int GetTotalWaves()
        {
            return _totalWaves;
        }

        public void SetTotalWaves(int tw)
        {
            _totalWaves = tw;
        }
        
        public int GetLocalWaves()
        {
            return _localWaves;
        }

        public void SetLocalWaves(int tw)
        {
            _localWaves = tw;
        }

        private bool GetHasCompleted()
        {
            return _hasCompleted;
        }
        

        private void SetHasCompleted(bool hasCompleted)
        {
            _hasCompleted = hasCompleted;
            if (_hasCompleted)
            {
                SceneManager.LoadScene(2);
            }
        }

        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
            _tracker = GetComponent<WaveTracker>();
            _player = MakePlayer();
            _playerHealth = _player.GetComponent<Health>();

            SetupSpawnerFunctionsList();
            _tracker.UpdateWaveText(0, 0);
            StartCoroutine(WaveController());
        }

        private void Update()
        {
            if (!_playerHealth.GetAlive())
            {
                SetHasCompleted(true);
            }
        }

        private void SetupSpawnerFunctionsList()
        {
            _enemySpawnerFunctions.Add("default", _enemyController.SpawnEnemies());
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
            /* TODO: Maak lijst aan Ienumerator functies die verschillende soorten enemies spawnen (net als spawnDefaultEnemies).
             Loop een paar keer, als index deelbaar is door X, spawn dan andere soort enemies, spawn soms random enemies.
            */
            SetTotalWaves(Random.Range(3, 10));

            for (int i = 0; i < GetTotalWaves(); i++)
            {
                bool finished = false;
                
                SetLocalWaves(Random.Range(1, 6));
                
                Task task = new Task(StartWaves(_enemySpawnerFunctions["default"], 20));

                task.Finished += delegate(bool manual) { finished = true; };

                yield return new WaitUntil(() => finished);

                int[] currentWave = GetCurrentWave();
                SetCurrentWave(currentWave[0] + 1, 0);
            }
            
            SetHasCompleted(true);
        }

        // Sets total amount of waves, starts them, gives some extra time between waves
        private IEnumerator StartWaves(IEnumerator waveType, int waveLengthInSeconds)
        {
            for (int i = 0; i < GetLocalWaves(); i++)
            {
                yield return new WaitForSeconds(1);
                // Debug.Log("wavetask started");

                StartTask(i, waveType, waveLengthInSeconds);

                var i1 = i;
                yield return new WaitUntil(() => GetCurrentWave()[1] == i1 + 1);

                // Debug.Log("wavetask finished");

                yield return new WaitForSeconds(5);
            }
        }


        // Starts a wave and listens for it to finish, updates the current wave index when its finished
        private void StartTask(int index, IEnumerator waveType, int waveLengthInSeconds)
        {
            Task spawnDefaultEnemies = new Task(WaveComponent(index + 1, waveType, waveLengthInSeconds));
            spawnDefaultEnemies.Finished += delegate(bool manual) { SetCurrentWave(GetCurrentWave()[0], index + 1); };
        }

        // Component for starting given coroutines
        private IEnumerator WaveComponent(int numberOfCouroutines, IEnumerator function, int waveLengthInSeconds)
        {
            List<Coroutine> coroutines = new List<Coroutine>();

            for (int i = 0; i < numberOfCouroutines; i++) coroutines.Add(StartCoroutine(function));
            
            yield return new WaitForSeconds(waveLengthInSeconds);

            StopCoroutines(coroutines);
            
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            yield return null;
        }

        private void StopCoroutines(List<Coroutine> coroutines)
        {
            coroutines.ForEach(delegate(Coroutine coroutine) { StopCoroutine(coroutine); });
        }
    }
}