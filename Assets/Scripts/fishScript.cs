using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishScript : sharkScript // extends from the shark
{
    // Start is called before the first frame update

    // counts how long a fish has existed for
    private float fishExistenceTimer = 0;

    // the fish's initial direction vector
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // gives it the same
        base.Update();
        fishExistenceTimer += Time.deltaTime;
        // destroys the fish if its been on the screen for too long
        if(fishExistenceTimer > 1000){
            Destroy(gameObject);
        }
    }


}
