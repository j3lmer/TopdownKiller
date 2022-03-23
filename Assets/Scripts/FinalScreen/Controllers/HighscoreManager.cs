using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace FinalScreen.Controllers
{
    [RequireComponent(typeof(HighscoreManagerHelper))]
    public class HighscoreManager : MonoBehaviour
    {
        private HighscoreManagerHelper _helper;
        private HighscorePlayers _hsPlayers;
        private string _localPlayerName = "";
        private int _localScore;
        private bool _busy;

        private void Awake()
        {
            _helper = GetComponent<HighscoreManagerHelper>();
            _hsPlayers = new HighscorePlayers();
        }

        /*
         * laad spelers
         * check of er spelers zijn, maak nieuwe en return die lijst, sla op
         * check of er in de spelers al een speler is met dezelfde naam, update die als hij een hogere score heeft, sla op
         * maak een nieuwe speler aan
         */

        public void AddOrUpdateHighscore(string playerName, int score)
        {
            _busy = true;
            _hsPlayers.highscorePlayerData = _helper.ConvertRawTextToPlayers(_helper.GetStorageFileRaw());

            _localScore = score;
            _localPlayerName = playerName;
            _helper.SaveToFile(UpdateList());
            
            _localScore = 0;
            _localPlayerName = "";
            _busy = false;
        }

        private string UpdateList()
        {
            //Create new HighscorePlayerData if object is empty
            if (_hsPlayers == null) return CreateAndConvertNewPlayer();
            int index = _hsPlayers.highscorePlayerData.FindIndex(p => p.name == _localPlayerName);

            if (index == -1) return CreateAndConvertNewPlayer();

            HighscorePlayerData player = _hsPlayers.highscorePlayerData[index];
            if (player == null) return CreateAndConvertNewPlayer();

            //update player score, en return hele lijst
            player.score = _localScore > player.score ? _localScore : player.score;
            _hsPlayers.highscorePlayerData[index] = player;

            return ConvertToString(_hsPlayers.highscorePlayerData);
        }

        private string CreateAndConvertNewPlayer()
        {
            if (_hsPlayers == null) _hsPlayers = new HighscorePlayers();
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