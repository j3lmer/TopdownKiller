using System.Collections;
using GameScene.Trackers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene.Controllers
{
    [
        RequireComponent(typeof(PowerupController)),
        RequireComponent(typeof(WaveTracker)),
        RequireComponent(typeof(GameControllerHelper))
    ]
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        private GameObject _player;
        private WaveTracker _tracker;
        private PowerupController _powerupController;
        private GameControllerHelper _gameControllerHelper;

        private int _currentWaveBig;
        private int _currentWaveSmall;
        private bool _hasCompleted;

        private Task _powerupSpawner;

        public int GetCurrentWaveBig()
        {
            return _currentWaveBig;
        }

        public void SetCurrentWaveBig(int cwb)
        {
            _currentWaveBig = cwb;
            _tracker.UpdateWaveText(cwb, GetCurrentWaveSmall());
        }

        public int GetCurrentWaveSmall()
        {
            return _currentWaveSmall;
        }

        public void SetCurrentWaveSmall(int cws)
        {
            _currentWaveSmall = cws;
            _tracker.UpdateWaveText(GetCurrentWaveBig(), cws);
        }

        private bool GetHasCompleted()
        {
            return _hasCompleted;
        }

        public void SetHasCompleted(bool hasCompleted)
        {
            _hasCompleted = hasCompleted;
            if (_hasCompleted)
            {
                SceneManager.LoadScene(2);
            }
        }

        private void Awake()
        {
            Application.targetFrameRate = 144;
            _gameControllerHelper = GetComponent<GameControllerHelper>();
            _tracker = GetComponent<WaveTracker>();
            _powerupController = GetComponent<PowerupController>();
            _player = MakePlayer();

            _tracker.UpdateWaveText(0, 0);
            StartCoroutine(AwakeWaveMaster());
            _powerupSpawner = new Task(_powerupController.SpawnPowerups());
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

        private IEnumerator AwakeWaveMaster()
        {
            Health player = _player.GetComponent<Health>();
            int index = 0;

            yield return new WaitForSecondsRealtime(3);
            while (player.GetAlive())
            {
                index++;
                bool bigWaveFinished = false;
                Task bigwave = new Task(BigWave(index));
                bigwave.Finished += delegate(bool manual) { bigWaveFinished = true; };
                yield return new WaitUntil(() => bigWaveFinished);
                yield return new WaitForSeconds(5);
            }
        }

        private IEnumerator BigWave(int index)
        {
            bool smallWaveFinished = false;
            Task smallWaveRound = new Task(SmallWaveRound(5, index));
            smallWaveRound.Finished += delegate(bool manual) { smallWaveFinished = true; };

            yield return new WaitUntil(() => smallWaveFinished);

            SetCurrentWaveSmall(0);
            SetCurrentWaveBig(GetCurrentWaveBig() + 1);
        }

        private IEnumerator SmallWaveRound(int numberOfSmallWaves, int index)
        {
            for (var i = 0; i < numberOfSmallWaves - 1; i++)
            {
                _gameControllerHelper.SmallWave(index, i);
                yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
                yield return new WaitForSeconds(5);
                SetCurrentWaveSmall(GetCurrentWaveSmall() + 1);
            }

            _powerupSpawner.Stop();
            _gameControllerHelper.StartBossWave(GetCurrentWaveBig() + 1);
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
            _powerupSpawner.Start();
            yield return new WaitForSeconds(8);
        }
    }
}