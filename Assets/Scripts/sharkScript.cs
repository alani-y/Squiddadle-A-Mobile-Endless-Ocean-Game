using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class sharkScript : fishScript
{
    public override float fishSpeed => 4f;

    // keeps the shark's movements to a limited area
    public override Vector2 movementBounds => new Vector2(-800f, 800f);
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
