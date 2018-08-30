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
        print("test");
        StartCoroutine(DelaySecondsPickupAction(arg));
    }
    public override void DestroyPickupObject()
    {
        print("test2");
        StartCoroutine(DelayDestroyObject());
    }

    IEnumerator DelayDestroyObject()
    {
        yield return new WaitForSeconds(3.5f);
        base.DestroyPickupObject();
        print("Done");
    }

    IEnumerator DelaySecondsPickupAction(object[] arg)
    {
        base.PickUpAction(arg);
        yield return new WaitForSeconds(0.5f);
        Collider2D collider = (Collider2D)arg[0];
        MenuManager.Instance.ActivateHeartMeter();
        collider.gameObject.GetComponent<CharacterStatus>().ActivateHeartEffect();
        print("done2");
    }
}
