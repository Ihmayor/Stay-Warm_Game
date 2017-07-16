using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    #region Singleton instance
    public static PlatformManager Instance { private set; get; }
    #endregion
    // Use this for initialization
    void Start () {

        //Reference this instance as singleton instance
        PlatformManager.Instance = this;

        DemoMovement();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DemoMovement()
    {
        //Get Demo GameObjects
        GameObject PlatformVertical = GameObject.Find("Background").transform.Find("vert").gameObject;
        GameObject PlatformHorizontal = GameObject.Find("Background").transform.Find("Hoz").gameObject;
        PlatformVertical.GetComponent<PlatformMovement>().SetNewDistance(0.5f);
        PlatformHorizontal.GetComponent<PlatformMovement>().SetNewDistance(3f);
    }
}
