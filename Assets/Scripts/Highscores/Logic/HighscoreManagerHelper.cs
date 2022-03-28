using System;
using System.Collections.Generic;
using System.IO;
using Highscores.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Highscores.Logic
{
    public class HighscoreManagerHelper : MonoBehaviour
    {
        public string GetStorageFileRaw()
        {
            Debug.Log(Application.persistentDataPath + "/json/Highscores.json");
            if (!Directory.Exists(Application.persistentDataPath + "/json")) Directory.CreateDirectory(Application.persistentDataPath + "/json");
            
            if (!File.Exists(Application.persistentDataPath + "/json/Highscores.json"))
            {
                var fs = new FileStream(Application.persistentDataPath + "/json/Highscores.json", FileMode.Create);
                fs.Dispose();
            }

            return File.ReadAllText(Application.persistentDataPath + "/json/Highscores.json");
        }

        public List<HighscorePlayerData> ConvertRawTextToPlayers(string rawData)
        {
            return JsonConvert.DeserializeObject<List<HighscorePlayerData>>(rawData);
        }

        public void SaveToFile(string json)
        {
            File.WriteAllText(Application.persistentDataPath + "/json/Highscores.json", json);
        }
    }
}