using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharkScript : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    public float sharkSpeed = 1f;
    public float directionInterval = 0.5f;
    public Vector2 movementBounds = new Vector2(-100f, 100f); // keeps the shark to a limited area

    private Vector2 targetPosition;
    private float timer;

    void Start()
    {
        targetPosition = GetRandomPosition();
        transform.position = targetPosition;
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * sharkSpeed);

        // checks if the shark arrives at its destination
         if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }
    }

    Vector2 GetRandomPosition()
    {
        // gets a random vector within the bounds defined in movementBounds
        return new Vector2(
            UnityEngine.Random.Range(-movementBounds.x, movementBounds.x),
            UnityEngine.Random.Range(-movementBounds.y, movementBounds.y)
        );
    }
}
