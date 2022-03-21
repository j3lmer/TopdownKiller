using GameScene.Controllers;
using TMPro;
using UnityEngine;

namespace GameScene.Trackers
{
    [RequireComponent(typeof(GameController))]
    public class WaveTracker : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _waveText;

        public void UpdateWaveText(int waveNumber)
        {
            if (waveNumber > GetComponent<GameController>().GetTotalWaves()) return;
            _waveText.SetText($"Wave {waveNumber +1}");
        }
    }
}
