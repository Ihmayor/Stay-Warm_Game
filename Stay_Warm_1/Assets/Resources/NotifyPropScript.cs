using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyPropScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            transform.Find("Exclaim").gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.Find("Exclaim").gameObject.SetActive(false);
        }
    }
}
