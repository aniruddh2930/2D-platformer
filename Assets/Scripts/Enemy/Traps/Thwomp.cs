using System;
using Unity.InferenceEngine;
using UnityEngine;

public class Thwomp : Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    private float checkTimer;
    private Vector2 destination;
    private bool attacking;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask playerMask;
    private Vector2 startPos;
    private Animator anim;
    private BoxCollider2D box;

    private Vector3[] directions= new Vector3[4];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        directions[0] = transform.right;
        directions[1] = -transform.right;
        directions[2] = transform.up;
        directions[3] = -transform.up;
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            //times 50 so speed doesnt seem to large
            rb.linearVelocity = destination.normalized *50* speed * Time.deltaTime;
            CheckDistance();
        }
        else
        {
            checkTimer -= Time.deltaTime;
            if (checkTimer <= 0)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckDistance()
    {
        float distanceMoved = Vector2.Distance(transform.position, startPos);
        if (distanceMoved>=range)
        {
            Stop();
        }
    }

    private void CheckForPlayer()
    {
        for (int i=0;i<directions.Length;i++)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, box.size*transform.lossyScale,transform.rotation.z, directions[i],range,playerMask);
            if (hit.collider != null)
             {
                destination = directions[i];
                attacking = true;
                checkTimer = checkDelay;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        //offset getting stuck in walls etc
        if(destination==Vector2.right)
        {
            anim.SetTrigger("rightHit");
            if (collision.tag != "Player")
            {
                transform.Translate(-0.2f, 0, 0);
            }
        }
        else if (destination == Vector2.left)
        {
            anim.SetTrigger("leftHit");
            if (collision.tag != "Player")
            {
                transform.Translate(0.2f, 0, 0);
            }
        }
        else if (destination == Vector2.up)
        {
            anim.SetTrigger("upHit");
            if (collision.tag != "Player")
            {
                transform.Translate(0, -0.2f, 0);
            }
        }
        else
        {
            anim.SetTrigger("downHit");
            if (collision.tag != "Player")
            {
                transform.Translate(0, 0.2f, 0);
            }
        }

        Stop();
    }


    private void Stop()
    {
        startPos = transform.position;
        rb.linearVelocity = Vector2.zero;
        attacking = false;
    }

    private void OnEnable()
    {
        Stop();
    }
}

