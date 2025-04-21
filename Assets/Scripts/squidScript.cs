using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class squidScript : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private Vector2 moveVector = new Vector2();
    public int swimSpeed = 10;
    public int score = 0;
    public gameManager gameManager;
    private Animator anim;
    public bool isAlive = true;
    private SpriteRenderer sp;

    public static event Action OnSquidInked; // Event to notify sharks

    private float inkCooldown = 10f; // cooldown for the inkblot ability
    private float inkTimer = 0f; //
    private bool canInk => inkTimer <= 0f; // stores if the ability can be used

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        inkTimer = 0f;
    }

    void Update()
    {
        Vector2 pos = transform.position;

        SpriteRenderer background = GameObject.Find("Ocean").GetComponent<SpriteRenderer>();
        float bgTop = background.bounds.max.y;
        float bgBottom = background.bounds.min.y;

        pos.y = Mathf.Clamp(pos.y, bgBottom, bgTop);
        transform.position = pos;

        // ink ability cooldown timer
        if (inkTimer > 0f)
            inkTimer -= Time.deltaTime;


        /*if (Input.GetKeyDown(KeyCode.Space) && canInk && isAlive)
        {
            UseInk();
        }*/
    }

    void FixedUpdate()
    {
        rigidbody2D.AddForce(moveVector * swimSpeed, ForceMode2D.Force);
        anim.SetFloat("xSpeed", rigidbody2D.velocity.x);

        sp.flipX = rigidbody2D.velocity.x <= 0;
    }

    public void moveWithVector(Vector2 squidMoveVector)
    {
        moveVector = squidMoveVector;
    }

    // method for the squid ink ability
    public void UseInk()
    {
        OnSquidInked?.Invoke();
        inkTimer = inkCooldown;
        Debug.Log("Squid ink used!");
    }

    //
    private void OnTriggerEnter2D(Collider2D animal)
    {
        if (animal.tag == "Fish")
        {
            animal.gameObject.SetActive(false);
            score += 1;
        }

        if (animal.tag == "Shark")
        {
            gameManager.gameOver();
            isAlive = false;
        }
    }
}
