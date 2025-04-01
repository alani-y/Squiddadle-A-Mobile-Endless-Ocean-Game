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
    public int fishSpawnRate = 3;
    public int sharkSpawnRate = 10;
    private Vector2 displacement;

    // counts how long its been since a new fish spawned
    private float fishTimer;
    private float sharkTimer;
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
            displacement = new Vector2(squid.transform.position.x + Random.Range(-15, 15),
            squid.transform.position.y + Random.Range(-15, 15));
            Instantiate(fish, displacement, transform.rotation);
            // resets the fishTimer when a new fish is created
            fishTimer = 0;
        }

        if (sharkTimer< sharkSpawnRate){
            sharkTimer = sharkTimer + Time.deltaTime;
        }
        else{
            // creates a shark near the squid
            displacement = new Vector2(squid.transform.position.x + Random.Range(-100, 100),
            squid.transform.position.y + Random.Range(-100, 100));
            Instantiate(shark, displacement, transform.rotation);
            // resets the fishTimer when a new fish is created
            sharkTimer = 0;
        }
    }
}
