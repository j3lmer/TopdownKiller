using System;
using UnityEngine;

namespace GameScene.Player
{
    public class Score : MonoBehaviour
    {
        private int _points;
        private ScoreTracker _tracker;

        private void Awake()
        {
            _tracker = GameObject.Find("GameController").GetComponent<ScoreTracker>();
            SetScore(0);
        }

        public int GetScore()
        {
            return _points;
        }
        
        public void SetScore(int score)
        {
            _points = score;
            _tracker.UpdateScore(score);
        }

        public void UpdateScore(int score)
        {
            _points += score;
            _tracker.UpdateScore(GetScore());
        }
    }
}
