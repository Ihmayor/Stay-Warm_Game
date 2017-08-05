using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class handles opening/closing menu systems. It does not set any messages only UI.
/// </summary>
public class MenuManager : MonoBehaviour
{

    /// <summary>
    /// Singleton Instance to trigger menu opening elsewhere
    /// </summary>
    public static MenuManager Instance { private set; get; }

    /// <summary>
    /// Global GameOver will be set here
    /// </summary>
    public bool GameOver { private set; get; }

    /// <summary>
    /// Menu objects
    /// </summary>
    private GameObject MainMenuSystem;
    private GameObject InteractionBox;
    private GameObject GameOverText;
    private GameObject WinMenu;
    private GameObject ThoughtBox;
    private GameObject Status;

    /// <summary>
    /// UI Helper variables
    /// </summary>
    private float MenuSlideLength;
    private AudioSource[] AudioSources;

    // Use this for initialization
    void Start()
    {
        MenuManager.Instance = this;
        MainMenuSystem = GameObject.Find("MenuSystem");

        Status = MainMenuSystem.transform.Find("StatusCanvas").transform.Find("Status").gameObject;
        GameOverText = MainMenuSystem.transform.Find("GameOver").gameObject;
        WinMenu = MainMenuSystem.transform.Find("WinMenu").gameObject;
        ThoughtBox = MainMenuSystem.transform.Find("Thoughts").gameObject;

        //InstructionBox = MainMenuSystem.transform.Find("GoofyPlaceHolderUI").transform.Find("InstructionsPanel").gameObject;
        // AudioSources = InstructionBox.GetComponents<AudioSource>();
        Screen.SetResolution(965, 600, false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Restart current scene/Replay Current Level
    /// </summary>
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Show Game Over Menu and disable user input
    /// </summary>
    public void ShowGameOver()
    {
        GameOverText.SetActive(true);
        GameOver = true;
    }

    /// <summary>
    /// Show Win Menu and Disable user input. TODO: Load new scene or animation of win scene.
    /// </summary>
    public void ShowWin()
    {
        WinMenu.SetActive(true);
        GameOver = true;
    }

    /// <summary>
    /// Changes Contents of thought box. 
    /// </summary>
    /// <param name="characterName">Name of Character with the thought</param>
    /// <param name="thoughtMessage">Thought Content</param>
    public void SetThought(string characterName, string thoughtMessage)
    {
        ThoughtBox.transform.Find("CharacterName").GetComponent<Text>().text = characterName;
        ThoughtBox.transform.Find("ThoughtText").GetComponent<Text>().text = thoughtMessage;
    }

    /// <summary>
    /// Goofy Instruction box from First Iteration. TODO: Re-purpose for _real_ menu system.
    /// </summary>
    /// <param name="message"></param>
    public void OpenInteractionMenu(string message)
    {
        InteractionBox = MainMenuSystem.transform.Find("PickupMenu").transform.Find("PickupPanel").gameObject;
        var InteractionBoxText = InteractionBox.transform.Find("PickupText").gameObject;
        InteractionBoxText.GetComponent<Text>().text = message;
        MenuSlideLength = 30f;
        StartCoroutine("SlideMenuIn");
    }

    IEnumerator SlideMenuIn()
    {
        InteractionBox.SetActive(true);
        for (float i = 0; i <= MenuSlideLength; i++)
        {
            yield return new WaitForSeconds(0.0001f);
            InteractionBox.transform.position -= new Vector3(20f,0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(3.4f);
        StartCoroutine("SlideMenuOut");
    }

    IEnumerator SlideMenuOut()
    {
        for (float i = 0; i <= MenuSlideLength; i++)
        {
            yield return new WaitForSeconds(0.0001f);
            InteractionBox.transform.position += new Vector3(20f, 0, 0);
            yield return null;
        }
        InteractionBox.SetActive(false);
    }

    internal void RemoveMatchAt(int matchCount)
    {
        GameObject matches = Status.transform.Find("Matches").gameObject;
        matches.transform.Find("Match" + matchCount).gameObject.SetActive(false);
    }
}
