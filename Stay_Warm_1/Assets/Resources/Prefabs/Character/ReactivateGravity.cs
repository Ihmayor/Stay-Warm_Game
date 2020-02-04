using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReactivateGravity: ReactivateCharacter {

   // Use this for initialization
	void Awake() {
        timeline = GetComponent<PlayableDirector>();
        timeline.stopped += Timeline_stopped;
	}
    private void OnDisable()
    {
        Character.GetComponent<CharacterMovement>().enabled = true;
        Character.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    internal override void Timeline_stopped(PlayableDirector obj)
    {
        Character.GetComponent<CharacterMovement>().enabled = true;
        Character.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

}
