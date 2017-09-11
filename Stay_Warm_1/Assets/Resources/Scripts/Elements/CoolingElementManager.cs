using Assets.Resources.Scripts.GameTriggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoolingElementManager : PuzzleManager
{
    
    /// <summary>
    /// Resources Pool for WindSources
    /// </summary>
    private GameObject[] WindSourceInstances;
    
    /// <summary>
    /// Total Number of Wind Sources
    /// </summary>
    private readonly int NumWindSourceInstances = 11;

    /// <summary>
    /// Wind Source Index
    /// </summary>
    private int WindSourceIndex;


    /// <summary>
    /// Resources Pool for DropSources
    /// </summary>
    private GameObject[] DropSourceInstances;

    /// <summary>
    /// Total Number of Drop Sources
    /// </summary>
    private readonly int NumDropSourceInstances = 6;

    /// <summary>
    /// Drop Source Index
    /// </summary>
    private int DropSourceIndex;

    public CoolingElementManager()
    {
        //Load Wind Source Prefab
        GameObject WindSource = Resources.Load<GameObject>("Prefabs/Elements/WindSource");

        //Create Wind Source Resource Pool Storage
        WindSourceInstances = new GameObject[NumWindSourceInstances];

        //Populate resources pool
        for (int i = 0; i < NumWindSourceInstances; i++)
        {
            WindSourceInstances[i] = MonoBehaviour.Instantiate(WindSource);
            WindSourceInstances[i].SetActive(false);
        }

        //Load DropSource Prefab
        GameObject DropSource = Resources.Load<GameObject>("Prefabs/Elements/DropSource");

        //Create Drop Source Resource Pool Storage
        DropSourceInstances = new GameObject[NumDropSourceInstances];

        //Populate resources pool
        for (int i =0; i<NumDropSourceInstances;i++)
        {
            DropSourceInstances[i] = MonoBehaviour.Instantiate(DropSource);
            DropSourceInstances[i].SetActive(false);
        }
        
        //Init
        WindSourceIndex = 0;
        DropSourceIndex = 0;
    }

    /// <summary>
    /// Set up Cooling Elements for Puzzle 0
    /// </summary>
    /// <param name="StartPosition">Start Position of Puzzle</param>
    public override void Puzzle0(Vector3 StartPosition)
    {
        CreateWindSource(StartPosition + new Vector3(28f, 0.8f, 0));
    }

    /// <summary>
    /// Set up Cooling Elements for Puzzle 1
    /// </summary>
    /// <param name="StartPosition">Start Position of Puzzle</param>
    public override void Puzzle1(Vector3 StartPosition)
    {
        CreateBlizzard(StartPosition + new Vector3(6f, 0.8f, 0), new float[] { 5, 3, 3, 2, 2, 3, 3});
    }

    /// <summary>
    /// Set up Cooling Elements for Puzzle 2
    /// </summary>
    /// <param name="StartPosition">Start Position of Puzzle</param>
    public override void Puzzle2(Vector3 StartPosition)
    {
       CreateWindSource(StartPosition + new Vector3(61f, 0.8f, 0));
       
    }

    /// <summary>
    /// Set up Cooling Elements for Puzzle 1
    /// </summary>
    /// <param name="StartPosition">Start Position of Puzzle</param>
    public override void Puzzle3(Vector3 StartPosition)
    {
       Vector3 LastPosition = CreateDropSource(StartPosition + new Vector3(2f, 0));
       LastPosition = CreateDropSource(LastPosition + new Vector3(1f, 0));
       LastPosition = CreateDropSource(LastPosition + new Vector3(3f, 0));
    }

    /// <summary>
    /// Set up Cooling Elements for Puzzle 1
    /// </summary>
    /// <param name="StartPosition">Start Position of Puzzle</param>
    public override void Puzzle4(Vector3 StartPosition)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Set up Cooling Elements for Puzzle 1
    /// </summary>
    /// <param name="StartPosition">Start Position of Puzzle</param>
    public override void Puzzle5(Vector3 StartPosition)
    {
        throw new NotImplementedException();
    }

    #region Helper Methods

    private Vector3 CreateDropSource(Vector3 DropPosition)
    {



        return DropPosition + new Vector3(0.5f, 0);
    }

    /// <summary>
    /// Place a wind source resource at specified position
    /// </summary>
    /// <param name="WindSourcePosition">Position to place windsource </param>
    /// <returns></returns>
    private Vector3 CreateWindSource(Vector3 WindSourcePosition)
    {
        //Prevent using more resources than available
        if (WindSourceIndex >= NumWindSourceInstances)
            throw new Exception("Error. Attempt to create more wind that available in resources pool.");
        
        //Fetch WindSource from Resources Pool
        GameObject Source = WindSourceInstances[WindSourceIndex];
        //Activate and Place
        Source.SetActive(true);
        Source.GetComponent<WindCreation>().RestartLoop();
        Source.transform.position = WindSourcePosition;

        //Increase Index to keep track of used resources;
        WindSourceIndex++;

        //Return the wind sources placed position
        return Source.transform.position;
    }

    /// <summary>
    /// Creates Blizzard of wind sources with specified x position differences
    /// </summary>
    /// <param name="StartPosition">Start Position of Blizzard</param>
    /// <param name="XPositionsVariants">Distances to place windsources from previous windsources</param>
    private void CreateBlizzard(Vector3 StartPosition, float[] XPositionsVariants)
    {
        //Calculate our used and required resources
        int totalSoFar = WindSourceIndex+1;
        int totalBlizzardNeeds = XPositionsVariants.Length + 1 + totalSoFar;

        //Prevents Creating Blizzards that use more resources than available
        if (totalBlizzardNeeds> NumWindSourceInstances )
        {
            throw new Exception("Error you tried to create blizzard that requires more instances than available. Please lessen instances required by: "+ (totalBlizzardNeeds - NumWindSourceInstances));
        }

        //Instantiate the first wind source at the start of the blizzard
        GameObject Source = WindSourceInstances[WindSourceIndex];
        Source.SetActive(true);
        Source.GetComponent<WindCreation>().RestartLoop();
        Source.transform.position = StartPosition;

        //Increase the index to fetch the next available windsource instance
        WindSourceIndex++;

        //Init Previous Position to place wind sources on after the other
        Vector3 PreviousPosition = Source.transform.position;

        //Populate Blizzard
        foreach(float XPosition in XPositionsVariants)
        {
            Source = WindSourceInstances[WindSourceIndex];
            Source.transform.position = PreviousPosition + new Vector3(XPosition, 0, 0);
            Source.SetActive(true);
            Source.GetComponent<WindCreation>().RestartLoop();
            PreviousPosition = Source.transform.position;
            WindSourceIndex++;
        }
    }

    /// <summary>
    /// Clear the stage of all instances to prevent loading issue. Stop looping of any wind instances.
    /// </summary>
    public void Clear()
    {
        foreach(GameObject source in WindSourceInstances)
        {
            source.GetComponent<WindCreation>().StopLoop();
            source.SetActive(false);
        }
        WindSourceIndex = 0;
    }
    #endregion

}
