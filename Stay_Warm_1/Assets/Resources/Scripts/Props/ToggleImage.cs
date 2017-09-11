using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour {
    public Sprite Image1;
    public Sprite Image2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Toggle()
    {
        if (gameObject.GetComponent<Image>().sprite == Image1)
        {
            gameObject.GetComponent<Image>().sprite = Image2;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Image1;
        }
        this.GetComponent<Animator>().ResetTrigger("Highlighted");
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        this.GetComponent<Animator>().SetTrigger("Normal");

    }


}
