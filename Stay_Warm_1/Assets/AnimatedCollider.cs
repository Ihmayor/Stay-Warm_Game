using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCollider : MonoBehaviour {

    public PolygonCollider2D[] AnimationFrameCollider;
    private PolygonCollider2D localCollider;

	// Use this for initialization
	void Start () {
        localCollider = gameObject.AddComponent<PolygonCollider2D>();
        localCollider.isTrigger = true;
        localCollider.pathCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetHitBox(int frame)
    {
        localCollider.SetPath(0, AnimationFrameCollider[frame].GetPath(0));

        //Handles when animation collider splits in two
        if (AnimationFrameCollider[frame].pathCount > 1)
        {
            localCollider.pathCount = 2;
            localCollider.SetPath(1, AnimationFrameCollider[frame].GetPath(1));
        }
        Debug.Log("==================================================");
        Debug.Log(AnimationFrameCollider[frame].name);
        Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite.name);
        Debug.Log("Frame" + frame);
        Debug.Log("==================================================");
    }
}
