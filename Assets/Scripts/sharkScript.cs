using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class sharkScript : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    public float sharkSpeed = 1f;
    public float directionInterval = 0.5f;
    public Vector2 movementBounds = new Vector2(-800f, 800f); // keeps the shark's movements to a limited area

    // the maximum x distance a shark can spawn away from the player
    public float xMaxBounds = 15;
    public float yMaxBounds = 5;

    private Vector2 targetPosition;
    private float timer;

    public virtual void Start()
    {
        targetPosition = GetInitialSpawn();
        transform.position = targetPosition;
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * sharkSpeed);

        // checks if the shark arrives at its destination
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }
    }

    public virtual Vector2 GetRandomPosition()
    {
        // gets a random vector within the movement bounds of the player
        return new Vector2(
            UnityEngine.Random.Range(-movementBounds.x + transform.position.x, movementBounds.x + transform.position.x),
            UnityEngine.Random.Range(-movementBounds.y + transform.position.y, movementBounds.y + transform.position.y)
        );
    }

    public virtual Vector2 GetInitialSpawn(){
        // x and y components of the shark's final spawn vector
        float xSpawnVector;
        float ySpawnVector;

        float rangeSection = UnityEngine.Random.Range(0f, 1.0f);

        // the size of half of the camera view
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = camHalfHeight * Camera.main.aspect;
        Debug.Log(camHalfHeight +", " + camHalfWidth);

        // calculates how close a shark can spawn to the player
        // a shark will always spawn outside of the player's camera view
        float buffer = 5;
        float leftMinBounds = transform.position.x - camHalfWidth - buffer;
        float rightMinBounds = transform.position.x + camHalfWidth + buffer;
        float topMinBounds = transform.position.y - camHalfHeight - buffer;
        float bottomMinBounds = transform.position.y + camHalfHeight + buffer;

        // randomly selects if the shark will spawn northwest or southeast of the player
        if (rangeSection < 0.5){
            xSpawnVector = UnityEngine.Random.Range(-1*xMaxBounds + transform.position.x, leftMinBounds);
            ySpawnVector = UnityEngine.Random.Range(-1*yMaxBounds + transform.position.y, topMinBounds);
        }
        else{
            xSpawnVector = UnityEngine.Random.Range(rightMinBounds, transform.position.x + xMaxBounds);
            ySpawnVector = UnityEngine.Random.Range(bottomMinBounds, transform.position.y + yMaxBounds);
        }

        return new Vector2(xSpawnVector, ySpawnVector);
    }
}
