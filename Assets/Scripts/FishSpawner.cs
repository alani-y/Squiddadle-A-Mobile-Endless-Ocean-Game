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
            yMaxBounds = 10f;
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
        float leftMinBounds = transform.position.x - camHalfWidth - buffer;
        float rightMinBounds = transform.position.x + camHalfWidth + buffer;
        float topMinBounds = transform.position.y - camHalfHeight - buffer;
        float bottomMinBounds = transform.position.y + camHalfHeight + buffer;

        // randomly selects if the fish will spawn northwest or southeast of the player
        if (rangeSection < 0.5){
            // spawn offscreen to the northwest
            xSpawnVector = Random.Range(leftMinBounds - xMaxBounds, leftMinBounds);
            ySpawnVector = Random.Range(topMinBounds - yMaxBounds, yMaxBounds);

        }
        else{
            // spawn offscreen to the southeast
            xSpawnVector = Random.Range(rightMinBounds, rightMinBounds + xMaxBounds);
            ySpawnVector = Random.Range(bottomMinBounds, bottomMinBounds+yMaxBounds);

        }
        return new Vector2(xSpawnVector, ySpawnVector);
    }
}
