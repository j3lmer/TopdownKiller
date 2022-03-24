using System.Collections.Generic;
using UnityEngine;

namespace GameScene.Powerup
{
    public class PowerupColors
    {
        public static readonly Dictionary<int, Color> Colors = new Dictionary<int, Color>
        {
            {(int) PowerupTypes.RemoveEnemies, Color.magenta},
            {(int) PowerupTypes.SpeedboostPlayer, Color.blue},
            {(int) PowerupTypes.HealthBoost, Color.green},
            {(int) PowerupTypes.Points,  Color.yellow},
            {(int) PowerupTypes.StopEnemies, Color.cyan},
            {(int) PowerupTypes.OneUp,  Color.white},
            {(int) PowerupTypes.QuickShot, new Color32(94, 136, 220, 255)},
            {(int) PowerupTypes.IncreaseBullets, new Color32(152, 42, 152, 255)},
        };
    }
}