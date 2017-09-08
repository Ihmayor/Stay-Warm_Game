using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolingElement : MonoBehaviour {

    internal AudioClip CoolingSound;

    public virtual void Start() { }

    public virtual void Update() { }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && 
            !collision.gameObject.GetComponent<CharacterStatus>().isBehindCoolingBlock)
        {
            //If this is the first encounter, character should act differently
            if (collision.gameObject.GetComponent<CharacterStatus>().isFirstCooling)
                collision.gameObject.GetComponent<CharacterStatus>().ToggleOffFirstCooling();

            collision.gameObject.GetComponent<CharacterStatus>().SetHeartCooling(true);
            this.GetComponent<AudioSource>().PlayOneShot(CoolingSound);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
           !collision.gameObject.GetComponent<CharacterStatus>().isBehindCoolingBlock)
        {
            collision.gameObject.GetComponent<CharacterStatus>().IncreaseCoolingStrength();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterStatus>().SetHeartCooling(false);
        }
    }
}
