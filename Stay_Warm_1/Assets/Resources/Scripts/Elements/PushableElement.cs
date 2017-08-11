﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableElement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player") && (collision.otherCollider is CircleCollider2D))
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Prefabs/Sprites/NonCharacterSpriteSheet");
            this.GetComponent<SpriteRenderer>().sprite = sprites[9];
            if (!this.GetComponent<AudioSource>().isPlaying)
                this.GetComponent<AudioSource>().Play();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            Rigidbody2D rb2d = this.GetComponent<Rigidbody2D>();
            rb2d.AddForce(Vector2.one*collision.gameObject.GetComponent<CharacterStatus>().PushPower);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Prefabs/Sprites/NonCharacterSpriteSheet");
            this.GetComponent<SpriteRenderer>().sprite = sprites[10];
        }
    }
}