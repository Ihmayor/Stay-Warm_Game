using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WarmingElement : MonoBehaviour {
    public float WarmingFactor;
    

    // Use this for initialization
    void Start () {
        WarmingFactor = 0.004f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterStatus>().SetHeartCooling(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterStatus>().WarmHeart(WarmingFactor);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject Character = collision.gameObject;

            Character.GetComponent<CharacterStatus>().CheckCharacterHealth(false);
            if (Character.GetComponent<CharacterStatus>().isFightingPlayer)
            {
                Character.transform.position = new Vector2(this.gameObject.transform.position.x, Character.transform.position.y);
                Character.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                Character.GetComponent<CharacterStatus>().FightPlayer();
            }

            if (this.gameObject.GetComponent<SpriteRenderer>().color == Color.white)
            {

                this.gameObject.GetComponent<SpriteRenderer>().color = WarmingElementManager.Instance.FetchNextColor();
                this.GetComponent<AudioSource>().PlayOneShot(WarmingElementManager.Instance.FetchRandomChime());
                this.GetComponent<AudioSource>().loop = false;
            }


        }
    }


}
