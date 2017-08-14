using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public string PickupName;
    internal AudioClip Sound;


    public static Dictionary<string, string> PropThoughts = new Dictionary<string, string>() {
        {"Note #1","There's nothing on it, but it feels warm. ... Oh!"},
        {"Note #2","Another one..."},
        {"Note #3","It's nice not being alone."},
        {"Glass Heart","This...heart. It feels important. I...I should take care of it."}
    };

    // Use this for initialization
    void Start () {
		
	}
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            MenuManager.Instance.OpenInteractionMenu("Picked up '" + PickupName + "'");
            MenuManager.Instance.SetThought(collision.GetComponent<CharacterStatus>().CharacterName, PropThoughts[PickupName]);
            PickUpAction(new object[] { collision });
             if (Sound != null)
            {
                Destroy(this.gameObject.GetComponent<BoxCollider2D>());
                DestroyPickupObject();
            }
            else
                Destroy(this.gameObject);
            Destroy(this);
        }
    }

    public virtual void PickUpAction(object[] arg){
        this.GetComponent<AudioSource>().clip = Sound;
        this.GetComponent<AudioSource>().Play();
    }

    public virtual void DestroyPickupObject(){
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        Destroy(this.gameObject, this.GetComponent<AudioSource>().clip.length);
    }


}
