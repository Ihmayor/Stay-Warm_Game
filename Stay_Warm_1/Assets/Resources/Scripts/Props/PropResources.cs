using System;
using System.Collections.Generic;
using UnityEngine;

public static class PropResources
{
    public enum PropType {
        Paper,
        Heart
    }

    public static Dictionary<string, string> PropThoughts = new Dictionary<string, string>() {
        {"Note #1","There's nothing on it, but it feels warm. ... Oh!"},
        {"Note #2","Another one..."},
        {"Note #3","It's nice not being alone."},
        {"Glass Heart","This...heart. It feels important. I...I should take care of it."}
    };

    public static Dictionary<PropType, Action<Collider2D>> PickupActions = new Dictionary<PropType, Action<Collider2D>> ()
    {
        { PropType.Paper, PickupPaper } ,
        { PropType.Heart, PickUpHeart}
    };

    public static AudioClip GetPickupAudio(PropType proptype)
    {
        switch (proptype)
        {
            case PropType.Paper:
                return Resources.Load<AudioClip>("Audio/paperflip");
            case PropType.Heart:
                return Resources.Load<AudioClip>("Audio/124546__cubix__waterdrop");
            default:
                return null;
        }
    }

    static void PickupPaper(Collider2D collider)
    {
        collider.GetComponent<CharacterStatus>().WarmHeart(0.01f);
        Collider2D[] nearby = new Collider2D[10];
        collider.GetContacts(nearby);
        foreach(Collider2D c in nearby)
        {
            if (c == null)
                break;
            if (c.GetComponent<Pickup>() != null)
                c.gameObject.AddComponent<FloatAndFollow>();
        }
    }

    static void PickUpHeart(Collider2D collider)
    {
        collider.gameObject.GetComponent<CharacterStatus>().hasHeart = true;
        MenuManager.Instance.ActivateHeartMeter();
    }


}