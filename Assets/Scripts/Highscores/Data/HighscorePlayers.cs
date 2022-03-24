using System;
using System.Collections.Generic;

namespace Highscores.Data
{
    [Serializable]
    public class HighscorePlayers
    {
        public List<HighscorePlayerData> highscorePlayerData = new List<HighscorePlayerData>();
    }
}