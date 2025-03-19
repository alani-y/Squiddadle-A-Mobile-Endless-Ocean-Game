using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishScript : MonoBehaviour
{
    // Start is called before the first frame update

    // counts how long a fish has existed for
    private float fishExistenceTimer = 0;

    // how long its been since the fish has changed directions
    private float changeDirectionTimer = 0;

    // the fish's initial direction vector
    //private Vector2 = new Vector2(Random(-10, 10), Random(-10, 10)))
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fishExistenceTimer += Time.deltaTime;
        // destroys the fish if its been on the screen for too long
        if(fishExistenceTimer > 10000){
            Destroy(gameObject);
        }


    }
}
