using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour {

    public PlayableDirector timeline;

    // Use this for initialization
    void Start()
    {
    }

    void OnTriggerExit2D(Collider2D c)
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            timeline.Stop(); // Make sure the timeline is stopped before starting it
            timeline.Play();
        }
    }
}
