using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCreation : MonoBehaviour {

    public float HeightVariationMin = 0.3f;
    public float HeightVariationMax = 0.7f;
    public float repeatMinTime = 3f;
    public float repeatMaxTime = 6f;

	// Use this for initialization
	void Start () {

        InvokeRepeating("LoopWind", 0, Random.Range(repeatMinTime, repeatMaxTime));
    }

    private void LoopWind()
    {
        //Instantiate Platforms
        GameObject Wind = Resources.Load<GameObject>("Prefabs/Elements/wind");
        GameObject SourceStart = this.gameObject;
        GameObject instance = Instantiate(Wind, null);
        instance.transform.position = SourceStart.transform.position - new Vector3(0.3f, Random.Range(HeightVariationMin, HeightVariationMax), 0);
    }

}
