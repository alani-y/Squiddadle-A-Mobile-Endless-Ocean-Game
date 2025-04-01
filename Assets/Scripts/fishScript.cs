using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishScript : MonoBehaviour // extends from the fish
{
    public virtual float fishSpeed => 2f;
    public float directionInterval = 0.5f;
    public virtual Vector2 movementBounds => new Vector2(-800f, 800f); // keeps the fish's movements to a limited area

    private Vector2 targetPosition;
    private float timer;

    public virtual void Start()
    {
        transform.position = targetPosition;
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * fishSpeed);

        // checks if the fish arrives at its destination
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }
    }

    public virtual Vector2 GetRandomPosition()
    {
        // gets a random vector within the movement bounds of the player
        return new Vector2(
            Random.Range(-movementBounds.x + transform.position.x, movementBounds.x + transform.position.x),
            Random.Range(-movementBounds.y + transform.position.y, movementBounds.y + transform.position.y)
        );
    }

    /*
    public virtual Vector2 GetInitialSpawn(){
        // x and y components of the fish's final spawn vector
        float xSpawnVector;
        float ySpawnVector;

        float rangeSection = Random.Range(0f, 1.0f);

        // the size of half of the camera view
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = camHalfHeight * Camera.main.aspect;
        Debug.Log(camHalfHeight +", " + camHalfWidth);

        // calculates how close a fish can spawn to the player
        // a fish will always spawn outside of the player's camera view
        float leftMinBounds = transform.position.x - camHalfWidth;
        float rightMinBounds = transform.position.x + camHalfWidth;
        float topMinBounds = transform.position.y - camHalfHeight;
        float bottomMinBounds = transform.position.y + camHalfHeight;

        // randomly selects if the fish will spawn northwest or southeast of the player
        if (rangeSection < 0.5){
            xSpawnVector = Random.Range(-1*xMaxBounds + transform.position.x, leftMinBounds);
            ySpawnVector = Random.Range(-1*yMaxBounds + transform.position.y, topMinBounds);
        }
        else{
            xSpawnVector = Random.Range(rightMinBounds, transform.position.x + xMaxBounds);
            ySpawnVector = Random.Range(bottomMinBounds, transform.position.y + yMaxBounds);
        }

        return new Vector2(xSpawnVector, ySpawnVector);
    }*/

}
