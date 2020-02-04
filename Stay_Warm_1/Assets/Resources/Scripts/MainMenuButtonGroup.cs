using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonGroup : MonoBehaviour {
    public IntVariable StageIndex;

    public void NewGameStayWarm()
    {
        int sceneIndex = 1;
        StageIndex.Value = -1;
        StartCoroutine(LoadScene(sceneIndex));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        yield return SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void LoadGameStayWarm(int stageNum)
    {
        StageIndex.Value = stageNum;
        int sceneIndex = 1;
        StartCoroutine(LoadScene(sceneIndex));
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
