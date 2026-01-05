using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed;
    public float jumpSpeed;
    private Animator anim;

    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header ("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header ("Multiple Jumps")]
    [SerializeField] private int numberOfJumps;
    // power of each jump weakens by this factor
    [SerializeField] [Range(0.0f,1.0f)] private float jumpPowerDampen;
    private int jumpsLeft;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpSpeedX;
    [SerializeField] private float wallJumpSpeedY;
    [SerializeField] private float wallJumpTimer;

    private float wallJumpCooldown;
    private float horizontalInput;

    [Header ("SFX")]
    [SerializeField] private AudioClip jumpSound;

    [Header("Physics Materials")]
    [SerializeField] private PhysicsMaterial2D friction;
    [SerializeField] private PhysicsMaterial2D noFriction;

    private void Awake()
    {
        jumpsLeft = numberOfJumps;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body.gravityScale = 3.0f;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        wallJumpCooldown -= Time.deltaTime;
        //makes character face right direction when moving
        if (horizontalInput > 0.01f && wallJumpCooldown<=0.0f)
        {
            transform.localScale = Vector3.one;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }
        else if (horizontalInput < -0.01f && wallJumpCooldown<=0.0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded",isGrounded());
        bool onWall = isOnWall();
        //jump
        if (Input.GetKeyDown(KeyCode.W) && !onWall && ((isGrounded() || coyoteCounter>0) || jumpsLeft>0))
        {
            jump();
        }

        //wall jump
        else if (Input.GetKeyDown(KeyCode.W) && onWall && wallJumpCooldown<=0.0f && jumpsLeft>0)
        {
            wallJump();
        }

        //adjustable jump
        if (Input.GetKeyUp(KeyCode.W) && body.linearVelocityY>0)
        {
            body.linearVelocityY = body.linearVelocityY/2;
        }

        if (isOnWall())
        {
            body.sharedMaterial = noFriction;
            jumpsLeft = numberOfJumps;
            body.gravityScale = 0.5f;
        }
        else
        {
            body.gravityScale = 3;
            if (isGrounded())
            {
                body.sharedMaterial = friction;
                jumpsLeft = numberOfJumps;
                coyoteCounter = coyoteTime;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }


    }

    

    private void jump()
    {
        coyoteCounter = coyoteTime;
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed*Mathf.Pow(jumpPowerDampen,(float)(numberOfJumps-jumpsLeft)));
        jumpsLeft--;
        AudioManager.instance.PlaySound(jumpSound);

    }

    private void wallJump()
    {

        body.linearVelocity = new Vector2(-transform.localScale.x*wallJumpSpeedX, wallJumpSpeedY);
        transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), 1, 1);
        wallJumpCooldown = wallJumpTimer;
        jumpsLeft--;
        AudioManager.instance.PlaySound(jumpSound);
    }
    
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool isOnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack() 
    {
        return Mathf.Abs(horizontalInput)<0.3f && isGrounded() && !isOnWall();
    }
}
