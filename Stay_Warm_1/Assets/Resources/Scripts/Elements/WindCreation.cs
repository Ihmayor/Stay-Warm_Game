using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCreation : MonoBehaviour {

    public float HeightVariationMin = 0.3f;
    public float HeightVariationMax = 0.7f;
    public float repeatMin = 3f;
    public float repeatMax = 6f;

	// Use this for initialization
	void Start () {

        //Reference this instance as singleton instance
        //  InstantiateAndDemoMovement();
        InvokeRepeating("LoopWind", 0, Random.Range(repeatMin, repeatMax));
    }

    private void LoopWind()
    {
        //Instantiate Platforms
        GameObject Wind = Resources.Load<GameObject>("Prefabs/wind");
        GameObject SourceStart = this.gameObject;
        GameObject instance = Instantiate(Wind, null);
        instance.transform.position = SourceStart.transform.position - new Vector3(0.3f, Random.Range(HeightVariationMin, HeightVariationMax), 0);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
