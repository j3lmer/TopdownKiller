using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Highscores.Data;
using Highscores.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	[RequireComponent(typeof(UIHelper)), RequireComponent(typeof(HighscoreManager))]
	public class MenuController : MonoBehaviour
	{
		[SerializeField] private Button[] buttons;

		private UIHelper _uiHelper;
		private GameObject _canvas;
		
		private Transform _menuTransform;
		private Transform _highscores;
		private Transform _explanation;

		private void Awake()
		{
			_canvas = GameObject.Find("Canvas");
			_menuTransform = _canvas.transform.Find("MainMenu");
			_highscores = _canvas.transform.Find("Highscores");
			_explanation = _canvas.transform.Find("Explanation");
			_uiHelper = GetComponent<UIHelper>();
			_uiHelper.Hospital(_highscores, false);
			_uiHelper.Hospital(_explanation, false);
			SetupButtons();
		}

		private void SetupButtons()
		{
			_uiHelper.SetButtonListeners(new List<Action>
				{
					StartGame,
					ShowExplanation,
					ShowHighscores,
					Quit,
					Back,
					Back
				}, buttons
			);
		}

		private void StartGame()
		{
			SceneManager.LoadScene(1);
		}

		private void ShowExplanation()
		{
			_menuTransform = _canvas.transform.Find("MainMenu");
			_uiHelper.Hospital(_menuTransform, false);
			_uiHelper.Hospital(_explanation, true);
		}

		private void ShowHighscores()
		{
			float templateHeight = 10f;
			
			_uiHelper.Hospital(_menuTransform, false);
			_uiHelper.Hospital(_highscores, true);

			HighscoreManager manager = GetComponent<HighscoreManager>();
			GameObject highscoreCanvas = GameObject.Find("HighscoreCanvas");
			GameObject template = GameObject.Find("LeaderBoardEntryTemplate");

			List<HighscorePlayerData> sortedHighscores = manager.GetHighscores().OrderByDescending(o => o.score).ToList();
			for(int i = 0; i< sortedHighscores.Count; i++)
			{
				if (i >= 8) return;
				CreateLeaderboardEntry(template, highscoreCanvas, sortedHighscores, i, templateHeight);
			}
		}

		private void CreateLeaderboardEntry(
				GameObject template, 
				GameObject highscoreCanvas, 
				List<HighscorePlayerData> sortedHighscores, 
				int i, 
				float templateHeight
			)
		{
			GameObject newTemplate = Instantiate(template, highscoreCanvas.transform);
				
			int pos = i + 1;
			newTemplate.transform.GetChild(0).GetComponent<TMP_Text>().SetText(pos.ToString());
			newTemplate.transform.GetChild(1).GetComponent<TMP_Text>().SetText(sortedHighscores[i].name);
			newTemplate.transform.GetChild(2).GetComponent<TMP_Text>().SetText(sortedHighscores[i].score.ToString(CultureInfo.InvariantCulture));
				
			newTemplate.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-templateHeight * i) - 10f);
			HonorTop3(i, newTemplate);
		}

		private void HonorTop3(int i, GameObject template)
		{
			switch (i)
			{
				case 0:
					setColors(template, new Color32(255, 215, 0, 255));
					break;
				case 1:
					setColors(template, new Color32(192, 192, 192, 255));
					break;
				case 2:
					setColors(template, new Color32(205, 127, 50, 255));
					break;
			}
		}

		private void setColors(GameObject thisTemplate, Color32 color)
		{
			TMP_Text[] list = thisTemplate.GetComponentsInChildren<TMP_Text>();
			foreach (TMP_Text tmpText in list)
			{
				tmpText.color = color;
			}
		}

		private void Back()
		{ 
			_uiHelper.Hospital(_menuTransform, true);
			_uiHelper.Hospital(_highscores, false);
			_uiHelper.Hospital(_explanation, false);
		}
		
		private void Quit()
		{
			Application.Quit();
		}
	}
}