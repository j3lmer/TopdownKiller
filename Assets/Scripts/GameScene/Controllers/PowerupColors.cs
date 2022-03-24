using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameScene.Controllers
{
    public class PowerupColors
    {
        public static readonly Dictionary<int, Color> Colors = new Dictionary<int, Color>
        {
            {0, Color.magenta},
            {1, Color.blue},
            {2, Color.green},
            {3,  Color.green},
            {4, Color.cyan},
            {5,  Color.white},
            {6, new Color32(94, 136, 220, 255)},
            {7, new Color32(152, 42, 152, 255)},
        };
    }
}