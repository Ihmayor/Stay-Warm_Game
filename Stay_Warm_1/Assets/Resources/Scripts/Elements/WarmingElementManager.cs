using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingElementManager : MonoBehaviour
{
    #region Static instance based on gameobject in scene
    public static WarmingElementManager Instance { private set; get; }
    #endregion

    private GameObject LightPolePrefab;
    private GameObject FlowerShowerPrefab;

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

    // Use this for initialization
    void Start()
    {
        Instance = this;
        LightPolePrefab = Resources.Load<GameObject>("Elements/lightpole");
        FlowerShowerPrefab = Resources.Load<GameObject>("Platforms/Particles/FlowerShower");
        loopColor = 0;
        foreach (string path in AudioChimePaths)
        {
            Chimes.Add(Resources.Load<AudioClip>(path));
        }
    }

    #region Puzzle Methods

    public void Puzzle0(Vector3 PuzzleStartPosition)
    {
        GameObject gObj = Instantiate(LightPolePrefab, null);
        gObj.transform.position = PuzzleStartPosition + new Vector3(10f, 0, 0);
        Instantiate(FlowerShowerPrefab, gObj.transform);
    }

    public void Puzzle1(Vector3 PuzzleStartPosition)
    {

    }

    public void Puzzle2(Vector3 PuzzleStartPosition)
    {

    }

    public void Puzzle3(Vector3 PuzzleStartPosition)
    {

    }

    public void Puzzle4(Vector3 PuzzleStartPosition)
    {

    }

    public void Puzzle5(Vector3 PuzzleStartPosition)
    {

    }

    #endregion

    #region Misc Helper Methods

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
