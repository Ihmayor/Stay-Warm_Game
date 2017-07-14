using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLog : MonoBehaviour {

    #region Variables

    public int logCount = 30;
    public float minLogDistance = 0.5f;
    public float maxLogDistance = 1f;
    public string LogSpriteLocation = "2D/Sprites/CratePinkGrid";

    #endregion

    #region Spawn Logs

    // Use this for initialization
    void Start () {

        //Load Log Sprite. Attach to Background parent for organizational purposes
        GameObject Background = GameObject.Find("Background");
        GameObject Log = Resources.Load<GameObject>(LogSpriteLocation);

        //Instantiate past bush startpoint
        GameObject Bush = Background.transform.Find("Bush").gameObject;
        Vector3 OriginalPosition = Bush.transform.position;
        OriginalPosition += new Vector3(0, -0.3f, 0);

        //Generate set count of logs going on forward
        for (int i = 0; i <= logCount;i++)
        {
            GameObject newlyCreated = Instantiate(Log,Background.transform);
            newlyCreated.transform.position = OriginalPosition + new Vector3(Random.Range(minLogDistance, maxLogDistance),0,0);
            OriginalPosition = newlyCreated.transform.position;
        }

    }

    #endregion
}
