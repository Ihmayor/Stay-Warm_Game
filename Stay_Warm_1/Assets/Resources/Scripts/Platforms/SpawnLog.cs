using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLog : MonoBehaviour {

    #region Variables

    public int logCount = 1;
    public float minLogDistance = 0.1f;
    public float maxLogDistance = 0.5f;

    #endregion

    #region Spawn Logs

    // Use this for initialization
    void Start () {
    //    SpawnInitialSpikeObstacle();
    }

    private void SpawnInitialSpikeObstacle()
    {
        GameObject lightpole = GameObject.Find("lightpoleBridge");
        GameObject ground = GameObject.Find("MainGround");
        GameObject step = Resources.Load<GameObject>("Prefabs/Platforms/step");
        GameObject spike = Resources.Load<GameObject>("Prefabs/Platforms/spike");
        Vector3 OriginalPosition = new Vector3 (lightpole.transform.position.x+1.5f, ground.transform.position.y+0.22f, 0);
        //Generate set count of logs going on forward
        for (int i = 0; i <= logCount; i++)
        {
            GameObject newlyCreated = Instantiate(spike, null);
            newlyCreated.transform.position = OriginalPosition + new Vector3(Random.Range(minLogDistance, maxLogDistance), 0, 0);
            OriginalPosition = newlyCreated.transform.position;
        }

        OriginalPosition = new Vector3(lightpole.transform.position.x + 0.1f, ground.transform.position.y, 0);
        for (int i = 0; i<= 9; i++)
        {
            GameObject newlyCreated = Instantiate(step, null);
            newlyCreated.transform.position = OriginalPosition + new Vector3(0.3f, 0.06f, 0);
            OriginalPosition = newlyCreated.transform.position;
        }
    }

    #endregion
}
