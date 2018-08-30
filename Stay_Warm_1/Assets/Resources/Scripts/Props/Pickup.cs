using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public string PickupName;
    internal AudioClip Sound;


    public static Dictionary<string, string> PropThoughts = new Dictionary<string, string>() {
        {"Note #1","There was nothing on the note, but it felt warm."},
        {"Note #2","They found another one..."},
        {"Note #3","They liked not being alone."},
        {"Glass Heart","The heart felt important to them, somehow. They cradled it as they marched onwards. It slowly cooled with every step."}
    };

    // Use this for initialization
    void Start () {
		
	}
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            MenuManager.Instance.OpenInteractionMenu("Picked up '" + PickupName + "'");
            MenuManager.Instance.SetThought(collision.GetComponent<CharacterStatus>().CharacterName, PropThoughts[PickupName]);
            PickUpAction(new object[] { collision });
             if (Sound != null)
            {
                DestroyPickupObject();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }

    public virtual void PickUpAction(object[] arg){
        this.GetComponent<AudioSource>().clip = Sound;
        this.GetComponent<AudioSource>().Play();
    }

    public virtual void DestroyPickupObject(){
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        Destroy(this.gameObject, this.GetComponent<AudioSource>().clip.length);
        Destroy(this);

    }


}
