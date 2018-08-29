using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReactivateGravity: ReactivateCharacter {

   // Use this for initialization
	void Start () {
        timeline = GetComponent<PlayableDirector>();
        timeline.stopped += Timeline_stopped;
	}

    internal override void Timeline_stopped(PlayableDirector obj)
    {
        Character.GetComponent<Rigidbody2D>().gravityScale = 1;
        Character.GetComponent<CharacterMovement>().enabled = true;
    }

}
