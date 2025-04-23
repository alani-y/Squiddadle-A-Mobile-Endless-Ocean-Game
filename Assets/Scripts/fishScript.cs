using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class fishScript : MonoBehaviour
{
    public virtual float fishSpeed => 1f;

    // how often a fish will change directions
    public float directionInterval = 10f;

    private SpriteRenderer sp;
    public virtual float maxSpeed => 4f;
    private Animator anim;
    protected Rigidbody2D rb;
    public virtual Vector2 movementBounds => new Vector2(-1000f, 1000f); // keeps the fish's movements to a limited area

    private float fishExistenceTimer; // how long a fish has existed for

    // where a fish is swimming towards
    private Vector2 targetPosition;

    // how long since a fish has changed direction
    private float directionTimer;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        //transform.position = targetPosition;
    }

    public virtual void Update()
    {
        directionTimer += Time.deltaTime;
        fishExistenceTimer += Time.deltaTime;

        // destroys fish that the squid didn't catch after 100s
        if (fishExistenceTimer > 100f) {
            Destroy(gameObject);
        }
    }

    public virtual Vector2 GetRandomPosition()
    {
        // gets a random vector within the movement bounds of the player
        return new Vector2(
            Random.Range(-movementBounds.x + transform.position.x, movementBounds.x + transform.position.x),
            Random.Range(-movementBounds.y + transform.position.y, movementBounds.y + transform.position.y)
        );
    }

   public virtual void FixedUpdate()
    {
        if (directionTimer > directionInterval)
        {
            targetPosition = GetRandomPosition();
            directionTimer = 0;
        }

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.AddForce(direction * fishSpeed, ForceMode2D.Force);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        anim.SetFloat("xSpeed", rb.velocity.x);

        if(rb.velocity.x > 0){
            sp.flipX = true;
        }
        else{
            sp.flipX = false;
        }
    }
}
