using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fish; // fish prefab
    public GameObject squid;
    public GameObject shark;
    public Camera cam;

    public float cameraHalfWidth;
    public float cameraHalfHeight;

    // How long it takes for each fish/shark to spawn
    public float fishSpawnRate = 0.1f;
    public float sharkSpawnRate = 10f;
    private Vector2 displacement;

    // counts how long its been since a new fish/shark spawned
    private float fishTimer;
    private float sharkTimer;

    // the furthest distance in the x and y axis the fish can spawn from the player
    public float xMaxBounds;
    public float yMaxBounds;

    // closest possible distance to the camera edge a fish or shark can spawn at
    public float buffer;
    void Start()
    {
        //Instantiate(fish, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (fishTimer < fishSpawnRate){
            fishTimer = fishTimer + Time.deltaTime;
        }
        else{
            // creates a fish near the squid
            buffer = 0;
            xMaxBounds = 0f;
            yMaxBounds = 0f;
            displacement = GetInitialSpawn();
            Debug.Log("fish displacement:" + displacement);
            Instantiate(fish, displacement, cam.transform.rotation);
            // resets the fishTimer when a new fish is created
            fishTimer = 0;
        }

        if (sharkTimer< sharkSpawnRate){
            sharkTimer = sharkTimer + Time.deltaTime;
        }
        else{
            // creates a shark near the squid
            xMaxBounds = 20f;
            yMaxBounds = 20f;
            buffer = 4f;
            displacement = GetInitialSpawn();
            Debug.Log(displacement);
            Instantiate(shark, displacement, cam.transform.rotation);
            // resets the fishTimer when a new shark is created
            sharkTimer = 0;
        }
    }

    public Vector2 GetInitialSpawn()
    {
        float xSpawnVector;
        float ySpawnVector;

        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = camHalfHeight * cam.aspect;

        Vector2 playerPos = cam.transform.position;
        Vector2 squidPos = squid.transform.position;

        float xOffset = camHalfWidth + buffer;
        float yOffset = camHalfHeight + buffer;

        // determine the direction based on the squid's position
        bool spawnLeft = squidPos.x < playerPos.x;
        bool spawnRight = squidPos.x > playerPos.x;
        bool spawnAbove = squidPos.y > playerPos.y;
        bool spawnBelow = squidPos.y < playerPos.y;

        // accounts for x axis
        if (spawnLeft)
            xSpawnVector = Random.Range(playerPos.x - xOffset - xMaxBounds, playerPos.x - xOffset);
        else if (spawnRight)
            xSpawnVector = Random.Range(playerPos.x + xOffset, playerPos.x + xOffset + xMaxBounds);
        else
            xSpawnVector = Random.Range(playerPos.x - xOffset - xMaxBounds, playerPos.x + xOffset + xMaxBounds);

        // accounts for y axis
        if (spawnAbove)
            ySpawnVector = Random.Range(playerPos.y + yOffset, playerPos.y + yOffset + yMaxBounds);
        else if (spawnBelow)
            ySpawnVector = Random.Range(playerPos.y - yOffset - yMaxBounds, playerPos.y - yOffset);
        else
            ySpawnVector = Random.Range(playerPos.y - yOffset - yMaxBounds, playerPos.y + yOffset + yMaxBounds);

        return new Vector2(xSpawnVector, ySpawnVector);
    }
}
