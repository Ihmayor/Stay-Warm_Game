using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public string PickupName;
    public PropResources.PropType prop;

	// Use this for initialization
	void Start () {
		
	}
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            MenuManager.Instance.OpenInteractionMenu("Picked up '" + PickupName + "'");
            MenuManager.Instance.SetThought(collision.GetComponent<CharacterStatus>().CharacterName, PropResources.PropThoughts[PickupName]);
            PropResources.PickupActions[this.prop](collision);
            AudioClip PickupSound = PropResources.GetPickupAudio(this.prop);
            this.GetComponent<AudioSource>().clip = PickupSound;
            this.GetComponent<AudioSource>().Play();
            if (PickupSound != null)
            {
                Destroy(this.gameObject.GetComponent<BoxCollider2D>());
                if (this.prop  != PropResources.PropType.Paper)
                {
                    this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    Destroy(this.gameObject, this.GetComponent<AudioSource>().clip.length);
                }
            }
            else if (this.prop != PropResources.PropType.Paper)
                Destroy(this.gameObject);
            Destroy(this);
        }
    }
}
