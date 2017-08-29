using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCreation : MonoBehaviour {

    public float HeightVariationMin = 0.3f;
    public float HeightVariationMax = 0.7f;
    public float repeatMinTime = 3f;
    public float repeatMaxTime = 6f;

    [Range(8,15)]
    public int NumWindInstances = 8;

    private GameObject[] totalWindInstances; //Increase iff repeat MinTime and repeatMaxTime have been changed

    private int WindIndex;

	// Use this for initialization
	void Start () {
        WindIndex = 0;
        GameObject WindPrefab = Resources.Load<GameObject>("Prefabs/Elements/wind");
        totalWindInstances = new GameObject[NumWindInstances];

        for (int i = 0; i< NumWindInstances;i ++)
        {
            GameObject instance = Instantiate(WindPrefab, null);
            instance.SetActive(false);
            totalWindInstances[i] = instance;
        }
        InvokeRepeating("LoopWind", 0, Random.Range(repeatMinTime, repeatMaxTime));
    }

    private void LoopWind()
    {
        //Instantiate Platforms
        GameObject SourceStart = this.gameObject;
        GameObject instance = totalWindInstances[WindIndex];
        instance.transform.position = SourceStart.transform.position - new Vector3(0.3f, Random.Range(HeightVariationMin, HeightVariationMax), 0);
        if (!instance.activeSelf)
        {
          //  instance.SetActive(true);
            instance.GetComponent<CoolingElement>().Reactivate();
        }

        WindIndex++;
        WindIndex%=NumWindInstances-1;
    }

    public void RestartLoop()
    {
        InvokeRepeating("LoopWind", 0, Random.Range(repeatMinTime, repeatMaxTime));
    }

    public void StopLoop()
    {
        CancelInvoke("LoopWind");
    }

}
