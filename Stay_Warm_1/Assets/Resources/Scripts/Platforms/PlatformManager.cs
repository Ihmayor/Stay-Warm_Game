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
            if (i == 4)
            {
                Platforms[i].gameObject.AddComponent<NotifyPropScript>();
                NotifyPropScript script = Platforms[i].gameObject.GetComponent<NotifyPropScript>();
                script.AddExclaim(Platforms[i].transform);
                script.optionalThoughtMessage = "They had noticed something paper on the ground and tried to pick it up. It broke apart as their fingers grazed it.";
                script.notificationMessage = "Found 'Dust'";
            }

            prevPlatformPosition = PlatformPosition+new Vector3(DistanceMovementVariance, 0)+new Vector3(0.9f,0,0);
        }
    }

    public override void Puzzle2(Vector3 GroundedStartPosition)
    {
        Vector3 LastPosition = CreateStaircase(GroundedStartPosition + new Vector3(2.5f, 0), 10, 4, false);
        float groundedY = GroundedStartPosition.y;
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
        LastPosition = CreateStepTower(LastPosition + new Vector3(1.28f,0f), 4,11);

        LastPosition = CreateSpike(new Vector3(LastPosition.x + 2.4f, groundedY, 0), 5);
        CreateStepTower(LastPosition, 1);
        CreatePlatform(VerticalPlatform, LastPosition + new Vector3(1.1f, 1f), null, 0.8f, 200, false);
        CreatePlatform(HorizontalPlatform, LastPosition + new Vector3(3.5f, 1f), null, 0.8f, 200, false);
        CreatePlatform(HorizontalPlatform, LastPosition + new Vector3(7f, 1f), null, 1.8f, 200, false);
    }

    public override void Puzzle3(Vector3 GroundedStartPosition)
    {

        CreatePlatform(VerticalPlatform, GroundedStartPosition + new Vector3(6.2f,1.1f), null,1, 200, false);
        Vector3 LastPosition = CreateStaircase(GroundedStartPosition + new Vector3(8f, 0), 10, 4, true);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x+0.4f, GroundedStartPosition.y + 0.7f), 1);
        LastPosition = CreateStepTower(LastPosition + new Vector3(0.85f,0.2f), 1);
        LastPosition = CreateStepTower(LastPosition + new Vector3(2.1f, -0.2f), 1);

        CreateStaircase(new Vector3(LastPosition.x +3, GroundedStartPosition.y), 10, 3, false);
        CreateStaircase(new Vector3(LastPosition.x + 6, GroundedStartPosition.y), 8, 3, true);
        CreateStaircase(new Vector3(LastPosition.x + 1, GroundedStartPosition.y+2.1f), 9, 3, true);
        CreatePlatform(HorizontalPlatform, new Vector3(LastPosition.x+6 , GroundedStartPosition.y +2.1f), null, 2, 300, true);

        CreatePlatform(HorizontalPlatform, new Vector3(LastPosition.x + 5.3f, GroundedStartPosition.y + 4.1f), null, 4.2f, 500, true);
        CreateStepTower(new Vector3(LastPosition.x + 10.3f, GroundedStartPosition.y + 4.6f), 1);
        CreateStepTower(new Vector3(LastPosition.x + 10.8f, GroundedStartPosition.y + 4.1f), 1);

        LastPosition = CreateStaircase(new Vector3(LastPosition.x + 11 , GroundedStartPosition.y), 8, 4, true);

        LastPosition = CreateStaircase(new Vector3(LastPosition.x + 5, GroundedStartPosition.y), 100, 40, false);
    }

    public override void Puzzle4(Vector3 GroundedStartPosition)
    {
        Vector3 LastPosition = GroundedStartPosition;

      
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6f, 0), 4);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(3.5f, 0), 3);

        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(4.6f, 0.86f), 25);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(5, 0f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(5, 1.6f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(5, 2.9f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(5, 4.2f), 0);

        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6.5f, 0f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6.5f, 0.86f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6.5f, 2.1f), 0);
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(6.5f, 3.8f), 0);
  
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(7f, 0.68f), 18);

        CreateSpike(new Vector3(LastPosition.x + 0.4f, GroundedStartPosition.y), 10);
        CreatePlatform(HorizontalPlatform, LastPosition + new Vector3(2.5f, 0), null, 2, 100, true);


        for (int i = 1; i <=6; i++)
        {
            CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(13.5f+(i*1.5f), (i*0.68f)+0.68f), 0);
        }
        CreatePlatform(HorizontalPlatform, LastPosition + new Vector3(27f,1f), null, 2.8f, 100, false);
       
        //TODO RECREATE INTO NEW FUNCTION
        for (int i = 0; i <= 10; i++)
        {
            CreateSpike(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(24f + (i * 0.5f), 4.76f), 1);
            CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(24f+(i*0.5f), 4.59f), 0);
        }
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(29.5f, 4.59f), 0);//Add Interaction
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(30f, 4.59f), 0);//Add Interaction
        CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(31f, 4.59f), 0);//Add Interaction
        GameObject step = Platforms[Platforms.Count - 1].gameObject;
        step.AddComponent<NotifyPropScript>();

        //Added Prop 
        NotifyPropScript script = step.GetComponent<NotifyPropScript>();
        script.AddExclaim(Platforms[Platforms.Count - 1].transform);
        script.optionalThoughtMessage = "It looks like sheet music...with a sad melody on it.";
        script.notificationMessage = "Found 'Sad Melody'";
        script.SetSound(Resources.Load<AudioClip>("Audio/sadsonggirl"));


        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(20f, 0f), 2);
        CreateSpike(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(89f, 0f), 4);
        LastPosition = CreateStepTower(new Vector3(LastPosition.x, GroundedStartPosition.y) + new Vector3(92f, 0f), 2);


    }

    public override void Puzzle5(Vector3 GroundedStartPosition)
    {
        Clear();
        GroundedStartPosition += new Vector3(1.1f, 0);
        CreateSpike(GroundedStartPosition + new Vector3(5f, 0), 20);
    
        CreatePlatform(VerticalPlatform, GroundedStartPosition + new Vector3(3.3f, 2.1f), null, 2f, 1000, false);
        CreatePlatform(VerticalPlatform, GroundedStartPosition + new Vector3(18.3f, 3.5f), null, 3.5f, 1000, false);
        CreatePlatform(HorizontalPlatform, GroundedStartPosition + new Vector3(22.6f, 6f), null, 2.8f, 1200, true);
        CreatePlatform(HorizontalPlatform, GroundedStartPosition + new Vector3(32f, 6f), null, 4.9f, 900, false);
        CreatePlatform(HorizontalPlatform, GroundedStartPosition + new Vector3(41f, 6f), null, 8f, -3000, false);
        CreatePlatform(HorizontalPlatform, GroundedStartPosition + new Vector3(41f, 6f), null, 8f, 900, true);
        CreatePlatform(HorizontalPlatform, GroundedStartPosition + new Vector3(39f, 6f), null, 7f, 900, false);

        Vector3 LastPosition = CreateStepTower(GroundedStartPosition + new Vector3(5.5f, 3.4f), 1, 4);
        LastPosition = CreateStepTower(LastPosition + new Vector3(2.3f, -1.5f), 1, 4);
        LastPosition = CreateStepTower(LastPosition + new Vector3(1.5f, 0.5f), 1, 4);
        LastPosition = CreateStepTower(LastPosition + new Vector3(1.1f, 0.5f), 1, 4);
        CreateStepTower(LastPosition + new Vector3(2.9f, 0.5f), 1, 5);
        LastPosition = CreateStepTower(LastPosition + new Vector3(-2.8f, 0.5f), 0);
        LastPosition = CreateStepTower(LastPosition + new Vector3(-1.6f, 0.57f), 0);
        LastPosition = CreateStepTower(LastPosition + new Vector3(-0.8f, 0.8f), 0, 2);
        LastPosition = CreateStepTower(LastPosition + new Vector3(1.1f, 1.2f), 0, 2);
        LastPosition = CreateStepTower(LastPosition + new Vector3(1.1f, 1.2f), 0, 2);
        LastPosition = CreateStepTower(GroundedStartPosition + new Vector3(35f, 7.1f), 0, 10);
        CreateStepTower(LastPosition + new Vector3(10f, 0), 0, 9);
        CreateStaircase(GroundedStartPosition + new Vector3(28.3f, 0f), 5, 4, false);
        CreateStaircase(GroundedStartPosition + new Vector3(31.3f, 0f), 5, 4, true);
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
    /// <param name="speed">Speed of platform movement (num x Times) </param>
    /// <param name="reverseDirection">Reverse the order of direction (moving backwards first then forwards)</param>
    /// <returns>Instantiated Platform</returns>
    private void CreatePlatform(GameObject prefab, Vector3 position, Transform parentTransform, float distance, float speed, bool reverseDirection)
    {
        GameObject instance = MonoBehaviour.Instantiate(prefab, parentTransform);
        instance.transform.position = position + new Vector3(0,0,-1);
        instance.GetComponent<PlatformMovement>().SetNewDistance(distance);
        instance.GetComponent<PlatformMovement>().ChangeSpeed(true, speed);
        instance.GetComponent<PlatformMovement>().ReverseDirection = reverseDirection;
        Platforms.Add(instance);//Keep Track of ALl platforms for later puzzle garbage clearence
    }
    
    /// <summary>
    /// Create a length of spikes 
    /// </summary>
    /// <param name="GroundPosition"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private Vector3 CreateSpike(Vector3 GroundPosition, int count)
    {
        GroundPosition += new Vector3(0, 0.09f);
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
        return PrevPosition + new Vector3(0.5f,-0.09f);
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
