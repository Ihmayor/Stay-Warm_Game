using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReactivateCharacter : MonoBehaviour {
    internal PlayableDirector timeline;
    public GameObject Character;
    // Use this for initialization
    void Awake () {
        timeline = GetComponent<PlayableDirector>();
        timeline.stopped += Timeline_stopped;
    }

    private void OnDisable()
    {
        Character.GetComponent<CharacterMovement>().enabled = true;
    }
    internal virtual void Timeline_stopped(PlayableDirector obj)
    {
        Character.GetComponent<CharacterMovement>().enabled = true;
        Character.GetComponent<CharacterStatus>().SetInCutscene(false);

    }


}
