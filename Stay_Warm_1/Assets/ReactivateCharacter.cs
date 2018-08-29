﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReactivateCharacter : MonoBehaviour {
    internal PlayableDirector timeline;
    public GameObject Character;
    // Use this for initialization
    void Start () {
        Character.GetComponent<CharacterMovement>().enabled = false;
        timeline.stopped += Timeline_stopped;
    }

    internal virtual void Timeline_stopped(PlayableDirector obj)
    {
        Character.GetComponent<CharacterMovement>().enabled = true;
    }

 
}
