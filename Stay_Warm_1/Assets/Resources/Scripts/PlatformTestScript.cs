using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTestScript : MonoBehaviour {

    private GameObject VerticalPlatform;
    private GameObject HorizontalPlatform;
    private GameObject Step;
    private GameObject Spike;

    // Use this for initialization
    void Start () {
        VerticalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/VerticalPlatform");
        HorizontalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/HorizontalPlatform");
        Step = Resources.Load<GameObject>("Prefabs/Platforms/step");
        Spike = Resources.Load<GameObject>("Prefabs/Platforms/Spike");
        Vector3 GroundedStartPosition = new Vector3(0, 0);

        Vector3 LastPosition = GroundedStartPosition;

        CreatePlatform(VerticalPlatform, GroundedStartPosition + new Vector3(15f, 2f), null, 2, 100, true);

        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(4.6f, 0.68f),25);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(5, 0f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(5, 1.3f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(5, 2.4f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(5, 3.6f), 0);

        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6.5f, 0f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6.5f, 0.68f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6.5f, 1.9f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6.5f, 3.0f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6.5f, 4.2f), 0);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(7f, 0.68f), 18);
        CreateSpike(new Vector3(LastPosition.x+0.4f, GroundedStartPosition.y), 10);
        CreatePlatform(HorizontalPlatform, LastPosition + new Vector3(2.2f, 0), null, 2, 100, true);

    }

    // Update is called once per frame
    void Update () {
        
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
        for (int i = 0; i < stepCount; i++)
        {
            step = Instantiate(Step, null);
            step.transform.position = PrevPosition + new Vector3(0, 0.2124999f);
            PrevPosition = step.transform.position;
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
    private GameObject CreatePlatform(GameObject prefab, Vector3 position, Transform parentTransform, float distance, float speed, bool reverseDirection)
    {
        GameObject instance = Instantiate(prefab, parentTransform);
        instance.transform.position = position;
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
    private GameObject CreatePlatform(GameObject prefab, Vector3 StartPoint, Vector3 EndPoint,float Gap, Transform parentTransform, float speed, bool reverseDirection)
    {
        Vector3 MidPoint = (StartPoint + EndPoint) / 2;

        //Calcualte Offsets
        float OffsetStartPoint = StartPoint.x + prefab.GetComponent<PlatformMovement>().AxisOffset/2; //offset dependent on platform size
        float OffsetEndPoint = EndPoint.x - prefab.GetComponent<PlatformMovement>().AxisOffset/2 ;//offset dependent on platform size

        //Adjust a gap for the platform to reach then find midpoint 
        OffsetStartPoint += Gap;
        OffsetEndPoint -= Gap;
        float OffsetMidPoint =( OffsetEndPoint - OffsetStartPoint) / 2;

        return CreatePlatform(prefab, MidPoint, parentTransform, OffsetMidPoint, speed, reverseDirection);
    }
    private Vector3 CreateSpike(Vector3 GroundPosition, int count)
    {
        GroundPosition += new Vector3(0, 0.09f);
        GameObject spike = MonoBehaviour.Instantiate(Spike);
        spike.transform.position = GroundPosition;
//        Platforms.Add(spike);
        Vector3 PrevPosition = spike.transform.position;
        for (int i = 0; i < count; i++)
        {
            spike = MonoBehaviour.Instantiate(Spike);
            spike.transform.position = PrevPosition + new Vector3(0.5f, 0, 0);
  //          Platforms.Add(spike);
            PrevPosition = spike.transform.position;
        }
        return PrevPosition + new Vector3(0.5f, -0.09f);
    }


}
