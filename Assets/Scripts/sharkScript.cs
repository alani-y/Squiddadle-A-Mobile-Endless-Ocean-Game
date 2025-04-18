using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class sharkScript : fishScript
{
    public GameObject squid;
    public override float fishSpeed => 3f;
    public override float maxSpeed => 8f;

    public float detectionRadius = 15f;

    // keeps the shark's movements to a limited area
    public override Vector2 movementBounds => new Vector2(-1200f, 1200f);
    public override void Start()
    {
        base.Start();
        if (squid == null)
        {
            squid = GameObject.FindWithTag("Squid"); // gets the player squid
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (squid != null)
        {
            float distance = Vector2.Distance(transform.position, squid.transform.position);
            if (distance <= detectionRadius)
            {
                // chases the squid
                Vector2 direction = (squid.transform.position - transform.position).normalized;
                rb.velocity = Vector2.Lerp(rb.velocity, direction * maxSpeed, Time.fixedDeltaTime * 2);

                return; // ignores the shark's default movement
            }
        }
    }
}
