using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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

        public List<HighscorePlayerData> ConvertRawTextToPlayers(string rawData)
        {
            return JsonConvert.DeserializeObject<List<HighscorePlayerData>>(rawData);
        }

        public void SaveToFile(string json)
        {
            File.WriteAllText(_highscoreStorageFile, json);
        }
    }
}