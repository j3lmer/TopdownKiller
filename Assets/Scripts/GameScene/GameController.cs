using System;
using UnityEngine;

namespace GameScene
{
    public class GameController : MonoBehaviour
    {
        public GameObject enemyPrefab;

        private void Start()
        {
            Instantiate(enemyPrefab);
        }
    }
}
