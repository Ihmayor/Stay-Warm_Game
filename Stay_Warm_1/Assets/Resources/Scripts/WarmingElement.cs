using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingElement : MonoBehaviour {
    public float WarmingFactor;

    // Use this for initialization
    void Start () {
        WarmingFactor = 0.004f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterStatus>().SetHeartCooling(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterStatus>().WarmHeart(WarmingFactor);
        }
    }

    
}
