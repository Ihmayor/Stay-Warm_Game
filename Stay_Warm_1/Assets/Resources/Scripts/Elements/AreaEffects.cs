using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffects : MonoBehaviour
{
    public float test = 0f;
    public string[] ThoughtsInArea;
    public float ThoughtDelayTime = 40;
    private int ThoughtCount = -1;

    private float SinceLastThought;


    // Use this for initialization
    void Start()
    {
        if (ThoughtsInArea.Length > 0)
        {
            ThoughtCount = 0;
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float currentTime = Time.fixedTime;
        if (collision.gameObject.tag.Contains("Player"))
        {
            GameObject player = collision.gameObject;

            //Set time since last thought
            if (SinceLastThought == 0)
            {
                SinceLastThought = currentTime;
                return;
            }

            float timeDifference = currentTime - SinceLastThought;

            //Check if we have any thoughts
            if (ThoughtCount >= 0 && timeDifference > ThoughtDelayTime)
            {
                MenuManager.Instance.SetThought(player.GetComponent<CharacterStatus>().CharacterName, ThoughtsInArea[ThoughtCount]);

                if (ThoughtCount + 1 < ThoughtsInArea.Length)
                    ThoughtCount++;

                SinceLastThought = currentTime;
            }
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(this.gameObject);
        Destroy(this);
    }
}
