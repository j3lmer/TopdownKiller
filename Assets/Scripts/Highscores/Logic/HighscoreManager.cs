using System.Collections.Generic;
using Highscores.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Highscores.Logic
{
    [RequireComponent(typeof(HighscoreManagerHelper))]
    public class HighscoreManager : MonoBehaviour
    {
        private HighscoreManagerHelper _helper;
        private HighscorePlayers _hsPlayers;
        private string _localPlayerName = "";
        private int _localScore;
        private void Awake()
        {
            _helper = GetComponent<HighscoreManagerHelper>();
            _hsPlayers = new HighscorePlayers();
            _hsPlayers.highscorePlayerData = GetHighscores();
        }

        public void AddOrUpdateHighscore(string playerName, int score)
        {
            _localScore = score;
            _localPlayerName = playerName;
            _helper.SaveToFile(UpdateList());
            
            _localScore = 0;
            _localPlayerName = "";
        }

        private string UpdateList()
        {
            if (_hsPlayers == null || _hsPlayers.highscorePlayerData == null || _hsPlayers.highscorePlayerData.Count <= 0) return CreateAndConvertNewPlayer();
            
            int index = _hsPlayers.highscorePlayerData.FindIndex(p => p.name == _localPlayerName);
            if (index == -1) return CreateAndConvertNewPlayer();

            HighscorePlayerData player = _hsPlayers.highscorePlayerData[index];
            if (player == null) return CreateAndConvertNewPlayer();

            player.score = _localScore > player.score ? _localScore : player.score;
            _hsPlayers.highscorePlayerData[index] = player;

            return ConvertToString(_hsPlayers.highscorePlayerData);
        }

        private string CreateAndConvertNewPlayer()
        {
            if (_hsPlayers == null) _hsPlayers = new HighscorePlayers();
            if (_hsPlayers.highscorePlayerData == null) _hsPlayers.highscorePlayerData = new List<HighscorePlayerData>();
            HighscorePlayerData newPlayerData = CreateNewPlayerData();
            _hsPlayers.highscorePlayerData.Add(newPlayerData);
            return ConvertToString(_hsPlayers.highscorePlayerData);
        }

        private HighscorePlayerData CreateNewPlayerData()
        {
            HighscorePlayerData newPlayerData = new HighscorePlayerData();
            newPlayerData.name = _localPlayerName;
            newPlayerData.score = _localScore;
            return newPlayerData;
        }

        private string ConvertToString(List<HighscorePlayerData> hsData)
        {
            return JsonConvert.SerializeObject(hsData, Formatting.Indented);
        }

        //let er op dat deze nooit tegelijk met sethighscores word gecalled
        public List<HighscorePlayerData> GetHighscores()
        {
            return _helper.ConvertRawTextToPlayers(_helper.GetStorageFileRaw());
        }
    }
}