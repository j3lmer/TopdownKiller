using GameScene.Controllers;
using TMPro;
using UnityEngine;

namespace GameScene.Trackers
{
    [RequireComponent(typeof(GameController))]
    public class WaveTracker : MonoBehaviour
    {
        [SerializeField] private TMP_Text _waveText;

        public void UpdateWaveText(int bigWave, int smallWave)
        {
            _waveText.SetText($"Wave {bigWave + 1} : {smallWave + 1}");
        }
    }
}