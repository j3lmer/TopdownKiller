using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FinalScreen.Controllers
{
    [RequireComponent(typeof(HighscoreManager))]
    public class FinalScreenController : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text[] letters;
        [SerializeField] private Button[] buttons;

        private HighscoreManager _highscoreManager;

        private int[] letterIndexes = {0, 0, 0};
        private readonly char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWQXYZ0123456789!?".ToCharArray();
        private float _timer;
        private int _selected = 0;
        private int _score;
        private Color _defaultColor;

        public int GetScore()
        {
            return _score;
        }

        public void SetScore(int score)
        {
            _score = score;
            scoreText.SetText($"Score: {GetScore()}");
        }

        private void Awake()
        {
            _highscoreManager = GetComponent<HighscoreManager>();
            _defaultColor = letters[_selected].color;
            
            SetButtonListeners();
            titleText.SetText("hoi");
            SetScore(PlayerPrefs.GetInt("latestPlayerScore"));
            PlayerPrefs.SetInt("latestPlayerScore", 0);
        }
        
        private void Update()
        {
            BlinkLetter();
        }

        private void BlinkLetter()
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.5)
            {
                Color color = letters[_selected].color;
                color.a = Mathf.Clamp(1, 0, 1);
                letters[_selected].color = color;
            }

            if (_timer >= 1)
            {
                Color color = letters[_selected].color;
                color.a = Mathf.Clamp(0.5f, 0, 1);
                letters[_selected].color = color;
                _timer = 0;
            }
        }

        private void SetButtonListeners()
        {
            List<Action> functions = new List<Action>
            {
                NextLetter,
                PrevLetter,
                NextLetterIndex,
                Submit
            };
            
            for (int i = 0; i < buttons.Length; i++)
            {
                var i1 = i;
                buttons[i].onClick.AddListener(delegate { functions[i1](); });
            }
        }

        private void NextLetter()
        {
            Debug.Log("clicked");
            letterIndexes[_selected] = letterIndexes[_selected] >= 38 ? 0 : letterIndexes[_selected] + 1;
            letters[_selected].text = _alphabet[letterIndexes[_selected]].ToString();
        }

        private void PrevLetter()
        {
            letterIndexes[_selected] = letterIndexes[_selected] <= 0 ? 38 : letterIndexes[_selected] - 1;
            letters[_selected].text = _alphabet[letterIndexes[_selected]].ToString();
        }

        private void NextLetterIndex()
        {
            letters[_selected].color = _defaultColor;
            _selected = _selected + 1 > letters.Length - 1 ? 0 : _selected + 1;
        }

        private void Submit()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TMP_Text letter in letters) sb.Append(letter.text);
            string playerName = sb.ToString();
            
            _highscoreManager.AddOrUpdateHighscore(playerName, GetScore());
        }
    }
}