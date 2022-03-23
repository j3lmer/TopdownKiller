using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	[RequireComponent(typeof(UIHelper))]
	public class MenuController : MonoBehaviour
	{
		[SerializeField] private Button[] buttons;

		private UIHelper _uiHelper;
		private GameObject _canvas;
		
		private void Awake()
		{
			_canvas = GameObject.Find("Canvas");
			_uiHelper = GetComponent<UIHelper>();
			SetupButtons();
		}

		private void SetupButtons()
		{
			_uiHelper.SetButtonListeners(new List<Action>
				{
					StartGame,
					ShowExplanation,
					ShowHighscores,
					Quit
				}, buttons
			);
		}

		private void StartGame()
		{
			SceneManager.LoadScene(1);
		}

		private void ShowExplanation()
		{
			Transform menuTransform = _canvas.transform.Find("MainMenu");
			_uiHelper.Hospital(menuTransform, false);
			//re-enable andere pre-made components
		}

		private void ShowHighscores()
		{
			Transform menuTransform = _canvas.transform.Find("MainMenu");
			_uiHelper.Hospital(menuTransform, false);
			//re-enable andere pre-made components
		}
		
		private void Quit()
		{
			Application.Quit();
		}
	}
}