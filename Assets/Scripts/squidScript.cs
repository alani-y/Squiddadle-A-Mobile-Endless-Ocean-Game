using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public class squidScript : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private Vector2 moveVector = new Vector2();
    public int swimSpeed = 10;
    public int score = 0;
    public gameManager gameManager;
    private saveManager saveManager;
    public sharkScript sharkScript;

    private Animator anim;
    public RuntimeAnimatorController altSquidAnimator;
    public bool isAlive = true;
    private SpriteRenderer sp;
    public ParticleSystem inkAbility;

    [Header("Ink Ability Settings")]
    public float inkCooldown = 10f;
    public float inkTimer = 0f;

    [Header("Ink Event")]
    public UnityEvent onInkUsed;

    public List<sharkScript> sharkListeners = new List<sharkScript>();

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        saveManager = gameManager.GetComponent<saveManager>();
        inkTimer = 0f;
    }

    public void RegisterShark(sharkScript shark)
    {
        sharkListeners.Add(shark);
    }

    void Update()
    {
        Vector2 pos = transform.position;

        SpriteRenderer background = GameObject.Find("Ocean").GetComponent<SpriteRenderer>();
        float bgTop = background.bounds.max.y;
        float bgBottom = background.bounds.min.y;

        pos.y = Mathf.Clamp(pos.y, bgBottom, bgTop);
        transform.position = pos;

        // hides the squid if its dead
        if(!isAlive){
            sp.enabled = false;
        }

        if (inkTimer > 0f){
            inkTimer -= Time.deltaTime;
        }

        // displays the alternate squid cosmetic if the user bought it
        if(saveManager.hasAlternateSquid()){
            anim.runtimeAnimatorController = altSquidAnimator;
        }
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

    public void UseInk()
    {
        if (inkTimer <= 0f && isAlive)
        {
            foreach (var shark in sharkListeners)
            {
                shark.StunFromInk();
            }
            inkAbility.Play();
            Debug.Log("squid ink activated");
            inkTimer = inkCooldown;
        }

    }

    private void OnTriggerEnter2D(Collider2D animal)
    {
        // destroys the fish if the squid ate it
        if (animal.CompareTag("Fish") && isAlive)
        {
            Destroy(animal.gameObject);
            score += 1;
        }

        // the squid is dead if it hits a shark
        if (animal.CompareTag("Shark"))
        {
            gameManager.gameOver();
            isAlive = false;
        }
    }
}
