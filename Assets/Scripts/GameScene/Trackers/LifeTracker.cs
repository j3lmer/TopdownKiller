using TMPro;
using UnityEngine;

namespace GameScene.Trackers
{
    public class LifeTracker : MonoBehaviour
    {
        [SerializeField]
        public TMP_Text lifeText;

        [SerializeField] 
        public TMP_Text healthText;

        public void UpdateLives(int lives)
        {
            lifeText.SetText($"Lives: {lives}");
        }

        public void UpdateHealth(int health)
        {
            healthText.SetText($"Health: {health}");
        }
    }
}
