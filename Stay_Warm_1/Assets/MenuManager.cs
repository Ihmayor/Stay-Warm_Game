using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class handles opening/closing menu systems. It does not set any messages only UI.
/// </summary>
public class MenuManager : MonoBehaviour {

    #region Singleton instance
    public static MenuManager Instance { private set; get; }
    #endregion
    public bool GameOver { private set; get; }
    private GameObject MainMenuSystem;
    private GameObject InstructionBox;
    private GameObject GameOverText;

    private float MenuRatio;
    private AudioSource[] AudioSources;
    // Use this for initialization
    void Start () {
        MenuManager.Instance = this;
        MainMenuSystem = GameObject.Find("MenuSystem");
        InstructionBox = MainMenuSystem.transform.Find("GoofyPlaceHolderUI").transform.Find("InstructionsPanel").gameObject;
        GameOverText = MainMenuSystem.transform.Find("GameOver").gameObject;
        AudioSources = InstructionBox.GetComponents<AudioSource>();
        Screen.SetResolution(965, 600, false);
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowGameOver()
    {
        GameOverText.SetActive(true);
        GameOver = true;
    }

    public void OpenCuteBox(string message)
    {
        InstructionBox = MainMenuSystem.transform.Find("GoofyPlaceHolderUI").transform.Find("InstructionsPanel").gameObject;
        InstructionBox.transform.Find("Text").gameObject.GetComponent<Text>().text = message;
        InstructionBox.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        MenuRatio = 1 / 0.1f;
        StartCoroutine("BiggerMenu");
    }


    IEnumerator BiggerMenu()
   {
        InstructionBox.SetActive(true);
        AudioSources[0].Play();
        for (float i = 0; i <= MenuRatio; i++)
        {
            yield return new WaitForSeconds(0.01f);
            InstructionBox.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            yield return null;
        }
        StartCoroutine("CloseInstructionBox");
    }

    private IEnumerator CloseInstructionBox()
    {
        yield return new WaitForSeconds(3.5f);
        AudioSources[1].Play();
        for (float i = MenuRatio/2; i >= 0; i--)
        {
            yield return new WaitForSeconds(0.01f);
            InstructionBox.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            yield return null;
        }
        InstructionBox.SetActive(false);
    }
}
