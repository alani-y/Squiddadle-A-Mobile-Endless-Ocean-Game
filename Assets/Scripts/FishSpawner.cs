using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fish;
    public GameObject squid;
    public GameObject shark;
    public Camera cam;

    public float cameraHalfWidth;
    public float cameraHalfHeight;

    // How long it takes for each fish to spawn
    public int fishSpawnRate = 2;
    public int sharkSpawnRate = 10;
    private Vector2 displacement;

    // counts how long its been since a new fish spawned
    private float fishTimer;
    private float sharkTimer;

    public float xMaxBounds;
    public float yMaxBounds;
    public float buffer;
    void Start()
    {
        Instantiate(fish, transform.position, transform.rotation);
        //Instantiate(shark, new Vector2(transform.position.x + 20, transform.position.y + 20), transform.rotation);
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
            xMaxBounds = 4f;
            yMaxBounds = 2f;
            displacement = GetInitialSpawn();
            Instantiate(fish, displacement, transform.rotation);
            // resets the fishTimer when a new fish is created
            fishTimer = 0;
        }

        if (sharkTimer< sharkSpawnRate){
            sharkTimer = sharkTimer + Time.deltaTime;
        }
        else{
            // creates a shark near the squid
            xMaxBounds = 100f;
            yMaxBounds = 50f;
            buffer = 200f;
            displacement = GetInitialSpawn();
            Debug.Log(displacement);
            Instantiate(shark, displacement, transform.rotation);
            // resets the fishTimer when a new shark is created
            sharkTimer = 0;
        }
    }

    public Vector2 GetInitialSpawn(){
        // x and y components of the fish's final spawn vector
        float xSpawnVector;
        float ySpawnVector;

        float rangeSection = Random.Range(0f, 1.0f);

        // the size of half of the camera view
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = camHalfHeight * Camera.main.aspect;
        //Debug.Log(camHalfHeight +", " + camHalfWidth);

        // calculates how close a fish can spawn to the player
        // a fish will always spawn outside of the player's camera view
        // Player position
    Vector2 playerPos = transform.position;

    float xOffset = camHalfWidth + buffer;
    float yOffset = camHalfHeight + buffer;

    if (rangeSection < 0.5f) {
        // spawn to the top left
        xSpawnVector = Random.Range(playerPos.x - xOffset - xMaxBounds, playerPos.x - xOffset);
        ySpawnVector = Random.Range(playerPos.y + yOffset, playerPos.y + yOffset + yMaxBounds);
    } else {
        // spawn to the bottom right
        xSpawnVector = Random.Range(playerPos.x + xOffset, playerPos.x + xOffset + xMaxBounds);
        ySpawnVector = Random.Range(playerPos.y - yOffset - yMaxBounds, playerPos.y - yOffset);
    }
        return new Vector2(xSpawnVector, ySpawnVector);
    }
}
