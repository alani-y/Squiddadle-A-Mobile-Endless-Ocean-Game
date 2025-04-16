using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
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

    private Animator anim;
    public bool isAlive = true;

    private SpriteRenderer sp;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 pos = transform.position;

        SpriteRenderer background = GameObject.Find("Ocean").GetComponent<SpriteRenderer>();

        float bgTop = background.bounds.max.y;
        float bgBottom = background.bounds.min.y;

        pos.y = Mathf.Clamp(pos.y, bgBottom, bgTop);
        transform.position = pos;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2D.AddForce(moveVector*swimSpeed, ForceMode2D.Force);
        anim.SetFloat("xSpeed", rigidbody2D.velocity.x);

        if(rigidbody2D.velocity.x > 0){
            sp.flipX = false;
        }
        else{
            sp.flipX = true;
        }
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
            gameManager.gameOver();
            isAlive = false; // squid is not alive
        }
    }

}
