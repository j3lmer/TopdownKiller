using System.IO;
using UnityEngine;

namespace FinalScreen.Controllers
{
    public class HighscoreManagerHelper : MonoBehaviour
    {
        private string _storagePath;
        private string _highscoreStorageFile;

        private void Awake()
        {
            _storagePath = $"{Application.dataPath}/json";
            _highscoreStorageFile = $"{Application.dataPath}/json/Highscores.json";
        }

        public string GetStorageFileRaw()
        {
            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);

            if (!File.Exists(_highscoreStorageFile))
            {
                var fs = new FileStream(_highscoreStorageFile, FileMode.Create);
                fs.Dispose();
            }

            return File.ReadAllText(_highscoreStorageFile);
        }

        public HighscorePlayers ConvertRawTextToPlayers(string rawData)
        {
            return JsonUtility.FromJson<HighscorePlayers>(rawData);
        }

        public void SaveToFile(string json)
        {
            File.WriteAllText(_highscoreStorageFile, json);
        }
    }
}