using Assets.Resources.Scripts.GameTriggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoolingElementManager : PuzzleManager
{
    GameObject WindSource;
    private List<GameObject> WindSources;


    public CoolingElementManager()
    {
        WindSource = Resources.Load<GameObject>("Prefabs/Elements/WindSource");
        WindSources = new List<GameObject>();
    }

    public override void Puzzle0(Vector3 StartPosition)
    {
        GameObject Source =  MonoBehaviour.Instantiate(WindSource);
        Source.transform.position = StartPosition + new Vector3(28f,0.8f,0);
        WindSources.Add(Source);
    }

    public override void Puzzle1(Vector3 StartPosition)
    {
        GameObject Source = MonoBehaviour.Instantiate(WindSource);
        Source.transform.position = StartPosition + new Vector3(6f, 0.8f, 0);
        WindSources.Add(Source);
        Vector3 PreviousPosition = Source.transform.position;

        StartPosition += new Vector3(6f, 0.8f, 0);//Don't start the puzzle windsources too close to the vertical platform
        CreateBlizzard(StartPosition, new float[] { 5, 3, 3, 2, 2, 3, 3, 3, 1});
    }

    public override void Puzzle2(Vector3 StartPosition)
    {
    }

    public override void Puzzle3(Vector3 StartPosition)
    {
        throw new NotImplementedException();
    }

    public override void Puzzle4(Vector3 StartPosition)
    {
        throw new NotImplementedException();
    }

    public override void Puzzle5(Vector3 StartPosition)
    {
        throw new NotImplementedException();
    }

    private void CreateBlizzard(Vector3 StartPosition, float[] XPositionsVariants)
    {
        GameObject Source = MonoBehaviour.Instantiate(WindSource);
        Source.transform.position = StartPosition;
        WindSources.Add(Source);
        Vector3 PreviousPosition = Source.transform.position;
        foreach(float XPosition in XPositionsVariants)
        {
            Source = MonoBehaviour.Instantiate(WindSource);
            Source.transform.position = PreviousPosition + new Vector3(XPosition, 0, 0);
            WindSources.Add(Source);
            PreviousPosition = Source.transform.position;
        }
    }

    public void Clear()
    {
        foreach(GameObject source in WindSources)
        {
            source.GetComponent<WindCreation>().StopLoop();
            MonoBehaviour.Destroy(source);
        }
        WindSources.Clear();
    }
}
