using Assets.Resources.Scripts.GameTriggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    //Managers
    private PlatformManager PlatformManager;
    private WarmingElementManager WarmManager;
    private PushableElementManager PushableManager;
    private List<PuzzleManager> Managers;
    private Queue<Action> LevelQueue;

	// Use this for initialization
	void Start () {
        Vector3 StartPosition = new Vector3(-45.5f, 3.3f, 0);
        Managers = new List<PuzzleManager>();
        //Call Platform Manager to build First Platforms
        PlatformManager = new PlatformManager();
        Managers.Add(PlatformManager);
        PlatformManager.Puzzle0(StartPosition);

        ////Warming Elements Manager
        WarmManager = new WarmingElementManager();
        Managers.Add(WarmManager);
        WarmManager.Puzzle0(StartPosition);

        PushableManager = new PushableElementManager();
        PushableManager.Puzzle0(StartPosition);
        Managers.Add(PlatformManager);

        //Store Level Setup
        LevelQueue = new Queue<Action>();

        //Puzzle 1
        LevelQueue.Enqueue(() => {
            StartPosition += new Vector3(50f, 0);
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle1(StartPosition);
            }
        });

        //Puzzle 2
        LevelQueue.Enqueue(() => {
            StartPosition += new Vector3(65f, 0);
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle2(StartPosition);
            }
        });

        //Puzzle 3
        LevelQueue.Enqueue(() => {
            StartPosition += new Vector3(65f, 0);
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle3(StartPosition);
            }
        });

        //Puzzle 4
        LevelQueue.Enqueue(() => {
            StartPosition += new Vector3(65f, 0);
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle4(StartPosition);
            }
        });

        //Puzzle 5
        LevelQueue.Enqueue(() => {
            StartPosition += new Vector3(65f, 0);
            foreach (PuzzleManager m in Managers)
            {
                m.Puzzle5(StartPosition);
            }
        });



        //Puzzle0(GameObject.Find("PlatformWhiteSprite").transform.position + new Vector3(6f, -1.45f, 0));
        //Puzzle1(GameObject.Find("lightpoleBridge").transform.position + new Vector3(4f, 1.5f, 0));

        //PushableElements 
        //WindCreation
        //Area Effects


    }

    // Update is called once per frame
    void Update () {
        if (WarmManager.IsNextLevel)
        {
            WarmManager.ToggleIsNextLevel();
            NextArea();
        }
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
        //Destroy/Deactivate all previous game objects
        PlatformManager.Clear();
        PushableManager.Clear();
        LevelQueue.Dequeue()();//Execute Level Set up

        //Instantiate Block => Area Effect "I can't go back" 
    }

    

}
