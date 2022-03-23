using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHelper : MonoBehaviour
    {
        public void SetButtonListeners(List<Action>functions, Button[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
        	{
        		var i1 = i;
        		buttons[i].onClick.AddListener(delegate { functions[i1](); });
        	}
        }
        
        public void Hospital(Transform parent, bool value)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform thisChild = parent.GetChild(i);
                thisChild.gameObject.SetActive(value);
            }
        }
    }
}