using System.IO;
using FinalScreen.Controllers;
using UnityEngine;

namespace FinalScreen
{
    [RequireComponent(typeof(HighscoreManagerHelper))]
    public class HighscoreManager : MonoBehaviour
    {
        private int localScore = 0;
        private string localPlayerName = "";
        private HighscorePlayers hsPlayers = new HighscorePlayers();
        private HighscorePlayerData hsData = new HighscorePlayerData();

        /*
         * laad spelers
         * check of er spelers zijn, maak nieuwe en return die lijst, sla op
         * check of er in de spelers al een speler is met dezelfde naam, update die als hij een hogere score heeft, sla op
         * maak een nieuwe speler aan
         */
        
        private void AddOrUpdateHighscore(string playerName, int score)
        {
            HighscoreManagerHelper helper = GetComponent<HighscoreManagerHelper>();
            hsPlayers = helper.ConvertRawTextToPlayers(helper.GetStorageFileRaw());
            localScore = score;
            localPlayerName = playerName;

            helper.SaveToFile( UpdateList());
        }
        
        private string UpdateList()
        {
            //Create new HighscorePlayerData if object is empty
            if (hsData == null) return CreateAndConvertNewPlayer();

            int index = hsPlayers.HighscorePlayerData.FindIndex(p => p.name == localPlayerName);
            HighscorePlayerData player = hsPlayers.HighscorePlayerData[index];
            
            if (player == null) return CreateAndConvertNewPlayer();
            //update player score, en return hele lijst
            player.score = localScore > player.score ? localScore : player.score;
            hsPlayers.HighscorePlayerData[index] = player;

            return ConvertToString();
        }

        private string ConvertToString()
        {
            return JsonUtility.ToJson(hsPlayers);
        }

        private string CreateAndConvertNewPlayer()
        {
            HighscorePlayerData newPlayerData = CreateNewPlayerData();
            hsPlayers.HighscorePlayerData.Add(newPlayerData);
            return ConvertToString();
        }

        private HighscorePlayerData CreateNewPlayerData()
        {
            HighscorePlayerData newPlayerData = new HighscorePlayerData();
            newPlayerData.name = localPlayerName;
            newPlayerData.score = localScore;
            return newPlayerData;
        }
    }
}
