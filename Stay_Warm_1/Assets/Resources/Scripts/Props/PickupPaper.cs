using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPaper : Pickup {

	// Use this for initialization
	void Start () {
		base.Sound = Resources.Load<AudioClip>("Audio/paperflip");
    }

    public override void PickUpAction(object[] arg)
    {
        base.PickUpAction(arg);
        Collider2D collider = (Collider2D)arg[0];
        collider.GetComponent<CharacterStatus>().WarmHeart(0.01f);
        Collider2D[] nearby = new Collider2D[10];
        collider.GetContacts(nearby);
        foreach (Collider2D c in nearby)
        {
            if (c == null)
                break;
            if (c.GetComponent<Pickup>() != null)
                c.gameObject.AddComponent<FloatAndFollow>();
        }
    }

    public override void DestroyPickupObject(){}
}
