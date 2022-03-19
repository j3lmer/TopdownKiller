using TMPro;
using UnityEngine;

namespace GameScene
{
    public class ScoreTracker : MonoBehaviour
    {
        [SerializeField]
        public TMP_Text scoreText;
        
        public void UpdateScore(int score)
        {
            scoreText.SetText($"Score: {score}");
        }
    }
}