﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolingElement : MonoBehaviour {

    [Range(0.026f, 0.036f)]
    private float FadeFactor;
    private float DriftSpeed;
    public bool isRightDirection;
	// Use this for initialization
	void Start () {
        FadeFactor = 0.026f;
        DriftSpeed = 0.01f;
        isRightDirection = false;
    }
	
	// Update is called once per frame
	void Update () {
        float exponentIncrease = 1.5f; //How fast it fades. This is the ideal value found.
        Color oldColor = this.GetComponent<SpriteRenderer>().color;
        if (oldColor.a > 0) {
            this.GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a - Mathf.Pow(FadeFactor, exponentIncrease));
            Vector3 WindPositionChange = new Vector3(DriftSpeed, 0, 0);
            if (isRightDirection)
                WindPositionChange *= -1;
            this.gameObject.transform.position -= WindPositionChange;
        }
        else
            Destroy(gameObject);
	}

    private void Fade()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterStatus>().SetHeartCooling(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 WindPositionChange = new Vector3(DriftSpeed*4f, 0, 0);
            if (isRightDirection)
                WindPositionChange *= -1;
            collision.gameObject.transform.position -= WindPositionChange;

            collision.gameObject.GetComponent<CharacterStatus>().IncreaseCoolingStrength();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterStatus>().SetHeartCooling(false);
        }
    }
}