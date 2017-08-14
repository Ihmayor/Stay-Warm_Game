﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    #region Singleton instance
    public static PlatformManager Instance { private set; get; }
    #endregion

    private GameObject VerticalPlatform;
    private GameObject HorizontalPlatform;
    private GameObject Step;
    private GameObject Spike;

    // Use this for initialization
    void Start () {
        PlatformManager.Instance = this;

        VerticalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/VerticalPlatform");
        HorizontalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/HorizontalPlatform");
        Step = Resources.Load<GameObject>("Prefabs/Platforms/step");
        Spike = Resources.Load<GameObject>("Prefabs/Platforms/Spike");
        //Reference this instance as singleton instance
        Puzzle0(GameObject.Find("PlatformWhiteSprite").transform.position + new Vector3(6f, -1.45f, 0));
        Puzzle1(GameObject.Find("lightpoleBridge").transform.position + new Vector3(4f, 1.5f, 0));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Puzzle0(Vector3 GroundedStartPosition)
    {
        CreateStepTower(GroundedStartPosition, 2);
        Vector3 LastPosition = CreateStepTower(GroundedStartPosition+ new Vector3(1.55f,0), 2);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(1.55f, 0), 2);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(1.55f, 0), 2);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x,GroundedStartPosition.y) + new Vector3(1.6f,0), 3);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(1.5f, 0), 3);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(0.5f, 0), 1);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(4.5f, 0), 6);
        CreatePlatform(HorizontalPlatform, LastPosition- new Vector3(2.5f,0,0), null, 1.3f, 400, false);
    }

    /// <summary>
    /// Creates non-movable obstacle
    /// </summary>
    /// <param name="position">Starting position of obstacle</param>
    /// <param name="stepCount">Height of obstacle</param>
    /// <returns>Latest Position of Tower</returns>
    private Vector3 CreateStepTower(Vector3 position, int stepCount)
    {
        GameObject step = Instantiate(Step, null);
        step.transform.position = position;
        Vector3 PrevPosition = step.transform.position;
        for (int i = 0; i< stepCount; i++)
        {
            step = Instantiate(Step, null);
            step.transform.position = PrevPosition + new Vector3(0, 0.2124999f);
            PrevPosition = step.transform.position;
        }
        return PrevPosition;
    }

    private void Puzzle1(Vector3 StartPosition)
    {
        List<GameObject> Platforms = new List<GameObject>();
      
        int HozPlatforms = 10;

        //Create A Vertical Platform to start
        Platforms.Add(CreatePlatform(VerticalPlatform, StartPosition, null, 2f, 1000, false));

        //Previous platform position. Instantiate following platforms within reach of it previous one. 
        Vector3 prevPlatformPosition = Platforms[Platforms.Count - 1].transform.position + new Vector3(0, 1.5f,0);

        //Start Horizontal Platform bridge across the sky.
        for (int i = 0; i <= HozPlatforms; i++)
        {
            bool DirectionVariance = false;
            float DistanceMovementVariance = Random.Range(0,1f);
            Vector3 DistanceVariance = new Vector3(DistanceMovementVariance, 0, 0);
            Vector3 PlatformPosition = prevPlatformPosition + DistanceVariance + new Vector3(0.9f,0,0);
            float SpeedVariance = Random.Range(2000, 7000);
            GameObject newPlatform = CreatePlatform(HorizontalPlatform, PlatformPosition, null, DistanceMovementVariance, SpeedVariance, DirectionVariance);
            Platforms.Add(newPlatform);
            //Give one of the platforms a piece of paper
            if (i == HozPlatforms - 2)
            {
                GameObject PaperInstance =Instantiate(Resources.Load<GameObject>("Prefabs/PropsAndNots/Paper"), Platforms[Platforms.Count - 1].transform);
                PaperInstance.GetComponent<Pickup>().PickupName = "Note #3";
            }

            prevPlatformPosition = PlatformPosition+new Vector3(DistanceMovementVariance, 0)+new Vector3(0.9f,0,0);
        }
        //Maintain Platforms
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
    private GameObject CreatePlatform(GameObject prefab, Vector3 position, Transform parentTransform, float distance, float speed, bool reverseDirection)
    {
        GameObject instance = Instantiate(prefab, parentTransform);
        instance.transform.position = position + new Vector3(0,0,-1);
        instance.GetComponent<PlatformMovement>().SetNewDistance(distance);
        instance.GetComponent<PlatformMovement>().ChangeSpeed(true, speed);
        instance.GetComponent<PlatformMovement>().ReverseDirection = reverseDirection;
        return instance;
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
    private GameObject CreatePlatform(GameObject prefab, Vector3 StartPoint, Vector3 EndPoint, float Gap, Transform parentTransform, float speed, bool reverseDirection)
    {
        Vector3 MidPoint = (StartPoint + EndPoint) / 2;

        //Calcualte Offsets
        float OffsetStartPoint = StartPoint.x + prefab.GetComponent<PlatformMovement>().AxisOffset / 2; //offset dependent on platform size
        float OffsetEndPoint = EndPoint.x - prefab.GetComponent<PlatformMovement>().AxisOffset / 2;//offset dependent on platform size

        //Adjust a gap for the platform to reach then find midpoint 
        OffsetStartPoint += Gap;
        OffsetEndPoint -= Gap;
        float OffsetMidPoint = (OffsetEndPoint - OffsetStartPoint) / 2;

        return CreatePlatform(prefab, MidPoint, parentTransform, OffsetMidPoint, speed, reverseDirection);
    }

    private void InstantiateAndDemoMovement()
    {

        //Instantiate Platforms
        //Ground is 3.3f
        GameObject VerticalPlatform = Resources.Load<GameObject>("VerticalPlatform");
        GameObject HorizontalPlatform = Resources.Load<GameObject>("HorizontalPlatform");

        //Reference Point of 0.0
        GameObject MainBackgroundParent = GameObject.Find("Background");
        GameObject Bush = MainBackgroundParent.transform.Find("Bush").gameObject;
        Vector3 startPosition = Bush.transform.position;

        int HozPlatforms = 10;

        //Create A Vertical Platform to start
        GameObject instance = Instantiate(VerticalPlatform, MainBackgroundParent.transform);
        instance.transform.position = startPosition + new Vector3(0.4f, 1f, 0);
        instance.GetComponent<PlatformMovement>().SetNewDistance(1.2f);
        instance.GetComponent<PlatformMovement>().ChangeSpeed(true, 10000);
        instance.GetComponent<PlatformMovement>().ReverseDirection = true;

        //Previous platform position. Adjust such that we are at the height of the starter vertical platform
        Vector3 prevPlatformPosition = instance.transform.position + new Vector3(0.9f, 1f, 0);

        //Start Horizontal Platform bridge across the sky.
        for (int i = 0; i <= HozPlatforms; i++)
        {
            GameObject newPlatform = Instantiate(HorizontalPlatform, MainBackgroundParent.transform);
            newPlatform.transform.position = prevPlatformPosition + new Vector3(Random.Range(0.1f, 0.4f) + HorizontalPlatform.transform.localScale.x, 0, 0);
            newPlatform.GetComponent<PlatformMovement>().SetNewDistance(Random.Range(1.5f, 2.5f));
            if (Random.Range(0, 1) == 1)
            {
                newPlatform.GetComponent<PlatformMovement>().ReverseDirection = true;
            }

            int SelectSpeed = Random.Range(0, 10);
            if (SelectSpeed == 5)
            {
                newPlatform.GetComponent<PlatformMovement>().ChangeSpeed(true, Random.Range(0.3f, 0.6f));
            }
            else if (SelectSpeed == 7)
            {
                newPlatform.GetComponent<PlatformMovement>().ChangeSpeed(false, Random.Range(0.3f, 0.6f));
            }

            prevPlatformPosition = newPlatform.transform.position;
        }
    }

    private void DemoMovement()
    {
        //Get Demo GameObjects
        GameObject PlatformVertical = GameObject.Find("Background").transform.Find("vert").gameObject;
        GameObject PlatformHorizontal = GameObject.Find("Background").transform.Find("Hoz").gameObject;
        PlatformVertical.GetComponent<PlatformMovement>().SetNewDistance(0.5f);
        PlatformHorizontal.GetComponent<PlatformMovement>().SetNewDistance(3f);
    }
}
