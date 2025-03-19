using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class squidScript : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private Vector2 moveVector = new Vector2();
    // Start is called before the first frame update
    public int swimSpeed = 10;
    public int score = 0;
    public gameManager gameManager;
    public bool isAlive = true;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2D.AddForce(moveVector*swimSpeed, ForceMode2D.Force);
    }

    // moves the squid in the dragged direction
    public void moveWithVector(Vector2 squidMoveVector){
        moveVector = squidMoveVector;
    }

    // checks if the squid found an animal
    private void OnTriggerEnter2D(Collider2D animal)
    {
        // if the animal is a fish, the squid eats it
        if(animal.tag == "Fish"){
            animal.gameObject.SetActive(false);
            score += 1;
        }

        // if the animal is a shark, the squid is eaten
        if(animal.tag == "Shark"){
            gameManager.GameOver();
            isAlive = false; // squid is not alive
        }
    }
}
