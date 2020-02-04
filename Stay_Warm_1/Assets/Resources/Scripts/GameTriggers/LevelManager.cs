using Assets.Resources.Scripts.GameTriggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    //Managers
    private PlatformManager PlatformManager;
    private WarmingElementManager WarmManager;
    private CoolingElementManager CoolManager;
    private PushableElementManager PushableManager;
    private List<PuzzleManager> Managers;
    private Queue<Action> LevelQueue;

    //Wall Boundary Object
    private GameObject Wall;

    //SignPost labelling start of puzzle
    private GameObject SignPost;

    //Instance of level manager
    public static LevelManager SingletonInstance { private set; get; }


    //Debugging Mode
    public BoolVariable IsDebuggingModeOn;

    //Loaded Level Variables
    public IntVariable LoadedStageIndex;
    public GameObject IntroScene;

    #region Init

    /// <summary>
    /// Init Level Manager
    /// </summary>
    void Start () {

        //Check that we have singleton instance of the level manager
        if (SingletonInstance == null)
            SingletonInstance = this;
        else
            throw new InvalidOperationException("Error! More than one level manager exists!");

        //Fetch Wall Boundary from scene. Moves forward as levels progress
        Wall = GameObject.Find("PlatformWhiteSprite");

        //Fetch SignPost from scene. Moves forward as levels progress.
        SignPost = GameObject.Find("SignPost");

        //Init Starting Position of Game
        Vector3 StartPosition = new Vector3(-45.5f, 3.3f, 0);

        //Init component managers and queue all level setup processes
        InitManagers(StartPosition);
        InitLevelQueue(StartPosition);

        //Load Stage via Index
        LoadingStage(LoadedStageIndex.Value);
    }

    /// <summary>
    /// Init component managers for platforms and elements
    /// </summary>
    /// <param name="StartPosition">Start position of the game</param>
    private void InitManagers(Vector3 StartPosition)
    {
        //Create Managers
        Managers = new List<PuzzleManager>();

        //Platform manager handles all step obstacles, moving platforms and spikes
        PlatformManager = new PlatformManager();
        Managers.Add(PlatformManager);

        //Warming Elements Manager Handles placement, colors,sounds
        WarmManager = new WarmingElementManager();
        Managers.Add(WarmManager);

        //Pushable Manager handles pushable element resources pool
        PushableManager = new PushableElementManager();
        Managers.Add(PushableManager);

        //Cooling Elements Manager handles cooling element resource pool
        CoolManager = new CoolingElementManager();
        Managers.Add(CoolManager);

  
    }

    /// <summary>
    /// Init and queue level set up processes 
    /// </summary>
    /// <param name="StartPosition">Start position of the game</param>
    private void InitLevelQueue(Vector3 StartPosition)
    {
        //Init Level Queue Setup
        LevelQueue = new Queue<Action>();

        int PuzzleNum = 0;
        float WallDistance = -3f;
        float SignPostDistance = 2f;
        float SignPostHeightAdjustment = 0.8f;

        //Queue Levels
        //Puzzle 0
        LevelQueue.Enqueue(() =>
        {
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle0(StartPosition);
            }
            PuzzleNum++;
        });

        //Puzzle 1
        LevelQueue.Enqueue(() => 
        {
            StartPosition += new Vector3(50f, 0);

            //Puzzle Start Position Starts a little further out than expected
            WallDistance = -10f;
            SignPostDistance = -3f;

            Wall.GetComponent<FadeIn>().MoveObject(StartPosition + new Vector3(-10f, 0));
            SignPost.GetComponent<GrowIn>().MoveObject(PuzzleNum, StartPosition + new Vector3(SignPostDistance, SignPostHeightAdjustment));
            PuzzleNum++;
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle1(StartPosition);
            }
        });

        //Puzzle 2
        LevelQueue.Enqueue(() => {
            StartPosition += new Vector3(35f, 0);
            
            //Reset WallDistance Value.
            WallDistance = -3f;
            SignPostDistance = 2f;

            Wall.GetComponent<FadeIn>().MoveObject(StartPosition + new Vector3(WallDistance, 0));
            SignPost.GetComponent<GrowIn>().MoveObject(PuzzleNum, StartPosition + new Vector3(SignPostDistance, SignPostHeightAdjustment));
            PuzzleNum++;
            // Instantiate(Resources.Load<GameObject>("Prefabs/PropsAndNots/Exclaim")).transform.position = StartPosition;
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle2(StartPosition);
            }
        });

        //Puzzle 3
        LevelQueue.Enqueue(() => {
            StartPosition += new Vector3(65f, 0);
            Wall.GetComponent<FadeIn>().MoveObject(StartPosition + new Vector3(WallDistance, 0));
            SignPost.GetComponent<GrowIn>().MoveObject(PuzzleNum, StartPosition + new Vector3(SignPostDistance, SignPostHeightAdjustment));
            PuzzleNum++;
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle3(StartPosition);
            }
        });

        //Puzzle 4
        LevelQueue.Enqueue(() => {
            StartPosition += new Vector3(82f, 0);
            Wall.GetComponent<FadeIn>().MoveObject(StartPosition + new Vector3(WallDistance, 0));
            SignPost.GetComponent<GrowIn>().MoveObject(PuzzleNum, StartPosition + new Vector3(SignPostDistance, SignPostHeightAdjustment));
            PuzzleNum++;
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle4(StartPosition);
            }
        });

        //Puzzle 5
        LevelQueue.Enqueue(() => {
            StartPosition += new Vector3(132f, 0);
            Wall.GetComponent<FadeIn>().MoveObject(StartPosition + new Vector3(WallDistance, 0));
            SignPost.GetComponent<GrowIn>().MoveObject(PuzzleNum, StartPosition + new Vector3(SignPostDistance, SignPostHeightAdjustment));
            PuzzleNum++;
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle5(StartPosition);
            }
        });
    }

    #endregion

    #region Runtime Methods

    /// <summary>
    /// Check for Next Level Progression. Activate chaser when timer runs out
    /// </summary>
    void Update () {
        //Check if end point of level has been reached
        if (WarmManager.IsNextLevel)
        {
            //Trigger to load the next level 
            WarmManager.ToggleIsNextLevel();
            NextArea();
        }

        //For Debugging Only
        if (Input.GetKeyDown(KeyCode.P) && IsDebuggingModeOn.Value)
        {
            NextArea();
            GameObject.Find("MainChar").transform.position = GameObject.FindGameObjectWithTag("EndPoint").transform.position;
        }

        //TODO: Start Timer of 15 minutes if still in warming element
        //TODO: Activate Chaser
    }

    public Vector3 GetClosestWarmingElementPosition(Vector3 MainCharPosition)
    {
        GameObject[] WarmingElements = GameObject.FindGameObjectsWithTag("WarmPoint");
        WarmingElements = FilterOutNotVisited(WarmingElements);
        if (WarmingElements.Length != 0)
        {
            float Distance = Mathf.Abs(Vector2.Distance(MainCharPosition, WarmingElements[0].transform.position));
            int indexClosest = 0;
            for (int i = 0;i < WarmingElements.Length;i++) 
            {
                GameObject warmElem = WarmingElements[i];
                float checkDistance = Mathf.Abs(Vector2.Distance(MainCharPosition, warmElem.transform.position));
                if (Distance > checkDistance)
                {
                    indexClosest = i;
                }
            }
            return WarmingElements[indexClosest].transform.position;
        }
        return MainCharPosition;
    }

    private GameObject[] FilterOutNotVisited(GameObject[] toFilter)
    {

        List<GameObject> filteredList = new List<GameObject>();
        foreach(GameObject g in toFilter)
        {
            if (g.GetComponent<SpriteRenderer>().color != Color.white)
                filteredList.Add(g);
        }
        return filteredList.ToArray();
    }

    /// <summary>
    /// Triggered when Warming Element Manager 
    /// </summary>
    public void NextArea()
    {
        //Paralyze Player
        //Move Main Ground Forward 
        //Destroy/Deactivate all previous game objects
        PlatformManager.Clear();
        PushableManager.Clear();
        CoolManager.Clear();

        if (LevelQueue.Count > 0)
            LevelQueue.Dequeue()();//Execute Level Set up

        //Instantiate Block => Area Effect "I can't go back" 
    }

    public void LoadingStage(int stageIndex)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (stageIndex == -1)
        {
            IntroScene.SetActive(true);
            playerObj.GetComponent<CharacterStatus>().InitFirstPassVars();
            playerObj.GetComponent<CharacterMovement>().enabled = false;
            LevelQueue.Dequeue()();
        }
        else
        {
            //TEMP Copy-Paste
            IntroScene.SetActive(true);
            playerObj.GetComponent<CharacterStatus>().InitFirstPassVars();
            playerObj.GetComponent<CharacterMovement>().enabled = false;
            LevelQueue.Dequeue()();

            /*
             * TODO: Handle Loading Scenes != 0
             * IntroScene.SetActive(false);
                        for (int i = 0; i <= stageIndex; i++)
                        {
                            NextArea();
                        }

                        playerObj.GetComponent<Rigidbody2D>().gravityScale = 1f;
                        playerObj.GetComponent<CharacterMovement>().enabled = true;

                        MenuManager.Instance.ActivateHeartMeter();
                        playerObj.GetComponent<CharacterStatus>().SetInCutscene(false);
                        playerObj.GetComponent<CharacterStatus>().SetHeartCooling(true);
                        playerObj.GetComponent<CharacterStatus>().ActivateHeartEffect();

                        playerObj.transform.position = SignPost.transform.position;

            */
        }
    }

    private void OnApplicationQuit()
    {
        LoadedStageIndex.Value = -1;  
    }

    #endregion

}
