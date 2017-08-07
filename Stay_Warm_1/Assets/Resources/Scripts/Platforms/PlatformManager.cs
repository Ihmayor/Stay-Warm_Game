﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    #region Singleton instance
    public static PlatformManager Instance { private set; get; }
    #endregion
    // Use this for initialization
    void Start () {

        //Reference this instance as singleton instance
        PlatformManager.Instance = this;
        Puzzle1(GameObject.Find("lightpoleBridge").transform.position + new Vector3(4f, 1.5f, 0));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Puzzle1(Vector3 StartPosition)
    {
        List<GameObject> Platforms = new List<GameObject>();
        GameObject VerticalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/VerticalPlatform");
        GameObject HorizontalPlatform = Resources.Load<GameObject>("Prefabs/Platforms/HorizontalPlatform");

        int HozPlatforms = 15;

        //Create A Vertical Platform to start
        Platforms.Add(CreatePlatform(VerticalPlatform, StartPosition, null, 1.5f, 500, false));

        //Previous platform position. Adjust such that we are at the height of the starter vertical platform
        Vector3 prevPlatformPosition = Platforms[Platforms.Count-1].transform.position + new Vector3(1.1f, 1f, 0);

        //Start Horizontal Platform bridge across the sky.
        for (int i = 0; i <= HozPlatforms; i++)
        {
            float DistanceMovementVariance = Random.Range(1, 3);
            Vector3 DistanceVariance = new Vector3(Random.Range(DistanceMovementVariance, DistanceMovementVariance+0.1f),0,0);
            Vector3 PlatformPosition = prevPlatformPosition + DistanceVariance;
            float SpeedVariance = Random.Range(10, 30);
            bool DirectionVariance = Random.value < 0.5f;
            Platforms.Add(CreatePlatform(HorizontalPlatform, PlatformPosition, null, DistanceMovementVariance, SpeedVariance, DirectionVariance));
            prevPlatformPosition = PlatformPosition;
        }
    }

    private GameObject CreatePlatform(GameObject prefab, Vector3 position, Transform parentTransform, float distance, float speed, bool reverseDirection )
    {
        GameObject instance = Instantiate(prefab, parentTransform);
        instance.transform.position = position;
        instance.GetComponent<PlatformMovement>().SetNewDistance(distance);
        instance.GetComponent<PlatformMovement>().ChangeSpeed(true, speed);
        instance.GetComponent<PlatformMovement>().ReverseDirection = reverseDirection;
        return instance;
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
