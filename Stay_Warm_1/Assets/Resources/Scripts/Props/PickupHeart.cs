using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHeart : Pickup {

	// Use this for initialization
	void Start () {
		base.Sound = Resources.Load<AudioClip>("Audio/124546__cubix__waterdrop");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PickUpAction(object[] arg)
    {
        base.PickUpAction(arg);
        Collider2D collider = (Collider2D)arg[0];
        MenuManager.Instance.ActivateHeartMeter();
        collider.gameObject.GetComponent<CharacterStatus>().hasHeart = true;
    }
}
