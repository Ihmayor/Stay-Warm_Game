using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour {

    public PlayableDirector timeline;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        timeline.stopped += Timeline_stopped;
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {

            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            player = c.gameObject;
            player.GetComponent<CharacterMovement>().enabled = false;
            player.GetComponent<CharacterMovement>().setAnimation("Idle");
            timeline.Play();
        }

    }

    private void Timeline_stopped(PlayableDirector obj)
    {
        player.GetComponent<CharacterMovement>().enabled = true;
        player.GetComponent<CharacterMovement>().setAnimation("Idle");
    }

    public void SetTimeline(PlayableDirector cutsceneLoaded)
    {
        timeline = cutsceneLoaded;
    }
}
