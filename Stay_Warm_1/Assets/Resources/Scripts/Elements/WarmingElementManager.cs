using Assets.Resources.Scripts.GameTriggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingElementManager : PuzzleManager
{
    private GameObject LightPolePrefab;
    private GameObject FlowerShowerPrefab;
    private GameObject CurrentLightPole;
    private GameObject PrevLightPole;
    private List<GameObject> AllMidPointPoles;
    public bool IsNextLevel;

    private Color[] ColorsToChangeTo = {
        new Color(0.90588235294f,0.41176470588f,0.30196078431f),
        new Color(0.9725490196f,0.70196078431f,0.41568627451f),
        new Color(0.89411764705f,0.43529411764f,0.64705882352f),
        new Color(0.87450980392f,0.50196078431f,0.53725490196f),
        new Color(0.85882352941f,0.89019607843f,0.34509803921f)
    };

    private string[] AudioChimePaths = {
        "Audio/husky_echo_chime-chime",
        "Audio/copperbells_admbar",
        "Audio/insepctorj_ambient_chimes",
        "Audio/monte32_wind-chimes" };

    private List<AudioClip> Chimes = new List<AudioClip>();

    private int loopColor;

    public WarmingElementManager()
    {
        LightPolePrefab = Resources.Load<GameObject>("Prefabs/Elements/lightpole");
        FlowerShowerPrefab = Resources.Load<GameObject>("Prefabs/Platforms/Particles/FlowerShower");
        loopColor = 0;
        AllMidPointPoles = new List<GameObject>();
        foreach (string path in AudioChimePaths)
        {
            Chimes.Add(Resources.Load<AudioClip>(path));
        }
        IsNextLevel = false;
    }

    #region Puzzle Methods

    public override void Puzzle0(Vector3 PuzzleStartPosition)
    {
        Vector3 LastPosition = CreateMidPoint(PuzzleStartPosition + new Vector3(20f, 0));
        CreateEndPoint(LastPosition + new Vector3(24f, 0, 0));
    }

    public override void Puzzle1(Vector3 PuzzleStartPosition)
    {
        CreateEndPoint(PuzzleStartPosition + new Vector3(35f, 0, 0));
    }

    public override void Puzzle2(Vector3 PuzzleStartPosition)
    {
        CreateEndPoint(PuzzleStartPosition + new Vector3(65f, 0, 0));
        Vector3 LastPosition = CreateMidPoint(PuzzleStartPosition + new Vector3(21.31f, 2.08f));
        CreateMidPoint(LastPosition + new Vector3(25f, -1.06f, 0));

    }

    public override void Puzzle3(Vector3 PuzzleStartPosition)
    {
        CreateEndPoint(PuzzleStartPosition + new Vector3(82f, 0, 0));
        CreateMidPoint(PuzzleStartPosition + new Vector3(25.65f, 4.98f, 0));
        CreateMidPoint(PuzzleStartPosition + new Vector3(71.745f, 19.5f, 0));

    }

    public override void Puzzle4(Vector3 PuzzleStartPosition)
    {
        CreateEndPoint(PuzzleStartPosition + new Vector3(58f, 0, 0));
    }

    public override void Puzzle5(Vector3 PuzzleStartPosition)
    {

    }

    #endregion

    #region Creation Methods
    public Vector3 CreateEndPoint(Vector3 GroundedStartPosition)
    {
        GameObject gObj = MonoBehaviour.Instantiate(LightPolePrefab, null);
        gObj.tag = "EndPoint";
        gObj.transform.position = GroundedStartPosition + new Vector3(0, 0.681f, 0);
        WarmingElement warmingScript = gObj.GetComponent<WarmingElement>();
        warmingScript.Color = FetchNextColor();
        warmingScript.Sound = FetchRandomChime();
        warmingScript.FirstVisit += WarmingScript_FirstVisit;
        MonoBehaviour.Instantiate(FlowerShowerPrefab, gObj.transform);

        if (CurrentLightPole == null)
            CurrentLightPole = gObj;
        else if (PrevLightPole == null)
        {
            PrevLightPole = CurrentLightPole;
            PrevLightPole.gameObject.tag = "WarmPoint";
            foreach (GameObject midPoint in AllMidPointPoles)
            {
                if (midPoint.transform.position.x > PrevLightPole.transform.position.x)
                    MonoBehaviour.Destroy(midPoint);
            }
            CurrentLightPole = gObj;
        }
        else
        {

            MonoBehaviour.Destroy(PrevLightPole);
            PrevLightPole = CurrentLightPole;
            PrevLightPole.gameObject.tag = "WarmPoint";
            CurrentLightPole = gObj;

            foreach (GameObject midPoint in AllMidPointPoles)
            {
                if (PrevLightPole != null && midPoint != null && midPoint.transform.position.x > PrevLightPole.transform.position.x)
                    MonoBehaviour.Destroy(midPoint);
            }
        }
        return GroundedStartPosition;
    }

    /// <summary>
    /// Both Sender and Arguments will be null
    /// </summary>
    /// <param name="sender">Null do not use</param>
    /// <param name="e">Null do not use</param>
    private void WarmingScript_FirstVisit(object sender, System.EventArgs e)
    {
        ToggleIsNextLevel();
    }

    public Vector3 CreateMidPoint(Vector3 GroundedStartPosition)
    {
        GameObject gObj = MonoBehaviour.Instantiate(LightPolePrefab, null);
        gObj.transform.position = GroundedStartPosition + new Vector3(0, 0.681f, 0);
        gObj.GetComponent<WarmingElement>().Color = FetchNextColor();
        gObj.GetComponent<WarmingElement>().Sound = FetchRandomChime();
        gObj.tag = "WarmPoint";
        AllMidPointPoles.Add(gObj);
        return GroundedStartPosition;
    }

    //TODO AFTER YOU HAVE FINISHED ALL THE PUZZLES AND/OR IF IT REALLY IS REALLY REALLY SLOW.
    public void CreateObjectPool()
    {
        //Load and Instantiate
    }

    //TODO AFTER YOU HAVE FINISHED ALL THE PUZZLES AND/OR IF IT REALLY IS REALLY REALLY SLOW.
    public Vector3 CreateEndPointOptimized(Vector3 GroundedStartPosition)
    {
        return Vector3.zero;
    }

    #endregion

    #region Misc Helper Methods
    public void ToggleIsNextLevel()
    {
        IsNextLevel = !IsNextLevel;
    }

    private Color FetchNextColor()
    {
        Color c = ColorsToChangeTo[loopColor++ % ColorsToChangeTo.Length];
        return c;
    }

    private AudioClip FetchRandomChime()
    {
        return Chimes[Random.Range(0, Chimes.Count)];
    }

    #endregion
}
