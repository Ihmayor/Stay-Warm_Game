using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployFog : MonoBehaviour {

    private GameObject Fog;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Fog != null)
        {
            Fog.transform.position += new Vector3(0.005f,0,0);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("here??");
            Invoke("FogCrawl", 0.0005f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("here??222");
            Invoke("FogDelete", 30f);
        }
    }

    private void FogCrawl()
    {
        Debug.Log("here??333");

        GameObject prefab = Resources.Load<GameObject>("Sprites/_Environment/Fog");
        if (Fog == null)
        {
            Fog = Instantiate(prefab, this.gameObject.transform); 
            Fog.transform.position += new Vector3(14,8, this.gameObject.transform.position.z);
            Debug.Log(Fog.transform.position);
        }
    }

    private void FogDelete()
    {
    //    Debug.Log("here??44");
    //    Destroy(Fog);
    //    Fog = null;
    }


}
