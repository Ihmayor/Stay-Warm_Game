using Assets.Resources.Scripts.GameTriggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformManager: PuzzleManager{
    
    #region Variables

    private GameObject VerticalPlatform;
    private GameObject HorizontalPlatform;
    private GameObject Step;
    private GameObject Spike;
    private List<GameObject> Platforms;

    #endregion

    public PlatformManager()
    {
        Platforms = new List<GameObject>();
        VerticalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/VerticalPlatform");
        HorizontalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/HorizontalPlatform");
        Step = Resources.Load<GameObject>("Prefabs/Platforms/step");
        Spike = Resources.Load<GameObject>("Prefabs/Platforms/Spike");
    }

    #region Puzzle/Level Setup Methods

    public override void Puzzle0(Vector3 GroundedStartPosition)
    {
        CreateStepTower(GroundedStartPosition, 2);
        Vector3 LastPosition = CreateStepTower(GroundedStartPosition+ new Vector3(1.55f,0), 2);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(1.55f, 0), 2);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(1.55f, 0), 3);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x,GroundedStartPosition.y) + new Vector3(1.6f,0), 3);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(1.5f, 0), 3);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(0.5f, 0), 1);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(4.5f, 0), 6);
        CreatePlatform(HorizontalPlatform, LastPosition- new Vector3(2.5f,0,0), null, 1.3f, 400, false);
    }

    public override void Puzzle1(Vector3 GroundedStartPosition)
    {
        Platforms = new List<GameObject>();

        int HozPlatforms = 10;
        GroundedStartPosition += new Vector3(0, 3.35f);
        //Create A Vertical Platform to start
       CreatePlatform(VerticalPlatform, GroundedStartPosition, null, 2f, 1000, false);
        //Previous platform position. Instantiate following platforms within reach of it previous one. 
        Vector3 prevPlatformPosition = Platforms[Platforms.Count - 1].transform.position + new Vector3(0, 1.5f,0);

        //Start Horizontal Platform bridge across the sky.
        for (int i = 0; i <= HozPlatforms; i++)
        {
            bool DirectionVariance = false;
            float DistanceMovementVariance = UnityEngine.Random.Range(0,1f);
            Vector3 DistanceVariance = new Vector3(DistanceMovementVariance, 0, 0);
            Vector3 PlatformPosition = prevPlatformPosition + DistanceVariance + new Vector3(0.9f,0,0);
            float SpeedVariance = UnityEngine.Random.Range(2000, 7000);
            CreatePlatform(HorizontalPlatform, PlatformPosition, null, DistanceMovementVariance, SpeedVariance, DirectionVariance);
            prevPlatformPosition = PlatformPosition+new Vector3(DistanceMovementVariance, 0)+new Vector3(0.9f,0,0);
        }
    }

    //TODO:
    public override void Puzzle2(Vector3 GroundedStartPosition)
    {

    }

    //TODO:
    public override void Puzzle3(Vector3 GroundedStartPosition)
    {

    }

    //TODO:
    public override void Puzzle4(Vector3 GroundedStartPosition)
    {

    }

    //TODO:
    public override void Puzzle5(Vector3 GroundedStartPosition)
    {

    }

    #endregion

    #region Creation & Deletion Methods

    /// <summary>
    /// Creates non-movable obstacle
    /// </summary>
    /// <param name="position">Starting position of obstacle</param>
    /// <param name="stepCount">Height of obstacle</param>
    /// <returns>Latest Position of Tower</returns>
    private Vector3 CreateStepTower(Vector3 position, int stepCount)
    {
        GameObject step = MonoBehaviour.Instantiate(Step, null);
        step.transform.position = position;
        Vector3 PrevPosition = step.transform.position;
        for (int i = 0; i < stepCount; i++)
        {
            step = MonoBehaviour.Instantiate(Step, null);
            step.transform.position = PrevPosition + new Vector3(0, 0.2124999f);
            PrevPosition = step.transform.position;
            Platforms.Add(step);//Keep Track of ALl platforms for later puzzle clearence
        }


        return PrevPosition;
    }

    /// <summary>
    /// Create a Platform at x point that moves distance +- foward/backwards
    /// </summary>
    /// <param name="prefab">GameObject Prefab of Moving Platform</param>
    /// <param name="position">Position platform should be instantiated at</param>
    /// <param name="parentTransform">Parent Transform of instantiated platform object</param>
    /// <param name="distance">Distance platform travels +-</param>
    /// <param name="speed">Speed of platform movement</param>
    /// <param name="reverseDirection">Reverse the order of direction (moving backwards first then forwards)</param>
    /// <returns>Instantiated Platform</returns>
    private void CreatePlatform(GameObject prefab, Vector3 position, Transform parentTransform, float distance, float speed, bool reverseDirection)
    {
        GameObject instance = MonoBehaviour.Instantiate(prefab, parentTransform);
        instance.transform.position = position + new Vector3(0,0,-1);
        instance.GetComponent<PlatformMovement>().SetNewDistance(distance);
        instance.GetComponent<PlatformMovement>().ChangeSpeed(true, speed);
        instance.GetComponent<PlatformMovement>().ReverseDirection = reverseDirection;
        Platforms.Add(instance);//Keep Track of ALl platforms for later puzzle clearence
    }

    /// <summary>
    /// Creates moving platform between two given points
    /// </summary>
    /// <param name="prefab">Moving Platform Prefab</param>
    /// <param name="StartPoint">Start Point that the platform must reach</param>
    /// <param name="EndPoint">End Point that the platform must reach</param>
    /// <param name="Gap">Gap Between the two reach points</param>
    /// <param name="parentTransform">Parent Tranform of instantiated platform object</param>
    /// <param name="speed">Speed of platform movement</param>
    /// <param name="reverseDirection">Reverse the order of direction (moving backwards first then forwards)</param>
    /// <returns>Instantiated Platform</returns>
    private void CreatePlatform(GameObject prefab, Vector3 StartPoint, Vector3 EndPoint, float Gap, Transform parentTransform, float speed, bool reverseDirection)
    {
        Vector3 MidPoint = (StartPoint + EndPoint) / 2;

        //Calcualte Offsets
        float OffsetStartPoint = StartPoint.x + prefab.GetComponent<PlatformMovement>().AxisOffset / 2; //offset dependent on platform size
        float OffsetEndPoint = EndPoint.x - prefab.GetComponent<PlatformMovement>().AxisOffset / 2;//offset dependent on platform size

        //Adjust a gap for the platform to reach then find midpoint 
        OffsetStartPoint += Gap;
        OffsetEndPoint -= Gap;
        float OffsetMidPoint = (OffsetEndPoint - OffsetStartPoint) / 2;
    }

    public void Clear()
    {
        foreach(GameObject platform in Platforms)
        {
            MonoBehaviour.Destroy(platform);
        }
        Platforms = new List<GameObject>();
    }

   
    #endregion

}
