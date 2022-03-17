using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class MenuController : MonoBehaviour
	{
		//TODO: MAKE BACK BTN INSTEAD OF HOME

		//variables for accessing menus + canvas
		GameObject _menuObject;
		Canvas _canvas;
		List<GameObject> _menuList = new List<GameObject>();
		//end menu blocks

		private void Start()
		{
			/* 
		 * retrieves all menu gameobjects + canvas.
		 * then sets onclickListeners to all buttons inside.
		 */
			GetMenuObjects();
			if (Debug.isDebugBuild)
			{
				Debug.Log("This is the (latest) debug build!");
			}
		
		}
		void GetMenuObjects()
		{
			_menuObject = GameObject.Find("MenuObject");
			Debug.Log(_menuObject);

			_canvas = _menuObject.GetComponentInChildren<Canvas>();
			var canvasTransform = _canvas.transform;

			//loop through canvas children and if its part of the vertical layoutgroup, it must a menu gameobject. 
			//so then add it to the list, and set an onclicklistener for the specific button
			for (var i=0; i< canvasTransform.childCount; i++)
			{
				var thisObj = canvasTransform.GetChild(i);
			
				if (thisObj.GetComponent<VerticalLayoutGroup>())
				{
					var thisGameObj = thisObj.gameObject;
					_menuList.Add(thisGameObj);
					setOnClickListeners(thisGameObj);
				}
			}
		}

		void setOnClickListeners(GameObject menu) 
		{
			//menu is one of the children on canvas.
			var menuTransform = menu.transform;
		
			for(var i=0; i<menuTransform.childCount; i++)
			{
				Transform thisObj = menuTransform.GetChild(i);
				if(thisObj == null)
				{
					print("breaking");
					break;
				}
			
				//GameObject thisGameObject = thisObj.gameObject;
				Button thisBtn = thisObj.GetComponent<Button>();	

				//add onclicklistener to any non-untagged button
				if (thisBtn != null && !thisBtn.CompareTag("Untagged"))
				{
					thisBtn.onClick.AddListener(delegate { CheckBtnTag(thisBtn); });
				}			
			}		
		}

		void CheckBtnTag(Button thisBtn)
		{
			switch (thisBtn.tag)
			{
				case "START":
					SceneManager.LoadScene(1);
					break;

				case "EXPLANATION":
					ShowExplanation();
					break;

				case "QUIT":
					Exit();
					break;
			}
		}
		

		void ShowExplanation()
		{		
			
		}


		public void Exit()
		{
			Application.Quit();
		}

	}
}