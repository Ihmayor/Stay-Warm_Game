using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelButtonScript : MonoBehaviour {


    public bool Disabled = false;
	// Use this for initialization
	void Start () {
		//Respond to load save file or have code in here. 
        if (Disabled)
        {
            Color panelColor;
            ColorUtility.TryParseHtmlString("#323232", out panelColor);
            Color textColor;
            ColorUtility.TryParseHtmlString("#ABADAE", out textColor);

            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<Image>().color = panelColor;
            gameObject.transform.GetComponentInChildren<Text>().color = textColor;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
