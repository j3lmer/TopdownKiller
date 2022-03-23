using System;
using UnityEngine;

namespace FinalScreen
{
    [Serializable]
    public class HighscorePlayerData
    {
        [SerializeField] public string name;
        [SerializeField] public float score;
    }
}