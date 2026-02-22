using System;
using System.Collections;
using Unity.InferenceEngine;
using UnityEngine;

public class Thwomp : Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private Health playerHealth;
    private float checkTimer;
    private Vector2 destination;
    private bool attacking;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask playerMask;
    private Vector2 startPos;
    private Animator anim;
    private BoxCollider2D box;
    private ThwompPatrol patrol;

    [Header("life time before enemy dies")]
    [SerializeField] private float lifetime;
    [SerializeField] private  Behaviour[] behaviours;
    private Vector3[] directions= new Vector3[4];

    [Header("SFX")]
    [SerializeField] private AudioClip thwompSound;

  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        directions[0] = transform.right;
        directions[1] = -transform.right;
        directions[2] = transform.up;
        directions[3] = -transform.up;
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        patrol=GetComponent<ThwompPatrol>();
    }

   

        // Update is called once per frame
        void Update()
    {
        if (!playerHealth.dead)
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0)
                Deactivate();
        }

        if (attacking)
        {
            box.enabled = true;
            patrol.enabled = false;
            rb.linearVelocity = destination.normalized*speed;
            CheckDistance();
        }
        else
        {
            checkTimer -= Time.deltaTime;
            if (checkTimer <= 0)
            {
                CheckForPlayer();
                if (!attacking)
                {
                    patrol.enabled = true;
                }
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
        if (!playerHealth.dead)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, patrol.size, transform.rotation.z, directions[i], range, playerMask);
                if (hit.collider != null)
                {
                    destination = directions[i];
                    attacking = true;
                    checkTimer = checkDelay;
                }
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
            return;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.PlaySound(thwompSound);
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

    private void Deactivate()
    {
        Stop();
        this.enabled = false;
        foreach (Behaviour component in behaviours)
        {
            component.enabled = false;
        }
        gameObject.SetActive(false);
    }

    //Debugging purposes
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(2 * range, patrol.size.y));
        Gizmos.DrawWireCube(transform.position, new Vector2(patrol.size.x, 2 * range));
    }
}

