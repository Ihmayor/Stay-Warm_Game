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
        Pushable = Resources.Load<GameObject>("Prefabs/Platforms/PushableBlock");
    }

    //Start Position of puzzle area 
    public override void Puzzle0(Vector3 StartPosition)
    {
        GameObject Pushable = MonoBehaviour.Instantiate(this.Pushable, null);
        Pushable.transform.position = StartPosition + new Vector3(17, 0, 0);
         Pushable = MonoBehaviour.Instantiate(this.Pushable, null);
        Pushable.transform.position = StartPosition + new Vector3(17, 0, 0);
         Pushable = MonoBehaviour.Instantiate(this.Pushable, null);
        Pushable.transform.position = StartPosition + new Vector3(17, 0, 0);
         Pushable = MonoBehaviour.Instantiate(this.Pushable, null);
        Pushable.transform.position = StartPosition + new Vector3(17, 0, 0);

    }

    public override void Puzzle1(Vector3 StartPosition)
    {
        throw new NotImplementedException();
    }

    public override void Puzzle2(Vector3 StartPosition)
    {
        throw new NotImplementedException();
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

    public void Clear()
    {
        foreach(GameObject push in Pushables)
        {
            MonoBehaviour.Destroy(push);
        }
    }
}
