using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTestScript : MonoBehaviour {

    private GameObject VerticalPlatform;
    private GameObject HorizontalPlatform;
    private GameObject Step;
    private GameObject Spike;
    public GameObject MainChar;

    private readonly float StepGapMin = 0.3f;
    private readonly float StepGapMax = 0.53f;

    // Use this for initialization
    void Start () {
        VerticalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/VerticalPlatform");
        HorizontalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/HorizontalPlatform");
        Step = Resources.Load<GameObject>("Prefabs/Platforms/step");
        Spike = Resources.Load<GameObject>("Prefabs/Platforms/Spike");



        GameObject LightPole = Resources.Load<GameObject>("Prefabs/Elements/lightpole");
        GameObject Pushable = Resources.Load<GameObject> ("Prefabs/Platforms/PushableBox");

        Vector3 GroundedStartPosition = MainChar.gameObject.transform.position;
        GroundedStartPosition.y = 0;
        Vector3 LastPosition = GroundedStartPosition;

        //Create Spike
        CreateSpike(GroundedStartPosition + new Vector3(5f,0),20);


        ///Warming Element Position
        GameObject lightInstance = Instantiate(LightPole, null);
        lightInstance.transform.position = GroundedStartPosition + new Vector3(15.6f,5.3f);

        //Create Pushables
        GameObject pushInstance = Instantiate(Pushable, null);
        pushInstance.transform.position = GroundedStartPosition + new Vector3(27f, 5.3f);
        pushInstance = Instantiate(Pushable, null);
        pushInstance.transform.position = GroundedStartPosition + new Vector3(27f, 5.3f);
        pushInstance = Instantiate(Pushable, null);
        pushInstance.transform.position = GroundedStartPosition + new Vector3(27f, 5.3f);
        pushInstance = Instantiate(Pushable, null);
        pushInstance.transform.position = GroundedStartPosition + new Vector3(27f, 5.3f);
        pushInstance = Instantiate(Pushable, null);
        pushInstance.transform.position = GroundedStartPosition + new Vector3(35f, 9.3f);
        pushInstance = Instantiate(Pushable, null);
        pushInstance.transform.position = GroundedStartPosition + new Vector3(45f, 10.4f);


     

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
        Vector3 PrevPosition = step.transform.position;
        for (int i = 0; i < stepCount; i++)
        {
            step = MonoBehaviour.Instantiate(Step, null);
            step.transform.position = PrevPosition + new Vector3(0, 0.2124999f);
            step.transform.localScale = new Vector3(step.transform.localScale.x * widthFactor,
                                                    step.transform.localScale.y,
                                                    step.transform.localScale.z);
            PrevPosition = step.transform.position;
        }

        return PrevPosition + new Vector3(widthFactor * 0.1f, 0);
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

        if (stepGap < StepGapMin)
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
