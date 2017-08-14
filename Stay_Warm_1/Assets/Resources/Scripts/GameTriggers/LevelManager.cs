using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Call Platform Manager to build First Platforms
        PlatformManager.Instance.Puzzle0(new Vector3(-49.5f, 3.3f,0));

        //Puzzle0(GameObject.Find("PlatformWhiteSprite").transform.position + new Vector3(6f, -1.45f, 0));
        //Puzzle1(GameObject.Find("lightpoleBridge").transform.position + new Vector3(4f, 1.5f, 0));


        ////Warming Elements Manager
        WarmingElementManager.Instance.Puzzle0(new Vector3(-49.5f, 3.3f, 0));

        //PushableElements 
        //WindCreation
        //Area Effects
    }

    // Update is called once per frame
    void Update () {
		//Start Time of 15 minutes if still in warming element
        //Activate Chaser
        //Check if Next Level Reached

	}

    /// <summary>
    /// Triggered when Warming Element Manager 
    /// </summary>
    public void NextArea()
    {
        //Paralyze Player
        //Move Main Ground Forward 
        //Destroy 
        //Instantiate Block => Area Effect "I can't go back" 
    }


}
