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

    private float wallJumpCooldown;
    private float horizontalInput;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body.gravityScale = 3.0f;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        wallJumpCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isOnWall() && !isGrounded())
        {
            body.gravityScale=0.1f;
            if (wallJumpCooldown <= 0.0f)
            {
                wallJump();
            }
        }
        else
        {
            body.gravityScale = 3.0f;
        }

    
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
        anim.SetBool("jump", !isGrounded());


    }

    private void jump()
    {

        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        anim.SetTrigger("jump");
    }

    private void wallJump()
    {

        body.linearVelocity = new Vector2(-transform.localScale.x*speed, jumpSpeed);
        transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), 1, 1);
        anim.SetTrigger("jump");
        wallJumpCooldown = 0.15f;
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

    public bool canAttack() {
        return Mathf.Abs(horizontalInput)<0.3f && isGrounded() && !isOnWall();
    }
}
