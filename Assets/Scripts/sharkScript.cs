using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class sharkScript : fishScript
{
    public static event System.Action<sharkScript> OnSharkChasingPlayer;
    public static event System.Action<sharkScript> OnSharkStoppedChasing;

    protected GameObject squid;
    protected squidScript squidScript;
    public override float fishSpeed => 3f;
    public override float maxSpeed => 8f;
    private bool isChasing;
    public float detectionRadius = 25f;
    private bool stunnedByInk = false;
    private float inkStunDuration = 3f;

    public override Vector2 movementBounds => new Vector2(-1200f, 1200f);

    public override void Start()
    {
        base.Start();
        squid = GameObject.FindWithTag("Squid");
        squidScript = squid.GetComponent<squidScript>();
        squidScript.RegisterShark(this);
    }

    public void StunFromInk()
    {
        StartCoroutine(StunCoroutine());
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (stunnedByInk || squid == null || !squidScript.isAlive)
            return;

        float distance = Vector2.Distance(transform.position, squid.transform.position);
        bool squidInRange = distance <= detectionRadius;

        if (squidInRange && !isChasing)
        {
            isChasing = true;
            OnSharkChasingPlayer?.Invoke(this);
        }
        else if (!squidInRange && isChasing)
        {
            isChasing = false;
            OnSharkStoppedChasing?.Invoke(this);
        }

        if (squidInRange)
        {
            Vector2 direction = (squid.transform.position - transform.position).normalized;
            rb.velocity = Vector2.Lerp(rb.velocity, direction * maxSpeed, Time.fixedDeltaTime * 2);
            return;
        }
    }

    private void HandleSquidInk()
    {
        StartCoroutine(StunCoroutine());
    }

    private IEnumerator StunCoroutine()
    {
        stunnedByInk = true;
        isChasing = false;
        rb.velocity = Vector2.zero;
        OnSharkStoppedChasing?.Invoke(this);

        yield return new WaitForSeconds(inkStunDuration);

        stunnedByInk = false;
    }
}
