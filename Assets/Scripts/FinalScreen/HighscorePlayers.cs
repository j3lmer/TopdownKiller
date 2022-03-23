using System;
using System.Collections.Generic;
using UnityEngine;

namespace FinalScreen
{
    [Serializable]
    public class HighscorePlayers
    {
        [SerializeField] public List<HighscorePlayerData> highscorePlayerData = new List<HighscorePlayerData>();
    }
}