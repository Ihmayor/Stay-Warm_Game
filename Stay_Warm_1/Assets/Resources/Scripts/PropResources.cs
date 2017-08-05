using System;
using System.Collections.Generic;
using UnityEngine;

public static class PropResources
{
    public enum PropType {
        Paper,
        Heart
    }

    public static Dictionary<PropType, Action> PickupActions = new Dictionary<PropType, Action>()
    {
        { PropType.Paper, ()=> {} } ,
        { PropType.Heart, PickUpHeart}
    };

    public static AudioClip GetPickupAudio(PropType proptype)
    {
        switch (proptype)
        {
            case PropType.Paper:
                return Resources.Load<AudioClip>("Audio/paperflip3.wav");
            default:
                return null;
        }
    }

    static void PickUpHeart()
    {
        MenuManager.Instance.ActivateHeartMeter();
    }

}