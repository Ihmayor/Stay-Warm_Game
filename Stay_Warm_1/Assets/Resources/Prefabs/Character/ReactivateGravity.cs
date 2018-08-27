using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReactivateGravity : MonoBehaviour {

    PlayableDirector timeline;
    public GameObject Character;
	// Use this for initialization
	void Start () {
        Character.GetComponent<CharacterMovement>().enabled = false;
        timeline = GetComponent<PlayableDirector>();
        timeline.stopped += ReactivateGravity_stopped;
	}

    private void ReactivateGravity_stopped(PlayableDirector obj)
    {
        Character.GetComponent<Rigidbody2D>().gravityScale = 1;
        Character.GetComponent<CharacterMovement>().enabled = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
