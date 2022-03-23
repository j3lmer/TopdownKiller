using System.IO;
using FinalScreen.Controllers;
using UnityEngine;

namespace FinalScreen
{
    public class HighscoreManagerHelper : MonoBehaviour
    {
        public string GetStorageFileRaw()
        {
            if (!Directory.Exists($"{Application.dataPath}/json"))
            {
                Directory.CreateDirectory($"{Application.dataPath}/json");
            }

            if (!File.Exists($"{Application.dataPath}/json/Highscores.json"))
            {
                var fs = new FileStream($"{Application.dataPath}/json/Highscores.json", FileMode.Create);
                fs.Dispose();
            }

            return File.ReadAllText($"{Application.dataPath}/json/Highscores.json");
        }

        public HighscorePlayers ConvertRawTextToPlayers(string rawData)
        {
            return JsonUtility.FromJson<HighscorePlayers>(rawData);
        }

        public void SaveToFile(string json)
        {
            File.WriteAllText($"{Application.dataPath}/json/Highscores.json", json);
        }
    }
}
