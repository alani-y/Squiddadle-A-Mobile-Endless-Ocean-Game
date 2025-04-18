using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class sharkScript : fishScript
{
    public static event Action<sharkScript> OnSharkChasingPlayer;
    public static event Action<sharkScript> OnSharkStoppedChasing;

    protected GameObject squid;
    protected squidScript squidScript; // gets if the squid is alive or dead
    public override float fishSpeed => 3f;
    public override float maxSpeed => 8f;

    private bool isChasing; // checks if the shark is chasing the squid
    public float detectionRadius = 15f; // how close the squid needs to be for detection

    // keeps the shark's movements to a limited area
    public override Vector2 movementBounds => new Vector2(-1200f, 1200f);
    public override void Start()
    {
        base.Start();
        if (squid == null)
        {
            squid = GameObject.FindWithTag("Squid"); // gets the player squid
            squidScript = squid.GetComponent<squidScript>();
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

        float distance = Vector2.Distance(transform.position, squid.transform.position);
        bool squidInRange = distance <= detectionRadius;

        // checks if the squid is in range and the shark isn't already chasing
        if (squidInRange && !isChasing)
        {
            isChasing = true;
            OnSharkChasingPlayer?.Invoke(this);
        }
        // checks if the squid is not in range and the shark is currently chasing
        else if (!squidInRange && isChasing)
        {
            isChasing = false;
            OnSharkStoppedChasing?.Invoke(this);
        }

        // only chases if the squid is alive
        if (squid != null && squidScript.isAlive)
        {
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
