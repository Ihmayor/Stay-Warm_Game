using Assets.Resources.Scripts.GameTriggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
      
        //Load First Level
        LevelQueue.Dequeue()();
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
            StartPosition += new Vector3(65f, 0);
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
            StartPosition += new Vector3(65f, 0);
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("MainChar").transform.position = GameObject.FindGameObjectWithTag("EndPoint").transform.position;
        }

        //TODO: Start Timer of 15 minutes if still in warming element
        //TODO: Activate Chaser
    }

    public Vector3 GetClosestWarmingElement(Vector3 MainCharPosition)
    {
        GameObject[] WarmingElements = GameObject.FindGameObjectsWithTag("WarmPoint");
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

        LevelQueue.Dequeue()();//Execute Level Set up

        //Instantiate Block => Area Effect "I can't go back" 
    }

    #endregion

}
