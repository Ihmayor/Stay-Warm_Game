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

        //Instantiate Platforms
        //Ground is 3.3f
        GameObject VerticalPlatform = Resources.Load<GameObject>("VerticalPlatform");
        GameObject HorizontalPlatform = Resources.Load<GameObject>("HorizontalPlatform");

        //Reference Point of 0.0
        GameObject MainBackgroundParent = GameObject.Find("Background");
        GameObject Bush = MainBackgroundParent.transform.Find("Bush").gameObject;
        Vector3 startPosition = Bush.transform.position;

        int HozPlatforms = 10;

        //Create A Vertical Platform to start
        GameObject instance = Instantiate(VerticalPlatform, MainBackgroundParent.transform);
        instance.transform.position = startPosition + new Vector3(0.4f, 1f, 0);
        instance.GetComponent<PlatformMovement>().SetNewDistance(1.2f);
        instance.GetComponent<PlatformMovement>().ChangeSpeed(true, 10000);
        instance.GetComponent<PlatformMovement>().ReverseDirection = true;

        //Previous platform position. Adjust such that we are at the height of the starter vertical platform
        Vector3 prevPlatformPosition = instance.transform.position + new Vector3(0.9f, 1f, 0);

        //Start Horizontal Platform bridge across the sky.
        for (int i = 0; i <= HozPlatforms; i++)
        {
            GameObject newPlatform = Instantiate(HorizontalPlatform, MainBackgroundParent.transform);
            newPlatform.transform.position = prevPlatformPosition + new Vector3(Random.Range(0.5f,1f)+ HorizontalPlatform.transform.localScale.x, 0, 0);
            newPlatform.GetComponent<PlatformMovement>().SetNewDistance(Random.Range(1.5f,2.5f));
            if (Random.Range(0, 1) == 1)
            {
                newPlatform.GetComponent<PlatformMovement>().ReverseDirection = true;
            }

            int SelectSpeed = Random.Range(0,10);
            if (SelectSpeed == 5)
            {
                newPlatform.GetComponent<PlatformMovement>().ChangeSpeed(true, Random.Range(0.3f,0.6f));
            }
            else if (SelectSpeed ==7) 
            {
                newPlatform.GetComponent<PlatformMovement>().ChangeSpeed(false, Random.Range(0.3f, 0.6f));
            }

            prevPlatformPosition = newPlatform.transform.position;
        }
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
