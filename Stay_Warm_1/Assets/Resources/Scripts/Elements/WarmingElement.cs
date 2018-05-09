using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WarmingElement : MonoBehaviour
{
    public float WarmingFactor;
    public AudioClip Sound;
    public UnityEngine.Color Color;
    public event EventHandler FirstVisit; 

    // Use this for initialization
    void Start()
    {
        WarmingFactor = 0.004f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterStatus charStatus = collision.gameObject.GetComponent<CharacterStatus>();
            
            //If this is the first encounter, character should react accordingly
            if (charStatus.isFirstWarming)
                charStatus.ToggleOffFirstWarming();

            //Lessen how much player is able to move if character is fighting against them.
            if (charStatus.isFightingPlayer)
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            charStatus.SetHeartCooling(false);
            charStatus.isHeartWarming = true;
            charStatus.GetComponentInChildren<SmokeScript>().SetSmokeColor(Color);

            if (this.gameObject.GetComponent<SpriteRenderer>().color == Color.white)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color;
                this.GetComponent<AudioSource>().PlayOneShot(Sound);
                this.GetComponent<AudioSource>().loop = false;
                try
                {
                    FirstVisit(gameObject, new EventArgs());
                }
                catch(NullReferenceException ex)
                {
                    Debug.Log(ex.Message);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Lessen how much player is able to move if character is fighting against them.
            if (collision.gameObject.GetComponent<CharacterStatus>().isFightingPlayer)
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            collision.gameObject.GetComponent<CharacterStatus>().WarmHeart(WarmingFactor);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject Character = collision.gameObject;
            CharacterStatus charStatus = Character.GetComponent<CharacterStatus>();

            charStatus.CheckCharacterHealth(false);
            if (charStatus.isFightingPlayer)
            {
                Character.transform.position = new Vector2(this.gameObject.transform.position.x, Character.transform.position.y);
                Character.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                charStatus.FightPlayer();
            }
            charStatus.isHeartWarming = false;
        }
    }


}
