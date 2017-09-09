using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGameStayWarm()
    {
        int sceneIndex = 1;
        StartCoroutine(LoadScene(sceneIndex));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        yield return SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void LoadGameStayWarm()
    {

    }


    public void NewGameFollow()
    {

    }


    public void OpenSettings(string SetVersion)
    {

    }

    public void CloseSettings(string SetVersion)
    {

    }

}
