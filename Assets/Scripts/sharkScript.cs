using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class sharkScript : fishScript
{
    public override float fishSpeed => 4f;
    public override Vector2 movementBounds => new Vector2(-800f, 800f); // keeps the shark's movements to a limited area

    // the maximum distance a shark can spawn away from the player
    public override float xMaxBounds => 8;
    public override float yMaxBounds => 3;
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
