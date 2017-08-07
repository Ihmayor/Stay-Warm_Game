using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingElementManager : MonoBehaviour
{
    #region Singleton instance
    public static WarmingElementManager Instance { private set; get; }
    #endregion

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
        loopColor = 0;
        foreach (string path in AudioChimePaths)
        {
            Chimes.Add(Resources.Load<AudioClip>(path));
        }
    }

    public Color FetchNextColor()
    {
        Color c = ColorsToChangeTo[loopColor++ % ColorsToChangeTo.Length];
        return c;
    }

    public AudioClip FetchRandomChime()
    {
        return Chimes[Random.Range(0, Chimes.Count)];
    }
}
