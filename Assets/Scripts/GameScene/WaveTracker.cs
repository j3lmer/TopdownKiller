using TMPro;
using UnityEngine;

namespace GameScene
{
    public class WaveTracker : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _waveText;


        public void UpdateWaveText(int waveNumber)
        {
            _waveText.SetText($"Wave {waveNumber +1}");
        }
    
    }
}
