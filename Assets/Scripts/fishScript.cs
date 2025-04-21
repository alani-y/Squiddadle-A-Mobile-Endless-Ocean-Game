using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class fishScript : MonoBehaviour
{
    public virtual float fishSpeed => 0.5f;
    public float directionInterval = 10f;

    private SpriteRenderer sp;
    public virtual float maxSpeed => 4f;
    private Animator anim;
    protected Rigidbody2D rb;
    public virtual Vector2 movementBounds => new Vector2(-1000f, 1000f); // keeps the fish's movements to a limited area

    private Vector2 targetPosition;
    private float timer;

    private float spawnBuffer = 5;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        //transform.position = targetPosition;
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;
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
        if (timer > directionInterval)
        {
            targetPosition = GetRandomPosition();
            timer = 0;
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
