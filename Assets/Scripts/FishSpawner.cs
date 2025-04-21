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

        // camera position is the base for the fish spawn range
        Vector2 playerPos = cam.transform.position;

        float xOffset = camHalfWidth + buffer;
        float yOffset = camHalfHeight + buffer;

        bool spawnLeft = Random.value < 0.5f;
        bool spawnAbove = Random.value < 0.5f;

        if (spawnLeft)
            xSpawnVector = Random.Range(playerPos.x - xOffset - xMaxBounds, playerPos.x - xOffset);
        else
            xSpawnVector = Random.Range(playerPos.x + xOffset, playerPos.x + xOffset + xMaxBounds);

        if (spawnAbove)
            ySpawnVector = Random.Range(playerPos.y + yOffset, playerPos.y + yOffset + yMaxBounds);
        else
            ySpawnVector = Random.Range(playerPos.y - yOffset - yMaxBounds, playerPos.y - yOffset);

        return new Vector2(xSpawnVector, ySpawnVector);
    }

}
