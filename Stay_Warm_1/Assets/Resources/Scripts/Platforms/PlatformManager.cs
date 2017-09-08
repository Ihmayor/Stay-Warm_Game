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
    private readonly float StepGapMin = 0.3f;
    private readonly float StepGapMax = 0.53f;

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
  
        int HozPlatforms = 10;
        GroundedStartPosition += new Vector3(0, 2.1f);
        //Create A Vertical Platform to start
       CreatePlatform(VerticalPlatform, GroundedStartPosition, null, 2f, 1000, false);
        //Previous platform position. Instantiate following platforms within reach of it previous one. 
        Vector3 prevPlatformPosition = Platforms[Platforms.Count - 1].transform.position + new Vector3(0, 1.5f,0);

        //Start Horizontal Platform bridge across the sky.
        for (int i = 0; i <= HozPlatforms; i++)
        {
            bool DirectionVariance = false;
            float DistanceMovementVariance = UnityEngine.Random.Range(0,0.7f);
            Vector3 DistanceVariance = new Vector3(DistanceMovementVariance, 0, 0);
            Vector3 PlatformPosition = prevPlatformPosition + DistanceVariance + new Vector3(0.9f,0,0);
            float SpeedVariance = UnityEngine.Random.Range(2000, 7000);
            CreatePlatform(HorizontalPlatform, PlatformPosition, null, DistanceMovementVariance, SpeedVariance, DirectionVariance);
            prevPlatformPosition = PlatformPosition+new Vector3(DistanceMovementVariance, 0)+new Vector3(0.9f,0,0);
        }
    }

    public override void Puzzle2(Vector3 GroundedStartPosition)
    {
        Vector3 LastPosition = CreateStaircase(GroundedStartPosition + new Vector3(2.5f, 0), 10, 4, false);
        LastPosition = CreateSpike(LastPosition + new Vector3(0.2f, 0, 0), 2);
        LastPosition = CreateStaircase(LastPosition + new Vector3(0f, 0), 4, 2, true);
        LastPosition = CreateStaircase(LastPosition + new Vector3(-0.5f, 0), 6, 3, false);
        LastPosition = CreateStaircase(LastPosition + new Vector3(0f, 0), 7, 4, true);
        LastPosition = CreateStaircase(LastPosition + new Vector3(0f, 0), 9, 4, false);
        LastPosition = CreateStaircase(LastPosition + new Vector3(0f, 0), 8, 5, true);
        LastPosition = CreateStaircase(LastPosition + new Vector3(0f, 0), 6, 6, false);

        CreatePlatform(VerticalPlatform, LastPosition + new Vector3(0.7f,1.2f), null,1, 500, false);
        CreatePlatform(HorizontalPlatform, LastPosition + new Vector3(4.2f, 1.5f), null, 1.5f, 200, true);
        CreatePlatform(VerticalPlatform, LastPosition + new Vector3(7.5f, 1.2f), null, 1, 500, false);
        CreatePlatform(HorizontalPlatform, LastPosition + new Vector3(10.5f, 1.2f), null, 1.3f, 700, false);
        LastPosition = CreateSpike(LastPosition + new Vector3(2f, 0, 0), 20);

        LastPosition = CreateStaircase(LastPosition + new Vector3(0.8f, 0), 5, 5, true);
        float groundedY = LastPosition.y;
        LastPosition = CreateStepTower(LastPosition + new Vector3(1.28f,0f), 4,11);

        LastPosition = CreateSpike(new Vector3(LastPosition.x + 2.4f, groundedY, 0), 5);
        CreateStepTower(LastPosition, 1);
        CreatePlatform(VerticalPlatform, LastPosition + new Vector3(1.1f, 1f), null, 0.8f, 200, false);
        CreatePlatform(HorizontalPlatform, LastPosition + new Vector3(3.5f, 1f), null, 0.8f, 200, false);
        CreatePlatform(HorizontalPlatform, LastPosition + new Vector3(7f, 1f), null, 1.8f, 200, false);
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
        Clear();
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
        Platforms.Add(step);
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
    /// Creates non-movable obstacle
    /// </summary>
    /// <param name="position">Starting position of obstacle</param>
    /// <param name="stepCount">Height of obstacle</param>
    /// <param name="widthFactor">Factor to increase width of tower</param>
    /// <returns>Latest Position of Tower</returns>
    private Vector3 CreateStepTower(Vector3 position, int stepCount, int widthFactor)
    {
        GameObject step = MonoBehaviour.Instantiate(Step, null);
        step.transform.position = position;
        step.transform.localScale = new Vector3(step.transform.localScale.x * widthFactor,
                                                    step.transform.localScale.y,
                                                    step.transform.localScale.z);
        Platforms.Add(step);
        Vector3 PrevPosition = step.transform.position;
        for (int i = 0; i < stepCount; i++)
        {
            step = MonoBehaviour.Instantiate(Step, null);
            step.transform.position = PrevPosition + new Vector3(0, 0.2124999f);
            step.transform.localScale = new Vector3(step.transform.localScale.x* widthFactor,            
                                                    step.transform.localScale.y, 
                                                    step.transform.localScale.z);
            PrevPosition = step.transform.position;
            Platforms.Add(step);//Keep Track of ALl platforms for later puzzle clearence
        }

        return PrevPosition + new Vector3(widthFactor * 0.1f,0);
    }

    /// <summary>
    /// Create stair case of steps
    /// </summary>
    /// <param name="GroundPosition">Grounded Position of stair case</param>
    /// <param name="height">Peak of height of stair case</param>
    /// <param name="width">Total Width of Stair Case</param>
    /// <param name="isDecayed">Bool to decide whether stair case ascends or descends</param>
    /// <returns></returns>
    private Vector3 CreateStaircase(Vector3 GroundPosition, int height, float width, bool isDecayed)
    {
        if (height == 0)
        {
            Debug.Log("Error. Height is 0. Cannot Produce Stairs");
            return Vector3.zero;
        }

        Vector3 EndPosition = GroundPosition + new Vector3(width, 0, 0);
        float stepGap = (EndPosition.x - GroundPosition.x) / height;

        if (stepGap < StepGapMin )
        {
            Debug.Log("Gap not large enough for player, given height width ratio");
            Debug.Log(height / width);
            return Vector3.zero;
        }
        else if (stepGap > StepGapMax)
        {
            stepGap = StepGapMax;
        }

        Vector3 CurrentPosition = GroundPosition;
        if (isDecayed)
        {
            for (int i = height; i > 0; i--)
            {
                CreateStepTower(CurrentPosition, i);
                CurrentPosition += new Vector3(stepGap, 0);
            }
        }
        else
        {
            for (int i = 0; i <= height; i++)
            {
                CreateStepTower(CurrentPosition, i);
                CurrentPosition += new Vector3(stepGap, 0);
            }
        }
        return CurrentPosition;
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

    /// <summary>
    /// Create a length of spikes 
    /// </summary>
    /// <param name="GroundPosition"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private Vector3 CreateSpike(Vector3 GroundPosition, int count)
    {
        GroundPosition += new Vector3(0, 0.9f);
        GameObject spike = MonoBehaviour.Instantiate(Spike);
        spike.transform.position = GroundPosition;
        Platforms.Add(spike);
        Vector3 PrevPosition = spike.transform.position;
        for(int i = 0; i< count; i++)
        {
            spike = MonoBehaviour.Instantiate(Spike);
            spike.transform.position = PrevPosition + new Vector3(0.5f,0,0);
            Platforms.Add(spike);
            PrevPosition = spike.transform.position;
        }
        return PrevPosition + new Vector3(0.5f,-0.11f);
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
