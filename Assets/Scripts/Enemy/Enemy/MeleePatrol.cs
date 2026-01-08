using UnityEngine;
using UnityEngine.UIElements;

public class MeleePatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy Sight")]
    [SerializeField] private float range;
    [SerializeField] private float sizeX;
    [SerializeField] private float sizeY;


    [SerializeField] private float speed;

    [Header("Player")]
    [SerializeField] private Transform player;
    private Rigidbody2D rb;
    private MeleeHitbox meleeHitbox;
    private int direction=1;

    private Animator anim;
    [Header("Idle Behavior")]
    [SerializeField] private float idleDuration;
    [SerializeField] private LayerMask playerLayer;
    private float idleTime;

    private BoxCollider2D box;
    private bool attacking = false;
    private Vector2 origin;
    private float currentSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meleeHitbox = GetComponentInChildren<MeleeHitbox>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        origin = (leftEdge.position + rightEdge.position) / 2;
        currentSpeed = speed;
    }
    private void Update()
    {
         if (!PlayerInSight() && !attacking)
        { 
            if (transform.position.x < leftEdge.position.x)
            {
                idleTime += Time.deltaTime;
                anim.SetBool("moving", false);
                if (idleTime > idleDuration)
                {
                    idleTime = 0;
                    direction = 1;
                    transform.localScale = new Vector2(1, 1);
                    Direction();
                }
            }
            else if (transform.position.x > rightEdge.position.x)
            {
                idleTime += Time.deltaTime;
                anim.SetBool("moving", false);
                if (idleTime > idleDuration)
                {
                    idleTime = 0;
                    direction = -1;
                    transform.localScale = new Vector2(-1, 1);
                    Direction();
                }
            }
            else
            {
                Direction();
            }
        }
        else
        {
            if (PlayerInSight())
            {
                attacking = true;
                if (Vector2.Distance(player.position, transform.position) < meleeHitbox.GetComponent<BoxCollider2D>().bounds.size.x + 0.7f)
                {
                    currentSpeed = 0;
                    anim.SetBool("moving", false);
                }
                else if (currentSpeed == speed)
                {
                    currentSpeed *= 1.5f;
                    anim.SetBool("moving", true);
                }
                else if (currentSpeed==0)
                {
                    currentSpeed = speed*1.5f;
                    anim.SetBool("moving", true);
                }
                else
                {
                    anim.SetBool("moving", true);
                }

                    rb.linearVelocity = Destination(1) * currentSpeed;
                if (Destination(1).x > 0)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    transform.localScale = new Vector2(-1, 1);
                }
            }
                            
            if (!PlayerInSight() && attacking)
            {
                if (currentSpeed != speed)
                {
                    currentSpeed = speed;
                }

                anim.SetBool("moving", true);
                rb.linearVelocity= Destination(2) * currentSpeed;
                if (rb.linearVelocity.x > 0)
                {
                    transform.localScale = new Vector2(1, 1);
                    direction = 1;
                }
                else
                {
                    transform.localScale = new Vector2(-1, 1);
                    direction = -1;
                }
                if (Vector2.Distance(new Vector2(transform.position.x,origin.y),origin)<0.1f)
                {
                    rb.linearVelocity = Vector2.zero;
                    attacking = false;
                }
            }

        }
    }

    private void Direction()
    {
        anim.SetBool("moving", true);
        transform.position= new Vector2(transform.position.x+Time.deltaTime*currentSpeed*direction, transform.position.y);

    }

    private Vector2 Destination(int num)
    {
        //to player
        if (num==1)
        return (new Vector2(player.position.x,0)-new Vector2(transform.position.x,0)).normalized;
        //to origin
        if (num==2)
        return (new Vector2(origin.x,0)-new Vector2(transform.position.x,0)).normalized;

        else return Vector2.zero;
    }

    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("moving", false);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * transform.localScale.x, new Vector2(box.bounds.size.x * sizeX, box.bounds.size.y*sizeY), 0, new Vector2(transform.localScale.x, 0), transform.rotation.z, playerLayer);
        return hit.collider != null;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(box.bounds.center + transform.right * range * transform.localScale.x, new Vector2(box.bounds.size.x * sizeX, box.bounds.size.y*sizeY));
    //}
}
