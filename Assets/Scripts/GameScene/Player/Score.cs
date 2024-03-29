using System;
using GameScene.Trackers;
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
            UpdatePlayerPrefScore();
        }

        public void UpdateScore(int score)
        {
            _points += score;
            _tracker.UpdateScore(GetScore());
            UpdatePlayerPrefScore();
        }

        private void UpdatePlayerPrefScore()
        {
            PlayerPrefs.SetInt("latestPlayerScore", GetScore());
        }
    }
}