using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    #region Singleton instance
    public static MenuManager Instance { private set; get; }
    #endregion

    private GameObject MainMenuSystem;
    private GameObject InstructionBox;

    // Use this for initialization
    void Start () {
        MenuManager.Instance = this;
        MainMenuSystem = GameObject.Find("MenuSystem");
        InstructionBox = MainMenuSystem.transform.Find("GoofyPlaceHolderUI").transform.Find("InstructionsPanel").gameObject;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OpenCuteBox(string message)
    {
        InstructionBox = MainMenuSystem.transform.Find("GoofyPlaceHolderUI").transform.Find("InstructionsPanel").gameObject;
        InstructionBox.transform.Find("Text").gameObject.GetComponent<Text>().text = message;
        InstructionBox.SetActive(true);
        Invoke("CloseInstructionBox", 5f);
    }

    private void CloseInstructionBox()
    {
        InstructionBox.SetActive(false);
    }
}
