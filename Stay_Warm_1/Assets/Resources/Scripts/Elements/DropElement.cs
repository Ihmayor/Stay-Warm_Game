using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DropElement : CoolingElement
{
    public override void Start()
    {
        base.Start();
        CoolingSound = Resources.Load<AudioClip>("Audio/cutmarcolo91_falling-branch");
        CoolingThought = "Snow dropped upon their shoulders. They felt that they time their steps carefully to avoid hurting the heart.";
    }

}
