using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharkScript : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    public float sharkSpeed = 5;
    public float xDirection;
    public float yDirection;
    public float timer;
    public float directionInterval = 5;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 newPosition = new Vector3(UnityEngine.Random.Range(-1, 1) * sharkSpeed, UnityEngine.Random.Range(-1, 1)*sharkSpeed);
        transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= directionInterval){
            //Vector2 newPosition = transform.position;
            Vector2 newPosition = new Vector3(UnityEngine.Random.Range(-1f, 1f) * sharkSpeed, UnityEngine.Random.Range(-1f, 1f)*sharkSpeed);
            transform.position = newPosition;
            timer = 0;
        }
    }
}
