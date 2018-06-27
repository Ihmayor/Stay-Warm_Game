using Assets.Resources.Scripts.GameTriggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PushableElementManager : PuzzleManager
{
    GameObject Pushable;
    List<GameObject> Pushables;

    public PushableElementManager()
    {
        Pushables = new List<GameObject>();
        Pushable = Resources.Load<GameObject>("Prefabs/Platforms/PushableBox");
    }

    //Start Position of puzzle area 
    public override void Puzzle0(Vector3 StartPosition)
    {
        GameObject pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(16, 2f, 0);
        Pushables.Add(pushObj);

        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(17f, 0.5f, 0);
        Pushables.Add(pushObj);

        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(15, 1.2f, 0);
        Pushables.Add(pushObj);

        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(15.5f, 1.5f, 0);
        Pushables.Add(pushObj);

        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(35.5f, 3f, 0);
        Pushables.Add(pushObj);

        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(35.5f, 4f, 0);
        Pushables.Add(pushObj);

        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(35.5f, 0.5f, 0);
        Pushables.Add(pushObj);

        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(35.5f, 5f, 0);
        Pushables.Add(pushObj);

    }

    public override void Puzzle1(Vector3 StartPosition)
    {
    }

    public override void Puzzle2(Vector3 StartPosition)
    {
        int PushableCount1 = 1;
        int PushableCount2 = 1;
        int PushableCount3 = 3;
        GameObject pushObj;
        Vector3 prevPosition = Vector3.zero;
        for (int i = 0;i < PushableCount1; i++ )
        {
            pushObj = MonoBehaviour.Instantiate(Pushable, null);
            pushObj.transform.position = StartPosition + new Vector3(45, 2f, 0);
            Pushables.Add(pushObj);
        }

        for (int i =0; i < PushableCount2;i++)
        {
            pushObj = MonoBehaviour.Instantiate(Pushable, null);
            pushObj.transform.position = StartPosition + new Vector3(45.7f, 2f, 0);
            Pushables.Add(pushObj);
        }

        for (int i = 0; i< PushableCount3; i++)
        {
            pushObj = MonoBehaviour.Instantiate(Pushable, null);
            pushObj.transform.position = StartPosition + new Vector3(47, 2f, 0);
            Pushables.Add(pushObj);
        }
        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(52.9f, 2f, 0);
        Pushables.Add(pushObj);

        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(53f, 2f, 0);
        Pushables.Add(pushObj);
    }

    public override void Puzzle3(Vector3 StartPosition)
    {
    }

    public override void Puzzle4(Vector3 StartPosition)
    {
        GameObject pushObj;
        for (int i = 0; i < 3; i++)
        {
            pushObj = MonoBehaviour.Instantiate(Pushable, null);
            pushObj.transform.position = StartPosition + new Vector3(0.8f, 2f, 0);
            Pushables.Add(pushObj);
        }

        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(23f, 2f, 0);
        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(23f, 3f, 0);
        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(23f, 5f, 0);


        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(38f, 5f, 0);


        pushObj = MonoBehaviour.Instantiate(Pushable, null);
        pushObj.transform.position = StartPosition + new Vector3(110f, 2f, 0);

    }

    public override void Puzzle5(Vector3 StartPosition)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        foreach(GameObject push in Pushables)
        {
            MonoBehaviour.Destroy(push);
        }
    }
}
