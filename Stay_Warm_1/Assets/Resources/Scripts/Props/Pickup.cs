using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public AudioClip PickUpSound;
    public string PickupName;
	// Use this for initialization
	void Start () {
		
	}
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MenuManager.Instance.OpenInteractionMenu("Picked up '" + PickupName + "'");
        this.GetComponent<AudioSource>().clip = PickUpSound;
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        Destroy(this.gameObject, PickUpSound.length);

    }
}
