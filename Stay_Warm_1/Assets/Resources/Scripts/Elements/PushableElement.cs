using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableElement : MonoBehaviour {

    private Collision2D PlayerCollision = null;


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
            collision.gameObject.GetComponent<CharacterStatus>().isBehindCoolingBlock = true;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            Rigidbody2D rb2d = this.GetComponent<Rigidbody2D>();
            rb2d.AddForce(Vector2.one*collision.gameObject.GetComponent<CharacterMovement>().pushForce);
            collision.gameObject.GetComponent<CharacterStatus>().isBehindCoolingBlock = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Prefabs/Sprites/NonCharacterSpriteSheet");
            this.GetComponent<SpriteRenderer>().sprite = sprites[10];
            StartCoroutine(MomentaryCoolImmunity(collision));
        }
    }

    private IEnumerator MomentaryCoolImmunity(Collision2D collision)
    {
        PlayerCollision = collision;
        yield return new WaitForSeconds(0.7f);
        collision.gameObject.GetComponent<CharacterStatus>().isBehindCoolingBlock = false;
        PlayerCollision = null;
    }

    private void OnDestroy()
    {
        //If we complete the level while still touching a pushable object
        if (PlayerCollision != null)
            PlayerCollision.gameObject.GetComponent<CharacterStatus>().isBehindCoolingBlock = false;
    }


}
