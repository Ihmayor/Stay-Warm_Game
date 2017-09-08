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
    }

}
